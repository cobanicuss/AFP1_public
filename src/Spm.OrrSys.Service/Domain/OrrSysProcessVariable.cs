using System;
using Spm.Shared;

namespace Spm.OrrSys.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class OrrSysProcessVariable : IMarkAsDomain
    {
        public virtual int Id { get; set; }
        public virtual DateTime RunProductionOrderStatusOnTime { get; set; }
    }
}