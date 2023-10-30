using Foundation.Stone.Application.Wrapper;
using RuntimeCardReader.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using Interop.Main.Cross.Domain.CardReader;
using System.Text;
using System.Threading.Tasks;
using RuntimeCardReader.Core.Implement.Creator;

namespace RuntimeCardReader.Core.Implement.GenPlus
{
    public class GenPlusReader : ReaderCard
    {
        public GenPlusReader() : base()
        {
            TypeReaderCard = TypeReaderCard.GENPLUS;
        }

        public override void Dispose()
        {

        }

        public override ResponseQuery<CommandCardReader> EjectCard()
        {
            ResponseQuery<CommandCardReader> resul = new ResponseQuery<CommandCardReader> { State = ResponseType.Success, Message = "Estado correcto del lector" };
            status = status_reader.ready;
            return resul;
        }

        public override ResponseQuery<CommandCardReader> InitReader()
        {
            ResponseQuery<CommandCardReader> resul = new ResponseQuery<CommandCardReader> { State = ResponseType.Success, Message = "Estado correcto del lector" };
            status = status_reader.ready;
            return resul;
        }

        public override ResponseQuery<CommandCardReader> ReadCard()
        {
            ResponseQuery<CommandCardReader> resul = new ResponseQuery<CommandCardReader> { };
            try
            {
                resul = VerificarStatusReader();
                if (resul.State != ResponseType.Success)
                {
                    resul.Message = "Lector con estado no valido: " + resul.Message;
                    return resul;
                }

                status = status_reader.bussy;
                ///leemos las tarjeta
                Task.WaitAll(Task.Delay(2000).ContinueWith((task) =>
                {
                    var resulTask = CardPanning();
                    resul.State = resulTask.State;
                    resul.Message = resulTask.Message;
                    resul.CodeBase = resulTask.CodeBase;
                }));
                ///liberamos la tarjeta 
                status = status_reader.ready;
            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
            }
            return resul;
        }

        public override ResponseQuery<CommandCardReader> Reset()
        {
            ResponseQuery<CommandCardReader> resul = new ResponseQuery<CommandCardReader> { State = ResponseType.Success, Message = "Estado correcto del lector" };
            status = status_reader.ready;
            return resul;
        }

        public override ResponseQuery<CommandCardReader> VerificarStatusReader()
        {
            ResponseQuery<CommandCardReader> resul = new ResponseQuery<CommandCardReader> { State = ResponseType.Success, Message = "Estado correcto del lector" };
            status = status_reader.ready;

            return resul;
        }
    }
}
