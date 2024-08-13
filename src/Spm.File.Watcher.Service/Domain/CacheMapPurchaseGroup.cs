﻿using Spm.Shared;

namespace Spm.File.Watcher.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class CacheMapPurchaseGroup : IMarkAsDomain
    {
        public virtual int Id { get; set; }
        public virtual string JdePurchaseGroup { get; set; }
        public virtual string JdeDescription { get; set; }
        public virtual string SapPurchaseGroup { get; set; }
        public virtual string SapDescription { get; set; }
    }
}
