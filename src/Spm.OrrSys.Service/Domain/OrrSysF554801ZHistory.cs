using System;
using Spm.Shared;

namespace Spm.OrrSys.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class OrrSysF554801ZHistory : IMarkAsDomain
    {
        public virtual string SYEDUS { get; set; }
        public virtual string SYEDBT { get; set; }
        public virtual string SYEDTN { get; set; }
        public virtual int SYEDLN { get; set; }
        public virtual string SYEDCT { get; set; }
        public virtual string SYTYTN { get; set; }
        public virtual decimal SYEDDT { get; set; }
        public virtual string SYDRIN { get; set; }
        public virtual int SYEDDL { get; set; }
        public virtual string SYEDSP { get; set; }
        public virtual string SYTNAC { get; set; }
        public virtual string SYDCTO { get; set; }
        public virtual int SYITM { get; set; }
        public virtual string SYLITM { get; set; }
        public virtual decimal SYDRQJ { get; set; }
        public virtual double SYUORG { get; set; }
        public virtual double SYSOQS { get; set; }
        public virtual string SYUM { get; set; }
        public virtual string SYMCU { get; set; }
        public virtual string SYVR01 { get; set; }
        public virtual DateTime DateTimeImplemented { get; set; }
        public virtual DateTime DateRequested { get; set; }

        public override bool Equals(object obj)
        {
            var t = obj as OrrSysF554801ZHistory;

            if (t == null) return false;

            if (SYEDUS == t.SYEDUS &&
                SYEDBT == t.SYEDBT &&
                SYEDTN == t.SYEDTN &&
                SYEDLN == t.SYEDLN &&
                SYEDCT == t.SYEDCT &&
                SYTYTN == t.SYTYTN &&
                SYEDDT == t.SYEDDT &&
                SYDRIN == t.SYDRIN &&
                SYEDDL == t.SYEDDL &&
                SYEDSP == t.SYEDSP &&
                SYTNAC == t.SYTNAC &&
                SYDCTO == t.SYDCTO &&
                SYITM == t.SYITM &&
                SYLITM == t.SYLITM &&
                SYDRQJ == t.SYDRQJ &&
                Math.Abs(SYUORG - t.SYUORG) < 0.01 &&
                Math.Abs(SYSOQS - t.SYSOQS) < 0.01 &&
                SYUM == t.SYUM &&
                SYMCU == t.SYMCU &&
                SYVR01 == t.SYVR01) { return true; }

            return false;
        }
        public override int GetHashCode()
        {
            return (SYEDUS + "|" +
             SYEDBT + "|" +
             SYEDTN + "|" +
             SYEDLN + "|" +
             SYEDCT + "|" +
             SYTYTN + "|" +
             SYEDDT + "|" +
             SYDRIN + "|" +
             SYEDDL + "|" +
             SYEDSP + "|" +
             SYTNAC + "|" +
             SYDCTO + "|" +
             SYITM + "|" +
             SYLITM + "|" +
             SYDRQJ + "|" +
             SYUORG + "|" +
             SYSOQS + "|" +
             SYUM + "|" +
             SYMCU + "|" +
             SYVR01).GetHashCode();
        }

        public override string ToString()
        {
            return (SYEDUS + "|" +
             SYEDBT + "|" +
             SYEDTN + "|" +
             SYEDLN + "|" +
             SYEDCT + "|" +
             SYTYTN + "|" +
             SYEDDT + "|" +
             SYDRIN + "|" +
             SYEDDL + "|" +
             SYEDSP + "|" +
             SYTNAC + "|" +
             SYDCTO + "|" +
             SYITM + "|" +
             SYLITM + "|" +
             SYDRQJ + "|" +
             SYUORG + "|" +
             SYSOQS + "|" +
             SYUM + "|" +
             SYMCU + "|" +
             SYVR01);
        }
    }
}