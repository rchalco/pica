using Foundation.Stone.Application.Wrapper;
using Foundation.Stone.CrossCuting.StoneException;
using Interop.Main.Cross.Domain.Logger;
using Interop.Main.Global;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Cross.Util
{
    public class CentralizedLogger
    {
        public string URIServerLog { get; set; }
        public void RegisterLog(RequestLogger requestLogger)
        {
            try
            {
                if (requestLogger == null)
                {
                    throw new ArgumentException("El parametro {requestLogger} no puede ser nulo!");
                }
                requestLogger.IP = Setttings.CurrentIp;
                ClientRestHelper clientRestHelper = new ClientRestHelper();
                //clientRestHelper.Consume<Response>(URIServerLog + "/RegisterBinnacle", requestLogger, "requestRegisterBinnacle").ContinueWith(x =>
                //{
                //    if (x.Result.State != ResponseType.Success)
                //    {
                //        Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO(
                //            System.Diagnostics.EventLogEntryType.Error, "Error en el registro de los centralizado de dispositivos ATM (Respuesta del servicio): " + x.Result.Message);
                //    }
                //}).Start();
            }
            catch (Exception ex)
            {
                Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO(
                    System.Diagnostics.EventLogEntryType.Error, "Error en el registro de log centralizado de dispositivos ATM: " + ex.GetAllErrorMessage());
            }
        }
    }
}
