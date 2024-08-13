namespace Spm.Shared
{
    public class ProfileConnectionString
    {
        public static string DevelopmentOrrSys = @"Data Source=(local);Initial Catalog=OrrSys;Integrated Security=True";
        public static string TestOrrSys = @"Data Source=STLSYDAPP144;Initial Catalog=OrrSys;Integrated Security=True";
        public static string ProductionOrrSys = @"Data Source=STLSYDAPP143;Initial Catalog=OrrSys;Integrated Security=True";

        //public static string DevelopmentJdeImport = @"Data Source=SQL_DEV\DEVELOPMENT;Initial Catalog=JDE_Import;Integrated Security=True";
        //public static string TestJdeImport = @"Data Source=STLSYDAPP144;Initial Catalog=JDE_Import;Integrated Security=True";
        public static string DevelopmentJdeImport = @"Data Source=OSSQL01;Initial Catalog=JDE_Import;Integrated Security=True";
        public static string TestJdeImport = @"Data Source=OSSQL01;Initial Catalog=JDE_Import;Integrated Security=True";
        public static string ProductionJdeImport = @"Data Source=STLSYDDB14;Initial Catalog=JDE_Import;Integrated Security=True";

        public static string DevelopmentSap = @"Data Source=SQL_DEV\DEVELOPMENT;Initial Catalog=SAP;Integrated Security=True";
        public static string TestSap = @"Data Source=STLSYDAPP144;Initial Catalog=SAP;Integrated Security=True";
        public static string ProductionSap = @"Data Source=STLSYDDB14;Initial Catalog=SAP;Integrated Security=True";

        public static string DevelopmentSpmAuditlog = @"Data Source=(local);Initial Catalog=SPM.Auditlog;Integrated Security=True";
        public static string TestSpmAuditlog = @"Data Source=STLSYDAPP144;Initial Catalog=SPM.Auditlog;Integrated Security=True";
        public static string ProductionSpmAuditlog = @"Data Source=STLSYDAPP143;Initial Catalog=SPM.Auditlog;Integrated Security=True";

        public static string DevelopmentSpmService = @"Data Source=(local);Initial Catalog=SPM.Service;Integrated Security=True";
        public static string TestSpmService = @"Data Source=STLSYDAPP144;Initial Catalog=SPM.Service;Integrated Security=True";
        public static string ProductionSpmService = @"Data Source=STLSYDAPP143;Initial Catalog=SPM.Service;Integrated Security=True";

        public static string DevelopmentSpmFileWatcher = @"Data Source=(local);Initial Catalog=SPM.FileWatcher;Integrated Security=True";
        public static string TestSpmFileWatcher = @"Data Source=STLSYDAPP144;Initial Catalog=SPM.FileWatcher;Integrated Security=True";
        public static string ProductionSpmFileWatcher = @"Data Source=STLSYDAPP143;Initial Catalog=SPM.FileWatcher;Integrated Security=True";

        public static string DevelopmentSpmTestHarness = @"Data Source=(local);Initial Catalog=SPM.TestHarness;Integrated Security=True";
    }
}