using OrchestratorDevice.Contracts.ExternalServices;
using OrchestratorDevice.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchestrator.Services
{
    public partial class HandlerMain
    {
        #region Pago servicios

        #region Common Services

        public ExternalEnableServicesResult GetEnableServicesForMobile(BasicSearchData objParamData)
        {
            ExternalPaymentManager integratorManager = new ExternalPaymentManager();
            return integratorManager.GetEnableServicesForMobile(objParamData);
        }

        #endregion

        #region Sintesis

        public SintesisSearchCriteriaResult SintesisGetSearchParametersByModule(SintesisSearchData objSearchData)
        {
            ExternalPaymentManager integratorManager = new ExternalPaymentManager();
            return integratorManager.SintesisGetSearchParametersByModule(objSearchData);
        }

        public SintesisSearchResult SintesisObtainOperatingDebtBalance(SintesisSearchData objSearchData)
        {
            ExternalPaymentManager integratorManager = new ExternalPaymentManager();
            return integratorManager.SintesisObtainOperatingDebtBalance(objSearchData);
        }

        public SintesisSubDetailResult SintesisGetSubItemDetails(SintesisSubItemDetailData objGetDetailData)
        {
            ExternalPaymentManager integratorManager = new ExternalPaymentManager();
            return integratorManager.SintesisGetSubItemDetails(objGetDetailData);
        }

        public SintesisPaymentResult SintesisPaymentProcess(SintesisPaymentData objPaymentData)
        {
            ExternalPaymentManager integratorManager = new ExternalPaymentManager();
            return integratorManager.SintesisPaymentProcess(objPaymentData);
        }

        #endregion

        #region Ende Tecnologías

        public ENDESearchResult EndeObtainOperatingDebtBalance(ENDESearchData objSearchData)
        {
            ExternalPaymentManager integratorManager = new ExternalPaymentManager();
            return integratorManager.EndeObtainOperatingDebtBalance(objSearchData);
        }


        public EndePaymentResult EndePaymentProcess(ENDEPaymentData objPaymentData)
        {
            ExternalPaymentManager integratorManager = new ExternalPaymentManager();
            return integratorManager.EndePaymentProcess(objPaymentData);
        }

        #endregion

        #endregion
    }
}
