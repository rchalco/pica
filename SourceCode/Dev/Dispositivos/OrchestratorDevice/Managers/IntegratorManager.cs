using Foundation.Stone.Application.Module;
using Foundation.Stone.Application.Wrapper;
using Foundation.Stone.Business.Core;
using Foundation.Stone.CrossCuting.StoneException;
using Foundation.Stone.CrossCuting.Util;
using Hangar.ServiceImplement.Config;
using Hangar.ServiceImplement.Factory;
using Interop.Main.Cross.Domain.Dispenser;
using Interop.Main.Cross.Domain.Orchestrator;
using Interop.Main.Cross.Domain.Receptor;
using Interop.Main.Service.Interface;
using Newtonsoft.Json;
using Orchestrator.Global;
using OrchestratorDevice.Contracts;
using OrchestratorDevice.Contracts.Base;
using OrchestratorDevice.Contracts.Common;
using OrchestratorDevice.Contracts.SavingAccount;
using OrchestratorDevice.Global;
using OrchestratorDevice.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ResponseOffSetMount = Interop.Main.Cross.Domain.Dispenser.ResponseOffSetMount;

namespace OrchestratorDevice.Managers
{
    public class IntegratorManager : CommonManager
    {
        public IntegratorManager()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

        public ResponseObject<ResulAuthencation> Authentication(RequestAuthentication requestAuthentication)
        {
            ResponseObject<ResulAuthencation> resul = new ResponseObject<ResulAuthencation> { };
            try
            {
                if (!requestAuthentication.credential.AditionalItems.Any(x => x.Key == "TypeAuthentication"))
                {
                    resul.State = ResponseType.Warning;
                    resul.Message = "No se tiene definido el tipo de tarjeta insertada";
                    return resul;
                }

                if (!requestAuthentication.credential.AditionalItems.Any(x => x.Key == "IdATM") && DeviceManager.globalConfigATM != null)
                {
                    requestAuthentication.credential.AditionalItems.Add(new AditionalItem { Key = "IdATM", Value = DeviceManager.globalConfigATM.Id.ToString() });
                }
                else if (DeviceManager.globalConfigATM != null)
                {
                    requestAuthentication.credential.AditionalItems.Find(x => x.Key == "IdATM").Value = DeviceManager.globalConfigATM.Id.ToString();
                }

                string eventPath = FileHelper.writeEvent("Authentication: " + JsonConvert.SerializeObject(requestAuthentication));
                resul = clientRestHelper.Consume<ResponseAuthentication>(Setttings.uriSecurity, requestAuthentication).Result.VerifyUserResult;
                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
                resul.Message = ex is FaultException ? ex.Message : ExceptionManager.MsgError;
            }
            return resul;

        }

