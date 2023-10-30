using Foundation.Stone.Application.Wrapper;
using RuntimeCardReader.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using Interop.Main.Cross.Domain.CardReader;
using System.Threading.Tasks;

namespace RuntimeCardReader.Core.Implement.Creator
{
    public class ParamCommand
    {
        public string NameComand { get; set; }
        public byte Cm { get; set; }
        public byte Pm { get; set; }
        public UInt16 TxDataLen { get; set; }
        public UInt16 RxDataLen { get; set; }
        public byte[] TxData { get; set; }
        public byte[] RxData { get; set; }
        public byte ReType { get; set; }
        public byte St0 { get; set; }
        public byte St1 { get; set; }

        public CommandCardReader ToCommandCardReader()
        {
            CommandCardReader resul = new CommandCardReader
            {
                NameCommand = this.NameComand,
                Cm = Convert.ToString((char)this.Cm),
                Pm = Convert.ToString((char)this.Pm),
                ReType = Convert.ToString((char)this.ReType),
                RxData = this.RxData == null || this.RxDataLen == 0 ? "" : Convert.ToBase64String(this.RxData),
                RxDataLen = Convert.ToInt16(this.RxDataLen),
                St0 = Convert.ToString((char)this.St0),
                St1 = Convert.ToString((char)this.St1),
                TxData = this.TxData == null ? "" : Convert.ToBase64String(this.TxData),
                TxDataLen = Convert.ToInt16(this.TxDataLen),
            };
            return resul;
        }
    }

    public class CreatorReader : ReaderCard
    {
        public enum ledColor
        {
            ledoff = 0x30,
            ledgreen = 0x31,
            ledred = 0x32,
            ledorange = 0x33
        }

        public CreatorReader() : base()
        {
            TypeReaderCard = TypeReaderCard.CREATOR;
        }
        internal class DataComand
        {


            internal enum command
            {
                initReader = 0,
                allowAllInCard = 1,
                eject = 2,
                card_status = 3,
                contact_card = 4,
                initReaderSilence = 5,
                sensor_verify = 6,
                realeasecontact_card = 7,
                ledPowerOff = 8,
                ledPowerGreen = 9,
                ledPowerRed = 10,
                ledPowerOrange = 11,
                ledBlink = 12,
                ledPowerGreenHold = 13
            }

