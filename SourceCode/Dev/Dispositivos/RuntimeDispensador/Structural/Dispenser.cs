using Foundation.Stone.Application.Wrapper;
using Foundation.Stone.CrossCuting.File;
using Foundation.Stone.CrossCuting.Util;
using Interop.Main.Cross.Domain.Dispenser;
using Interop.Main.Cross.Domain.Orchestrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuntimeDispensador.Structural
{
    public class Dispenser
    {
        public static Dispenser CurrentDispenser = null;
        public static GlobalConfigATM globalConfigATM = null;
        private class SequenceOffSetMount
        {
            public int Squence { get; set; }
            public int Court { get; set; }
            public int TotalNotes { get; set; }
        }

        public static void InitDispenser(GlobalConfigATM _globalConfigATM, bool force = false)
        {

            if (CurrentDispenser == null || force)
            {
                CurrentDispenser = new Dispenser
                {
                    Cassettes = _globalConfigATM.configDispenserStatus.Cassettes,
                    Tipo = _globalConfigATM.configDispenserStatus.Tipo,
                    MaxQuantityBill = _globalConfigATM.configDispenserStatus.MaxQuantityBill,
                    NumBandejas = _globalConfigATM.configDispenserStatus.NumBandejas
                };
                globalConfigATM = _globalConfigATM;
            }

        }

        private Dispenser()
        {
            Cassettes = new List<Cassette>();
            MaxQuantityBill = 30;
        }

        public int MaxQuantityBill { get; set; }
        public List<Cassette> Cassettes { get; set; }
        public int NumBandejas { get; set; }
        public string Tipo { get; set; }
        public ResponseObject<ResponseOffSetMount> OffSetMountToString(int mountDispenser, bool force = false, int pAmountToSplit = 0)
        {
            ResponseObject<ResponseOffSetMount> result = new ResponseObject<ResponseOffSetMount>
            {
                State = ResponseType.Success,
                Message = "El Dispensador puede otorgar la cantidad solicitada"
            };
            int mountDispenserCount = mountDispenser;
            List<SequenceOffSetMount> sequenceDispenser = new List<SequenceOffSetMount>();

            ///TODO: deshabilitacion de cassetes cn menos del minimo
            Cassettes.ForEach(x =>
            {
                if (x.Status == "0" && x.TotalQuantity <= x.MinQuantity)
                {
                    x.Status = "2";
                }
                else if (x.Status == "2" && x.TotalQuantity > x.MinQuantity)
                {
                    x.Status = "0";
                }

            });

            //verificamos que exista el monto necesario si no es dispensacion forzada
            if (!force)
            {
                if (Cassettes.Where(z => z.Status.Equals("0")).Sum(x => (x.TotalQuantity - x.MinQuantity) * x.CurrencyCourt) < mountDispenser)
                {
                    result.State = ResponseType.Warning;
                    result.Message = "Saldo del ATM Insuficiente";
                    return result;
                }
            }

            ///TODO si es dispensacion forzosa llenamos las bandejas
            if (force)
            {
                Cassettes.ForEach(x =>
                {
                    x.TotalQuantity = 2000;
                });
            }

            //Si es fraccionamiento
            Cassettes.Where(x => x.CurrencyCourt == pAmountToSplit)
                .ToList()
                .ForEach(y => { y.Status = "2"; });


            Cassettes.Where(x => x.CurrencyCourt <= mountDispenser && (x.Status.Equals("0") || x.Status.Equals("1")))
                .OrderByDescending(x => x.CurrencyCourt)
                .ToList()
                .ForEach(
                x =>
                {
                    if (mountDispenserCount > 0)
                    {
                        if ((x.TotalQuantity - x.MinQuantity) >= (mountDispenserCount / x.CurrencyCourt))
                        {
                            sequenceDispenser.Add(
                                new SequenceOffSetMount
                                {
                                    Squence = x.Sequence,
                                    Court = x.CurrencyCourt,
                                    TotalNotes = mountDispenserCount / x.CurrencyCourt
                                });
                            mountDispenserCount -= mountDispenserCount / x.CurrencyCourt * x.CurrencyCourt;
                        }
                        else if (x.TotalQuantity > x.MinQuantity)
                        {
                            sequenceDispenser.Add(
                                new SequenceOffSetMount
                                {
                                    Squence = x.Sequence,
                                    Court = x.CurrencyCourt,
                                    TotalNotes = x.TotalQuantity - x.MinQuantity
                                });
                            mountDispenserCount -= (x.TotalQuantity - x.MinQuantity) * x.CurrencyCourt;
                        }
                    }
                }
                );

            if (mountDispenserCount != 0)
            {
                result.State = ResponseType.Warning;
                result.Message = "No se cuenta con las denominaciones para dispensar";
                return result;
            }

            int NBill = sequenceDispenser.Sum(x => x.TotalNotes);

            if (NBill <= MaxQuantityBill)
            {
                result.State = ResponseType.Success;
                result.Message = "Exito";
                result.Object = new ResponseOffSetMount
                {
                    Detail = new List<ItemOffSet>(),
                    Mount = mountDispenser,
                    Comand = ""
                };

                sequenceDispenser
                    .ForEach(x =>
                    {
                        if (x.TotalNotes > 0)
                        {
                            result.Object.Comand += x.Squence.ToString() + x.TotalNotes.ToString().PadLeft(3, '0');
                            result.Object.Detail.Add(new ItemOffSet
                            {
                                Sequence = x.Squence,
                                TotalNotes = x.TotalNotes,
                                Court = x.Court
                            });
                        }
                    }
                    );
            }
            else
            {
                result.State = ResponseType.NoData;
                result.Message = "Se requiere Demasiados billetes para el monto solicitado, imposible continuar!";
                result.Object = null;
            }


            return result;
        }

        public ResponseObject<ResponseOffSetMount> OffSetMountToStringBalance(int mountDispenser, int pAmountToSplit = 0)
        {
            ResponseObject<ResponseOffSetMount> result = new ResponseObject<ResponseOffSetMount>
            {
                State = ResponseType.Success,
                Message = "El Dispensador puede otorgar la cantidad solicitada"
            };
            int mountDispenserCount = mountDispenser;
            Dictionary<int, int> sequenceDispenser = new Dictionary<int, int>();
            //verificamos que exista el monto necesario si no es dispensacion forzada


            if (Cassettes.Where(z => z.Status.Equals("0")).Sum(x => (x.TotalQuantity - x.MinQuantity) * x.CurrencyCourt) < mountDispenser)
            {
                result.State = ResponseType.Warning;
                result.Message = "Saldo del ATM Insuficiente";
                return result;
            }


            ///TODO: deshabilitacion de cassetes cn menos del minimo
            Cassettes.ForEach(x =>
            {

                if (x.TotalQuantity <= x.MinQuantity)
                {
                    x.Status = "2";
                }
            });
            List<Delivery> deliverys = new List<Delivery>();
            int intento = 0;
            int piezas = 0;

            int indice = 0;
            bool salir = false;
            foreach (Cassette cassette in Cassettes)
            {// si el estado de bandejas es 0 o 1 se adiciona para el calculo
                if ((cassette.Status == "0" || cassette.Status == "1") && cassette.CurrencyCourt != pAmountToSplit)
                {
                    deliverys.Add(new Delivery { Sequence = cassette.Sequence, Court = cassette.CurrencyCourt, Available = cassette.TotalQuantity, Deliver = 0, MinAvailable = cassette.MinQuantity });
                }
            }

            indice = deliverys.Count - 1;
            do
            {
                do
                {
                    int indiceAux = indice == 0 ? indice : indice - 1;
                    if ((deliverys[indice].Available + intento) < deliverys[indiceAux].Available || deliverys[indice].Available < deliverys[indice].MinAvailable)
                    {
                        indice--;
                    }
                    else
                    {
                        if (mountDispenserCount >= deliverys[indice].Court)
                        {
                            deliverys[indice].Deliver++;
                            deliverys[indice].Available--;
                            mountDispenserCount = mountDispenserCount - deliverys[indice].Court;
                            piezas++;

                        }
                        else
                        {
                            indice--;
                        }
                    }
                }
                while (mountDispenserCount != 0 && indice >= 0);
                if (piezas <= 30 && mountDispenserCount == 0)
                {
                    salir = true;
                }
                else
                {
                    intento++;
                    mountDispenserCount = mountDispenser;
                    indice = deliverys.Count - 1;
                    piezas = 0;
                    deliverys.ForEach(x => x.Deliver = 0);
                }

            }
            while (!salir && intento < 30);

            if (mountDispenserCount != 0)
            {
                result.State = ResponseType.Warning;
                result.Message = "No se cuenta con las denominaciones para dispensar";
                return result;
            }

            //int NBill = 0;
            //sequenceDispenser.Keys.ToList().ForEach(x => { NBill += sequenceDispenser[x]; });

            if (piezas <= MaxQuantityBill)
            {
                result.State = ResponseType.Success;
                result.Message = "Exito";
                result.Object = new ResponseOffSetMount
                {
                    Detail = new List<ItemOffSet>(),
                    Mount = mountDispenser,
                    Comand = ""
                };
                deliverys.ForEach(d =>
                {
                    if (d.Deliver > 0)
                    {
                        result.Object.Comand += d.Sequence.ToString() + d.Deliver.ToString().PadLeft(3, '0');
                        result.Object.Detail.Add(new ItemOffSet
                        {
                            Sequence = d.Sequence,
                            TotalNotes = d.Deliver
                        });
                    }
                });
            }
            else
            {
                result.State = ResponseType.NoData;
                result.Message = "Se requiere Demasiados billetes para el monto solicitado, imposible continuar!";
                result.Object = null;
            }


            return result;
        }

        public ResponseQuery<Court> GetCoutAvailable()
        {
            ResponseQuery<Court> responseObject = new ResponseQuery<Court>
            {
                ListEntities = new List<Court>(),
                State = ResponseType.Success,
                Message = "Cortes obtenidos correctamente"
            };
            try
            {
                Cassettes.Where(c => c.TotalQuantity > c.MinQuantity).ToList().ForEach(c =>
                {
                    responseObject.ListEntities.Add(new Court
                    {
                        denomination = c.CurrencyCourt.ToString(),
                        quantity = c.TotalQuantity
                    });
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los cortes disponibles en los casstess: {ex.Message} {ex.StackTrace}");
            }
            return responseObject;
        }
    }
    public class Delivery
    {
        public int Sequence { get; set; }
        public int Court { get; set; }
        public int Available { get; set; }
        public int Deliver { get; set; }
        public int MinAvailable { get; set; }
    }
}
