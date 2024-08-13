using Spm.OrrSys.Service.Dto;

namespace Spm.OrrSys.Service.TestCertificates
{
    public interface IWriteTestCertificates
    {
        void ToBakup(byte[] pdf, TestCertBakupDto testCertDto, string bakupFolder);
        void ToFile(byte[] pdf, TestCertFileDto testCertDto, string testCertificatePath);
        string ToBase64EncodingWithLineBreaks(byte[] pdf);
    }
}