namespace Spm.OrrSys.Service.TestCertificates
{
    public interface IDeleteTestCertificates
    {
        void DeleteBakupTestCertificateFilesIfFolderIsFull(string folder, int testCertificateBufferSize);
        void DeletePreviousTestCertficateFiles(string folder);
    }
}