        public ResponseDebitAccount CalibrateDispenser()
        {
            ResponseDebitAccount resul = new ResponseDebitAccount { DebitAccountResult = new ResponseObject<ResulDebitAccount>() };
            int amountToRequest = 0;
            var DispenserInterop = MicroService.CreateInstance<IDispenserInterop>();
            if (DispenserInterop.NumBandejas() == 2)
            {
                amountToRequest = 120; // para que dispence un billete de 20 y otro de 100
            }
            else
            {
                amountToRequest = 180; //para que dispence un billete de cada banceja
            }
            //borrar memoria
            var resulDispenser = DispenserInterop.BorrarNQ();
            //entrega de efectivo
            ResponseObject<ResponseDispensarATM> resultDispenserDelivered = DispenserInterop.DispensarATM(amountToRequest);

            int total = 0;
            foreach (StateCassette outReault in resultDispenserDelivered.Object.billDelivered.FirstResult)
            {
                total = total + (outReault.Quantity * outReault.CurrencyCourt);
            }
            foreach (StateCassette outReault in resultDispenserDelivered.Object.billDelivered.Delivered)
            {
                total = total + (outReault.Quantity * outReault.CurrencyCourt);
            }


            return resul;

        }
        public ResponseDebitAccount DebitAccount(RequestDebitAccount requestDebitAccount)
        {
            ResponseDebitAccount resul = new ResponseDebitAccount
            {
                DebitAccountResult = new ResponseObject<ResulDebitAccount>
                {
                    State = ResponseType.Success,
                    Message = "RETIRO REALIZADO CORRECTAMENTE"
                }
            };
            
            try
            {
                ///TODO creacion del microservicio
                var DispenserInterop = MicroService.CreateInstance<IDispenserInterop>();

                ///TODO verificamos el estado del dispensador
                var resulStatusDipenser = DispenserInterop.Diagnosticar();
                if (resulStatusDipenser.ListEntities.Count > 0 && resulStatusDipenser.ListEntities[0].Result.Type == "F")
                {
                    resul.DebitAccountResult.State = ResponseType.Error;
                    resul.DebitAccountResult.Message = "TENEMOS PROBLEMAS CON EL DISPENSADOR. OPERACION NO REALIZADA";
                    return resul;
                }

                ///TODO creamos el archivo de evidencia
                string eventPath = FileHelper.writeEvent("DebitAccount: " + JsonConvert.SerializeObject(requestDebitAccount));

                ///TODO realizamos el debito en el MF .Net
                ResponseObject<ResponseOffSetMount> resulOffSetMount = DispenserInterop.OffSetMountToString((int)requestDebitAccount.ObjParameter.AmountTrans);
                if (resulOffSetMount.State != ResponseType.Success)
                {
                    resul.DebitAccountResult.State = ResponseType.Error;
                    resul.DebitAccountResult.Message = resulOffSetMount.Message;
                    return resul;
                }

                resul = clientRestHelper.Consume<ResponseDebitAccount>(Setttings.uriBaseServices + "/DebitAccount", requestDebitAccount, requestDebitAccount.Token).Result;
                string jsonCashDispensed = "";//lo entregado
                string jsonCashRejected = "";//lo rechazado
                string jsonCashTry = "";//lo solicitado

                if (resul.DebitAccountResult.State != ResponseType.Success)
                {
                    return resul;
                }

                var resulDispenser = DispenserInterop.DispensarATM(Convert.ToInt32(requestDebitAccount.ObjParameter.AmountTrans));
                jsonCashDispensed = JsonConvert.SerializeObject(resulDispenser.Object.billDelivered.Delivered);
                jsonCashRejected = JsonConvert.SerializeObject(new List<StateCassette>());
                List<StateCassette> LCashRejected = new List<StateCassette>();
                List<StateCassette> LCashTry = new List<StateCassette>();
                resulOffSetMount.Object.Detail.ForEach(yy =>
                {
                    StateCassette item = new StateCassette
                    {
                        CurrencyCourt = yy.Court,
                        IdMoney = 1,
                        Quantity = yy.TotalNotes,
                        State = "0"
                    };
                    LCashTry.Add(item);
                });
                jsonCashTry = JsonConvert.SerializeObject(LCashTry);

                ///TODO llenamos los rechazados 
                LCashTry.ForEach(z =>
                {
                    int dispensedQuantity = resulDispenser.Object.billDelivered.FirstResult.FirstOrDefault(yy => yy.CurrencyCourt == z.CurrencyCourt) == null ? 0
                    : resulDispenser.Object.billDelivered.FirstResult.FirstOrDefault(yy => yy.CurrencyCourt == z.CurrencyCourt).Quantity;
                    if (z.Quantity != dispensedQuantity)
                    {
                        LCashRejected.Add(new StateCassette
                        {
                            CurrencyCourt = z.CurrencyCourt,
                            IdMoney = z.IdMoney,
                            Quantity = Math.Abs(z.Quantity - dispensedQuantity),
                            State = "0"
                        });
                    }
                });
                jsonCashRejected = JsonConvert.SerializeObject(LCashRejected);
                var commandResulDispenser = resulDispenser.Object.traces.Where(yy => yy.IdCommand == Interop.Main.Cross.Domain.Dispenser.Enumerators.Comando.Dispensar).FirstOrDefault();

                ///TODO asignamos los valores a enviar para confirmacion
                resulDispenser.Object.billDelivered.Required = LCashTry;

                if (resulDispenser.State != ResponseType.Success
                    && (commandResulDispenser == null || commandResulDispenser.Result.Code != "1"))
                {
                    //Confirmamos la transaccion en el MF
                    ATMTransactionConfirm(requestDebitAccount, new ATMTransactionConfirmData
                    {
                        ATMCoinageDetail = jsonCashDispensed,
                        ATMCoinageRejected = jsonCashRejected,
                        ATMCoinageTray = jsonCashTry,
                        WithError = true,
                        ATMErrorDetail = JsonConvert.SerializeObject(resulDispenser.Object),
                        ATMTransactionCode = resul.DebitAccountResult.Object.ATMTransaction
                    });
                    resul.DebitAccountResult.State = resulDispenser.State;
                    resul.DebitAccountResult.Message = "TENEMOS PROBLEMAS CON NUESTRO DISPENSADOR! MIL DISCULPAS";
                    return resul;
                }
                resul.DebitAccountResult.State = ResponseType.Success;
                ///TODO enviamos el voucher para impresion
                resul.DebitAccountResult.Object.Voucher = resul.DebitAccountResult.Object.Voucher.Replace("</ClosureMessage>", "");
                resul.DebitAccountResult.Object.Voucher = resul.DebitAccountResult.Object.Voucher.Replace("<ClosureMessage>", "");
                //PrintDocument("RETIRO", resul.DebitAccountResult.Object.Voucher);                    

                //TODO Confirmamos la transaccion en el MF
                if (!ATMTransactionConfirm(requestDebitAccount, new ATMTransactionConfirmData
                {
                    ATMCoinageDetail = jsonCashDispensed,
                    ATMCoinageRejected = jsonCashRejected,
                    ATMCoinageTray = jsonCashTry,
                    WithError = resul.DebitAccountResult.State != ResponseType.Success,
                    ATMErrorDetail = "",
                    ATMTransactionCode = resul.DebitAccountResult.Object.ATMTransaction
                }))
                {
                    resul.DebitAccountResult.State = ResponseType.Warning;
                    resul.DebitAccountResult.Message = "IMPOSIBLE CONFIRMAR TRANSACCION";
                }

                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(resul.DebitAccountResult, ex);
            }
            return resul;

        }

