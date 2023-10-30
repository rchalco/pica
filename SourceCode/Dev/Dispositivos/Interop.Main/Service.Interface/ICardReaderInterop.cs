using Foundation.Stone.Application.Module;
using Foundation.Stone.Application.Wrapper;
using Interop.Main.Cross.Domain.CardReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Service.Interface
{
    [ModuleStone(FullName = "RuntimeCardReader.Services.CardReaderService")]
    public interface ICardReaderInterop
    {
        ResponseQuery<CommandCardReader> InitReader(TypeReaderCard typeReader);
        ResponseQuery<CommandCardReader> ReadCard();
        ResponseQuery<CommandCardReader> EjectCard();
        ResponseQuery<CommandCardReader> Reset();
    }
}
