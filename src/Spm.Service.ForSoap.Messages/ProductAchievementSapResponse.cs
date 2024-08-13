using NServiceBus;

namespace Spm.Service.ForSoap.Messages
{
    public class ProductAchievementSapResponse : BaseResponseIdoc, IMessage
    {
        public ProductAchievementSapResponse(BaseResponseIdoc baseResponseIdoc) : base(baseResponseIdoc) { }

        public string SagaReferenceId { get; set; }

        public override string ToString()
        {
            var str = $@"SagaReferenceId={SagaReferenceId}
            Tabnam={Tabnam}
            Direct={Direct}
            Idoctyp={Idoctyp}
            Mestyp={Mestyp}
            Sndpor={Sndpor}
            Sndprt={Sndprt}
            Sndprn={Sndprn}
            Rcvpor={Rcvpor}
            Rcvprt={Rcvprt}
            Rcvprn={Rcvprn}
            Docnum={Docnum}
            Logdat={Logdat}
            Logtim={Logtim}
            Status={Status}
            Uname={Uname}
            Repid={Repid}
            Stacod={Stacod}
            Statxt={Statxt}
            Statyp={Statyp}";

            return str;
        }
    }
}