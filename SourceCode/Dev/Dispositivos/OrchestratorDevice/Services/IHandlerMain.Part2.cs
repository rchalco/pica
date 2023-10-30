using Foundation.Stone.Application.Wrapper;
using OrchestratorDevice.Contracts.Common;
using OrchestratorDevice.Contracts.LoanFlow;
using OrchestratorDevice.Contracts.SavingAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Orchestrator.Services
{
    public partial interface IHandlerMain
    {

        #region " Loan FLow "

        [OperationContract]
        ResponseObject<ResulComplexLoanFlow> LoanFlowAuthentication(RequestAuthentication requestAuthentication);

        [OperationContract]
        ResponseObject<ResulComplexLoanFlow> LoanFlowGetLoanCreditByCreditCode(RequestATMLoanCreditByLoanCreditCode objParameter);

        [OperationContract]
        ResponseObject<ReportToPaymentByAtm> LoanFlowCreditRecoveryForExternalChannel(ExternalChannelRecoveryData objParameter);

        [OperationContract]
        ResponseObject<string> CreateTicketForOperationExternalService(RequestAtmOperationTicket objParameter);

        #endregion

        #region " Electronic Banking "

        [OperationContract]
        ResponseObject<string> ElectronicBankingAuthentication(RequestAuthentication requestAuthentication);

        [OperationContract]
        ResponseObject<int> GetWebClientDataForATM(RequestGetPersonToProdemNet objRequest);

        [OperationContract]
        ResponseObject<string> ProdemNetActivationProcessByCI(RequestATMUserInfo objUserInfo);

        [OperationContract]
        ResponseQuery<ItemForATM> GetWebPersonDeviceWithActivationPendingForATM(RequestATMUserInfo objUserInfo);

        [OperationContract]
        ResponseObject<InfoDeviceActivation> GetWebPersonDeviceActivationCodeFromAtm(RequestDeviceActivation objUserInfo);

        [OperationContract]
        ResponseQuery<ItemForATM> GetWebPersonDeviceForATM(RequestDeviceActivation objUserInfo);

        [OperationContract]
        ResponseObject<string> WebPersonDeviceInactiveFromATM(RequestDeviceInactivation objDeviceInfo);

        [OperationContract]
        Response ATMResetProdemKeyPIN(RequestATMUserInfo objUserInfo);

        [OperationContract]
        ResponseObject<NewProdemNetCustomer> AtmGetNewCustomerDataByCI(RequestATMUserInfo objUserInfo);

        [OperationContract]
        ResponseObject<string> CreateExternalWebPersonClientFromATM(RequestNewUserSolicitation objUserInfo);

        #endregion


    }
}
