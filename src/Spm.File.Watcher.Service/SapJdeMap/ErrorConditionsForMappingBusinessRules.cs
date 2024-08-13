namespace Spm.File.Watcher.Service.SapJdeMap
{
    public interface IDoErrorConditionsForMappingBusinessRules
    {
        string ConnotFindJdeStockType(string jdeStockType);
        string CannotFindDefaultJdeDepartmentCode(string defaultJdeDepartmentCode);
        string CannotFindDefaultJdeGlAccountCode(string defaultJdeGlAccountCode);
        string CannotFindDefaultJdeUnitOfMeasure(string defaultJdeUnitOfMeasure);
        string CannotFindDefaultJdeBranchCode(string defaultJdeBranchCode);
        string CannotFindDefaultJdePurchaseGroup(string defaultJdePurchaseGroup);
        string CannotFindJdeDocType(string jdeDocType);
        string CannotFindDefaultJdeCompanyCode(string defaultJdeCompanyCode);
        string CannotFindDefaultJdeLocation(string defaultJdeLocation);

        string ProblemWithPostingDate(string postingDate);
        string ProblemWithDocDate(string docDate);
        string ProblemWithVendor(string vendor);
        string ProblemWithNetPriceEmpty(string netPrice);
        string ProblemWithNetPriceFormat(string netPrice);
    }

    public class ErrorConditionsForMappingBusinessRules : IDoErrorConditionsForMappingBusinessRules
    {
        public string ConnotFindJdeStockType(string jdeStockType)
        { return string.Format("Cannot find JdeStockType={0}, in MaterailGroupMapping table. Cannot proceed!", jdeStockType); }

        public string CannotFindDefaultJdeDepartmentCode(string defaultJdeDepartmentCode)
        { return string.Format("Cannot find DEFAULT JdeDepartmentCode={0} in CostCentreGlPostingMapping Table. Cannot proceed!", defaultJdeDepartmentCode); }

        public string CannotFindDefaultJdeGlAccountCode(string defaultJdeGlAccountCode)
        { return string.Format("Cannot find DEFAULT JdeGlAccount={0}, in GlAccountGlPostingMapping table. Cannot proceed!", defaultJdeGlAccountCode); }

        public string CannotFindDefaultJdeUnitOfMeasure(string defaultJdeUnitOfMeasure)
        { return string.Format("Cannot find DEFAULT JdeUom={0} in UnitOfMeasureMappingTable. Cannot proceed.", defaultJdeUnitOfMeasure); }

        public string CannotFindDefaultJdeBranchCode(string defaultJdeBranchCode)
        { return string.Format("Cannot find DEFAULT JdeBranchCode={0} in PlantMappingTable. Cannot proceed.", defaultJdeBranchCode); }

        public string CannotFindDefaultJdePurchaseGroup(string defaultJdePurchaseGroup)
        { return string.Format("Cannot find DEFAULT JdePurchaseGroup={0} in PurchaseGroupMapping Table. Cannot proceed.", defaultJdePurchaseGroup); }

        public string CannotFindJdeDocType(string jdeDocType)
        { return string.Format("Cannot find JdeDocType={0} in DocTypeMapping table. Cannot proceed.", jdeDocType); }

        public string CannotFindDefaultJdeCompanyCode(string defaultJdeCompanyCode)
        { return string.Format("Cannot find DEFAULT JdeCompanyCode={0} in CompanyCodeMapping table. Cannot proceed.", defaultJdeCompanyCode); }

        public string CannotFindDefaultJdeLocation(string defaultJdeLocation)
        { return string.Format("Cannot find DEFAULT JdeLocationCode={0} in LocationMappingTable.", defaultJdeLocation); }

        public string ProblemWithPostingDate(string postingDate)
        { return string.Format("The supplied JDE date, PostingDate='{0}' cannot be transformed into a SAP date. Invalid format. Cannot proceed!!!!", postingDate); }

        public string ProblemWithDocDate(string docDate)
        { return string.Format("The supplied JDE date, DocDate='{0}' cannot be transformed into a SAP date. Invalid format. Cannot proceed!!!!", docDate); }

        public string ProblemWithVendor(string vendor)
        { return string.Format("The supplied JDE Vendor; vendor='{0}' cannot be null or empty. Invalid rule. Cannot proceed!!!!", vendor); }

        public string ProblemWithNetPriceEmpty(string netPrice)
        { return string.Format("The supplied JDE Net-Price; netPrice='{0}' cannot be null or empty. Invalid rule. Cannot proceed!!!!", netPrice); }

        public string ProblemWithNetPriceFormat(string netPrice)
        { return string.Format("The Net-Price cannot be converted to a proper decimal value, netPrice='{0}'. Cannot proceed!!!!",netPrice);}
    }
}