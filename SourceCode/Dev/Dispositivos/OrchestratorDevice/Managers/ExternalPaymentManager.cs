using Foundation.Stone.Application.Wrapper;
using Foundation.Stone.Business.Core;
using Foundation.Stone.CrossCuting.StoneException;
using Hangar.ServiceImplement.Config;
using Hangar.ServiceImplement.Factory;
using Interop.Main.Cross.Domain.Orchestrator;
using Interop.Main.Service.Interface;
using Newtonsoft.Json;
using Orchestrator.Global;
using OrchestratorDevice.Contracts.Base;
using OrchestratorDevice.Contracts.ExternalServices;
using OrchestratorDevice.Global;
using OrchestratorDevice.Util;
using System;
using System.IO;
using System.Net;
using System.ServiceModel;

namespace OrchestratorDevice.Managers
{
    public class ExternalPaymentManager : CommonManager
    {
        #region Common Services

        public ExternalEnableServicesResult GetEnableServicesForMobile(BasicSearchData objParamData)
        {
            ExternalEnableServicesResult resMFConfirm = new ExternalEnableServicesResult { GetEnableServicesForMobileResult = new ResponseObject<System.Collections.Generic.List<long>>() };

            try
            {
                resMFConfirm = clientRestHelper.Consume<ExternalEnableServicesResult>(Setttings.uriBaseServices + "/GetEnableServicesForMobile", null, objParamData.Token).Result;
            }
            catch (Exception ex)
            {
                ProcessError(resMFConfirm.GetEnableServicesForMobileResult, ex);
            }

            return resMFConfirm;
        }

        #endregion

        #region Sintesis

        public SintesisSearchCriteriaResult SintesisGetSearchParametersByModule(SintesisSearchData objSearchData)
        {
            SintesisSearchCriteriaResult resMFResult = new SintesisSearchCriteriaResult { SintesisGetSearchParametersByModuleResult = new ResponseObject<System.Collections.Generic.List<SintesisSearchCriteriaDTO>>() };

            try
            {
                string eventPath = FileHelper.writeEvent("SintesisGetSearchParametersByModule: " + JsonConvert.SerializeObject(objSearchData));

                resMFResult = clientRestHelper.Consume<SintesisSearchCriteriaResult>(Setttings.uriBaseServices + "/SintesisGetSearchParametersByModule", objSearchData, objSearchData.Token).Result;

                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(resMFResult.SintesisGetSearchParametersByModuleResult, ex);
            }

            return resMFResult;
        }

        public SintesisSearchResult SintesisObtainOperatingDebtBalance(SintesisSearchData objSearchData)
        {
            SintesisSearchResult resMFResult = new SintesisSearchResult { SintesisObtainOperatingDebtBalanceResult = new ResponseObject<DTOSintesisSearchResult>() };

            try
            {
                string eventPath = FileHelper.writeEvent("SintesisObtainOperatingDebtBalance: " + JsonConvert.SerializeObject(objSearchData));

                resMFResult = clientRestHelper.Consume<SintesisSearchResult>(Setttings.uriBaseServices + "/SintesisObtainOperatingDebtBalance", objSearchData, objSearchData.Token).Result;

                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(resMFResult.SintesisObtainOperatingDebtBalanceResult, ex);
            }

            return resMFResult;
        }

        public SintesisSubDetailResult SintesisGetSubItemDetails(SintesisSubItemDetailData objGetDetailData)
        {
            SintesisSubDetailResult resMFResult = new SintesisSubDetailResult { SintesisGetSubItemDetailsResult = new ResponseObject<DTOSintesisAccountSubItem>() };

            try
            {
                string eventPath = FileHelper.writeEvent("SintesisGetSubItemDetails: " + JsonConvert.SerializeObject(objGetDetailData));

                resMFResult = clientRestHelper.Consume<SintesisSubDetailResult>(Setttings.uriBaseServices + "/SintesisGetSubItemDetails", objGetDetailData, objGetDetailData.Token).Result;

                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(resMFResult.SintesisGetSubItemDetailsResult, ex);
            }

            return resMFResult;
        }

        public SintesisPaymentResult SintesisPaymentProcess(SintesisPaymentData objPaymentData)
        {
            SintesisPaymentResult resMFResult = new SintesisPaymentResult { SintesisPaymentProcessResult = new ResponseObject<DTOSintesisPaymentResult>() };            

            try
            {
                string eventPath = FileHelper.writeEvent("SintesisPaymentProcess: " + JsonConvert.SerializeObject(objPaymentData));
                if (resMFResult?.SintesisPaymentProcessResult?.State == ResponseType.Success)
                {
                    resMFResult.SintesisPaymentProcessResult.Object.ReportString = resMFResult.SintesisPaymentProcessResult.Object.ReportString.Replace("<ClosureMessage>", "");
                    resMFResult.SintesisPaymentProcessResult.Object.ReportString = resMFResult.SintesisPaymentProcessResult.Object.ReportString.Replace("</ClosureMessage>", "");
                }

                resMFResult = clientRestHelper.Consume<SintesisPaymentResult>(Setttings.uriBaseServices + "/SintesisPaymentProcess", objPaymentData, objPaymentData.Token).Result;
                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(resMFResult.SintesisPaymentProcessResult, ex);
            }

            return resMFResult;
        }

        #endregion

        #region Ende Tecnologías

        public ENDESearchResult EndeObtainOperatingDebtBalance(ENDESearchData objSearchData)
        {
            ENDESearchResult resMFResult = new ENDESearchResult { EndeObtainOperatingDebtBalanceResult = new ResponseObject<DTOENDESearchResult>() };

            try
            {
                string eventPath = FileHelper.writeEvent("EndeObtainOperatingDebtBalance: " + JsonConvert.SerializeObject(objSearchData));

                resMFResult = clientRestHelper.Consume<ENDESearchResult>(Setttings.uriBaseServices + "/EndeObtainOperatingDebtBalance", objSearchData, objSearchData.Token).Result;

                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(resMFResult.EndeObtainOperatingDebtBalanceResult, ex);
            }

            return resMFResult;
        }

        public EndePaymentResult EndePaymentProcess(ENDEPaymentData objPaymentData)
        {
            EndePaymentResult resMFResult = new EndePaymentResult { EndePaymentProcessResult = new ResponseObject<DTOENDEPaymentResult>() };
            

            try
            {
                string eventPath = FileHelper.writeEvent("EndePaymentProcess: " + JsonConvert.SerializeObject(objPaymentData));

                resMFResult = clientRestHelper.Consume<EndePaymentResult>(Setttings.uriBaseServices + "/EndePaymentProcess", objPaymentData, objPaymentData.Token).Result;

                FileHelper.deleteEvent(eventPath);
            }
            catch (Exception ex)
            {
                ProcessError(resMFResult.EndePaymentProcessResult, ex);
            }

            return resMFResult;
        }

        #endregion
    }
}
