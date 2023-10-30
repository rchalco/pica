using Foundation.Stone.Application.Wrapper;
using Foundation.Stone.CrossCuting.StoneException;
using Foundation.Stone.CrossCuting.Util;
using Interop.Main.Cross.Domain.CardReader;
using Interop.Main.Cross.Util;
using Interop.Main.Global;
using Mono.Security;
using Newtonsoft.Json;
using PCSC;
using RuntimeCardReader.Core.Implement.Creator;
using RuntimeCardReader.Domain;
using RuntimeCardReader.Global;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RuntimeCardReader.Core.Base
{
    public abstract class ReaderCard : IDisposable
    {
        protected static PCSCReader cardReader;

        public ReaderCard()
        {
            status = status_reader.none;
        }

        public enum status_reader
        {
            none = -1,
            ready = 0,
            bussy = 1,
            damaged = 2
        }

        public status_reader status { get; set; }
        public abstract ResponseQuery<CommandCardReader> InitReader();
        public abstract ResponseQuery<CommandCardReader> ReadCard();
        public abstract ResponseQuery<CommandCardReader> EjectCard();
        public abstract ResponseQuery<CommandCardReader> Reset();
        public abstract ResponseQuery<CommandCardReader> VerificarStatusReader();
        public TypeReaderCard TypeReaderCard { get; set; }

        private const string TypeCardGemalto = "Gemalto";
        private const string TypeCardMasterCard = "MasterCard";
        private void IniReader()
        {

            if (cardReader == null)
            {
                cardReader = new PCSCReader();
                cardReader.CardInserted += new PCSCReader.CardInsertedEventHandler(cardReader_CardInserted);
                cardReader.CardRemoved += new PCSCReader.CardRemovedEventHandler(cardReader_CardRemoved);
            }
            string selectedReader = VerificarLector(cardReader);
            cardReader.Connect(selectedReader);
        }

        private void Reconnect()
        {
            if (!cardReader.Disconnect())
            {
                cardReader.Dispose();
                cardReader = null;
                IniReader();
            }

            Thread.Sleep(500);
            string selectedReader = VerificarLector(cardReader);
            cardReader.Connect(selectedReader);
        }

        protected void ProcessError(Response response, Exception exception, System.Diagnostics.EventLogEntryType eventLogEntryType = System.Diagnostics.EventLogEntryType.Error)
        {
            status = status_reader.bussy;
            response.State = ResponseType.Error;
            response.Message = exception.GetAllErrorMessage();
            Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO($"Reader Card Logger", $"ver{Setings.CurrentVersion} " + response.Message + " . StackTrace: " + exception.StackTrace, eventLogEntryType);
            status = status_reader.damaged;


            ///TODO registramos el error centralizado 
            (string, string) resul = NameOfCallingClass();
            CentralizedLogger centralizedLogger = new CentralizedLogger { URIServerLog = RuntimeCardReader.Global.Setings.URIServerLog };
            Interop.Main.Cross.Domain.Logger.RequestLogger requestLogger = new Interop.Main.Cross.Domain.Logger.RequestLogger
            {
                Device = "RreaderCard",
                Evento = resul.Item1,
                IdTransacction = string.Empty,
                IP = string.Empty,
                Operation = resul.Item2,
                Trace = exception.GetAllErrorMessage(),
                Level = "Error",
                StateDevice = response.SerializarToXml()
            };            
            centralizedLogger.RegisterLog(requestLogger);
        }

        protected void ProcessError(ResponseQuery<CommandCardReader> response, Exception exception, System.Diagnostics.EventLogEntryType eventLogEntryType = System.Diagnostics.EventLogEntryType.Error)
        {
            status = status_reader.bussy;
            response.State = ResponseType.Error;
            response.Message = exception.GetAllErrorMessage();
            Foundation.Stone.CrossCuting.Logger.BitacoraWriter.RegisterTraceSO($"Reader Card Logger", $"ver{Setings.CurrentVersion} " + response.Message + " . StackTrace: " + exception.StackTrace + $". Detail {JsonConvert.SerializeObject(response)}", eventLogEntryType);


            status = status_reader.damaged;

            ///TODO registramos el error centralizado 
            //(string, string) resul = NameOfCallingClass();
            //CentralizedLogger centralizedLogger = new CentralizedLogger { URIServerLog = RuntimeCardReader.Global.Setings.URIServerLog };
            //Interop.Main.Cross.Domain.Logger.RequestLogger requestLogger = new Interop.Main.Cross.Domain.Logger.RequestLogger
            //{
            //    Device = "RreaderCard",
            //    Evento = resul.Item1,
            //    IdTransacction = string.Empty,
            //    IP = string.Empty,
            //    Operation = resul.Item2,
            //    Trace = exception.GetAllErrorMessage(),
            //    Level = "Error",
            //    StateDevice = response.SerializarToXml()
            //};
            //centralizedLogger.RegisterLog(requestLogger);
        }

        private string VerificarLector(PCSCReader cardReader)
        {
            List<string> refernce = new List<string>();
            switch (TypeReaderCard)
            {
                case TypeReaderCard.CREATOR:
                    refernce.Add("CREATOR");
                    break;
                case TypeReaderCard.ATHENA:
                    refernce.Add("ATHENA");
                    break;
                case TypeReaderCard.GENPLUS:
                    refernce.Add("GEMPLUS");
                    refernce.Add("GEMALTO");
                    break;
                default:
                    throw new Exception("No se tiene inicializado el lecto!!!!");
            }

            bool existeLector = false;
            string referenceFound = string.Empty;
            refernce.ForEach(referenceInner =>
            {
                if (!existeLector)
                {
                    existeLector = cardReader.Readers.Any(x => x.ToUpper().Contains(referenceInner));
                    referenceFound = referenceInner;
                }
            });

            if (!existeLector)
            {
                string lectoresDispoibles = string.Empty;
                cardReader.Readers.ToList().ForEach(x =>
                {
                    lectoresDispoibles += "**" + x;
                });
                throw new Exception($"No se tiene un lectror {TypeReaderCard} conectador!!!!, se tiene solo los siguientes lectores {lectoresDispoibles}");
            }

            return cardReader.Readers.First(x => x.ToUpper().Contains(referenceFound));
        }

        private void DisconectCardReader()
        {
            if (cardReader != null)
            {
                if (!cardReader.Disconnect())
                {
                    cardReader.Dispose();
                    cardReader = null;
                }
            }
        }

        protected ResponseQuery CardPanning()
        {
            ResponseQuery resulData = new ResponseQuery();

            try
            {
                ///TODO: conectamos el API PSCS 
                IniReader();
                resulData.CodeBase = TypeCardGemalto;
                ///Intentamos leer la tarjeta como MPCOS BS
                resulData = ReadCardPSCMPCOSWallet01();
                ///Intentamos leer la tarjeta como MPCOS USD
                if (Convert.ToString(resulData.Message).StartsWith("\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0"))
                {
                    Reconnect();
                    resulData = ReadCardPSCMPCOSWallet02();
                }
                ///Intentamos leer la tarjeta como EMV
                if (resulData.State != ResponseType.Success)
                {
                    Reconnect();
                    resulData.CodeBase = TypeCardMasterCard;
                    resulData = ReadCardPSCEMV();
                }
                DisconectCardReader();
                ///TODO logueamos la cantidad de veces que no se pudo leer una tarjeta
                if (resulData.State != ResponseType.Success)
                {
                    ProcessError(resulData, new Exception(resulData.Message));
                }
            }
            catch (Exception ex)
            {
                resulData.State = ResponseType.Error;
                resulData.Message = ex.Message;
                DisconectCardReader();
            }
            return resulData;
        }


        /// <summary>
        /// Primero billetera BS TOTEM
        /// </summary>
        /// <returns></returns>
        protected ResponseQuery ReadCardPSCMPCOSWallet01()
        {
            ResponseQuery resulData = new ResponseQuery();
            APDUCommand apdu = null;
            APDUResponse response = null;

            try
            {
                byte[] pse = new byte[2] { 0X3F, 0X00 };
                //Ingreasmos al primer nivel de la tarjeta
                apdu = new APDUCommand(0x00, 0XA4, 0X00, 0X00, pse, (byte)pse.Length);
                response = cardReader.Transmit(apdu);

                if (response.SW1 != 0x61)
                {

                    resulData.Message = "LA TARJETA NO TIENE EL FORMATO CORRECTO";
                    resulData.CodeBase = "CARD_NOVALID";
                    resulData.State = ResponseType.Warning;
                    return resulData;
                }

                apdu = new APDUCommand(0x00, 0xC0, 0x00, 0x00, null, response.SW2);
                response = cardReader.Transmit(apdu);

                //sw1 = 0X90  sw = 00
                //SELECCION SEGUNDO NIVEL BOLIVIANOS
                byte[] arrayByte = new byte[2] { 0X04, 0X00 };
                apdu = new APDUCommand(0x00, 0XA4, 0X01, 0X00, arrayByte, (byte)arrayByte.Length);
                response = cardReader.Transmit(apdu);

                if (response.SW1 != 0x61)
                {
                    resulData.State = ResponseType.Warning;
                    resulData.CodeBase = "CARD_NOVALID";
                    resulData.Message = "Tarjta no válida";
                    return resulData;
                }

                //SELECCION DE ARCHIVO                            
                arrayByte = new byte[2] { 0X04, 0X06 };
                apdu = new APDUCommand(0x00, 0XA4, 0X02, 0X00, arrayByte, (byte)arrayByte.Length);
                response = cardReader.Transmit(apdu);

                if (response.SW1 != 0x61)
                {
                    resulData.State = ResponseType.Warning;
                    resulData.CodeBase = "CARD_NOVALID";
                    resulData.Message = "Tarjta no válida";
                    return resulData;
                }

                //OBTENEMOS LA DATA DE LA TARJETA
                arrayByte = new byte[1] { 0X64 };
                apdu = new APDUCommand(0x00, 0XB0, 0X00, 0X00, arrayByte, 1);
                resulData.Message = cardReader.TransmitExplicit(new byte[5] { 0X00, 0XB0, 0X00, 0X00, 0X64 }).Substring(0, 15);

                ///****************ADICIONAR LA RECUPERACION DE LOS 2 NROS DE SERIE, EL FALLADO Y EL CORRECTO
                string[] csns = new string[2];
                /*[0] el csn correcto [1]csn con bug simulado*/
                csns[0] = get_csn(cardReader, ref csns[1]);
                StringBuilder sb = new StringBuilder(csns[0].Substring(0, csns[0].Length - 4)); //primero la serie buena
                resulData.Message += "|" + sb.Append("|").Append(csns[1].Substring(0, csns[1].Length - 4)).ToString(); //luego la serie buggy
                                                                                                                       //---------------------------------------------------------------
                resulData.CodeBase = "ACCOUNT";
                resulData.State = ResponseType.Success;

            }
            catch (Exception ex)
            {
                resulData.State = ResponseType.Error;
                resulData.Message = ex.Message;
            }
            return resulData;
        }

        /// <summary>
        /// Primero billetera USD TOTEM
        /// </summary>
        /// <returns></returns>
        protected ResponseQuery ReadCardPSCMPCOSWallet02()
        {
            ResponseQuery resulData = new ResponseQuery();

            APDUCommand apdu = null;
            APDUResponse response = null;

            try
            {
                byte[] pse = new byte[2] { 0X3F, 0X00 };
                //Ingreasmos al primer nivel de la tarjeta
                apdu = new APDUCommand(0x00, 0XA4, 0X00, 0X00, pse, (byte)pse.Length);
                response = cardReader.Transmit(apdu);

                if (response.SW1 != 0x61)
                {
                    resulData.Message = "LA TARJETA NO TIENE EL FORMATO CORRECTO";
                    resulData.CodeBase = "CARD_NOVALID";
                    resulData.State = ResponseType.Warning;
                    return resulData;
                }

                apdu = new APDUCommand(0x00, 0xC0, 0x00, 0x00, null, response.SW2);
                response = cardReader.Transmit(apdu);

                //sw1 = 0X90  sw = 00
                //SELECCION SEGUNDO NIVEL DOALRES
                byte[] arrayByte = new byte[2] { 0X02, 0X00 };
                apdu = new APDUCommand(0x00, 0XA4, 0X01, 0X00, arrayByte, (byte)arrayByte.Length);
                response = cardReader.Transmit(apdu);

                if (response.SW1 != 0x61)
                {
                    resulData.State = ResponseType.Warning;
                    resulData.CodeBase = "CARD_NOVALID";
                    resulData.Message = "Tarjta no válida";
                    return resulData;
                }

                //SELECCION DE ARCHIVO                            
                arrayByte = new byte[2] { 0X02, 0X06 };
                apdu = new APDUCommand(0x00, 0XA4, 0X02, 0X00, arrayByte, (byte)arrayByte.Length);
                response = cardReader.Transmit(apdu);

                if (response.SW1 != 0x61)
                {
                    resulData.State = ResponseType.Warning;
                    resulData.CodeBase = "CARD_NOVALID";
                    resulData.Message = "Tarjta no válida";
                    return resulData;
                }

                //OBTENEMOS LA DATA DE LA TARJETA
                arrayByte = new byte[1] { 0X64 };
                apdu = new APDUCommand(0x00, 0XB0, 0X00, 0X00, arrayByte, 1);
                resulData.Message = cardReader.TransmitExplicit(new byte[5] { 0X00, 0XB0, 0X00, 0X00, 0X64 }).Substring(0, 15);

                ///****************ADICIONAR LA RECUPERACION DE LOS 2 NROS DE SERIE, EL FALLADO Y EL CORRECTO
                string[] csns = new string[2];
                /*[0] el csn correcto [1]csn con bug simulado*/
                csns[0] = get_csn(cardReader, ref csns[1]);
                StringBuilder sb = new StringBuilder(csns[0].Substring(0, csns[0].Length - 4)); //primero la serie buena
                resulData.Message += "|" + sb.Append("|").Append(csns[1].Substring(0, csns[1].Length - 4)).ToString(); //luego la serie buggy

                //---------------------------------------------------------------
                resulData.CodeBase = "ACCOUNT";
                resulData.State = ResponseType.Success;

            }
            catch (Exception ex)
            {
                resulData.State = ResponseType.Error;
                resulData.Message = ex.Message;
            }
            return resulData;
        }

        private string get_csn(PCSCReader card_reader, ref string csn_bug)
        {
            StringBuilder sb = new StringBuilder(),
                           sb_bug = new StringBuilder();

            byte[] csn = card_reader.transmit(new byte[] { 0x80, 0xc0, 0x02, 0xa1, 0x08 });
            for (int i = 0; null == csn ? false : i < csn.Length;
                            sb.Append(csn[i].ToString("X2")),
                            sb_bug.Append(VB6_buggy_format(csn[i])), ++i) ;
            if (null != csn)
                csn_bug = sb_bug.ToString();

            return null == csn ? null : sb.ToString();
        }

        /// <summary>
        /// REPLIKA DEL BUG DE OBTENCION DEL NUMERO DE SERIE CON CHAR Y NO CON UNSIGNED CHAR
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private string VB6_buggy_format(byte b)
        {
            bool result = b == 0x1a;
            for (int j = 0x1a; j <= 0x9A && !result; j += 0x10)
                result = (b == j);
            return /*result?"00":b.ToString(b<0xa?"X2":"X")+" "; *///no se hace el padding porke eso esta implementado en la serie real
                (result ? "00" :
                    b.ToString(b < 0xa ? "X2" : "X"))/*+" "*/;
        }

        protected ResponseQuery ReadCardPSCEMV()
        {
            ResponseQuery resulData = new ResponseQuery();

            APDUCommand apdu = null;
            APDUResponse response = null;


            try
            {
                List<byte[]> pseIdentifiers = new List<byte[]>();
                List<byte[]> applicationIdentifiers = new List<byte[]>();
                ASCIIEncoding encoding = new ASCIIEncoding();

                ///TODO AID MasterCard
                var AID = HexStringToBytes("A0000000041010");//Aplication ID MasterCard

                List<ApplicationFileLocator> applicationFileLocators = new List<ApplicationFileLocator>();
                StringBuilder sb = new StringBuilder();

                // Select AID
                apdu = new APDUCommand(0x00, 0xA4, 0x04, 0x00, AID, (byte)AID.Length);
                response = cardReader.Transmit(apdu);

                // Get response nescesary
                if (response.SW1 == 0x61)
                {
                    apdu = new APDUCommand(0x00, 0xC0, 0x00, 0x00, null, response.SW2);
                    response = cardReader.Transmit(apdu);
                }

                // Application not found
                if (response.SW1 == 0x6A && response.SW2 == 0x82)
                {
                    resulData.State = ResponseType.Warning;
                    resulData.CodeBase = "CARD_NOVALID";
                    resulData.Message = "Tarjta no válida";
                    return resulData;
                }

                if (response.SW1 != 0x90)
                {
                    resulData.State = ResponseType.Warning;
                    resulData.CodeBase = "CARD_NOVALID";
                    resulData.Message = "Tarjta no válida";
                    return resulData;
                }

                // Get processing options (with empty PDOL)
                apdu = new APDUCommand(0x80, 0xA8, 0x00, 0x00, new byte[] { 0x83, 0x00 }, 0x02);
                response = cardReader.Transmit(apdu);

                // Get response nescesary
                if (response.SW1 == 0x61)
                {
                    apdu = new APDUCommand(0x00, 0xC0, 0x00, 0x00, null, response.SW2);
                    response = cardReader.Transmit(apdu);
                }

                if (response.SW1 != 0x90)
                {
                    resulData.State = ResponseType.Warning;
                    resulData.CodeBase = "CARD_NOVALID";
                    resulData.Message = "Tarjta no válida";
                    return resulData;
                }

                ASN1 template = new ASN1(response.Data);
                ASN1 aip = null;
                ASN1 afl = null;

                // constructed data object response (Template Format 2) EMV                
                if (template.Tag[0] == 0x77)
                {
                    aip = template.Find(0x82);
                    afl = template.Find(0x94);
                }

                //CPA la informacion viene quemada
                if (template.Tag[0] == 0x80)
                {
                    aip = new ASN1(0x82, new byte[] { 18, 00 });
                    afl = new ASN1(0x94, new byte[] { 0x10, 0x01, 0x01, 0x00, 0x18, 0x01, 0x01, 0x00 }); ;
                }

                // Chop up AFL's
                for (int i = 0; i < afl.Length; i += 4)
                {
                    byte[] AFL = new byte[4];
                    Buffer.BlockCopy(afl.Value, i, AFL, 0, 4);

                    ApplicationFileLocator fileLocator = new ApplicationFileLocator(AFL);
                    applicationFileLocators.Add(fileLocator);
                }

                ASN1 aipafl = new ASN1(response.Data);
                string pan = string.Empty;

                foreach (ApplicationFileLocator file in applicationFileLocators)
                {
                    int r = file.FirstRecord;// +afl.OfflineRecords;     // We'll read SDA records too
                    int lr = file.LastRecord;

                    byte p2 = (byte)((file.SFI << 3) | 4);

                    ASN1 recordChld = null;
                    while (r <= lr)
                    {
                        apdu = new APDUCommand(0x00, 0xB2, (byte)r, p2, null, 0x00);
                        response = cardReader.Transmit(apdu);
                        // Retry with correct length
                        if (response.SW1 == 0x6C)
                        {
                            apdu = new APDUCommand(0x00, 0xB2, (byte)r, p2, null, response.SW2);
                            response = cardReader.Transmit(apdu);
                        }

                        recordChld = new ASN1(response.Data);
                        foreach (ASN1 a in recordChld)
                        {
                            var tag = byteToString(a.Tag);
                            if (tag.Equals(Setings.PAN_TAG) && byteToString(a.Value).Length >= 16)
                            {
                                pan = byteToString(a.Value).Substring(0, 16);
                            }

                        }
                        r++;
                    }
                }


                apdu = new APDUCommand(0x80, 0xCA, 0x9f, 0x13, null, 0);
                response = cardReader.Transmit(apdu);

                apdu = new APDUCommand(0x80, 0xCA, 0x9f, 0x17, null, 0);
                response = cardReader.Transmit(apdu);

                apdu = new APDUCommand(0x80, 0xCA, 0x9f, 0x36, null, 0);
                response = cardReader.Transmit(apdu);

                resulData.State = string.IsNullOrEmpty(pan) ? ResponseType.Warning : ResponseType.Success;
                resulData.Message = string.IsNullOrEmpty(pan) ? "Tarjeta no válida" : pan;
                resulData.CodeBase = string.IsNullOrEmpty(pan) ? "CARD_NOVALID" : "PAN";

                if (resulData.State == ResponseType.Success && !Setings.LIST_ALLOWBIN.Any(x => pan.StartsWith(x)))
                {
                    resulData.Message = string.IsNullOrEmpty(pan) ? $"Tarjeta no válida (BIN no permitido) {pan}" : pan;
                    resulData.CodeBase = "CARD_NOBINVALID";
                    resulData.State = ResponseType.Warning;
                }
            }
            catch (Exception ex)
            {
                resulData.CodeBase = "CARD_DAMMED";
                resulData.State = ResponseType.Error;
                resulData.Message = ex.Message;
            }
            return resulData;
        }

        private string byteToString(byte[] value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in value)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            return sb.ToString();
        }

        private static byte[] HexStringToBytes(string hexString)
        {
            if (hexString == null)
            {
                throw new ArgumentNullException("hexString");
            }

            if ((hexString.Length & 1) != 0)
            {
                throw new ArgumentOutOfRangeException("hexString", hexString, "hexString must contain an even number of characters.");
            }

            byte[] result = new byte[hexString.Length / 2];

            for (int i = 0; i < hexString.Length; i += 2)
            {
                result[i / 2] = byte.Parse(hexString.Substring(i, 2), NumberStyles.HexNumber);
            }

            return result;
        }
        private void cardReader_CardRemoved(string reader)
        {

        }
        private void cardReader_CardInserted(string reader, byte[] atr)
        {

        }

        public abstract void Dispose();

        private (string, string) NameOfCallingClass()
        {
            string fullName;
            string methodName = string.Empty;
            string ClassName = string.Empty;

            Type declaringType;
            int skipFrames = 2;
            do
            {
                MethodBase method = new StackFrame(skipFrames, false).GetMethod();
                declaringType = method.DeclaringType;
                if (declaringType == null)
                {
                    return (method.DeclaringType.FullName, method.Name);
                }
                skipFrames++;
                fullName = declaringType.FullName;
                methodName = method.Name;
                ClassName = method.DeclaringType.FullName;
            }
            while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));

            return (ClassName, methodName);
        }
    }
}
