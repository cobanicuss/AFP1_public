using NServiceBus;

namespace Spm.OrrSys.Messages
{
    public class ProductAchievementResponseCommand : ICommand
    {
        public string LotNumber { get; set; }
        public string SagaReferenceId { get; set; }

        public override string ToString()
        {
            var str = $@"LotNumber={LotNumber},SagaReferenceId={SagaReferenceId}";

            return str;
        }
    }
}