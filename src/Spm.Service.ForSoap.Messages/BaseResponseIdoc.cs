namespace Spm.Service.ForSoap.Messages
{
    public class BaseResponseIdoc
    {
        public BaseResponseIdoc(BaseResponseIdoc baseResponseIdoc)
        {
            Tabnam = baseResponseIdoc.Tabnam;
            Direct = baseResponseIdoc.Direct;
            Idoctyp = baseResponseIdoc.Idoctyp;
            Mestyp = baseResponseIdoc.Mestyp;
            Sndpor = baseResponseIdoc.Sndpor;
            Sndprt = baseResponseIdoc.Sndprt;
            Sndprn = baseResponseIdoc.Sndprn;
            Rcvpor = baseResponseIdoc.Rcvpor;
            Rcvprt = baseResponseIdoc.Rcvprt;
            Rcvprn = baseResponseIdoc.Rcvprn;
            Docnum = baseResponseIdoc.Docnum;
            Logdat = baseResponseIdoc.Logdat;
            Logtim = baseResponseIdoc.Logtim;
            Status = baseResponseIdoc.Status;
            Uname = baseResponseIdoc.Uname;
            Repid = baseResponseIdoc.Repid;
            Stacod = baseResponseIdoc.Stacod;
            Statxt = baseResponseIdoc.Statxt;
            Statyp = baseResponseIdoc.Statyp;
        }

        public BaseResponseIdoc(){}

        public string Tabnam { get; set; }
        public string Direct { get; set; }
        public string Idoctyp { get; set; }
        public string Mestyp { get; set; }
        public string Sndpor { get; set; }
        public string Sndprt { get; set; }
        public string Sndprn { get; set; }
        public string Rcvpor { get; set; }
        public string Rcvprt { get; set; }
        public string Rcvprn { get; set; }

        public string Docnum { get; set; }
        public string Logdat { get; set; }
        public string Logtim { get; set; }
        public string Status { get; set; }
        public string Uname { get; set; }
        public string Repid { get; set; }
        public string Stacod { get; set; }
        public string Statxt { get; set; }
        public string Statyp { get; set; }
    }
}
