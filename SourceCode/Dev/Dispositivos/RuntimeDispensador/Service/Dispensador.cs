using Foundation.Stone.Application.Wrapper;
using RuntimeDispensador.Core;
using RuntimeDispensador.Structural;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ServiceModel;
using Foundation.Stone.CrossCuting.StoneException;
using Interop.Main.Service.Interface;
using Interop.Main.Cross.Domain.Orchestrator;
using Interop.Main.Cross.Domain.Dispenser;
using DispenserEnnumerator = Interop.Main.Cross.Domain.Dispenser.Enumerators;
using Interop.Main.Cross.Domain.Dispenser.Enumerators;
using System.Threading.Tasks;

namespace RuntimeDispensador.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single, MaxItemsInObjectGraph = 2147483647)]
    public class Dispensador : IDispensador, IDispenserInterop
    {

        public Dispensador()
        {

        }

        ~Dispensador()
        {

        }

        public virtual void Dispose()
        {

        }

        protected void ProcessError(Response response, Exception exception, System.Diagnostics.EventLogEntryType eventLogEntryType = System.Diagnostics.EventLogEntryType.Error)
        {
            response.State = ResponseType.Error;
            GetwayDispensador.GetInstance().ClearBatchs();
            response.Message = exception.GetAllErrorMessage();
            Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO($"Dispenser Logger", response.Message + " . StackTrace: " + exception.StackTrace, eventLogEntryType);
        }

        public ResponseQuery<Trace> Inicializar(GlobalConfigATM globalConfigATM, bool force)
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "Inicializar ejecutado correctamente"
            };
            try
            {
                ProviderComunication.InitProviderCominication(globalConfigATM);
                Dispenser.InitDispenser(globalConfigATM, force);

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.AbrirBandejas);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.ResetLento : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Reset : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.LeeIdBandeja : DispenserEnnumerator.Comando.Fin;
                    return request;
                });


                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();

                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseQuery<Trace> AsegurarBandeja()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "AsegurarBandeja ejecutado correctamente"
            };
            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.AbrirBandejas);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.ResetLento : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal ))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });
                    
                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }
            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }
            return resul;
        }

        public ResponseQuery<Trace> LiberarBandeja()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "LiberarBandeja ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.CerrarBandejas);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;

        }

        public int NumBandejas()
        {
            Dispenser disp = Dispenser.CurrentDispenser;
            return disp.NumBandejas;
        }

        public ResponseQuery<Trace> Dispensar(int pAmount)
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "Dispensar ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.LeeIdBandeja);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();

                    var responeDisp = disp.OffSetMountToString(pAmount);
                    if (responeDisp.State != ResponseType.Success)
                    {
                        request.command = DispenserEnnumerator.Comando.Fin;
                        request.AdditionalInformation = responeDisp.Message;
                        return request;
                    }

                    if (x.state == DispenserEnnumerator.ResponseDispensador.Exito)
                    {
                        request.command = DispenserEnnumerator.Comando.Dispensar;
                        request.parameter = responeDisp.Object.Comand;
                    }
                    else if (x.Code.Equals("1") && x.state == DispenserEnnumerator.ResponseDispensador.Advertencia)
                    {
                        request.command = DispenserEnnumerator.Comando.Dispensar;
                        request.parameter = responeDisp.Object.Comand;
                    }
                    else
                    {
                        request.command = DispenserEnnumerator.Comando.Fin;
                    }
                    return request;
                });

                if (disp.Tipo == "NMD100")
                {
                    batch.AddComand((StatusDispenser x) =>
                    {
                        GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();

                        if (x.state == DispenserEnnumerator.ResponseDispensador.Exito)
                        {
                            request.command = DispenserEnnumerator.Comando.Liberar;
                        }
                        else if (x.Code.Equals("1") && x.state == DispenserEnnumerator.ResponseDispensador.Advertencia)
                        {
                            request.command = DispenserEnnumerator.Comando.Liberar;
                        }
                        else
                        {
                            request.command = DispenserEnnumerator.Comando.Fin;
                        }
                        return request;
                    });
                }


                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = DispenserEnnumerator.Comando.VerifEntrega;
                    return request;
                });


                if (disp.Tipo == "NMD100")
                {
                    batch.AddComand((StatusDispenser x) =>
                    {
                        GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                        request.command = Comando.Fin;
                        if (x.state == ResponseDispensador.Exito)
                        {
                            System.Threading.Thread.Sleep(15000);
                            request.command = DispenserEnnumerator.Comando.CheckBundle;
                        }
                        return request;
                    });

                    batch.AddComand((StatusDispenser x) =>
                    {
                        GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                        request.command = Comando.Fin;
                        if (x.state != ResponseDispensador.Exito)
                        {
                            request.command = DispenserEnnumerator.Comando.Reset;
                        }
                        return request;
                    });
                }


                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();

                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;
                resul.ListEntities = trace;
                //respuesta
                if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito && x.IdCommand == Comando.VerifEntrega)
                    && trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito && x.IdCommand == Comando.Dispensar))
                {

                    resul.State = ResponseType.Success;
                }
                else
                {
                    resul.State = ResponseType.Warning;

                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseQuery<Trace> DispensarForce(int pAmount)
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = ""
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.LeeIdBandeja);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();

                    var responeDisp = disp.OffSetMountToString(pAmount, true);
                    if (responeDisp.State != ResponseType.Success)
                    {
                        request.command = DispenserEnnumerator.Comando.Fin;
                        request.AdditionalInformation = responeDisp.Message;
                        resul.Message += responeDisp.Message;
                        return request;
                    }

                    if (x.state == DispenserEnnumerator.ResponseDispensador.Exito)
                    {
                        request.command = DispenserEnnumerator.Comando.Dispensar;
                        request.parameter = responeDisp.Object.Comand;
                    }
                    else if (x.Code.Equals("1") && x.state == DispenserEnnumerator.ResponseDispensador.Advertencia)
                    {
                        request.command = DispenserEnnumerator.Comando.Dispensar;
                        request.parameter = responeDisp.Object.Comand;
                    }
                    else
                    {
                        request.command = DispenserEnnumerator.Comando.Fin;
                    }
                    return request;
                });

                if (disp.Tipo == "NMD100")
                {
                    batch.AddComand((StatusDispenser x) =>
                    {
                        GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();

                        if (x.state == DispenserEnnumerator.ResponseDispensador.Exito)
                        {
                            request.command = DispenserEnnumerator.Comando.Liberar;
                        }
                        else if (x.Code.Equals("1") && x.state == DispenserEnnumerator.ResponseDispensador.Advertencia)
                        {
                            request.command = DispenserEnnumerator.Comando.Liberar;
                        }
                        else
                        {
                            request.command = DispenserEnnumerator.Comando.Fin;
                        }
                        return request;
                    });
                }


                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = DispenserEnnumerator.Comando.VerifEntrega;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();

                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message += mensaje_traza;
                resul.ListEntities = trace;
                //respuesta
                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.Message = "Dispensacion correcta";
                }
                else
                {
                    resul.State = ResponseType.Warning;
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseObject<ResponseDispensarATM> DispensarATM(int pAmount)
        {
            ResponseObject<ResponseDispensarATM> resul = new ResponseObject<ResponseDispensarATM>
            {
                State = ResponseType.Success,
                Message = "Dispensar ejecutado correctamente",
                Object = new ResponseDispensarATM()
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.LeeIdBandeja);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();

                    var responeDisp = disp.OffSetMountToString(pAmount);
                    if (responeDisp.State != ResponseType.Success)
                    {
                        request.command = DispenserEnnumerator.Comando.Fin;
                        request.AdditionalInformation = responeDisp.Message;
                        return request;
                    }

                    if (x.state == DispenserEnnumerator.ResponseDispensador.Exito)
                    {
                        request.command = DispenserEnnumerator.Comando.Dispensar;
                        request.parameter = responeDisp.Object.Comand;
                    }
                    else if (x.Code.Equals("1") && x.state == DispenserEnnumerator.ResponseDispensador.Advertencia)
                    {
                        request.command = DispenserEnnumerator.Comando.Dispensar;
                        request.parameter = responeDisp.Object.Comand;
                    }
                    else
                    {
                        request.command = DispenserEnnumerator.Comando.Fin;
                    }
                    return request;
                });

                if (disp.Tipo == "NMD100")
                {
                    batch.AddComand((StatusDispenser x) =>
                    {
                        GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();

                        if (x.state == DispenserEnnumerator.ResponseDispensador.Exito)
                        {
                            request.command = DispenserEnnumerator.Comando.Liberar;
                        }
                        else if ((x.Code.Equals("1") || x.Code.Equals("4")) && x.state == DispenserEnnumerator.ResponseDispensador.Advertencia)
                        {
                            request.command = DispenserEnnumerator.Comando.Liberar;
                        }
                        else
                        {
                            request.command = DispenserEnnumerator.Comando.Fin;
                        }
                        return request;
                    });
                }

                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = DispenserEnnumerator.Comando.VerifEntrega;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();

                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;
                resul.Object.traces = trace;
                //respuesta
                if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito && x.IdCommand == Comando.VerifEntrega)
                    && trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito && x.IdCommand == Comando.Dispensar))
                {
                    resul.State = ResponseType.Success;
                }
                else
                {
                    resul.State = ResponseType.Warning;
                }

                ///TODO descontamos las pizas de los cassttes              
                BillDelivered billDelivereds = new BillDelivered
                {
                    Delivered = new System.Collections.Generic.List<StateCassette>(),
                    FirstResult = new System.Collections.Generic.List<StateCassette>(),
                    Required = new System.Collections.Generic.List<StateCassette>()
                };
                foreach (var entity in resul.Object.traces)
                {
                    foreach (Cassette cassette in Dispenser.globalConfigATM.configDispenserStatus.Cassettes)
                    {
                        if (entity.IdCommand == Comando.VerifEntrega)
                        {
                            foreach (CassetteStatus entregado in entity.Result.Cassettes)
                            {
                                if (Convert.ToInt16(entregado.StrResponse.Substring(0, 1)) == cassette.Sequence)
                                {
                                    cassette.TotalQuantity = cassette.TotalQuantity - Convert.ToInt16(entregado.StrResponse.Substring(1));
                                    StateCassette billDelivered = new StateCassette();
                                    billDelivered.IdMoney = 1;
                                    billDelivered.Quantity = Convert.ToInt16(entregado.StrResponse.Substring(1));
                                    billDelivered.CurrencyCourt = cassette.CurrencyCourt;
                                    billDelivered.State = entregado.StrResponse.Substring(0, 1);
                                    billDelivereds.Delivered.Add(billDelivered);
                                }

                            }
                        }
                        if (entity.IdCommand == Comando.Dispensar)
                        {
                            foreach (CassetteStatus entregado in entity.Result.Cassettes)
                            {
                                if (Convert.ToInt16(entregado.StrResponse.Substring(0, 1)) == cassette.Sequence)
                                {
                                    StateCassette billDelivered = new StateCassette();
                                    billDelivered.IdMoney = 1;
                                    billDelivered.Quantity = Convert.ToInt16(entregado.StrResponse.Substring(2));
                                    billDelivered.CurrencyCourt = cassette.CurrencyCourt;
                                    billDelivered.State = entregado.StrResponse.Substring(1, 1);
                                    billDelivereds.FirstResult.Add(billDelivered);
                                }

                            }
                        }
                    }
                }
                resul.Object.billDelivered = billDelivereds;

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseQuery<Trace> DispensarCmd(string pParam)
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "Entrega por CMD ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.LeeIdBandeja);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();


                    if (x.state == DispenserEnnumerator.ResponseDispensador.Exito)
                    {
                        request.command = DispenserEnnumerator.Comando.Dispensar;
                        request.parameter = pParam;
                    }
                    else if (x.Code.Equals("1") && x.state == DispenserEnnumerator.ResponseDispensador.Advertencia)
                    {
                        request.command = DispenserEnnumerator.Comando.Dispensar;
                        request.parameter = pParam;
                    }
                    else
                    {
                        request.command = DispenserEnnumerator.Comando.Fin;
                    }
                    return request;
                });
                if (disp.Tipo == "NMD100")
                {
                    batch.AddComand((StatusDispenser x) =>
                    {
                        GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();

                        if (x.state == DispenserEnnumerator.ResponseDispensador.Exito)
                        {
                            request.command = DispenserEnnumerator.Comando.Liberar;
                        }
                        else if ((x.Code.Equals("1") || x.Code.Equals("4")) && x.state == DispenserEnnumerator.ResponseDispensador.Advertencia)
                        {
                            request.command = DispenserEnnumerator.Comando.Liberar;
                        }
                        else
                        {
                            request.command = DispenserEnnumerator.Comando.Fin;
                        }
                        return request;
                    });
                }
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = DispenserEnnumerator.Comando.VerifEntrega;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();

                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;
                resul.ListEntities = trace;
                //respuesta
                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;

                }
                else
                {
                    resul.State = ResponseType.Warning;

                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseQuery<Trace> VerificaBanejas()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "VerificaBanejas ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.AbrirBandejas);

                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.LeeIdBandeja : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();

                if (!trace.Any(x => x.IdCommand == DispenserEnnumerator.Comando.LeeIdBandeja && (x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Result.Code.Equals("1"))))
                {
                    throw new Exception("No se pudo leer las bandejas del dispensador");
                }
                var trazaLeerBandeja = trace.First(x => x.IdCommand == DispenserEnnumerator.Comando.LeeIdBandeja);

                disp.Cassettes.ForEach(
                    x =>
                    {
                        x.Id = Convert.ToInt32(trazaLeerBandeja.Result.Cassettes[x.Sequence].PartsResponse[DispenserEnnumerator.PartRespuesta.IDCassete].code);
                    }
                    );
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;
                resul.ListEntities = trace;
            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseQuery<Trace> Diagnosticar()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "Diagnosticar ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.NMDStatus);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = DispenserEnnumerator.ResponseDispensador.Exito == x.state ||
                                    (x.Code.Equals("1") && DispenserEnnumerator.ResponseDispensador.Advertencia == x.state) ?
                                        DispenserEnnumerator.Comando.Fin :
                                        DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();

                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;
                resul.ListEntities = trace;
                //respuesta
                //resul.State = trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito) ?
                //        ResponseType.Success :
                //        ResponseType.Warning;
                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }
            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseQuery<Trace> TablaDeEstadosGrl()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "LiberarBandeja ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                //303
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.TotalNotesDelivered);
                batch.AddComand((StatusDispenser x) =>
                {
                    //304
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.TotalNotesRejected : DispenserEnnumerator.Comando.Fin;
                    return request;
                });
                batch.AddComand((StatusDispenser x) =>
                {
                    //305
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.TotalBundleRejects : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                batch.AddComand((StatusDispenser x) =>
                {
                    //308
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.TotalNotesSingleRejected : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                batch.AddComand((StatusDispenser x) =>
                {
                    //309
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.TotalNotesBundleRejected : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                batch.AddComand((StatusDispenser x) =>
                {
                    //320
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.TotalBundlesDelivered : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                batch.AddComand((StatusDispenser x) =>
                {
                    //390
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.TotalRetracts : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                batch.AddComand((StatusDispenser x) =>
                {
                    //318
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.TotalNotesDelivered2 : DispenserEnnumerator.Comando.Fin;
                    return request;
                });
                batch.AddComand((StatusDispenser x) =>
                {
                    //319
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.TotalBundleRejectsHighPressureFeed : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                batch.AddComand((StatusDispenser x) =>
                {
                    //330
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.TotalNotesDeliveredLifeTime : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                batch.AddComand((StatusDispenser x) =>
                {
                    //331
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.TotalNotesRejectedLifeTime : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                batch.AddComand((StatusDispenser x) =>
                {
                    //332
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.TotalBundleRejectsLifeTime : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                batch.AddComand((StatusDispenser x) =>
                {
                    //333
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.TotalBundlesDeliveredLifeTime : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                batch.AddComand((StatusDispenser x) =>
                {
                    //334
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.TotalRetractsLifeTime : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;

        }

        public ResponseQuery<Trace> AbrirShutter()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "LiberarBandeja ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.AbrirShutter);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;

        }

        public ResponseQuery<Trace> CerrarShutter()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "LiberarBandeja ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.CerrarShutter);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;

        }

        public ResponseQuery<Trace> LeeEstadoShutter()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "Estado"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.ReadEmulatorShutter);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;

        }

        public ResponseQuery<Trace> ActualizaEstadoShutter(string pEstado)
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "Actualiza estado de shutter"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.WriteEmulatorShutter, pEstado);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                }
                else
                {
                    resul.State = ResponseType.Warning;

                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;

        }

        public ResponseQuery<Trace> TablaDeEstado()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "Tabla de estado"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.StatusCodeTable);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseQuery<Trace> Reset()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "LiberarBandeja ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.Reset);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseQuery<Trace> ResetLento()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "LiberarBandeja ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.ResetLento);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseQuery<Trace> BorrarTabla()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "LiberarBandeja ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.ClearTable);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseQuery<Trace> BorrarNQ()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "Se borró NQ para calibrar"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.NQInitialisation);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }
        public ResponseQuery<Trace> LeeIdDePrograma()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "LiberarBandeja ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.ReadPromID);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseQuery<Trace> LeeIdBlock()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "LeeIdBlock ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.ProgramIDBlock);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseQuery<Trace> LeeNumeroSerieNMD()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "LiberarBandeja ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.NumberSerialNMD);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseQuery<Trace> ActualizaNumeroSerieNMD(string pSerie)
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "LiberarBandeja ejecutado correctamente"
            };

            try
            {
                int numSerie = Convert.ToInt32(pSerie);

                string numberSerie = numSerie.ToString("D14");
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.WriteNumberSerialNMD, numberSerie);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseQuery<Trace> HabilitaDeteccionDeBandeja()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "LiberarBandeja ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.EnabledCassette);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                }
                else
                {
                    resul.State = ResponseType.Warning;

                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseQuery<Trace> HabilitaValidadorDeEntrega()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "LiberarBandeja ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.EnabledCheckNotes);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }


        public ResponseQuery<Trace> SelfTest0()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "SelfTest0 ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.SelfTest0);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }
            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseQuery<Trace> SelfTest1()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "SelfTest0 ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.SelfTest1);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseQuery<Trace> SelfTestA()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "SelfTest0 ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.SelfTestA);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseQuery<Trace> SelfTest9()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "SelfTest0 ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.SelfTest9);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }
        public ResponseQuery<Trace> LimpiarNSRollers()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "Limpiar NS Rollers ejecutado"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.CleanNSRollers);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }
        public ResponseQuery<Trace> LimpiarNF(int pNF)
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "Limpiar NF " + pNF.ToString()
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.CleanNF, pNF.ToString());
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito || x.Code.Equals("1")) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }

        public ResponseQuery<Trace> VerificaBrazoMecanico()
        {
            ResponseQuery<Trace> resul = new ResponseQuery<Trace>
            {
                State = ResponseType.Success,
                Message = "Valida brazo mecanico ejecutado correctamente"
            };

            try
            {
                Dispenser disp = Dispenser.CurrentDispenser;

                //Creando Batch de comandos para dispensador 
                GetwayDispensador.BatchCommand batch = new GetwayDispensador.BatchCommand(DispenserEnnumerator.Comando.CheckBundle);
                batch.AddComand((StatusDispenser x) =>
                {
                    GetwayDispensador.CommandRequest request = new GetwayDispensador.CommandRequest();
                    request.command = (x.state == DispenserEnnumerator.ResponseDispensador.Exito) ? DispenserEnnumerator.Comando.Fin : DispenserEnnumerator.Comando.Fin;
                    return request;
                });

                GetwayDispensador.GetInstance().AddBatches(batch);
                var trace = GetwayDispensador.GetInstance().ExecuteBatch();
                //respuesta
                resul.ListEntities = trace;
                string mensaje_traza = string.Empty;
                trace.ToList().ForEach(x => { mensaje_traza += x.Result.Description + "|"; });
                resul.Message = mensaje_traza;

                if (trace.All(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.Exito))
                {
                    resul.State = ResponseType.Success;
                    resul.CodeBase = "00";
                }
                else if (trace.Any(x => x.Result.state == DispenserEnnumerator.ResponseDispensador.ErrorFatal))
                {
                    resul.State = ResponseType.Error;
                    resul.CodeBase = "02";
                    trace.ForEach(x =>
                    {
                        resul.Message += $"{x.CommandName} : {x.Result.state} - {x.Result.Description}|";
                    });

                }
                else
                {
                    resul.State = ResponseType.Warning;
                    resul.CodeBase = "01";
                }

            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }

            return resul;
        }
        public ResponseObject<ResponseOffSetMount> OffSetMountToString(int mountDispenser)
        {
            return Dispenser.CurrentDispenser.OffSetMountToString(mountDispenser);

        }
        public ResponseQuery<Court> GetCoutAvailable()
        {
            return Dispenser.CurrentDispenser.GetCoutAvailable();
        }
    }

}


