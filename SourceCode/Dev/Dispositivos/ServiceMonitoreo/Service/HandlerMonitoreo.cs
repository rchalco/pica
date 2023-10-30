using Foundation.Stone.Application.Wrapper;
using Foundation.Stone.Business.Core;
using Foundation.Stone.CrossCuting.Util;
using Foundation.Stone.Data.Implement.EntityFramwork;
using Hangar.ServiceImplement.Config;
using Newtonsoft.Json;
using ServiceMonitoreo.Contract;
using ServiceMonitoreo.Data;
using ServiceMonitoreo.Global;
using ServiceMonitoreo.Util;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoreo.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, MaxItemsInObjectGraph = 2147483647)]
    public class HandlerMonitoreo : BaseManager, IHandlerMonitoreo
    {
        static ExternalConfigurartion externalConfiguration = new ExternalConfigurartion();
        Repository<MonitoreoCENTRAL_ATMEntities> repositoryATM = new Repository<MonitoreoCENTRAL_ATMEntities>(externalConfiguration.GetConexionString("MonitoreoCENTRAL_ATMEntities"), false);

        public Response RegisterBinnacle(RequestRegisterBinnacle requestRegisterBinnacle)
        {
            Response response = new Response
            {
                Message = "Registro de evento exitoso",
                State = ResponseType.Success,
            };
            try
            {
                ///TODO: obtenemos el id del ATM                
                var resulATM = repositoryATM.GetAll<ATM>().Where(x => x.IP == requestRegisterBinnacle.IP);
                Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO(Settings.LogName, $"Se ejecuto el getall {resulATM?.Count()}", System.Diagnostics.EventLogEntryType.Warning);
                int IdATM = resulATM?.Count() > 0 ? resulATM.First().IdATM : 0;

                Binnacle binnacle = new Binnacle
                {
                    Description = requestRegisterBinnacle.Evento,
                    Device = requestRegisterBinnacle.Device,
                    IdAtm = IdATM,
                    IdDevice = null,
                    IdTransacction = requestRegisterBinnacle.IdTransacction,
                    LevelLog = requestRegisterBinnacle.Level,
                    Operation = requestRegisterBinnacle.Operation,
                    Trace = requestRegisterBinnacle.Trace,
                    StateDevice = requestRegisterBinnacle.StateDevice,
                };

                repositoryATM.SaveChanges<Binnacle>(binnacle, Foundation.Stone.Data.Enumerators.Operation.Add);
                repositoryATM.CommitChanges();
            }
            catch (Exception ex)
            {
                ProcessError(ex, response, Settings.LogName, null);
            }
            return response;
        }

        #region Registro de ATMs
        /// <summary>
        /// Obtiene la informacion de todos los registro de los ATMs
        /// </summary>
        /// <returns>Una lista con todos los ATM's</returns>
        public ResponseQuery<ATMDTO> GetATMs()
        {
            ResponseQuery<ATMDTO> response = new ResponseQuery<ATMDTO>
            {
                Message = "Get GetATMs success",
                State = ResponseType.Success,
                ListEntities = new List<ATMDTO>()
            };
            try
            {
                response.ListEntities = MapStone.Map<List<ATM>, List<ATMDTO>>(repositoryATM.GetAll<ATM>(), response.ListEntities);
            }
            catch (Exception ex)
            {
                ProcessError(ex, response, Settings.LogName, null);
            }
            return response;
        }

        #endregion

        #region Lector de Huella
        /// <summary>
        /// Obtiene el estado del lector de la tarjeta
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public ResponseObject<string> GetStateFingerPrint(string IP)
        {
            ResponseObject<string> response = new ResponseObject<string>
            {
                Message = "Get state finger print success",
                State = ResponseType.Success,
                Object = ""
            };
            try
            {
                ClientRestHelper clientRestHelper = new ClientRestHelper();
                string url = Settings.URITemplateFingerPrint.Replace("IP", IP) + "CaptureFinger";
                response.Object = clientRestHelper.ConsumeToString(url, "{\"timeOut\": 5000}").Result;
            }
            catch (Exception ex)
            {
                ProcessError(ex, response, Settings.LogName, null);
            }
            return response;
        }
        #endregion

        #region Lector de tarjeta
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="typeCardReader">CREATOR = 1, GENPLUS = 3</param>
        /// <returns></returns>
        public ResponseQuery<ResulCommandCardReader> GetStateCardReader(string IP, string typeCardReader)
        {
            ResponseQuery<ResulCommandCardReader> response = new ResponseQuery<ResulCommandCardReader>
            {
                Message = "Get state card reader success",
                State = ResponseType.Success,
            };
            try
            {
                ClientRestHelper clientRestHelper = new ClientRestHelper();
                string url = Settings.URITemplateCardReader.Replace("IP", IP) + "InitReader";
                response = clientRestHelper.Consume<ResponseQuery<ResulCommandCardReader>>(url, "InitReaderResult", "{\"typeReader\": " + typeCardReader.ToString() + "}").Result;
                response.ListEntities.ForEach(x => x.convertB64Data());
            }
            catch (Exception ex)
            {
                ProcessError(ex, response, Settings.LogName, null);
            }
            return response;
        }

        /// <summary>
        /// Reset del lector de tarjeta
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="typeCardReader">CREATOR = 1, GENPLUS = 3</param>
        /// <returns></returns>
        public ResponseQuery<ResulCommandCardReader> ResetCardReader(string IP)
        {
            ResponseQuery<ResulCommandCardReader> response = new ResponseQuery<ResulCommandCardReader>
            {
                Message = "Get state card reader success",
                State = ResponseType.Success
            };
            try
            {
                ClientRestHelper clientRestHelper = new ClientRestHelper();
                string url = Settings.URITemplateCardReader.Replace("IP", IP) + "Reset";
                response = clientRestHelper.Consume<ResponseQuery<ResulCommandCardReader>>(url, "ResetResult", "{}").Result;
                response.ListEntities.ForEach(x => x.convertB64Data());
            }
            catch (Exception ex)
            {
                ProcessError(ex, response, Settings.LogName, null);
            }
            return response;
        }

        /// <summary>
        /// Expulsa la tarjeta del lector
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="typeCardReader">CREATOR = 1, GENPLUS = 3</param>
        /// <returns></returns>
        public ResponseQuery<ResulCommandCardReader> EjectCard(string IP)
        {
            ResponseQuery<ResulCommandCardReader> response = new ResponseQuery<ResulCommandCardReader>
            {
                Message = "Get state card reader success",
                State = ResponseType.Success
            };
            try
            {
                ClientRestHelper clientRestHelper = new ClientRestHelper();
                string url = Settings.URITemplateCardReader.Replace("IP", IP) + "EjectCard";
                response = clientRestHelper.Consume<ResponseQuery<ResulCommandCardReader>>(url, "EjectCardResult", "{}").Result;
                response.ListEntities.ForEach(x => x.convertB64Data());
            }
            catch (Exception ex)
            {
                ProcessError(ex, response, Settings.LogName, null);
            }
            return response;
        }

        /// <summary>
        /// Lee un tarjeta
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="typeCardReader">CREATOR = 1, GENPLUS = 3</param>
        /// <returns></returns>
        public ResponseQuery<ResulCommandCardReader> ReadCard(string IP)
        {
            ResponseQuery<ResulCommandCardReader> response = new ResponseQuery<ResulCommandCardReader>
            {
                Message = "Get state card reader success",
                State = ResponseType.Success
            };
            try
            {
                ClientRestHelper clientRestHelper = new ClientRestHelper();
                string url = Settings.URITemplateCardReader.Replace("IP", IP) + "ReadCard";
                response = clientRestHelper.Consume<ResponseQuery<ResulCommandCardReader>>(url, "ReadCardResult", "{}").Result;
                response.ListEntities.ForEach(x => x.convertB64Data());
            }
            catch (Exception ex)
            {
                ProcessError(ex, response, Settings.LogName, null);
            }
            return response;
        }
        #endregion
    }
}