            private static Dictionary<command, ParamCommand> _StoreCommnads;
            internal static Dictionary<command, ParamCommand> StoreCommnads
            {
                get
                {
                    if (_StoreCommnads == null)
                    {
                        _StoreCommnads = new Dictionary<command, ParamCommand>();
                        ///TODO parametrizamos el comando de inicializacion
                        _StoreCommnads.Add(command.initReader, new ParamCommand
                        {
                            Cm = 0x30,
                            Pm = 0x34,
                            St0 = 0,
                            St1 = 0,
                            RxDataLen = 0,
                            TxDataLen = 11,
                            RxData = new byte[1024],
                            ReType = 0,
                            TxData = new byte[11] { 0x33, 0x32, 0x34, 0x30, 0x30, 0x33, 0x30, 0x31, 0x30, 0x30, 0x31 }
                        });
                        ///TODO parametrizamos el comando para permitir el acceso de la tarjeta en todo momento
                        _StoreCommnads.Add(command.allowAllInCard, new ParamCommand
                        {
                            Cm = 0x3A,
                            Pm = 0x30,
                            St0 = 0,
                            St1 = 0,
                            TxDataLen = 0,
                            RxDataLen = 0
                        });
                        ///TODO parametrizamos el comando para expulsar la tarjeta
                        _StoreCommnads.Add(command.eject, new ParamCommand
                        {
                            Cm = 0x33,
                            Pm = 0x30,
                            St0 = 0,
                            St1 = 0,
                            TxDataLen = 0,
                            RxDataLen = 0,
                        });
                        ///TODO parametrizamos el comando para obtener el estado de la tarjeta
                        _StoreCommnads.Add(command.card_status, new ParamCommand
                        {
                            Cm = 0x31,
                            Pm = 0x30,
                            St0 = 0,
                            St1 = 0,
                            TxDataLen = 0,
                            RxDataLen = 0,
                        });
                        ///TODO parametrizamos el comando para realizar el contacto de la tarjeta
                        _StoreCommnads.Add(command.contact_card, new ParamCommand
                        {
                            Cm = 0x40,
                            Pm = 0x30,
                            St0 = 0,
                            St1 = 0,
                            RxDataLen = 0,
                            TxDataLen = 0
                        });
                        ///TODO parametrizamos el comando para realizar el contacto de la tarjeta
                        _StoreCommnads.Add(command.realeasecontact_card, new ParamCommand
                        {
                            Cm = 0x40,
                            Pm = 0x32,
                            St0 = 0,
                            St1 = 0,
                            RxDataLen = 0,
                            TxDataLen = 0
                        });
                        ///TODO parametrizamos el comando de inicializacion
                        _StoreCommnads.Add(command.initReaderSilence, new ParamCommand
                        {
                            Cm = 0x30,
                            Pm = 0x33,
                            St0 = 0,
                            St1 = 0,
                            RxDataLen = 0,
                            TxDataLen = 11,
                            RxData = new byte[1024],
                            ReType = 0,
                            TxData = new byte[11] { 0x33, 0x32, 0x34, 0x30, 0x30, 0x33, 0x30, 0x31, 0x30, 0x30, 0x31 }
                        });
                        ///TODO parametrizamos el comando de verificacion del sensor
                        _StoreCommnads.Add(command.sensor_verify, new ParamCommand
                        {
                            Cm = 0x31,
                            Pm = 0x40,
                            St0 = 0,
                            St1 = 0,
                            TxDataLen = 0,
                            RxDataLen = 0,
                            ReType = 0,
                            RxData = new byte[1024],
                            TxData = new byte[1024]
                        });

                        ///TODO parametrizamos el comando para el manejo del LED                        
                        _StoreCommnads.Add(command.ledPowerOff, new ParamCommand
                        {
                            Cm = 0x35,
                            Pm = (int)ledColor.ledoff,
                            St0 = 0,
                            St1 = 0,
                            TxDataLen = 0,
                            RxDataLen = 0,
                            ReType = 0,
                            RxData = new byte[1024],
                            TxData = new byte[1024]
                        });
                        _StoreCommnads.Add(command.ledPowerGreen, new ParamCommand
                        {
                            Cm = 0x35,
                            Pm = (int)ledColor.ledgreen,
                            St0 = 0,
                            St1 = 0,
                            TxDataLen = 4,
                            RxDataLen = 0,
                            ReType = 0,
                            RxData = new byte[1024],
                            TxData = new byte[4] { 0x31, 0x30, 0x30, 0x35 }
                        });
                        _StoreCommnads.Add(command.ledPowerGreenHold, new ParamCommand
                        {
                            Cm = 0x35,
                            Pm = (int)ledColor.ledgreen,
                            St0 = 0,
                            St1 = 0,
                            TxDataLen = 0,
                            RxDataLen = 0,
                            ReType = 0,
                            RxData = new byte[1024],
                            TxData = new byte[1024]
                        });
                        _StoreCommnads.Add(command.ledPowerRed, new ParamCommand
                        {
                            Cm = 0x35,
                            Pm = (int)ledColor.ledred,
                            St0 = 0,
                            St1 = 0,
                            TxDataLen = 4,
                            RxDataLen = 0,
                            ReType = 0,
                            RxData = new byte[1024],
                            TxData = new byte[4] { 0x31, 0x30, 0x30, 0x35 }
                        });
                        _StoreCommnads.Add(command.ledPowerOrange, new ParamCommand
                        {
                            Cm = 0x35,
                            Pm = (int)ledColor.ledorange,
                            St0 = 0,
                            St1 = 0,
                            TxDataLen = 4,
                            RxDataLen = 0,
                            ReType = 0,
                            RxData = new byte[1024],
                            TxData = new byte[4] { 0x31, 0x30, 0x30, 0x35 }
                        });
                    }
                    return _StoreCommnads;
                }
            }

            public byte Cm { get; set; }
            public byte Pm { get; set; }
            public UInt16 TxDataLen { get; set; }
            public UInt16 RxDataLen { get; set; }
            public byte[] TxData { get; set; }
            public byte[] RxData { get; set; }
            public byte ReType { get; set; }
            public byte St0 { get; set; }
            public byte St1 { get; set; }

            internal static ParamCommand ExecuteCommand(command command)
            {
                ParamCommand dataComand = StoreCommnads.FirstOrDefault(x => x.Key == command).Value;
                dataComand.NameComand = command.ToString();
                byte _ReType = 0, _St0 = 0, _St1 = 0;
                UInt16 _RxDataLen = 0;
                if (IntrefaceAPICreator.USB_ExeCommand(Hdle, dataComand.Cm, dataComand.Pm, dataComand.TxDataLen, dataComand.TxData, ref _ReType, ref _St0, ref _St1, ref _RxDataLen, dataComand.RxData) != 0)
                {
                    throw new Exception($"Error al ejecutar el comando {command}");
                }
                dataComand.ReType = _ReType; dataComand.St0 = _St0; dataComand.St1 = _St1; dataComand.RxDataLen = _RxDataLen;
                return dataComand;
            }

        }
        internal static UInt32 Hdle = 0;

