namespace Spm.Service.ForSoap.SendToSapImplementation
{
    public interface IDuplicateSetupInSap
    {
        string Setup(string sagaReferenceId, string queryString, string absoluteUri);
    }
}