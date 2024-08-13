namespace Spm.OrrSys.Service.Config
{
    internal class ProfileConfigVariables
    {
        internal static string Sap { get; set; }
        internal static string JdeImport { get; set; }
        internal static string TestCertFolder { get; set; }
        internal static string TestCertBakupFolder { get; set; }
        internal static bool RestrictTestCertFiles { get; set; }
        internal static int TestCertificateBufferSize { get; set; }
    }
}