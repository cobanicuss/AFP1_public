using Spm.OrrSys.Service.Config;

namespace Spm.OrrSys.Service.Dto
{
    public class ProfileVariableDto
    {
        public string TestCertPdfFolder
        {
            get { return ProfileConfigVariables.TestCertFolder; }
            set { ProfileConfigVariables.TestCertFolder = value; }
        }

        public string TestCertPdfBakupFolder
        {
            get { return ProfileConfigVariables.TestCertBakupFolder; }
            set { ProfileConfigVariables.TestCertBakupFolder = value; }
        }

        public bool RestrictTestCertFiles
        {
            get { return ProfileConfigVariables.RestrictTestCertFiles; }
            set { ProfileConfigVariables.RestrictTestCertFiles = value; }
        }

        public int PdfFileBufferSize
        {
            get { return ProfileConfigVariables.TestCertificateBufferSize; }
            set { ProfileConfigVariables.TestCertificateBufferSize = value; }
        }
    }
}