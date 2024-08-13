using Spm.AuditLog.Service.Dto;
using Spm.Shared;

namespace Spm.AuditLog.Service.Repository
{
    public interface IAuditLogRepository
    {
        void ExportAuditLogToFile(ExportSprocDto dto);

        int GetCountOfThisAuditLogTable(string auditLogTable);

        void SaveThisAuditLog(IMarkAsDomain domain);

        void TruncateThisAuditLogTable(string auditLogTable);

        void CreateAllNewAuditActionTypes();
    }
}