        public ResponseDepositAccount DepositAccount(RequestCreditAccount requestDepositAccount)
        {
            string idTransacDeposit = string.Empty;
            ResponseDepositAccount resul = new ResponseDepositAccount
            {
                DepositAccountResult = new ResponseObject<ResulDebitAccount>
                {
                    State = ResponseType.Success,
                    Message = "DEPOSITO REALIZADO CORRECTAMENTE"
                }
            };
            
            try
            {
                if (requestDepositAccount == null)
                {
                    throw new ArgumentException("El parametro requestDepositAccount no puede ser null");
                }

                
                
                ///TODO creamos el archivo de evidencia
                string eventPath = FileHelper.writeEvent("DepositAccount: " + JsonConvert.SerializeObject(requestDepositAccount));
                string jsonCashDeposit = JsonConvert.SerializeObject(requestDepositAccount.ObjParameter.adittedBills);//lo entregado

                requestDepositAccount.ObjParameter.ATMCashDetail = JsonConvert.SerializeObject(requestDepositAccount.ObjParameter.adittedBills);
                resul = clientRestHelper.Consume<ResponseDepositAccount>(Setttings.uriBaseServices + "/DepositAccount" ,   requestDepositAccount, requestDepositAccount.Token).Result;


                if (resul.DepositAccountResult.State == ResponseType.Success)
                {
                    ///TODO enviamos el voucher para impresion
                    resul.DepositAccountResult.Object.Voucher = resul.DepositAccountResult.Object.Voucher.Replace("</ClosureMessage>", "");
                    resul.DepositAccountResult.Object.Voucher = resul.DepositAccountResult.Object.Voucher.Replace("<ClosureMessage>", "");
                    idTransacDeposit = resul.DepositAccountResult.Object.IdTransaction;
                }
                foreach (var bill in requestDepositAccount.ObjParameter.adittedBills)
                {
                    if (bill.Detalle == "JamDetected" | bill.Detalle == "Error")
                    {
                        ATMTransactionConfirm(resul, new ATMTransactionConfirmData
                        {
                            ATMCoinageDetail = "",
                            ATMCoinageRejected = "",
                            ATMCoinageTray = "",
                            WithError = true,
                            ATMErrorDetail = "Billete trancado",
                            ATMTransactionCode = idTransacDeposit .ToString(),
                        });
                    }
                }
                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(resul.DepositAccountResult, ex);
            }
            return resul;

        }

