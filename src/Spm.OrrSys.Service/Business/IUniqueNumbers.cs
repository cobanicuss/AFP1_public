namespace Spm.OrrSys.Service.Business
{
    public interface IUniqueNumbers
    {
        string GetUniqueNDigitNumberAsString(int n);
        string CreateUniqueCertificateNumber();
    }
}