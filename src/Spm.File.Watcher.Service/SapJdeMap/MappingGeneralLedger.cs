using System.Collections.Generic;
using System.Linq;
using Spm.File.Watcher.Service.Domain;
using Spm.File.Watcher.Service.Dto;
using Spm.File.Watcher.Service.Repository;
using Spm.Service.Messages;
using Spm.Shared;

namespace Spm.File.Watcher.Service.SapJdeMap
{
    public class MappingGeneralLedger : IMapJdeToSapForGeneralLedger
    {
        private readonly IDoMappingBusinessRules _mappingBusinessRules;

        private readonly List<CacheMapGlAccountsGlPosting> _glAcountsGlPosting;
        private readonly List<CacheMapProfitCentreGlPosting> _profitCenterGlPosting;
        private readonly List<CacheMapCostCentreGlPosting> _costCenterGlPosting;

        public MappingGeneralLedger(ICacheMapRepository cacheMapRepository, IDoMappingBusinessRules mappingBusinessRules)
        {
            _mappingBusinessRules = mappingBusinessRules;

            _glAcountsGlPosting = cacheMapRepository.GetGlAccountsGlPostingMapping();
            _profitCenterGlPosting = cacheMapRepository.GetProfitCentreGlPostingMapping();
            _costCenterGlPosting = cacheMapRepository.GetCostCentreGlPostingMapping();
        }

        public MappingValidationGeneralLedgerDto ValidateMapping(IList<GeneralLedgerDto> input)
        {
            var validationList = new List<ValidationResult>();
            var mappedList = new List<GeneralLedgerDto>();
            var rowNumber = 0;

            foreach (var inItem in input)
            {
                rowNumber++;

                var outItem = new GeneralLedgerDto();

                CreateMappingByItem(inItem, outItem, validationList, rowNumber);

                mappedList.Add(outItem);
            }

            var returnVal = new MappingValidationGeneralLedgerDto
            {
                MappedList = mappedList,
                ValidationList = validationList
            };

            return returnVal;
        }

        public GeneralLedgerCommand MapPayload(IList<GeneralLedgerDto> input)
        {
            var outputPayloadList = (
               from inItem in input
               let outItem = new GeneralLedgerPayloadItem
               {
                   E1BpAche09UserName = inItem.UserName,
                   E1BpAche09HeaderTxt = inItem.HeaderTxt,
                   E1BpAche09CompanyCode = inItem.CompCode,
                   E1BpAche09DocDate = inItem.DocDate,
                   E1BpAche09PstngDate = inItem.PstingDate,
                   E1BpAche09DocType = inItem.DocType,
                   E1BpAche09RefDocNo = inItem.RefDocNo,
                   E1BpAcGl09ItemNoAcc = inItem.ItemNoAcc.ToString(),
                   E1BpAcGl09ItemText = inItem.ItemText,
                   E1BpAcGl09AllocNmbr = inItem.AllocNmbr,
                   E1BpAcGl09CostCentre = inItem.CostCentre,
                   E1BpAcGl09GlAccount = inItem.GlAccount,
                   E1BpAcGl09ProfitCentre = inItem.GlProfitCenter,
                   E1BpAccr09ItemNoAcc = inItem.ItemNoAcc.ToString(),
                   E1BpAccr09Currency = inItem.Currency,
                   E1BpAccr09AmtDoccur = inItem.AmtDoccur
               }
               select outItem).ToList();

            var outputPayload = new GeneralLedgerPayload { GeneralLedgerPayloadItem = outputPayloadList };

            var output = new GeneralLedgerCommand
            {
                Payload = outputPayload
            };

            return output;
        }

        private void CreateMappingByItem(GeneralLedgerDto inItem, GeneralLedgerDto outItem, ICollection<ValidationResult> validationList, int rowNumber)
        {
            outItem.UserName = MapDefaults.DefaultUserName;

            outItem.HeaderTxt = inItem.HeaderTxt;

            outItem.CompCode = MapDefaults.DefaultCompCode;

            var glDocDateResult = _mappingBusinessRules.MapGlDocDate(inItem.DocDate);
            if (glDocDateResult.IsMappingOk) outItem.DocDate = glDocDateResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = glDocDateResult.Output });

            var postingDateResult = _mappingBusinessRules.MapPostingDate(inItem.PstingDate);
            if (postingDateResult.IsMappingOk) outItem.PstingDate = postingDateResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = postingDateResult.Output });

            outItem.DocType = MapDefaults.DefaultGlDocType;

            outItem.RefDocNo = inItem.RefDocNo;

            outItem.ItemNoAcc = inItem.ItemNoAcc;

            outItem.AllocNmbr = inItem.AllocNmbr;

            outItem.ItemText = string.Empty;

            outItem.AllocNmbr = inItem.AllocNmbr;

            var glCostCenterResult = _mappingBusinessRules.MapGlCostCentre(_glAcountsGlPosting, _costCenterGlPosting, inItem.GlAccount, inItem.RefDocNo, inItem.CostCentre);
            if (glCostCenterResult.IsMappingOk) outItem.CostCentre = glCostCenterResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = glCostCenterResult.Output });

            var glAccountResult = _mappingBusinessRules.MapGlAccount(_glAcountsGlPosting, inItem.GlAccount, inItem.RefDocNo);
            if (glAccountResult.IsMappingOk) outItem.GlAccount = glAccountResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = glAccountResult.Output });

            var glProfitCentreResult = _mappingBusinessRules.MapGlProfitCentre(_glAcountsGlPosting, _profitCenterGlPosting, inItem.GlAccount, inItem.CostCentre);
            if (glProfitCentreResult.IsMappingOk) outItem.GlProfitCenter = glProfitCentreResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = glProfitCentreResult.Output });

            outItem.AmtDoccur = inItem.AmtDoccur;

            outItem.Currency = MapDefaults.DefaultCurrency;
        }
    }
}