        public ResponseGetAccountBalance GetAccountBalance(RequestGetAccountBalance requestGetAccountBalance)
        {
            ResponseGetAccountBalance resul = new ResponseGetAccountBalance { GetAccountBalanceResult = new ResponseObject<ResulGetAccountBalance>() };
            try
            {
                string eventPath = FileHelper.writeEvent("GetAccountBalance: " + JsonConvert.SerializeObject(requestGetAccountBalance));
                resul = clientRestHelper.Consume<ResponseGetAccountBalance>(Setttings.uriBaseServices + "/GetAccountBalance", requestGetAccountBalance, requestGetAccountBalance.Token).Result;
                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(resul.GetAccountBalanceResult, ex);
                resul.GetAccountBalanceResult.Message = ex is FaultException ? ex.Message : ExceptionManager.MsgError;
            }
            return resul;

        }

        public ResponseGetHoldersAccount GetHoldersAccount(RequestGetHoldersAccount requestGetHoldersAccount)
        {
            ResponseGetHoldersAccount resul = new ResponseGetHoldersAccount { GetHoldersAccountResult = new ResponseQuery<HolderAccount>() };
            try
            {
                string eventPath = FileHelper.writeEvent("GetHoldersAccount: " + JsonConvert.SerializeObject(requestGetHoldersAccount));
                resul = clientRestHelper.Consume<ResponseGetHoldersAccount>(Setttings.uriBaseServices + "/GetHoldersAccount", requestGetHoldersAccount, requestGetHoldersAccount.Token).Result;
                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(resul.GetHoldersAccountResult, ex);
                resul.GetHoldersAccountResult.Message = ex is FaultException ? ex.Message : ExceptionManager.MsgError;
            }
            return resul;

        }

        public ResponseQuery<DTOLightningSpin> GetLightningSpin(RequestGetLightningSpin requestGetLightningSpin)
        {
            ResponseQuery<DTOLightningSpin> resul = new ResponseQuery<DTOLightningSpin> { };
            try
            {
                string eventPath = FileHelper.writeEvent("GetLightningSpin: " + JsonConvert.SerializeObject(requestGetLightningSpin));
                ResponseGetLightningSpin responseGetLightningSpin = clientRestHelper.Consume<ResponseGetLightningSpin>(Setttings.uriBaseServices + "/GetLightningSpin", requestGetLightningSpin, requestGetLightningSpin.Token).Result;
                resul = responseGetLightningSpin.GetLightningSpinResult;
                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
                resul.Message = ex is FaultException ? ex.Message : ExceptionManager.MsgError;
            }
            return resul;
        }

