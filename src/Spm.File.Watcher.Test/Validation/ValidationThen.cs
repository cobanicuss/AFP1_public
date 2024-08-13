using NUnit.Framework;

namespace Spm.File.Watcher.Test.Validation
{
    public partial class ValidationTest
    {
        #region THEN
        private void ValidationPassed()
        {
            Assert.IsTrue(_resultDto.IsOk);
        }

        private void ValidationFailed()
        {
            Assert.IsFalse(_resultDto.IsOk);
        }
        #endregion
    }
}