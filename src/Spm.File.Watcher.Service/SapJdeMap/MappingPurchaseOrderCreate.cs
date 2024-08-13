using System.Collections.Generic;
using Spm.File.Watcher.Service.Domain;
using Spm.File.Watcher.Service.Dto;
using Spm.File.Watcher.Service.Repository;
using Spm.Service.Messages;

namespace Spm.File.Watcher.Service.SapJdeMap
{
    public class MappingPurchaseOrderCreate : IMapJdeToSapForPurchaseOrderCreate
    {
        private readonly IDoMappingBusinessRules _mappingBusinessRules;
        private readonly IMapPayloads _mapPayloads;

        private readonly List<CacheMapCompanyCode> _companyCodeMapping;
        private readonly List<CacheMapDocTypes> _docTypeMapping;
        private readonly List<CacheMapMaterialGroup> _materialGroupMapping;
        private readonly List<CacheMapPurchaseGroup> _purchaseGroupMapping;
        private readonly List<CacheMapUnitOfMeasure> _unitOfMeasureMapping;
        private readonly List<CacheMapGlAccountsGlPosting> _glAccountsGlPostingMapping;
        private readonly List<CacheMapCostCentreGlPosting> _costCentreGlPostingMapping;
        private readonly List<CacheMapPlant> _plantMapping;

        public MappingPurchaseOrderCreate(ICacheMapRepository cacheMapRepository, IDoMappingBusinessRules mappingBusinessRules, IMapPayloads mapPayloads)
        {
            _mappingBusinessRules = mappingBusinessRules;
            _mapPayloads = mapPayloads;
            _docTypeMapping = cacheMapRepository.GetDocTypeMapping();
            _companyCodeMapping = cacheMapRepository.GetCompanyCodeMapping();
            _materialGroupMapping = cacheMapRepository.GetMaterialGroupMapping();
            _purchaseGroupMapping = cacheMapRepository.GetPurchaseGroupMapping();
            _unitOfMeasureMapping = cacheMapRepository.GetUnitOfMeasureMapping();
            _glAccountsGlPostingMapping = cacheMapRepository.GetGlAccountsGlPostingMapping();
            _costCentreGlPostingMapping = cacheMapRepository.GetCostCentreGlPostingMapping();
            _plantMapping = cacheMapRepository.GetPlantMapping();
        }

        public MappingValidationPurchaseOrderDto ValidateMapping(IList<PurchaseOrderDto> input)
        {
            var validationList = new List<ValidationResult>();
            var mappedList = new List<PurchaseOrderDto>();
            var rowNumber = 0;

            foreach (var inItem in input)
            {
                rowNumber++;

                var mappedItem = new PurchaseOrderDto();

                ValidateMappingByItem(inItem, mappedItem, validationList, rowNumber);

                mappedList.Add(mappedItem);
            }

            var returnVal = new MappingValidationPurchaseOrderDto
            {
                MappedList = mappedList,
                ValidationList = validationList
            };

            return returnVal;
        }

        public PurchaseOrderCreateCommand MapPayload(IList<PurchaseOrderDto> input)
        {
            var output = new PurchaseOrderCreateCommand
            {
                Payload = _mapPayloads.MapPurchaseOrderPayload(input)
            };

            return output;
        }

        private void ValidateMappingByItem(PurchaseOrderDto inItem, PurchaseOrderDto outItem, ICollection<ValidationResult> validationList, int rowNumber)
        {
            outItem.PoNumber = inItem.PoNumber;

            var compCodeResult = _mappingBusinessRules.MapCompCode(_companyCodeMapping, inItem.CompCode);
            if (compCodeResult.IsMappingOk) outItem.CompCode = compCodeResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = compCodeResult.Output });

