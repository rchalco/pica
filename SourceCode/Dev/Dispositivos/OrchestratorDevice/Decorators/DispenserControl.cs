using Hangar.ServiceImplement.Factory;
using Interop.Main.Service.Interface;
using OrchestratorDevice.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Decorators
{
    public class DispenserControl : Attribute
    {
        public string NamePropMountToControl { get; set; } = string.Empty;

        public bool IsPossibleEmitNote(Object req)
        {

            int TotalMount = 0;
            if (!req.GetType().GetProperties().ToList().Any(x => x.Name.Equals(NamePropMountToControl)))
            {
                Setttings.LoggerEvent($"DispenserControl: No se tiene la propiedad {NamePropMountToControl} en el request", System.Diagnostics.EventLogEntryType.Error);
                return false;
            }
            string valorProp = Convert.ToString(req.GetType().GetProperties().First(x => x.Name.Equals(NamePropMountToControl)).GetValue(req, null));
            if (!int.TryParse(valorProp, out TotalMount))
            {
                Setttings.LoggerEvent($"DispenserControl: La propiedad {NamePropMountToControl} no se puede convertir a un valor entero, valor actual: {valorProp}", System.Diagnostics.EventLogEntryType.Error);
                return false;
            }
            if (TotalMount == 0)
            {
                return false;
            }
            var serviceDispenser = MicroService.CreateInstance<IDispenserInterop>();
            var resulOffSetAmount = serviceDispenser.OffSetMountToString(TotalMount);

            if (resulOffSetAmount.State != Foundation.Stone.Application.Wrapper.ResponseType.Success)
            {
                resulOffSetAmount.Message = "DispenserControl: " + resulOffSetAmount.Message;
                Setttings.LoggerResponse(resulOffSetAmount);
            }
            return resulOffSetAmount.State == Foundation.Stone.Application.Wrapper.ResponseType.Success;
        }
    }
}
