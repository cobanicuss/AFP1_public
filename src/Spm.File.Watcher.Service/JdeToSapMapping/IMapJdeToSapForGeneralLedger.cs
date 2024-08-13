using System.Collections.Generic;
using Spm.File.Watcher.Service.Dto;
using Spm.Service.Messages;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public interface IMapJdeToSapForGeneralLedger : IMapJdeToSapBase<GeneralLedgerSapDto, GeneralLedgerCommand>
    {
        MappingResultGeneralLedgerDto CreateMapping(IList<GeneralLedgerSapDto> input);
    }
}