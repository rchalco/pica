using Foundation.Stone.Application.Wrapper;
using Foundation.Stone.Business.Core;
using Foundation.Stone.CrossCuting.StoneException;
using Hangar.ServiceImplement.Config;
using Hangar.ServiceImplement.Factory;
using Interop.Main.Service.Interface;
using Newtonsoft.Json;
using Orchestrator.Global;
using OrchestratorDevice.Contracts.LoanFlow;
using OrchestratorDevice.Contracts.SavingAccount;
using OrchestratorDevice.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Managers
{
    public class LoanFlowManager : CommonManager
    {
        public LoanFlowManager()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

        public ResponseObject<ResulComplexLoanFlow> LoanFlowAuthentication(RequestAuthentication requestAuthentication)
        {
            ResponseObject<ResulComplexLoanFlow> objResult = new ResponseObject<ResulComplexLoanFlow> { Object = new ResulComplexLoanFlow() };
            try
            {
                if (!requestAuthentication.credential.AditionalItems.Any(x => x.Key == "IdATM") && DeviceManager.globalConfigATM != null)
                {
                    requestAuthentication.credential.AditionalItems.Add(new AditionalItem { Key = "IdATM", Value = DeviceManager.globalConfigATM.Id.ToString() });
                }
                else if (DeviceManager.globalConfigATM != null)
                {
                    requestAuthentication.credential.AditionalItems.Find(x => x.Key == "IdATM").Value = DeviceManager.globalConfigATM.Id.ToString();
                }

                string eventPath = FileHelper.writeEvent("Authentication: " + JsonConvert.SerializeObject(requestAuthentication));
                var resulOut = clientRestHelper.Consume<ResponseAuthentication>(Setttings.uriSecurity, requestAuthentication).Result.VerifyUserResult;

                if (resulOut.State == ResponseType.Success)
                {
                    string iddentityCardNumber = resulOut.Object.AditionalItems.Where(x => x.Key.Equals("IdNumberThatAutorize")).First().Value;
                    RequestATMIdentityCardParameter objSend = new RequestATMIdentityCardParameter
                    {
                        objParameter = new RequestATMIdentityCard
                        {
                            IdentityCardNumber = iddentityCardNumber.Replace("\"", "")
                        }
                    };
                    objResult.State = ResponseType.Success;
                    objResult.Object.Token = resulOut.Object.token;
                    objResult.Object.NumberOfTransaction = Guid.NewGuid().ToString();
                    objResult.Object.ColLoanFlow = clientRestHelper.Consume<ResposeLoanFlowGetLoanCreditByIdentityCard>(Setttings.uriBaseServices + "/LoanFlowGetLoanCreditByIdentityCard", objSend, resulOut.Object.token).Result.LoanFlowGetLoanCreditByIdentityCardResult?.ListEntities;
                    objResult.Object.ColAccount = JsonConvert.DeserializeObject<List<Account>>(resulOut.Object.AditionalItems.Where(x => x.Key == "SavingAccounts").First().Value);
                    //if (objResult.Object.ColLoanFlow==null || objResult.Object.ColLoanFlow.Count == 0)
                    //{ 
                    //    objResult.State = ResponseType.Warning;
                    //    objResult.Message = "Usted no tiene créditos pendientes de pago.";
                    //}
                }

                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(objResult, ex);
                objResult.Message = ex is FaultException ? ex.Message : ExceptionManager.MsgError;
            }
            return objResult;
        }

        public ResponseObject<ReportToPaymentByAtm> LoanFlowCreditRecoveryForExternalChannel(ExternalChannelRecoveryData objParameter)
        {
            ResponseObject<ReportToPaymentByAtm> objResult = new ResponseObject<ReportToPaymentByAtm> { };
            var CardReaderInterop = MicroService.CreateInstance<ICardReaderInterop>();
            try
            {
                string eventPath = FileHelper.writeEvent("Authentication: " + JsonConvert.SerializeObject(objParameter));

                if (objParameter.ColAdittedBills != null && objParameter.ColAdittedBills.Count > 0)
                {
                    string jsonCashDispensed = JsonConvert.SerializeObject(objParameter.ColAdittedBills);
                    objParameter.CashDetail = jsonCashDispensed;
                }
                else
                    objParameter.CashDetail = string.Empty;

                objParameter.ColAdittedBills = null;

                RequestATMLoanCreditByAccountParameter objSend = new RequestATMLoanCreditByAccountParameter { objParameter = objParameter };

                objResult = clientRestHelper.Consume<ResponseReportToPaymentByAtm>(Setttings.uriBaseServices + "/LoanFlowCreditRecoveryForExternalChannel", objSend, objSend.objParameter.Token).Result.LoanFlowCreditRecoveryForExternalChannelResult;

                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(objResult, ex);
                objResult.Message = ex is FaultException ? ex.Message : ExceptionManager.MsgError;
            }
            return objResult;
        }


        public ResponseObject<ResulComplexLoanFlow> LoanFlowGetLoanCreditByCreditCode(RequestATMLoanCreditByLoanCreditCode objParameter)
        {
            ResponseObject<ResulComplexLoanFlow> objResult = new ResponseObject<ResulComplexLoanFlow>();
            var CardReaderInterop = MicroService.CreateInstance<ICardReaderInterop>();
            try
            {
                string eventPath = FileHelper.writeEvent("Authentication: " + JsonConvert.SerializeObject(objParameter));

                RequestATMLoanCreditByLoanCreditCodeParameter objSend = new RequestATMLoanCreditByLoanCreditCodeParameter { objParameter = objParameter };

                objResult.Object = new ResulComplexLoanFlow();
                objResult.Object.ColLoanFlow = clientRestHelper.Consume<ResposeLoanFlowGetLoanCreditByCreditCode>(Setttings.uriBaseServices + "/LoanFlowGetLoanCreditByCreditCode", objSend, objSend.objParameter.Token).Result.LoanFlowGetLoanCreditByCreditCodeResult.ListEntities;
                objResult.Object.ColAccount = null;
                objResult.Object.Token = string.Empty;
                objResult.Object.NumberOfTransaction = Guid.NewGuid().ToString();
                objResult.State = ResponseType.Success;
                objResult.Message = "";
                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(objResult, ex);
                objResult.Message = ex is FaultException ? ex.Message : ExceptionManager.MsgError;
                objResult.State = ResponseType.Error;
                objResult.Object = null;
            }
            return objResult;
        }
    }
}
