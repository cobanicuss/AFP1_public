using Spm.Shared;

namespace Spm.AuditLog.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class AuditActionType : IMarkAsDomain
    {
        public virtual string Id { get; set; }
        public virtual int ActionId { get; set; }
        public virtual string ActionName { get; set; }
    }
}