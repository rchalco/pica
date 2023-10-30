using Foundation.Stone.Application.Wrapper;
using Interop.Main.Cross.Domain.CardReader;
using Interop.Main.Service.Interface;
using RuntimeCardReader.Core.Base;
using RuntimeCardReader.Core.Implement.Creator;
using RuntimeCardReader.Core.Implement.GenPlus;
using System;
using System.ServiceModel;

namespace RuntimeCardReader.Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class CardReaderService : ICardReaderService, ICardReaderInterop
    {
        private static TypeReaderCard _typeReader = TypeReaderCard.NONE;
        private static ReaderCard readerCard = null;

        private static ReaderCard GetReaderCard(TypeReaderCard typeReader)
        {

            if (readerCard == null)
            {
                switch (typeReader)
                {
                    case TypeReaderCard.CREATOR:
                        readerCard = new CreatorReader();
                        break;
                    case TypeReaderCard.GENPLUS:
                        readerCard = new GenPlusReader();
                        break;
                    default:
                        readerCard = new GenPlusReader();
                        break;
                }
            }
            return readerCard;
        }
        public ResponseQuery<CommandCardReader> EjectCard()
        {
            int tryCount = 0;
            var resulEject = ExecActionReaderCard(() => { return GetReaderCard(_typeReader).EjectCard(); });
            while (resulEject.State != ResponseType.Success && tryCount <= 2)
            {
                var resulReset = ExecActionReaderCard(() => { return GetReaderCard(_typeReader).Reset(); });
                tryCount++;
            }
            return resulEject;
        }
        public ResponseQuery<CommandCardReader> InitReader(TypeReaderCard typeReader)
        {
            _typeReader = typeReader;
            readerCard = null;
            return GetReaderCard(typeReader).InitReader();
        }
        public ResponseQuery<CommandCardReader> ReadCard()
        {
            return ExecActionReaderCard(() => { return GetReaderCard(_typeReader).ReadCard(); });
        }
        public ResponseQuery<CommandCardReader> Reset()
        {
            return GetReaderCard(_typeReader).Reset();
        }
        public ResponseQuery<CommandCardReader> VerifyStatus()
        {
            return ExecActionReaderCard(() => { return GetReaderCard(_typeReader).Reset(); });
        }
        private ResponseQuery<CommandCardReader> ExecActionReaderCard(Func<ResponseQuery<CommandCardReader>> action)
        {
            if (readerCard == null)
            {
                return new ResponseQuery<CommandCardReader> { State = ResponseType.Warning, Message = "Lector de tarjeta no incializado" };
            }
            lock (readerCard)
            {
                ResponseQuery<CommandCardReader> response = new ResponseQuery<CommandCardReader>();
                switch (GetReaderCard(_typeReader).status)
                {
                    case ReaderCard.status_reader.none:
                        response = new ResponseQuery<CommandCardReader> { State = ResponseType.Warning, Message = "Lector de tarjeta no incializado" };
                        break;
                    case ReaderCard.status_reader.ready:
                        response = action?.Invoke();
                        break;
                    case ReaderCard.status_reader.bussy:
                        response = new ResponseQuery<CommandCardReader> { State = ResponseType.Warning, Message = "Lector de tarjeta no disponible" };
                        break;
                    case ReaderCard.status_reader.damaged:
                        Reset();
                        response = action?.Invoke();
                        break;
                }
                return response;
            }

        }
    }
}