        #region metodos disponibles
        public override ResponseQuery<CommandCardReader> InitReader()
        {
            //Response resul = new Response { };
            ResponseQuery<CommandCardReader> resul = new ResponseQuery<CommandCardReader>() { ListEntities = new List<CommandCardReader>() };
            List<CommandCardReader> cmd = new List<CommandCardReader>();
            try
            {
                status = status_reader.bussy;
                Hdle = IntrefaceAPICreator.CRT310NUOpen();
                if (Hdle == 0)
                {
                    resul.Message = "Lector no encontrado, imposible inicializar";
                    resul.State = ResponseType.Warning;
                    status = status_reader.damaged;
                    ProcessError(resul, new Exception(resul.Message));
                    return resul;
                }

                var resulInit = DataComand.ExecuteCommand(DataComand.command.initReader);
                cmd.Add(resulInit.ToCommandCardReader());
                if (resulInit.ReType != 0x50)
                {
                    resul.Message = "INITIALIZE ERROR. Error Code:  " + (char)resulInit.St0 + (char)resulInit.St1;
                    resul.State = ResponseType.Warning;
                    status = status_reader.damaged;
                    ProcessError(resul, new Exception(resul.Message));
                    return resul;
                }

                var resulAllowInCard = DataComand.ExecuteCommand(DataComand.command.allowAllInCard);
                cmd.Add(resulAllowInCard.ToCommandCardReader());
                if (resulAllowInCard.ReType != 0x50)
                {
                    resul.Message = "Allowed card in ERROR." + "\r\n" + "Error Code:  " + (char)resulAllowInCard.ReType + (char)resulAllowInCard.St0 + (char)resulAllowInCard.St1;
                    resul.State = ResponseType.Warning;
                    status = status_reader.damaged;
                    ProcessError(resul, new Exception(resul.Message));
                    return resul;
                }

                var resulLed = DataComand.ExecuteCommand(DataComand.command.ledPowerGreen);
                cmd.Add(resulLed.ToCommandCardReader());
                if (resulLed.ReType != 0x50)
                {
                    resul.Message = "Led Power Green ERROR" + "\r\n" + "Error Code:  " + (char)resulLed.ReType + (char)resulLed.St0 + (char)resulLed.St1;
                    resul.State = ResponseType.Warning;
                    ProcessError(resul, new Exception(resul.Message));
                }

                resul.State = ResponseType.Success;
                resul.Message = "Card Reader Ready";
                status = status_reader.ready;
                resul.ListEntities = cmd;
            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
                status = status_reader.damaged;
            }
            return resul;
        }
        public ResponseQuery<CommandCardReader> InitReaderSilence()
        {
            ResponseQuery<CommandCardReader> resul = new ResponseQuery<CommandCardReader>();
            List<CommandCardReader> cmd = new List<CommandCardReader>();
            try
            {
                status = status_reader.bussy;
                Hdle = IntrefaceAPICreator.CRT310NUOpen();
                if (Hdle == 0)
                {
                    resul.Message = "InitReaderSilence: Lector no encontrado";
                    resul.State = ResponseType.Warning;
                    status = status_reader.damaged;
                    ProcessError(resul, new Exception(resul.Message));
                    return resul;
                }

                var resulInit = DataComand.ExecuteCommand(DataComand.command.initReaderSilence);
                cmd.Add(resulInit.ToCommandCardReader());
                if (resulInit.ReType != 0x50)
                {
                    resul.Message = "InitReaderSilence: INITIALIZE ERROR. Error Code:  " + (char)resulInit.St0 + (char)resulInit.St1;
                    resul.State = ResponseType.Warning;
                    status = status_reader.damaged;
                    ProcessError(resul, new Exception(resul.Message));
                    return resul;
                }

                var resulAllowInCard = DataComand.ExecuteCommand(DataComand.command.allowAllInCard);
                cmd.Add(resulAllowInCard.ToCommandCardReader());
                if (resulAllowInCard.ReType != 0x50)
                {
                    resul.Message = "InitReaderSilence: Allowed card in ERROR. " + "\r\n" + "Error Code:  " + (char)resulAllowInCard.ReType + (char)resulAllowInCard.St0 + (char)resulAllowInCard.St1;
                    resul.State = ResponseType.Warning;
                    status = status_reader.damaged;
                    ProcessError(resul, new Exception(resul.Message));
                    return resul;
                }

                var resulLed = DataComand.ExecuteCommand(DataComand.command.ledPowerGreen);
                cmd.Add(resulLed.ToCommandCardReader());
                if (resulLed.ReType != 0x50)
                {
                    resul.Message = "InitReaderSilence: Led Power Green ERROR" + "\r\n" + "Error Code:  " + (char)resulLed.ReType + (char)resulLed.St0 + (char)resulLed.St1;
                    resul.State = ResponseType.Warning;
                    ProcessError(resul, new Exception(resul.Message));
                }

                resul.State = ResponseType.Success;
                resul.Message = "Card Reader Ready";
                status = status_reader.ready;
                resul.ListEntities = cmd;
            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
                status = status_reader.damaged;
            }
            return resul;
        }
        public override ResponseQuery<CommandCardReader> EjectCard()
        {
            ResponseQuery<CommandCardReader> resul = new ResponseQuery<CommandCardReader>() { ListEntities = new List<CommandCardReader>() };
            try
            {
                status = status_reader.bussy;
                var resulVerif = VerificarStatusReader();
                if (resulVerif.State != ResponseType.Success)
                {
                    resul = resulVerif;
                    resul.Message = "Lector con estado no valido: " + resul.Message;
                    status = status_reader.damaged;
                    return resul;
                }

                ///TODO liberamos la tarejta del contact
                var resulrelease_contect = DataComand.ExecuteCommand(DataComand.command.realeasecontact_card);
                resul.ListEntities.Add(resulrelease_contect.ToCommandCardReader());
                ///TODO expulsamos la tarjeta
                var resulInit = DataComand.ExecuteCommand(DataComand.command.eject);
                resul.ListEntities.Add(resulInit.ToCommandCardReader());
                if (resulInit.ReType != 0x50)
                {
                    resul.Message = "Eject Card ERROR. Error Code:  " + (char)resulInit.St0 + (char)resulInit.St1 + (char)resulInit.ReType;
                    resul.State = ResponseType.Warning;
                    ProcessError(resul, new Exception(resul.Message));
                    status = status_reader.damaged;
                    return resul;
                }

                var resulLed = DataComand.ExecuteCommand(DataComand.command.ledPowerGreen);
                resul.ListEntities.Add(resulLed.ToCommandCardReader());
                if (resulLed.ReType != 0x50)
                {
                    resul.Message = "EjectCard Led Power Green ERROR" + "\r\n" + "Error Code:  " + (char)resulLed.ReType + (char)resulLed.St0 + (char)resulLed.St1;
                    resul.State = ResponseType.Warning;
                    ProcessError(resul, new Exception(resul.Message));
                }

                resul.State = ResponseType.Success;
                resul.Message = "Card remove sucess";
                status = status_reader.ready;
            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
                status = status_reader.ready;
            }
            return resul;
        }
        public override ResponseQuery<CommandCardReader> ReadCard()
        {
            ResponseQuery<CommandCardReader> resul = new ResponseQuery<CommandCardReader>();
            List<CommandCardReader> cmd = new List<CommandCardReader>();
            try
            {
                status = status_reader.bussy;

                resul = VerificarStatusReader();
                if (resul.State != ResponseType.Success)
                {
                    resul.Message = "Lector con estado no valido: " + resul.Message;
                    status = status_reader.damaged;
                    ProcessError(resul, new Exception(resul.Message));
                    return resul;
                }

                ///TODO verificamos el estado de la tarjeta
                var resulInit = DataComand.ExecuteCommand(DataComand.command.card_status);
                cmd.Add(resulInit.ToCommandCardReader());
                if (resulInit.ReType != 0x50)
                {
                    resul.Message = "Status Card ERROR. Error Code:  " + (char)resulInit.St0 + (char)resulInit.St1;
                    resul.State = ResponseType.Warning;
                    status = status_reader.ready;
                    ProcessError(resul, new Exception(resul.Message));
                    return resul;
                }

                switch (resulInit.St1)
                {
                    case 48:
                        {
                            resul.Message = "No existe tarjeta en el lector";
                            resul.CodeBase = "NO_CARD";
                            resul.State = ResponseType.Warning;
                            status = status_reader.ready;
                            return resul;
                        }
                    case 49:
                        {

                            resul.Message = "Terjeta en la puerta del lector";
                            resul.CodeBase = "PUSH_CARD";
                            resul.State = ResponseType.Warning;
                            status = status_reader.ready;
                            return resul;
                        }
                    case 50:
                        {
                            resul.Message = "Tarjeta lista para operar";
                            resul.State = ResponseType.Success;
                            status = status_reader.ready;
                            break;
                        }
                }

                ///TODO realizamos el contacto de la tarjeta
                resulInit = DataComand.ExecuteCommand(DataComand.command.contact_card);
                cmd.Add(resulInit.ToCommandCardReader());
                if (resulInit.ReType != 0x50)
                {
                    resul.Message = "CONTACT CARD ERROR" + "\r\n" + "Error Code:  " + (char)resulInit.St0 + (char)resulInit.St1 + "INITIALIZE";
                    resul.State = ResponseType.Warning;
                    status = status_reader.damaged;
                    ProcessError(resul, new Exception(resul.Message));
                    return resul;
                }

                var resulLed = DataComand.ExecuteCommand(DataComand.command.ledPowerGreenHold);
                cmd.Add(resulLed.ToCommandCardReader());
                if (resulLed.ReType != 0x50)
                {
                    resul.Message = "Led Power Green Hold ERROR" + "\r\n" + "Error Code:  " + (char)resulLed.ReType + (char)resulLed.St0 + (char)resulLed.St1;
                    resul.State = ResponseType.Warning;
                    ProcessError(resul, new Exception(resul.Message));
                }

                ///TODO leemos las tarjeta
                Task.WaitAll(Task.Delay(2000).ContinueWith((task) =>
                {
                    var resulTask = CardPanning();
                    resul.Message = resulTask.Message;
                    resul.State = resulTask.State;
                    resul.CodeBase = resulTask.CodeBase;
                }));

                resulLed = DataComand.ExecuteCommand(DataComand.command.ledPowerGreen);
                cmd.Add(resulLed.ToCommandCardReader());
                if (resulLed.ReType != 0x50)
                {
                    resul.Message = "Led Power Green ERROR" + "\r\n" + "Error Code:  " + (char)resulLed.ReType + (char)resulLed.St0 + (char)resulLed.St1;
                    resul.State = ResponseType.Warning;
                    ProcessError(resul, new Exception(resul.Message));
                }

                ///liberamos la tarejta del contact
                var resulrelease_contect = DataComand.ExecuteCommand(DataComand.command.realeasecontact_card);
                cmd.Add(resulrelease_contect.ToCommandCardReader());
                resul.ListEntities = cmd;
                status = status_reader.ready;
            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
                status = status_reader.ready;
            }
            return resul;
        }


