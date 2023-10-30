using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CentralServiceDispensador.Wraper;
using Foundation.Stone.CrossCuting.StoneException;
using CentralServiceDispensador.Data;
using Foundation.Stone.CrossCuting.Util;

namespace CentralServiceDispensador.Service
{
    public class CentralConfigService : ICentralConfigService
    {
        public ResponseCentral GetConfig(RequestConfig req)
        {
            ResponseCentral response = new ResponseCentral();
            try
            {
                using (PICA_DISPENSADOREntities context = new PICA_DISPENSADOREntities())
                {
                    var resul = context.GetDispenserByATM(req.IdATM).FirstOrDefault();
                    if (resul == null)
                    {
                        response.State = StateCentral.Warning;
                        response.Message = "El ATM no cuenta con un registro de configuracion";
                        return response;
                    }
                    response.State = StateCentral.Sucess;
                    response.Message = "Configuracion obtenida exitosamente";
                    response.ConfigSource = resul.XmlConfig.BinarySerializeCompress();
                }
            }
            catch (Exception ex)
            {
                Foundation.Stone.CrossCuting.StoneException.StoneException exception = new Foundation.Stone.CrossCuting.StoneException.StoneException(ex, Foundation.Stone.CrossCuting.StoneException.Enumerators.CodeExceptionAplication.ErrorGenerico);
                ExceptionManager.ProcessError(exception);
                response.State = StateCentral.Error;                
                response.Message = ExceptionManager.MsgError;
            }
            return response;
        }
    }
}
