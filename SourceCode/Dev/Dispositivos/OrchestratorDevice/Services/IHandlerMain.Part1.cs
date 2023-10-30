using OrchestratorDevice.Contracts.Base;
using OrchestratorDevice.Contracts.ExternalServices;
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
        #region Pago servicios

        #region Common Services

        [OperationContract]
        ExternalEnableServicesResult GetEnableServicesForMobile(BasicSearchData objParamData);

        #endregion

        #region Sintesis

        [OperationContract]
        SintesisSearchCriteriaResult SintesisGetSearchParametersByModule(SintesisSearchData objSearchData);

        [OperationContract]
        SintesisSearchResult SintesisObtainOperatingDebtBalance(SintesisSearchData objSearchData);

        [OperationContract]
        SintesisSubDetailResult SintesisGetSubItemDetails(SintesisSubItemDetailData objGetDetailData);

        [OperationContract]
        SintesisPaymentResult SintesisPaymentProcess(SintesisPaymentData objPaymentData);

        #endregion

        #region Ende Tecnologías

        [OperationContract]
        ENDESearchResult EndeObtainOperatingDebtBalance(ENDESearchData objSearchData);

        [OperationContract]
        EndePaymentResult EndePaymentProcess(ENDEPaymentData objPaymentData);

        #endregion

        #endregion
    }
}
