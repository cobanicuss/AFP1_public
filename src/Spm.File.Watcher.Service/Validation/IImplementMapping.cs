using System.Collections.Generic;
using Spm.File.Watcher.Service.Dto;
using Spm.Shared;

namespace Spm.File.Watcher.Service.Validation
{
    public interface IImplementMapping : IMarkAsValidator
    {
        string ForPoNumber(string poNumber, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForDeliveryDate(string deliveryDate, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForCostCenter(string plant, string lnType, string costCenter, string taxable, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForGlAccount(string plant, string lnType, string glAccount, string taxable, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForNetPrice(string netPrice, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForOrderPrUn(string orderprUn, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForPoUnit(string poUnit, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForMaterialGroup(string plant, string lnType, string taxable, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForPlant(string plant, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForPlantBranch(string mcu, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForPoItem(string poItem, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForPurGroup(string purGroup, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForPurchaseOrg(string purchOrg);
        string ForVendor(string vendor, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForCreateDate(string creatDate, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForDocType(string type, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForCompCode(string compCode, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForPhysicalPackSize(string mcu, string ghMm, string gwMm, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForRedBlueBlack(string srp4Desc);
        string ForThreeDecimalPlacesOnly(string mt);
        string ForActualWidth(string mcu, string sec1, string sec2, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForActualHeight(string sec1, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForProductHierachy(string mcu, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForMaterialGroupByPlant(string mcu, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForStorageSection(string mcu, string prp4, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForStorageType(string mcu, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForProfitCenre(string mcu, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForNumerator(string input, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForDenominator(string input, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForKgDivMt(string kg, string mt, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForPackWeight(string bwck, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForLocation(string stgeLoc, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForGmCode(string gmCode);
        string ForHeaderText(string id, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForDocDate(string docDate);
        string ForPostingDate(string pstngDate, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForGlAccount(string glAccount, string refDocNo, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForGlProfitCentre(string glAccount, string costCentre, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForGlCostCenter(string glAccount, string refDocNo, string costCentre, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForGlDocDate(string docDate, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForPrp(string mcu, string prp0, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        string ForProductAttribute(string mcu);
        string ForSizeOne(string mcu, string sec1, string dsc1, ICollection<ProblemDto> problemList, int rowNumber);
        string ForTdLine(string prp2Desc2, string dsc1, string dsc2);
        string ForMaktx(string dsc12);
        string ForDzeit(string srp1);
        string ForZzdm2N(string sec1, string sec2);
        string ForUnitCost(string kg, string unitCost, ICollection<ProblemDto> mappingProblemList, int rowNumber);
    }
}