            var docTypeResult = _mappingBusinessRules.MapDocType(_docTypeMapping, MapDefaults.PurchaseOrderCreate);
            if (docTypeResult.IsMappingOk) outItem.DocType = docTypeResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = docTypeResult.Output });

            var createDateResult = _mappingBusinessRules.MapCreatDate(inItem.CreatDate);
            if (createDateResult.IsMappingOk) outItem.CreatDate = createDateResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = createDateResult.Output });

            outItem.CreatedBy = MapDefaults.DefaultUserName;

            outItem.ItemIntvl = MapDefaults.DefaultItemIntvlCreate;

            var venderoResult = _mappingBusinessRules.MapVendor(inItem.Vendor);
            if (venderoResult.IsMappingOk) outItem.Vendor = venderoResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = venderoResult.Output });

            var purchOrgResult = _mappingBusinessRules.MapPurchOrg(inItem.PurchOrg);
            if (purchOrgResult.IsMappingOk) outItem.PurchOrg = purchOrgResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = purchOrgResult.Output });

            var purGroupResult = _mappingBusinessRules.MapPurchaseGroup(_purchaseGroupMapping, inItem.PurGroup);
            if (purGroupResult.IsMappingOk) outItem.PurGroup = purGroupResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = purGroupResult.Output });

            outItem.Currency = inItem.Currency;

            var poItemResult = _mappingBusinessRules.MapPoItem(inItem.PoItem);
            if (poItemResult.IsMappingOk) outItem.PoItem = poItemResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = poItemResult.Output });

            outItem.DeleteInd = inItem.DeleteInd;

            outItem.ShortText = inItem.ShortText;

            var plantResult = _mappingBusinessRules.MapPlant(_plantMapping, inItem.Plant);
            if (plantResult.IsMappingOk) outItem.Plant = plantResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = plantResult.Output });

            outItem.StgeLoc = MapDefaults.DefaultStgeLoc;

            var matlGroupResult = _mappingBusinessRules.MapMaterialGroup(_materialGroupMapping, inItem.Plant, inItem.LnType, inItem.Taxable);
            if (matlGroupResult.IsMappingOk) outItem.MatlGroup = matlGroupResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = matlGroupResult.Output });

            outItem.VendMat = inItem.VendMat;

            outItem.Quantity = inItem.Quantity;

            var poUnitResult = _mappingBusinessRules.MapPoUnit(_unitOfMeasureMapping, inItem.PoUnit);
            if (poUnitResult.IsMappingOk) outItem.PoUnit = poUnitResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = poUnitResult.Output });

            var orderPrUnResult = _mappingBusinessRules.MapOrderPrUn(_unitOfMeasureMapping, inItem.OrderprUn);
            if (orderPrUnResult.IsMappingOk) outItem.OrderprUn = orderPrUnResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = orderPrUnResult.Output });

            var netPriceResult = _mappingBusinessRules.MapNetPrice(inItem.NetPrice);
            if (netPriceResult.IsMappingOk) outItem.NetPrice = netPriceResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = netPriceResult.Output });

            outItem.PriceUnit = inItem.PriceUnit;

            outItem.OverDlvTol = inItem.OverDlvTol;

            outItem.Acctasscat = inItem.Acctasscat;

            outItem.PreqName = inItem.PreqName;

            outItem.SchedLine = MapDefaults.DefaultSchedLine;

            outItem.SerialNo = inItem.SerialNo;

            var glAccountResult = _mappingBusinessRules.MapPurchaseOrderGlAccount(_materialGroupMapping,
                    _glAccountsGlPostingMapping,
                    inItem.Plant,
                    inItem.LnType,
                    inItem.GlAccount,
                    inItem.Taxable);
            if (glAccountResult.IsMappingOk) outItem.GlAccount = glAccountResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = glAccountResult.Output });

            var costCenterResult = _mappingBusinessRules.MapCostCenter(_materialGroupMapping,
                    _costCentreGlPostingMapping,
                    inItem.Plant,
                    inItem.LnType,
                    inItem.Costcenter,
                    inItem.Taxable);
            if (costCenterResult.IsMappingOk) outItem.Costcenter = costCenterResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = costCenterResult.Output });

            var deliveryDateResult = _mappingBusinessRules.MapDeliveryDate(inItem.DeliveryDate);
            if (deliveryDateResult.IsMappingOk) outItem.DeliveryDate = deliveryDateResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = deliveryDateResult.Output });
        }
    }
}