        public ResponseObject<ResponseDebitLightningSpin> DebitLightningSpin(RequestDebitLightningSpin requestDebitLightningSpin)
        {
            ResponseObject<ResponseDebitLightningSpin> resul = new ResponseObject<ResponseDebitLightningSpin> { };
            try
            {
                var DispenserInterop = MicroService.CreateInstance<IDispenserInterop>();
                ResponseObject<ResponseOffSetMount> resulOffSetMount = DispenserInterop.OffSetMountToString((int)requestDebitLightningSpin.ObjParameter.AmountDispenser);
                if (resulOffSetMount.State != ResponseType.Success)
                {
                    resul.State = ResponseType.Error;
                    resul.Message = "NO SE CUENTA CON SUFICIENTE EFECTIVO PARA DISPENSAR EL MONTO SOLICITADO";
                    return resul;
                }

                ///TODO verificamos el estado del dispensador
                var resulStatusDipenser = DispenserInterop.Diagnosticar();
                if (resulStatusDipenser.ListEntities.Count > 0 && resulStatusDipenser.ListEntities[0].Result.Type == "F")
                {
                    resul.State = ResponseType.Error;
                    resul.Message = "TENEMOS PROBLEMAS CON EL DISPENSADOR. OPERACION NO REALIZADA";
                    return resul;
                }


                string eventPath = FileHelper.writeEvent("DebitLightningSpin: " + JsonConvert.SerializeObject(requestDebitLightningSpin));
                string jsonCashDispensed = "";//lo entregado
                string jsonCashRejected = "";//lo rechazado
                string jsonCashTry = "";//lo solicitado
                requestDebitLightningSpin.ObjParameter.Amount = requestDebitLightningSpin.ObjParameter.AmountDispenser;
                DTODebitLightningSpinResult responseGetLightningSpin = clientRestHelper.Consume<DTODebitLightningSpinResult>(Setttings.uriBaseServices + "/DebitLightningSpin", requestDebitLightningSpin, requestDebitLightningSpin.Token).Result;
                resul = responseGetLightningSpin.DebitLightningSpinResult;

                if (resul.State != ResponseType.Success)
                {
                    return resul;
                }

                var resulDispenser = DispenserInterop.DispensarATM(Convert.ToInt32(requestDebitLightningSpin.ObjParameter.AmountDispenser));
                jsonCashDispensed = JsonConvert.SerializeObject(resulDispenser.Object.billDelivered.Delivered);
                jsonCashRejected = JsonConvert.SerializeObject(new List<StateCassette>());
                List<StateCassette> LCashRejected = new List<StateCassette>();
                List<StateCassette> LCashTry = new List<StateCassette>();
                resulOffSetMount.Object.Detail.ForEach(yy =>
                {
                    StateCassette item = new StateCassette
                    {
                        CurrencyCourt = yy.Court,
                        IdMoney = 1,
                        Quantity = yy.TotalNotes,
                        State = "0"
                    };
                    LCashTry.Add(item);
                });
                jsonCashTry = JsonConvert.SerializeObject(LCashTry);

                ///TODO llenamos los rechazados 
                LCashTry.ForEach(z =>
                {
                    int dispensedQuantity = resulDispenser.Object.billDelivered.FirstResult.FirstOrDefault(yy => yy.CurrencyCourt == z.CurrencyCourt) == null ? 0
                    : resulDispenser.Object.billDelivered.FirstResult.FirstOrDefault(yy => yy.CurrencyCourt == z.CurrencyCourt).Quantity;
                    if (z.Quantity != dispensedQuantity)
                    {
                        LCashRejected.Add(new StateCassette
                        {
                            CurrencyCourt = z.CurrencyCourt,
                            IdMoney = z.IdMoney,
                            Quantity = Math.Abs(z.Quantity - dispensedQuantity),
                            State = "0"
                        });
                    }
                });
                var commandResulDispenser = resulDispenser.Object.traces.Where(yy => yy.IdCommand == Interop.Main.Cross.Domain.Dispenser.Enumerators.Comando.Dispensar).FirstOrDefault();

                if (resulDispenser.State != ResponseType.Success
                    && (commandResulDispenser == null || commandResulDispenser.Result.Code != "1"))
                {
                    resul.State = resulDispenser.State;
                    resul.Message = "NO SE CUENTA CON LAS DENOMINACIONES PARA DISPENSAR EL MONTO DEL GIRO";
                }
                else
                {
                    resul.State = ResponseType.Success;
                    ///TODO impresion del vocher
                    resul.Object.Voucher = resul.Object.Voucher.Replace("</ClosureMessage>", "");
                    resul.Object.Voucher = resul.Object.Voucher.Replace("<ClosureMessage>", "");
                    PrintDocument("GIRO RELÁMPAGO", resul.Object.Voucher);
                }
                //Confirmamos la transaccion en el MF
                ATMTransactionConfirmData dataConfirm = new ATMTransactionConfirmData
                {
                    ATMCoinageDetail = jsonCashDispensed,
                    ATMCoinageRejected = jsonCashRejected,
                    ATMCoinageTray = jsonCashTry,
                    WithError = resul.State != ResponseType.Success,
                    ATMErrorDetail = JsonConvert.SerializeObject(resulDispenser.Object),
                    ATMTransactionCode = responseGetLightningSpin.DebitLightningSpinResult.Object.ATMTransaction
                };
                Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO(System.Diagnostics.EventLogEntryType.Warning, "DebitLightningSpin: " + dataConfirm.SerializarToXml());
                if (!ATMTransactionConfirm(requestDebitLightningSpin, dataConfirm))
                {
                    resul.State = ResponseType.Warning;
                    resul.Message = "IMPOSIBLE CONFIRMAR TRANSACCION";
                }
                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
                resul.Message = ex is FaultException ? ex.Message : ExceptionManager.MsgError;
            }
            return resul;
        }