        public Response Latch()
        {
            Response resul = new Response { };
            try
            {
                status = status_reader.bussy;
                resul = VerificarStatusReader();
                if (resul.State != ResponseType.Success)
                {
                    resul.Message = "Lector con estado no valido: " + resul.Message;
                    status = status_reader.damaged;
                    return resul;
                }

                ///TODO verificamos el estado de la tarjeta
                var resulInit = DataComand.ExecuteCommand(DataComand.command.card_status);
                if (resulInit.ReType != 0x50)
                {
                    resul.Message = "Eject Card ERROR. Error Code:  " + (char)resulInit.St0 + (char)resulInit.St1;
                    resul.State = ResponseType.Warning;
                    status = status_reader.damaged;
                    return resul;
                }

                switch (resulInit.St1)
                {
                    case 48:
                        {
                            resul.Message = "No existe tarjeta en el lector";
                            resul.CodeBase = "NO_CARD";
                            resul.State = ResponseType.Warning;
                            status = status_reader.ready;
                            return resul;
                        }
                    case 49:
                        {

                            resul.Message = "Terjeta en la puerta del lector";
                            resul.CodeBase = "PUSH_CARD";
                            resul.State = ResponseType.Warning;
                            status = status_reader.ready;
                            return resul;
                        }
                    case 50:
                        {
                            resul.Message = "Tarjeta lista para operar";
                            resul.State = ResponseType.Success;
                            break;
                        }
                }

                ///TODO realizamos el contacto de la tarjeta
                resulInit = DataComand.ExecuteCommand(DataComand.command.contact_card);
                if (resulInit.ReType != 0x50)
                {
                    resul.Message = "CONTACT CARD ERROR" + "\r\n" + "Error Code:  " + (char)resulInit.St0 + (char)resulInit.St1 + "INITIALIZE";
                    ProcessError(resul, new Exception(resul.Message));
                    status = status_reader.damaged;
                    resul.State = ResponseType.Warning;
                    return resul;
                }

                status = status_reader.ready;
            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
                status = status_reader.ready;
            }
            return resul;
        }

