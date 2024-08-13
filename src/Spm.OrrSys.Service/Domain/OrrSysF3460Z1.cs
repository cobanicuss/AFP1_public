using System;
using Spm.Shared;

namespace Spm.OrrSys.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class OrrSysF3460Z1 : IMarkAsDomain
    {
        public virtual string  FTEDUS { get; set; }
        public virtual string  FTEDBT { get; set; }
        public virtual string  FTEDTN { get; set; }
        public virtual int     FTEDLN { get; set; }
        public virtual string  FTTYTN { get; set; }
        public virtual decimal FTEDDT { get; set; }
        public virtual string  FTDRIN { get; set; }
        public virtual string  FTEDSP { get; set; }
        public virtual string  FTTNAC { get; set; }
        public virtual double  FTITM  { get; set; }
        public virtual string  FTLITM { get; set; }
        public virtual string  FTAITM { get; set; }
        public virtual string  FTMCU  { get; set; }
        public virtual decimal FTDRQJ { get; set; }
        public virtual double  FTUORG { get; set; }
        public virtual double  FTFQT  { get; set; }
        public virtual string  FTTYPF { get; set; }
        public virtual string  FTDCTO { get; set; }

        public override bool Equals(object obj)
        {
            var t = obj as OrrSysF3460Z1;

            if (t == null) return false;

            if (FTEDUS == t.FTEDUS &&
                FTEDBT == t.FTEDBT &&
                FTEDTN == t.FTEDTN &&
                FTEDLN == t.FTEDLN &&
                FTTYTN == t.FTTYTN &&
                FTEDDT == t.FTEDDT &&
                FTDRIN == t.FTDRIN &&
                FTEDSP == t.FTEDSP &&
                FTTNAC == t.FTTNAC &&
                Math.Abs(FTITM - t.FTITM) < 0.01 &&
                FTLITM == t.FTLITM &&
                FTAITM == t.FTAITM &&
                FTMCU == t.FTMCU &&
                FTDRQJ == t.FTDRQJ &&
                Math.Abs(FTUORG - t.FTUORG) < 0.01 &&
                Math.Abs(FTFQT - t.FTFQT) < 0.01 &&
                FTTYPF == t.FTTYPF &&
                FTDCTO == t.FTDCTO) { return true; }

            return false;
        }
        public override int GetHashCode()
        {
            return (FTEDUS + "|" +
             FTEDBT + "|" +
             FTEDTN + "|" +
             FTEDLN + "|" +
             FTTYTN + "|" +
             FTEDDT + "|" +
             FTDRIN + "|" +
             FTEDSP + "|" +
             FTTNAC + "|" +
             FTITM + "|" +
             FTLITM + "|" +
             FTAITM + "|" +
             FTMCU + "|" +
             FTDRQJ + "|" +
             FTUORG + "|" +
             FTFQT + "|" +
             FTTYPF + "|" +
             FTDCTO).GetHashCode();
        }

        public override string ToString()
        {
            return (FTEDUS + "|" +
             FTEDBT + "|" +
             FTEDTN + "|" +
             FTEDLN + "|" +
             FTTYTN + "|" +
             FTEDDT + "|" +
             FTDRIN + "|" +
             FTEDSP + "|" +
             FTTNAC + "|" +
             FTITM + "|" +
             FTLITM + "|" +
             FTAITM + "|" +
             FTMCU + "|" +
             FTDRQJ + "|" +
             FTUORG + "|" +
             FTFQT + "|" +
             FTTYPF + "|" +
             FTDCTO);
        }
    }
}
