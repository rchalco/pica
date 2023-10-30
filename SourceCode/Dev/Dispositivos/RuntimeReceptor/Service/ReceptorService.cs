using Foundation.Stone.Application.Wrapper;
using Foundation.Stone.CrossCuting.StoneException;
using Interop.Main.Cross.Domain.Receptor;
using Interop.Main.Service.Interface;
using NMDManagement;
using OrchestratorDevice.Contracts.Common;
using OrchestratorDevice.Managers;
using RuntimeReceptor.Core.Implemernt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RuntimeReceptor.Service
{
    public class ReceptorService : IReceptorService, IReceptorServiceInterop
    {
        private int intentos = 0;
        private RequestMaxAmount requestInit;
        static ReceptorMEI receptorMEI = new ReceptorMEI();


        public Response InitReceptor(string _portCOM)
        {
            Response response = new Response { State = Foundation.Stone.Application.Wrapper.ResponseType.Success, Message = "Receptor iniciado correctamente" };
            receptorMEI.PortCOM = _portCOM;
            return response;
        }

        /// <summary>
        /// Activa receptor para ingreso de billetes. Si el monto máximo es 0 no se valida el monto
        /// </summary>
        /// <param name="pMaxAmount"></param>
        /// <returns></returns>
        [STAThread]
        public Response ActivarReceptor(RequestMaxAmount pMaxAmount)
        {
            Response response = new Response();
            receptorMEI = new ReceptorMEI();
            requestInit = pMaxAmount;
            try
            {
                while (intentos < 5 && (response = receptorMEI.Open(pMaxAmount.MaxAmount)).State != Foundation.Stone.Application.Wrapper.ResponseType.Success) intentos++;
                if (intentos >= 5)
                {
                    response = receptorMEI.Reset();
                    response = receptorMEI.Open(pMaxAmount.MaxAmount);

                }
                intentos = 0;
                if (response.State != Foundation.Stone.Application.Wrapper.ResponseType.Success)
                {
                    ProcessError(response, new Exception(response.Message));
                }
            }
            catch (Exception ex)
            {
                intentos = 0;
                ProcessError(response, ex);
            }
            return response;
        }

        [STAThread]
        public ResponseQuery<ItemReceptor> DesactivarReceptor()
        {
            ResponseQuery<ItemReceptor> response = new ResponseQuery<ItemReceptor> { State = Foundation.Stone.Application.Wrapper.ResponseType.Success };
            try
            {
                receptorMEI.Close();
                response.ListEntities = receptorMEI.GetBills();

            }
            catch (Exception ex)
            {
                ProcessError(response, ex);
            }
            return response;
        }

        public ResponseQuery<ItemReceptor> GetBills()
        {
            
            ResponseQuery<ItemReceptor> response = new ResponseQuery<ItemReceptor> { State = Foundation.Stone.Application.Wrapper.ResponseType.Success };
            try
            {
                response.ListEntities = receptorMEI.GetBills();

                foreach(ItemReceptor item in receptorMEI.GetBills())
                {
                    if (item.Detalle =="JamDetected")
                    {
                        try
                        {
                            Services service = new Services();
                            //bloquear recepto
                            Core.Implemernt.ConfigurationATM configurationATM = service.GetATM(requestInit.Token, requestInit.IdATM);
                            ProcessError(response,  new Exception ($"luego de llamada a getATM:{configurationATM.GetATMByIdResult.State }"));
                            
                            configurationATM.GetATMByIdResult.Object.Configuration.configReceptorCOM.HasReceptor = false;
                            Core.Implemernt.ResultUpdateATM resultUpdateATM = service.UpadateATM(configurationATM.GetATMByIdResult.Object, requestInit.Token);
                            ProcessError(response, new Exception($"luego de llamada a UpdateATM:{resultUpdateATM.UpdateATMResult.Message }"));
                        }
                        catch (Exception error)
                        {
                            response.State= Foundation.Stone.Application.Wrapper.ResponseType.Error;
                            response.Message=error.Message;
                        }     
                    }
                }

            }
            catch (Exception ex)
            {
                ProcessError(response, ex);
            }
            return response;
        }



        protected void ProcessError(Response response , Exception exception, System.Diagnostics.EventLogEntryType eventLogEntryType = System.Diagnostics.EventLogEntryType.Error)
        {
            response.State = Foundation.Stone.Application.Wrapper.ResponseType.Error;
            response.Message = exception.GetAllErrorMessage();
            Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO($"Receptor Logger", response.Message + " . StackTrace: " + exception.StackTrace, eventLogEntryType);
        }
    }

  
         
}
