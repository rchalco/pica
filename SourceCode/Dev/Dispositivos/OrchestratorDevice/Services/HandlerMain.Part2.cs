using Foundation.Stone.Application.Wrapper;
using OrchestratorDevice.Contracts.Common;
using OrchestratorDevice.Contracts.LoanFlow;
using OrchestratorDevice.Contracts.SavingAccount;
using OrchestratorDevice.Managers;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchestrator.Services
{
    public partial class HandlerMain
    {
        #region " Loan Flow "

        public ResponseObject<ResulComplexLoanFlow> LoanFlowAuthentication(RequestAuthentication requestAuthentication)
        {
            LoanFlowManager loanFlowManager = new LoanFlowManager();
            return loanFlowManager.LoanFlowAuthentication(requestAuthentication);
        }

        public ResponseObject<ReportToPaymentByAtm> LoanFlowCreditRecoveryForExternalChannel(ExternalChannelRecoveryData objParameter)
        {
            LoanFlowManager loanFlowManager = new LoanFlowManager();
            return loanFlowManager.LoanFlowCreditRecoveryForExternalChannel(objParameter);
        }

        public ResponseObject<ResulComplexLoanFlow> LoanFlowGetLoanCreditByCreditCode(RequestATMLoanCreditByLoanCreditCode objParameter) 
        {
            LoanFlowManager loanFlowManager = new LoanFlowManager();
            return loanFlowManager.LoanFlowGetLoanCreditByCreditCode(objParameter);
        }

        public ResponseObject<string> CreateTicketForOperationExternalService(RequestAtmOperationTicket objParameter)
        {
            CommonManager commonManager = new CommonManager();
            return commonManager.CreateTicketForOperationExternalService(objParameter);
        }

        #endregion

        #region " Electronic Banking "


        public ResponseObject<string> ElectronicBankingAuthentication(RequestAuthentication requestAuthentication)
        {
            ElectronicBankingManager electronicBankingManager = new ElectronicBankingManager();
            return electronicBankingManager.ElectronicBankingAuthentication(requestAuthentication);
        }

        /// <summary>
        /// Metodo para Validar Usuarios
        /// </summary>
        /// <param name="objRequest">Parametros</param>
        /// <returns></returns>
        public ResponseObject<int> GetWebClientDataForATM(RequestGetPersonToProdemNet objRequest)
        {
            ElectronicBankingManager electronicBankingManager = new ElectronicBankingManager();
            return electronicBankingManager.GetWebClientDataForATM(objRequest);
        }

        public ResponseObject<string> ProdemNetActivationProcessByCI(RequestATMUserInfo objUserInfo)
        {
            ElectronicBankingManager electronicBankingManager = new ElectronicBankingManager();
            return electronicBankingManager.ProdemNetActivationProcessByCI(objUserInfo);
        }

        /// <summary>
        /// Servicio que obtiene la lista de dispositivos pendientes de activación desde el ATM
        /// </summary>
        /// <param name="objCustomerData">Información</param>
        /// <returns></returns>
        public ResponseQuery<ItemForATM> GetWebPersonDeviceWithActivationPendingForATM(RequestATMUserInfo objUserInfo)
        {
            ElectronicBankingManager electronicBankingManager = new ElectronicBankingManager();
            return electronicBankingManager.GetWebPersonDeviceWithActivationPendingForATM(objUserInfo);
        }

        /// <summary>
        /// Obtiene el codigo de activación QR para el dispositivo
        /// </summary>
        /// <param name="objCustomerData"> datos desde el ATM</param>
        /// <param name="idWebPersonDevice">Id de la persona en la web</param>
        /// <returns></returns>
        public ResponseObject<InfoDeviceActivation> GetWebPersonDeviceActivationCodeFromAtm(RequestDeviceActivation objUserInfo)
        {
            ElectronicBankingManager electronicBankingManager = new ElectronicBankingManager();
            return electronicBankingManager.GetWebPersonDeviceActivationCodeFromAtm(objUserInfo);
        }

        /// <summary>
        /// Servicio que ontiene la lista de dispositivos activos del Cliente
        /// </summary>
        /// <param name="objCustomerData">información del cliente</param>
        /// <returns></returns>
        public ResponseQuery<ItemForATM> GetWebPersonDeviceForATM(RequestDeviceActivation objUserInfo)
        {
            ElectronicBankingManager electronicBankingManager = new ElectronicBankingManager();
            return electronicBankingManager.GetWebPersonDeviceForATM(objUserInfo);
        }

        /// <summary>
        /// Inactiva el dispositivo desde el ATM
        /// </summary>
        /// <param name="idWebPersonDevice">Id del dispositivo</param>
        public ResponseObject<string> WebPersonDeviceInactiveFromATM(RequestDeviceInactivation objDeviceInfo)
        {
            ElectronicBankingManager electronicBankingManager = new ElectronicBankingManager();
            return electronicBankingManager.WebPersonDeviceInactiveFromATM(objDeviceInfo);
        }

        /// <summary>
        /// Reset al PIN de ProdemMovil, ProdemKey
        /// </summary>
        /// <param name="objCustomerData">datos desde el ATM</param>
        public Response ATMResetProdemKeyPIN(RequestATMUserInfo objUserInfo)
        {
            ElectronicBankingManager electronicBankingManager = new ElectronicBankingManager();
            return electronicBankingManager.ATMResetProdemKeyPIN(objUserInfo);
        }

        /// <summary>
        /// Servicio que valida el usuario web
        /// </summary>
        /// <param name="objCustomerData">Información del usuario logueado en el ATM</param>
        /// <returns></returns>
        public ResponseObject<NewProdemNetCustomer> AtmGetNewCustomerDataByCI(RequestATMUserInfo objUserInfo)
        {
            ElectronicBankingManager electronicBankingManager = new ElectronicBankingManager();
            return electronicBankingManager.AtmGetNewCustomerDataByCI(objUserInfo);
        }

        /// <summary>
        /// Servicio que crea un usuario web desde el ATM
        /// </summary>
        /// <param name="objUserData">Datos del usuario</param>
        /// <returns></returns>
        public ResponseObject<string> CreateExternalWebPersonClientFromATM(RequestNewUserSolicitation objUserInfo)
        {
            ElectronicBankingManager electronicBankingManager = new ElectronicBankingManager();
            return electronicBankingManager.CreateExternalWebPersonClientFromATM(objUserInfo);
        }

        #endregion

    }
}
