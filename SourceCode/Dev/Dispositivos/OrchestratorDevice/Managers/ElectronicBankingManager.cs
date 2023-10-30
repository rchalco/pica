using Foundation.Stone.Application.Wrapper;
using Foundation.Stone.CrossCuting.StoneException;
using Hangar.ServiceImplement.Factory;
using Interop.Main.Service.Interface;
using Newtonsoft.Json;
using OrchestratorDevice.Contracts.Common;
using OrchestratorDevice.Contracts.LoanFlow;
using OrchestratorDevice.Contracts.SavingAccount;
using OrchestratorDevice.Global;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Managers
{
    internal class ElectronicBankingManager : CommonManager
    {

        public ElectronicBankingManager()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

        public ResponseObject<string> ElectronicBankingAuthentication(RequestAuthentication requestAuthentication)
        {
            ResponseObject<string> objResult = new ResponseObject<string> { Object = string.Empty };
            try
            {
                if (!requestAuthentication.credential.AditionalItems.Any(x => x.Key == "IdATM") && DeviceManager.globalConfigATM != null)
                {
                    requestAuthentication.credential.AditionalItems.Add(new AditionalItem { Key = "IdATM", Value = DeviceManager.globalConfigATM.Id.ToString() });
                }
                else if (DeviceManager.globalConfigATM != null)
                {
                    requestAuthentication.credential.AditionalItems.Find(x => x.Key == "IdATM").Value = DeviceManager.globalConfigATM.Id.ToString();
                }

                string eventPath = FileHelper.writeEvent("Authentication: " + JsonConvert.SerializeObject(requestAuthentication));
                var resulOut = clientRestHelper.Consume<ResponseAuthentication>(Setttings.uriSecurity, requestAuthentication).Result.VerifyUserResult;

                if (resulOut.State == ResponseType.Success)
                {
                    string iddentityCardNumber = resulOut.Object.AditionalItems.Where(x => x.Key.Equals("IdNumberThatAutorize")).First().Value;
                    RequestATMIdentityCardParameter objSend = new RequestATMIdentityCardParameter
                    {
                        objParameter = new RequestATMIdentityCard
                        {
                            IdentityCardNumber = iddentityCardNumber.Replace("\"", "")
                        }
                    };
                    objResult.State = ResponseType.Success;
                    objResult.Object = resulOut.Object.token;
                    objResult.Message = resulOut.Message;
                }
                else
                {
                    objResult.State = ResponseType.Warning;
                    objResult.Object = "";
                    objResult.Message = resulOut.Message;
                }

                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(objResult, ex);
                objResult.Message = ex is FaultException ? ex.Message : ExceptionManager.MsgError;
            }
            return objResult;
        }


        /// <summary>
        /// Metodo para Validar Usuarios
        /// </summary>
        /// <param name="objRequest">Parametros</param>
        /// <returns></returns>
        public ResponseObject<int> GetWebClientDataForATM(RequestGetPersonToProdemNet objRequest)
        {
            ResponseObject<int> objResult = new ResponseObject<int> { State = ResponseType.Success, Object = 0, Message = "" };

            try
            {
                string eventPath = FileHelper.writeEvent("RequestPersonToProdemNet: " + JsonConvert.SerializeObject(objRequest));

                var result = clientRestHelper.Consume<ResponseGetPersonToProdemNet>(Setttings.uriBaseServices + "/GetWebClientDataForATM", objRequest.ObjParameter, objRequest.Token).Result.GetWebClientDataForATMResult;

                if (result != null)
                {
                    if (result.Object == null)
                    {
                        objResult.Object = 3;
                        objResult.Message = "Señor Cliente: Usted no tiene usuario WEB para activar, apersónese a cualquier agencia para solicitar su cuenta.";
                    }
                    else 
                    {
                        switch (result.Object.IdcState)
                        {
                            case 13105L:
                                objResult.Object = 0;
                                objResult.Message = "Su cuenta se encuentra ACTIVA. |Si no recuerda su Usuario, presione el botón SI para otorgarle uno nuevo.";
                                break;
                            case 13106L:
                            case 13107L:
                            case 13108L:
                            case 13104L:
                                objResult.Object = 1;
                                objResult.Message = "";//La cueenta esta bloqueada y se solicita huella directamente.
                                break;
                            case 13266L:
                                objResult.Object = 2;
                                objResult.Message = "Señor Cliente: Su cuenta se encuentra pendiente de autorización, apersónese a la agencia donde solicitó su usuario WEB.";
                                break;
                            case 14276L:
                                objResult.Message = "Señor Cliente: Su usuario se encuentra INACTIVO, no es posible activarlo.";
                                objResult.Object = 2;
                                break;
                            case 14282L:
                                objResult.Object = 2;
                                objResult.Message = "Señor Cliente: Su usuario se encuentra CERRADO, no es posible activarlo.";
                                break;
                            default:
                                objResult.Object = 3;
                                objResult.Message = "Señor Cliente: Usted no tiene usuario WEB para activar, apersónese a cualquier agencia para solicitar su cuenta.";
                                break;
                        }
                    }                    
                }

                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(objResult, ex);
                objResult.Message = ex is FaultException ? ex.Message : ExceptionManager.MsgError;
            }

            return objResult;
        }


        /// <summary>
        /// Método que activa cuentas prodem net usando el ATM
        /// </summary>
        public ResponseObject<string> ProdemNetActivationProcessByCI(RequestATMUserInfo objUserInfo)
        {
            ResponseObject<string> objResult = new ResponseObject<string>();
            
            try
            {
                string eventPath = FileHelper.writeEvent("ProdemNetActivationProcessByCI: " + JsonConvert.SerializeObject(objUserInfo));
                var result = clientRestHelper.Consume<ResponseProdemNetActivationProcessByCI>(Setttings.uriBaseServices + "/ProdemNetActivationProcessByCI", objUserInfo, objUserInfo.Token).Result.ProdemNetActivationProcessByCIResult;

                if (result.State == ResponseType.Success)
                {
                    objResult.State = ResponseType.Success;
                    objResult.Object = result.Object;
                    objResult.Message = result.Message;
                }
                else
                {
                    objResult.State = ResponseType.Error;
                    objResult.Object = null;
                    objResult.Message = result.Message;
                }

                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(objResult, ex);
            }
            return objResult;
        }

        /// <summary>
        /// Servicio que obtiene la lista de dispositivos pendientes de activación desde el ATM
        /// </summary>
        /// <param name="objCustomerData">Información</param>
        /// <returns></returns>
        public ResponseQuery<ItemForATM> GetWebPersonDeviceWithActivationPendingForATM(RequestATMUserInfo objUserInfo)
        {
            ResponseQuery<ItemForATM> objResult = new ResponseQuery<ItemForATM>();

            try
            {
                string eventPath = FileHelper.writeEvent("GetWebPersonDeviceWithActivationPendingForATM: " + JsonConvert.SerializeObject(objUserInfo));
                var result = clientRestHelper.Consume<ResponseWebPersonDeviceWithActivationPending>(Setttings.uriBaseServices + "/GetWebPersonDeviceWithActivationPendingForATM", objUserInfo, objUserInfo.Token).Result.GetWebPersonDeviceWithActivationPendingForATMResult;

                if (result.State == ResponseType.Success)
                {
                    objResult.State = ResponseType.Success;
                    objResult.ListEntities = result.ListEntities;
                    objResult.Message = result.Message;
                }
                else
                {
                    objResult.State = ResponseType.Error;
                    objResult.ListEntities = null;
                    objResult.Message = result.Message;
                }

                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(objResult, ex);
            }
            return objResult;
        }

        /// <summary>
        /// Obtiene el codigo de activación QR para el dispositivo
        /// </summary>
        /// <param name="objCustomerData"> datos desde el ATM</param>
        /// <param name="idWebPersonDevice">Id de la persona en la web</param>
        /// <returns></returns>
        public ResponseObject<InfoDeviceActivation> GetWebPersonDeviceActivationCodeFromAtm(RequestDeviceActivation objUserInfo)
        {
            ResponseObject<InfoDeviceActivation> objResult = new ResponseObject<InfoDeviceActivation>();

            try
            {
                string eventPath = FileHelper.writeEvent("GetWebPersonDeviceActivationCodeFromAtm: " + JsonConvert.SerializeObject(objUserInfo));
                var result = clientRestHelper.Consume<ResponseInfoDeviceActivation>(Setttings.uriBaseServices + "/GetWebPersonDeviceActivationCodeFromAtm", objUserInfo, objUserInfo.Token).Result.GetWebPersonDeviceActivationCodeFromAtmResult;

                if (result.State == ResponseType.Success)
                {
                    objResult.State = ResponseType.Success;
                    objResult.Object = result.Object;
                    objResult.Message = result.Message;
                }
                else
                {
                    objResult.State = ResponseType.Error;
                    objResult.Object = null;
                    objResult.Message = result.Message;
                }

                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(objResult, ex);
            }
            return objResult;
        }

        /// <summary>
        /// Servicio que ontiene la lista de dispositivos activos del Cliente
        /// </summary>
        /// <param name="objCustomerData">información del cliente</param>
        /// <returns></returns>
        public ResponseQuery<ItemForATM> GetWebPersonDeviceForATM(RequestDeviceActivation objUserInfo)
        {
            ResponseQuery<ItemForATM> objResult = new ResponseQuery<ItemForATM>();

            try
            {
                string eventPath = FileHelper.writeEvent("GetWebPersonDeviceForATM: " + JsonConvert.SerializeObject(objUserInfo));
                var result = clientRestHelper.Consume<ResponseWebPersonDevice>(Setttings.uriBaseServices + "/GetWebPersonDeviceForATM", objUserInfo, objUserInfo.Token).Result.GetWebPersonDeviceForATMResult;

                if (result.State == ResponseType.Success)
                {
                    objResult.State = ResponseType.Success;
                    objResult.ListEntities = result.ListEntities;
                    objResult.Message = result.Message;
                }
                else
                {
                    objResult.State = ResponseType.Error;
                    objResult.ListEntities = null;
                    objResult.Message = result.Message;
                }

                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(objResult, ex);
            }
            return objResult;
        }

        /// <summary>
        /// Inactiva el dispositivo desde el ATM
        /// </summary>
        /// <param name="idWebPersonDevice">Id del dispositivo</param>
        public ResponseObject<string> WebPersonDeviceInactiveFromATM(RequestDeviceInactivation objDeviceInfo)        
        {
            ResponseObject<string> objResult = new ResponseObject<string>();

            try
            {
                string eventPath = FileHelper.writeEvent("WebPersonDeviceInactiveFromATM: " + JsonConvert.SerializeObject(objDeviceInfo));
                var result = clientRestHelper.Consume<ResponseInfoDeviceInactivation>(Setttings.uriBaseServices + "/WebPersonDeviceInactiveFromATM", objDeviceInfo, objDeviceInfo.Token).Result.WebPersonDeviceInactiveFromATMResult;

                if (result.State == ResponseType.Success)
                {
                    objResult.State = ResponseType.Success;
                    objResult.Object = result.Object;
                    objResult.Message = result.Message;
                }
                else
                {
                    objResult.State = ResponseType.Error;
                    objResult.Object = null;
                    objResult.Message = result.Message;
                }

                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(objResult, ex);
            }
            return objResult;
        }

        /// <summary>
        /// Reset al PIN de ProdemMovil, ProdemKey
        /// </summary>
        /// <param name="objCustomerData">datos desde el ATM</param>
        public Response ATMResetProdemKeyPIN(RequestATMUserInfo objUserInfo)
        {
            Response objResult = new Response();

            try
            {
                string eventPath = FileHelper.writeEvent("ATMResetProdemKeyPIN: " + JsonConvert.SerializeObject(objUserInfo));
                var result = clientRestHelper.Consume<ResponseResetPIN>(Setttings.uriBaseServices + "/ATMResetProdemKeyPIN", objUserInfo, objUserInfo.Token).Result.ATMResetProdemKeyPINResult;//cambiar resultado

                if (result.State == ResponseType.Success)
                {
                    objResult.State = ResponseType.Success;
                    objResult.Message = result.Message;
                }
                else
                {
                    objResult.State = ResponseType.Error;
                    objResult.Message = "Señor(a) Cliente: No tiene equipos pendientes de activación.";
                }

                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(objResult, ex);
            }
            return objResult;
        }

        /// <summary>
        /// Servicio que valida el usuario web
        /// </summary>
        /// <param name="objCustomerData">Información del usuario logueado en el ATM</param>
        /// <returns></returns>
        public ResponseObject<NewProdemNetCustomer> AtmGetNewCustomerDataByCI(RequestATMUserInfo objUserInfo)
        {
            ResponseObject<NewProdemNetCustomer> objResult = new ResponseObject<NewProdemNetCustomer>();

            try
            {
                string eventPath = FileHelper.writeEvent("AtmGetNewCustomerDataByCI: " + JsonConvert.SerializeObject(objUserInfo));
                var result = clientRestHelper.Consume<ResponseAtmGetNewCustomerData>(Setttings.uriBaseServices + "/AtmGetNewCustomerDataByCI", objUserInfo, objUserInfo.Token).Result.AtmGetNewCustomerDataByCIResult;

                if (result.State == ResponseType.Success)
                {
                    objResult.State = ResponseType.Success;
                    objResult.Object = result.Object; 
                    objResult.Message = result.Message;
                }
                else
                {
                    objResult.State = ResponseType.Error;
                    objResult.Object = null;
                    objResult.Message = result.Message;
                }

                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(objResult, ex);
            }
            return objResult;
        }

        /// <summary>
        /// Servicio que crea un usuario web desde el ATM
        /// </summary>
        /// <param name="objUserData">Datos del usuario</param>
        /// <returns></returns>
        public ResponseObject<string> CreateExternalWebPersonClientFromATM(RequestNewUserSolicitation objUserInfo)
        {
            ResponseObject<string> objResult = new ResponseObject<string>();

            try
            {
                string eventPath = FileHelper.writeEvent("CreateExternalWebPersonClientFromATM: " + JsonConvert.SerializeObject(objUserInfo));
                var result = clientRestHelper.Consume<ResponseNewUserSolicitation>(Setttings.uriBaseServices + "/CreateExternalWebPersonClientFromATM", objUserInfo, objUserInfo.Token).Result.CreateExternalWebPersonClientFromATMResult;

                if (result.State == ResponseType.Success)
                {
                    objResult.State = ResponseType.Success;
                    objResult.Object = result.Object;
                    objResult.Message = result.Message;
                }
                else
                {
                    objResult.State = ResponseType.Error;
                    objResult.Object = null;
                    objResult.Message = result.Message;
                }

                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(objResult, ex);
            }
            return objResult;
        }

    }
}
