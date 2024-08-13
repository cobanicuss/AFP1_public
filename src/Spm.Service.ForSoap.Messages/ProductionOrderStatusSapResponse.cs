using NServiceBus;

namespace Spm.Service.ForSoap.Messages
{
    public class ProductionOrderStatusSapResponse : BaseResponseIdoc, IMessage
    {
        public ProductionOrderStatusSapResponse(BaseResponseIdoc baseResponseIdoc): base(baseResponseIdoc){}

        public string ProductionOrderId { get; set; }
        public string SagaReferenceId { get; set; }

        public override string ToString()
        {
           var str = $@"ProductionOrderId={ProductionOrderId}
            SagaReferenceId={SagaReferenceId}
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