        public Response LedOn(ledColor _ledColor)
        {
            Response resul = new Response { };
            try
            {
                resul = VerificarStatusReader();
                if (resul.State != ResponseType.Success)
                {
                    resul.Message = "Lector con estado no valido: " + resul.Message;
                    return resul;
                }

                status = status_reader.bussy;
                DataComand.command _command = new DataComand.command();
                ///TODO verificamos el estado de la tarjeta
                switch (_ledColor)
                {
                    case ledColor.ledoff:
                        _command = DataComand.command.ledPowerOff;
                        break;
                    case ledColor.ledgreen:
                        _command = DataComand.command.ledPowerGreen;
                        break;
                    case ledColor.ledred:
                        _command = DataComand.command.ledPowerRed;
                        break;
                    case ledColor.ledorange:
                        _command = DataComand.command.ledPowerOrange;
                        break;
                    default:
                        _command = DataComand.command.ledPowerOff;
                        break;
                }
                var resulInit = DataComand.ExecuteCommand(_command);
                if (resulInit.ReType != 0x50)
                {
                    resul.Message = "Led ON ERROR. Error Code:  " + (char)resulInit.St0 + (char)resulInit.St1;
                    resul.State = ResponseType.Warning;
                    return resul;
                }

                status = status_reader.ready;
            }
            catch (Exception ex)
            {
                ProcessError(resul, ex);
                status = status_reader.ready;
            }
            return resul;
        }
        public override ResponseQuery<CommandCardReader> Reset()
        {
            if (Hdle != 0)
            {
                try
                {
                    int i = IntrefaceAPICreator.CRT310NUClose(Hdle);                    
                }
                catch (Exception ex)
                {
                    ProcessError(new Response(), new Exception("desconexion subita!", ex));
                    status = status_reader.damaged;
                    Hdle = 0;
                }

            }
            cardReader = null;            
            var resulInit = InitReader();            
            if (resulInit.State == ResponseType.Success)
            {
                Task.WaitAll(Task.Delay(2000).ContinueWith((task) =>
                {
                    resulInit = EjectCard();
                }));                
                status = resulInit.State != ResponseType.Success ? status_reader.damaged : status_reader.ready;
            }
            else
            {
                ProcessError(new Response(), new Exception("Imposible reiniciar lector: " + resulInit.Message));
                status = status_reader.damaged;
            }
            status = status_reader.ready;
            return resulInit;
        }
        public override ResponseQuery<CommandCardReader> VerificarStatusReader()
        {
            ResponseQuery<CommandCardReader> resul = new ResponseQuery<CommandCardReader>
            {
                State = ResponseType.Success,
                Message = "Estado correcto del lector",
                ListEntities = new List<CommandCardReader>()
            };
            try
            {
                if (Hdle == 0)
                {
                    resul = InitReader();
                    status = resul.State == ResponseType.Success ? status_reader.ready : status_reader.damaged;
                    if (resul.State != ResponseType.Success)
                    {
                        ProcessError(resul, new Exception(resul.Message), System.Diagnostics.EventLogEntryType.Warning);
                    }
                    resul.Message += ". Operacion de inicializacion a partir de la verificacion";
                    return resul;
                }

                ///verificamos el sensor para ver la disponibilidad del lector
                try
                {
                    var resulVerifySensor = DataComand.ExecuteCommand(DataComand.command.sensor_verify);
                    resul.ListEntities.Add(resulVerifySensor.ToCommandCardReader());
                }
                catch (Exception ex)
                {
                    status = status_reader.damaged;
                    ProcessError(resul, new Exception("VerificarStatusReader: Imposible verificar el sensor del lector", ex));
                    status = status_reader.damaged;
                }

                return resul;
            }
            catch (Exception ex)
            {
                ProcessError(resul, new Exception("VerificarStatusReader: Error al verificar el estado de lector", ex));
                resul.Message += "Lector dañado, imposible verificar el estado";
            }
            return resul;
        }
        #endregion

        #region artefactos utilitarios              
        public override void Dispose()
        {
            if (Hdle != 0)
            {
                int i = IntrefaceAPICreator.CRT310NUClose(Hdle);
                Hdle = 0;
            }
        }

        #endregion


    }
}