        public ResponseQuery<Court> GetCoutAvailable()
        {
            ResponseQuery<Court> resul = new ResponseQuery<Court>
            {
                State = ResponseType.Success,
                Message = ""
            };
            try
            {
                IDispenserInterop dispenserInterop = MicroService.CreateInstance<IDispenserInterop>();
                resul = dispenserInterop.GetCoutAvailable();
            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
                resul.Message = ex is FaultException ? ex.Message : ExceptionManager.MsgError;
            }
            return resul;

        }

        public ConfigurationATM GetConfigATM(int pIdATM , string pToken)
        {
            
            dynamic param = new { idATM = pIdATM };
            ConfigurationATM globalConfigATM =new ConfigurationATM { GetATMByIdResult =new ConfigurationATM.ReadResult () };

            try
            {
                globalConfigATM = clientRestHelper.Consume<ConfigurationATM>(Setttings.uriBaseServices + "/GetATMById", param, pToken).Result;
            }
            catch (Exception ex)
            {
                globalConfigATM.GetATMByIdResult.Message= ex is FaultException ? ex.Message : ExceptionManager.MsgError;
                globalConfigATM.GetATMByIdResult.State = ResponseType.Error;                
            }

            return globalConfigATM;
        }
        public ResultUpdateATM  UpdateConfigATM(DataATM dataATM, string pToken)
        {
            dynamic param = new { atmDTO = dataATM };
            ResultUpdateATM resultUpdateATM = new ResultUpdateATM { UpdateATMResult  =new ResultUpdateATM.ReadResult() };
            resultUpdateATM.UpdateATMResult .State= ResponseType.Success;

            try
            {
                resultUpdateATM = clientRestHelper.Consume<ResultUpdateATM>(Setttings.uriBaseServices + "/UpdateATM", param, pToken).Result;
                

            }
            catch (Exception ex)
            {
                resultUpdateATM.UpdateATMResult .Message = ex is FaultException ? ex.Message : ExceptionManager.MsgError;
                resultUpdateATM.UpdateATMResult.State= ResponseType.Error ;
            }


            return resultUpdateATM;
        }


        //deshabilita receptor
        public Response DisableReceiver()
        {
            Response response = new Response();
            response.State = ResponseType.Success;
            response.Message = "Receptor deshabilitado";
            try
            {
                IntegratorManager integrator = new IntegratorManager();
                ConfigurationATM configATM = integrator.GetConfigATM(DeviceManager.globalConfigATM.Id, DeviceManager.token);

                configATM.GetATMByIdResult.Object.Configuration.configReceptorCOM.HasReceptor = false;

                var result = integrator.UpdateConfigATM(configATM.GetATMByIdResult.Object, DeviceManager.token);
            }
            catch (Exception ex)
            {
                response.State = ResponseType.Error;
                response.Message = ex.Message;

            }

            return response;
        }

        //habilita receptor
        public Response EnableReceiver()
        {
            Response response = new Response();
            response.State = ResponseType.Success;
            response.Message = "Receptor habilitado";
            try
            {
               
                ConfigurationATM configATM = GetConfigATM( DeviceManager. globalConfigATM.Id, DeviceManager.token  );

                configATM.GetATMByIdResult.Object.Configuration.configReceptorCOM.HasReceptor = true;

                var result = UpdateConfigATM(configATM.GetATMByIdResult.Object, DeviceManager.token);
            }
            catch (Exception ex)
            {
                response.State = ResponseType.Error;
                response.Message = ex.Message;

            }

            return response;
        }

        public Response GetStateReceiver()
        {
            Response response = new Response();
            response.State = ResponseType.Success;
            response.Message = "Receptor habilitado";
            try
            {
                IntegratorManager integrator = new IntegratorManager();
                ConfigurationATM configATM = integrator.GetConfigATM(DeviceManager.globalConfigATM.Id, DeviceManager.token);

                if (!configATM.GetATMByIdResult.Object.Configuration.configReceptorCOM.HasReceptor )
                {
                    response.State = ResponseType.Error ;
                    response.Message = "Receptor Desabilitado";
                }
                                
            }
            catch (Exception ex)
            {
                response.State = ResponseType.Error;
                response.Message = ex.Message;

            }

            return response;
        }
    }
}
