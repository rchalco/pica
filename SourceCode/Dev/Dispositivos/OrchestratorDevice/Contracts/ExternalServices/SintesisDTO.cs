using Foundation.Stone.Application.Wrapper;
using OrchestratorDevice.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Contracts.ExternalServices
{
    [DataContract]
    public class SintesisSearchData : BaseRequest
    {
        [DataMember]
        public DTOSintesisSearchDataCriteria objSearchData { get; set; }
    }

    [DataContract]
    public class DTOSintesisSearchDataCriteria : DTOBaseSearchData
    {
        [DataMember]
        public short SearchCriteria { get; set; }

        [DataMember]
        public string[] SearchParameter { get; set; }
    }

    [DataContract]
    public class SintesisSearchCriteriaResult : BaseRequest
    {
        [DataMember]
        public ResponseObject<List<SintesisSearchCriteriaDTO>> SintesisGetSearchParametersByModuleResult { get; set; }
    }

    [Serializable]
    [DataContract]
    public class SintesisSearchCriteriaDTO
    {
        [DataMember]
        public short CodeModule { get; set; }

        [DataMember]
        public string ModuleName { get; set; }

        [DataMember]
        public List<SintesisSearchCriteriaFieldsDTO> ColFields { get; set; }
    }

    [Serializable]
    [DataContract]
    public class SintesisSearchCriteriaFieldsDTO
    {
        [DataMember]
        public string ParameterName { get; set; }

        [DataMember]
        public string ParameterType { get; set; }

        [DataMember]
        public List<ExternalParameterDTO> ColComboData { get; set; }
    }

    [DataContract]
    [Serializable]
    public class ExternalParameterDTO
    {
        [DataMember]
        public long IdDataParam { get; set; }

        [DataMember]
        public string DataParamCode { get; set; }

        [DataMember]
        public string ParamDescription { get; set; }
    }

    [DataContract]
    public class SintesisSearchResult : BaseRequest
    {
        [DataMember]
        public ResponseObject<DTOSintesisSearchResult> SintesisObtainOperatingDebtBalanceResult { get; set; }
    }

    [Serializable]
    [DataContract]
    public class DTOSintesisSearchResult
    {
        [DataMember]
        public string[] SearchParameters { get; set; }

        [DataMember]
        public int OperationNumber { get; set; }

        [DataMember]
        public int OperativeDate { get; set; }

        [DataMember]
        public List<DTOSintesisCustomerAccount> ColAccounts { get; set; }
    }

    [Serializable]
    [DataContract]
    public class DTOSintesisCustomerAccount
    {
        [DataMember]
        public string AccountCode { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string AccountDescription { get; set; }

        [DataMember]
        public short ServiceCode { get; set; }

        [DataMember]
        public string ServiceDescription { get; set; }

        [DataMember]
        public short CodeCurrency { get; set; }

        [DataMember]
        public string InvoicingNIT { get; set; }

        [DataMember]
        public string InvoicingCustomerName { get; set; }

        [DataMember]
        public string InvoicingNITComplement { get; set; }

        [DataMember]
        public string InvoicingEmail { get; set; }

        [DataMember]
        public string InvoicingDocType { get; set; }

        [DataMember]
        public string InvoicingPhone { get; set; }

        [DataMember]
        public bool WithInvoice { get; set; }

        [DataMember]
        public string CollectionType { get; set; }

        [DataMember]
        public string CarDepartment { get; set; }

        [DataMember]
        public long? CarType { get; set; }

        [DataMember]
        public long? CarUseType { get; set; }

        [DataMember]
        public string InternalUserToken { get; set; }

        [DataMember]
        public string UserMF { get; set; }

        [DataMember]
        public string AdditionalInfo { get; set; }

        [DataMember]
        public long IdcChangeInvoicingData { get; set; }

        [DataMember]
        public bool ItHasRequirement { get; set; }

        [DataMember]
        public List<DTOSintesisCustomerAccountItemDetail> ColAccountItemDetail { get; set; }
    }

    [Serializable]
    [DataContract]
    public class DTOSintesisCustomerAccountItemDetail
    {
        [DataMember]
        public bool SelectedItem { get; set; }

        [DataMember]
        public string ItemNumberCode { get; set; }

        [DataMember]
        public string ItemDescription { get; set; }

        [DataMember]
        public string ItemCurrency { get; set; }

        [DataMember]
        public string ItemDependency { get; set; }

        [DataMember]
        public string PaymentMethodType { get; set; }

        [DataMember]
        public decimal ItemAmount { get; set; }

        [DataMember]
        public bool AllowMultipleSelection { get; set; }

        [DataMember]
        public bool IsMandatory { get; set; }

        [DataMember]
        public bool NeedLoadSubDetails { get; set; }

        [DataMember]
        public bool AllowMoreOnePay { get; set; }

        [DataMember]
        public bool CustomerDefineAmount { get; set; }

        [DataMember]
        public bool WithInvoice { get; set; }

        [DataMember]
        public string BatchDosage { get; set; }

        [DataMember]
        public string RentNumber { get; set; }

        [DataMember]
        public decimal? AmountMin { get; set; }

        [DataMember]
        public decimal? AmountMax { get; set; }

        [DataMember]
        public int ItemPeriod { get; set; }

        [DataMember]
        public DateTime DetailDate { get; set; }
    }

    [DataContract]
    public class SintesisSubItemDetailData : BaseRequest
    {
        [DataMember]
        public DTOSintesisGetDetailData objGetDetailData { get; set; }
    }

    [Serializable]
    [DataContract]
    public class DTOSintesisGetDetailData
    {
        [DataMember]
        public long SintesisModule { get; set; }

        [DataMember]
        public int OperationNumber { get; set; }

        [DataMember]
        public int OperativeDate { get; set; }

        [DataMember]
        public string AccountCode { get; set; }

        [DataMember]
        public short ServiceCode { get; set; }

        [DataMember]
        public string ItemCode { get; set; }

        [DataMember]
        public decimal AmountOrQuantity { get; set; }

        [DataMember]
        public int OriginType { get; set; }
    }

    [DataContract]
    public class SintesisSubDetailResult : BaseRequest
    {
        [DataMember]
        public ResponseObject<DTOSintesisAccountSubItem> SintesisGetSubItemDetailsResult { get; set; }
    }

    [Serializable]
    [DataContract]
    public class DTOSintesisAccountSubItem
    {
        [DataMember]
        public decimal TotalToPay { get; set; }

        [DataMember]
        public List<DTOSintesisAccountSubItemDetail> ColSubItemDetail { get; set; }
    }

    [Serializable]
    [DataContract]
    public class DTOSintesisAccountSubItemDetail
    {
        [DataMember]
        public string SubItemDescription { get; set; }

        [DataMember]
        public decimal SubItemAmount { get; set; }
    }

    [DataContract]
    public class SintesisPaymentData : BaseRequest
    {
        [DataMember]
        public DTOSintesisPaymentData objPaymentData { get; set; }
    }

    [Serializable]
    [DataContract]
    public class DTOSintesisPaymentData : DTOExternalServiceBaseData
    {
        [DataMember]
        public int OperationNumber { get; set; }

        [DataMember]
        public int OperativeDate { get; set; }

        [DataMember]
        public string AccountCode { get; set; }

        [DataMember]
        public short ServiceCode { get; set; }

        [DataMember]
        public string CollectionType { get; set; }

        [DataMember]
        public string CarDepartment { get; set; }

        [DataMember]
        public long CarType { get; set; }

        [DataMember]
        public long CarUseType { get; set; }

        [DataMember]
        public string UserToken { get; set; }

        [DataMember]
        public string UserCode { get; set; }

        [DataMember]
        public short CodeSintesisModule { get; set; }

        [DataMember]
        public List<string> ColInvoiceCustomerEmail { get; set; }

        [DataMember]
        public List<DTOSintesisPaymentDetail> ColPaymentDetail { get; set; }
    }

    [Serializable]
    [DataContract]
    public class DTOSintesisPaymentDetail
    {
        [DataMember]
        public string ItemNumberCode { get; set; }

        [DataMember]
        public decimal ItemAmount { get; set; }

        [DataMember]
        public string BatchDosage { get; set; }

        [DataMember]
        public string RentNumber { get; set; }

        [DataMember]
        public int ItemPeriod { get; set; }
    }

    [DataContract]
    public class SintesisPaymentResult : BaseRequest
    {
        [DataMember]
        public ResponseObject<DTOSintesisPaymentResult> SintesisPaymentProcessResult { get; set; }
    }

    [Serializable]
    [DataContract]
    public class DTOSintesisPaymentResult : DTOExternalServiceBaseResult
    {
        [DataMember]
        public byte[] VoucherData { get; set; }

        [DataMember]
        public string[] VoucherLines { get; set; }

        [DataMember]
        public byte[] EBillData { get; set; }

        [DataMember]
        public bool PendingInvoice { get; set; }

        [DataMember]
        public List<DTOSintesisEBill> ColEBill { get; set; }
    }

    [Serializable]
    [DataContract]
    public class DTOSintesisEBill
    {
        [DataMember]
        public string BillData { get; set; }

        [DataMember]
        public string BillDescription { get; set; }

        [DataMember]
        public int BillType { get; set; }

        [DataMember]
        public string BillURL { get; set; }
    }
}
