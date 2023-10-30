using Foundation.Stone.Application.Wrapper;
using Interop.Main.Cross.Domain.Receptor;
using MPOST;
using OrchestratorDevice.Global;
using OrchestratorDevice.Managers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RuntimeReceptor.Core.Implemernt
{
    public class ReceptorMEI : IReceptor
    {

        private static MPOST.Acceptor BillAcceptor = new MPOST.Acceptor();
        private List<ItemReceptor> bills = new List<ItemReceptor>();
        private int MaxAmount = 0;
        public string PortCOM { get; set; }

        public Response Close()
        {
            Response response = new Response();
            BillAcceptor.EnableAcceptance = false;
            Task.Delay(4000).Wait();
            BillAcceptor.Close();
            BillAcceptor = new MPOST.Acceptor();
            return response;
        }



        public Response Eject()
        {
            Response response = new Response();
            BillAcceptor.EscrowReturn();
            response.State = Foundation.Stone.Application.Wrapper.ResponseType.Success;
            return response;
        }

        public Response GetState()
        {
            Response response = new Response();
            response.State = BillAcceptor.DeviceState == MPOST.State.Disconnected ? Foundation.Stone.Application.Wrapper.ResponseType.Warning : Foundation.Stone.Application.Wrapper.ResponseType.Success;
            response.Message = Convert.ToString(BillAcceptor.DeviceState);
            response.State = Foundation.Stone.Application.Wrapper.ResponseType.Success;
            return response;
        }

        public Response Open(int pMaxAmout )
        {
            MaxAmount = pMaxAmout;
            Response response = new Response();
            if (BillAcceptor.DeviceState == MPOST.State.Failed)
            {
                response.State = Foundation.Stone.Application.Wrapper.ResponseType.Warning;
                response.Message = "Receptor con fallas: " + BillAcceptor.LastBNFError;
                return response;
            }

            bills = new List<ItemReceptor>();
            if (BillAcceptor.DeviceState == MPOST.State.Disconnected)
            {
                BillAcceptor.OnConnected += BillAcceptor_OnConnected;
                BillAcceptor.OnDisconnected += BillAcceptor_OnDisconnected;
                //BillAcceptor.OnStacked += BillAcceptor_OnStacked;
                BillAcceptor.OnJamDetected += BillAcceptor_OnJamDetected;
                BillAcceptor.OnCashBoxRemoved += BillAcceptor_OnCashBoxRemoved;
                //BillAcceptor.OnFailureDetected += BillAcceptor_OnFailureDetected;
                BillAcceptor.OnJamCleared += BillAcceptor_OnJamCleared;
                BillAcceptor.OnRejected += BillAcceptor_OnRejected;
                BillAcceptor.OnReturned += BillAcceptor_OnReturned;
                BillAcceptor.OnSendMessageFailure += BillAcceptor_OnSendMessageFailure;
                BillAcceptor.OnStackerFull += BillAcceptor_OnStackerFull;
                BillAcceptor.OnStackedWithDocInfo += BillAcceptor_OnStackedWithDocInfo;
                BillAcceptor.OnEscrow += BillAcceptor_OnEscrow;
                
                BillAcceptor.Open(PortCOM, MPOST.PowerUp.A);
                
                
            }
            response.State = Foundation.Stone.Application.Wrapper.ResponseType.Success;
            response.Message = "Receptor Activado correctamente";
            return response;
        }

        private void BillAcceptor_OnEscrow(object sender, EventArgs e)
        {
            try
            {
                if (BillAcceptor.DocType != MPOST.DocumentType.NoValue && BillAcceptor.getDocument() != null && BillAcceptor.getDocument().ValueString != string.Empty)
                {
                    if (BillAcceptor.Bill.Country != "BOB")
                    {
                        //retorma el billete
                        BillAcceptor.EscrowReturn();
                        return;
                    }
                    if (MaxAmount != 0)
                    {
                        if (MaxAmount < GetTotalAmount() + BillAcceptor.Bill.Value)
                        {
                            BillAcceptor.EscrowReturn();
                            bills.Add(new ItemReceptor
                            {
                                Detalle = "Límite de depósito máximo",
                                Index = bills.Count + 1,
                                Moneda = "0",
                                Monto = 0,
                                Almacenado = false,
                                FechaHora = DateTime.Now
                            });
                            BillAcceptor.EnableAcceptance = false;
                            return;
                        }
                    }
                    string valueStacked = BillAcceptor.getDocument().ValueString;
                    bills.Add(new ItemReceptor
                    {
                        Detalle = valueStacked,
                        Index = bills.Count + 1,
                        Moneda = valueStacked.Substring(0, 3),
                        Monto = Convert.ToInt32(valueStacked.Split(' ')[1]),
                        Almacenado = false,
                        FechaHora = DateTime.Now
                    });
                    BillAcceptor.EscrowStack();
                }
                //else
                //{
                //    string valueStacked = BillAcceptor.getDocument().ValueString;
                //    bills.Add(new ItemReceptor
                //    {
                //        Detalle = valueStacked,
                //        Index = bills.Count + 1,
                //        Moneda = valueStacked.Substring(0, 3),
                //        Monto = Convert.ToInt32(valueStacked.Split(' ')[1]),
                //        Almacenado = true,
                //        FechaHora = DateTime.Now
                //    });
                //}
            }
            catch (Exception error)
            {
                bills.Add(new ItemReceptor
                {
                    Detalle = error.Message,
                    Index = bills.Count + 1,
                    Moneda = "0",
                    Monto = 0,
                    Almacenado = false,
                    FechaHora = DateTime.Now
                });

            }
        }

        private void BillAcceptor_OnStackedWithDocInfo(object sender, StackedEventArgs e)
        {
            try
            {
                if (e.DocType != MPOST.DocumentType.NoValue && e.Document != null && e.Document.ValueString != string.Empty)
                {
                    if (e.Document.ValueString.Contains(bills[bills.Count-1].Monto.ToString()))
                    {
                        bills[bills.Count - 1].Almacenado=true;
                    }
                }
              
                
            }
            catch (Exception error)
            {
             
            }
            
        }

        //private void BillAcceptor_OnStackedWithDocInfo(object sender, MPOST.StackedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        private void BillAcceptor_OnStackerFull(object sender, EventArgs e)
        {
            bills.Add(new ItemReceptor
            {
                Detalle = "StakerFull",
                Index = bills.Count + 1,
                Moneda = "",
                Monto = 0,
                Almacenado = false,
                FechaHora=DateTime.Now
            });
            BillAcceptor.EnableAcceptance = false;
            BlockReceiver();
        }

        private void BillAcceptor_OnSendMessageFailure(object sender, MPOST.AcceptorMessageEventArgs e)
        {
            bills.Add(new ItemReceptor
            {
                Detalle = e.Msg.Description ,
                Index = bills.Count + 1,
                Moneda = "",
                Monto = 0,
                Almacenado = false,
                FechaHora = DateTime.Now
            });
            BillAcceptor.EnableAcceptance = false;
            BlockReceiver();
        }

        private void BillAcceptor_OnReturned(object sender, EventArgs e)
        {
            bills.Add(new ItemReceptor
            {
                Detalle = "Returned",
                Index = bills.Count + 1,
                Moneda = "",
                Monto = 0,
                Almacenado = false,
                FechaHora = DateTime.Now
            }); 
        }

        private void BillAcceptor_OnRejected(object sender, EventArgs e)
        {
            bills.Add(new ItemReceptor
            {
                Detalle = "Rejected",
                Index = bills.Count + 1,
                Moneda = "",
                Monto = 0,
                Almacenado=false,
                FechaHora = DateTime.Now
            }); 
        }

        private void BillAcceptor_OnJamCleared(object sender, EventArgs e)
        {
            bills.Add(new ItemReceptor
            {
                Detalle = "JamCleared",
                Index = bills.Count + 1,
                Moneda = "",
                Monto = 0,
                Almacenado = false,
                FechaHora = DateTime.Now
            });
            BillAcceptor.EnableAcceptance = false;
        }

        //private void BillAcceptor_OnFailureDetected(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        private void BillAcceptor_OnCashBoxRemoved(object sender, EventArgs e)
        {
            bills.Add(new ItemReceptor
            {
                Detalle = "CashBoxRemoved",
                Index = bills.Count + 1,
                Moneda = "",
                Monto = 0,
                Almacenado = false,
                FechaHora = DateTime.Now
            });
            BillAcceptor.EnableAcceptance = false;
            BlockReceiver();
        }

        private void BillAcceptor_OnJamDetected(object sender, EventArgs e)
        {
            bills.Add(new ItemReceptor
            {
                Detalle = "JamDetected",
                Index = bills.Count + 1,
                Moneda = "",
                Monto = 0,
                Almacenado = false,
                FechaHora = DateTime.Now
            });
            BillAcceptor.EnableAcceptance = false ;
            BlockReceiver();
        }

        private void BillAcceptor_OnDisconnected(object sender, EventArgs e)
        {
            bills = new List<ItemReceptor>();
        }

        //private void BillAcceptor_OnStacked(object sender, EventArgs e)
        //{
        //    if (BillAcceptor.DocType != MPOST.DocumentType.NoValue && BillAcceptor.getDocument() != null && BillAcceptor.getDocument().ValueString != string.Empty)
        //    {
        //        string valueStacked = BillAcceptor.getDocument().ValueString;
        //        bills.Add(new ItemReceptor
        //        {
        //            Detalle = valueStacked,
        //            Index = bills.Count + 1,
        //            Moneda = valueStacked.Substring(0, 3),
        //            Monto = Convert.ToInt32(valueStacked.Split(' ')[1]),
        //            Almacenado = true,
        //            FechaHora = DateTime.Now
        //        }); 
        //    }
        //}

        private void BillAcceptor_OnConnected(object sender, EventArgs e)
        {
            
            BillAcceptor.DisconnectTimeout = 300000;
            BillAcceptor.AutoStack = false;
            BillAcceptor.HighSecurity = true;
            BillAcceptor.EnableAcceptance = true;

            //bills.Add(new ItemReceptor
            //{
            //    Detalle = "conectado",
            //    Index = bills.Count + 1,
            //    Moneda = "0",
            //    Monto = 0,
            //    Almacenado = false,
            //    FechaHora = DateTime.Now
            //});
        }

        public Response Stack()
        {
            Response response = new Response();
            BillAcceptor.EscrowStack();
            response.State = Foundation.Stone.Application.Wrapper.ResponseType.Success;
            return response;
        }

        public Response Reset()
        {
            Response response = new Response();
            BillAcceptor.SoftReset();
            response.State = Foundation.Stone.Application.Wrapper.ResponseType.Success;
            return response;
        }

        public List<ItemReceptor> GetBills()
        {
            return bills;
        }

        private int GetTotalAmount()
        {
            int totalAmount = 0;
            //totalAmount= bills.ForEach(x => x.Almacenado == true)
            foreach(ItemReceptor bill in bills)
            {
                if(bill.Almacenado)
                { 
                    totalAmount += bill.Monto;
                }
            }
            return totalAmount;
        }
        private void BlockReceiver()
        {
            DeviceManager.globalConfigATM.configReceptorCOM.HasReceptor = false;
            FileHelper.updateStateATM(DeviceManager.globalConfigATM);
        }
        //ReceptorMEI_OnCashBoxRemoved, bloquear el receptor--

        //ReceptorMEI_OnEscrow, valida billetes si es el maximo y si el billete es correcto

        //ReceptorMEI_OnFailureDetected, bloquear el receptor--
        //ReceptorMEI_OnJamCleared, el cliente saco el billete trancado--
        //ReceptorMEI_OnJamDetected,  billete trancado bloquear el receptor--

        //ReceptorMEI_OnRejected, rechazado--
        //ReceptorMEI_OnReturned, retornado o rechazado--
        //ReceptorMEI_OnSendMessageFailure, error de receptor--
        //ReceptorMEI_OnStackedWithDocInfo, informa que el billete fue almacenado correctamente
        //ReceptorMEI_OnStackerFull , bloquear receptor



    }
}



