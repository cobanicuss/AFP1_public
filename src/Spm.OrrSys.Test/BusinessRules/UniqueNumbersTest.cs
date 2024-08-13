using NUnit.Framework;
using Spm.OrrSys.Service.Business;
using TestStack.BDDfy;

namespace Spm.OrrSys.Test.BusinessRules
{
    [TestFixture]
    public class UniqueNumbersTest
    {
        private IUniqueNumbers _classUnderTest;
        private string _result1 = string.Empty;
        private string _result2 = string.Empty;

        [Test]
        public void JdeRequiresUniqueNumbersWhichContainsFifteenDigits()
        {
            this.Given("A 15 digit unique number needs to be generated")
            .When(_ => PassingIn15AsParameter())
            .Then(_ => ResultMustHave15Digits())

            .BDDfy();
        }

        [Test]
        public void ConsecutiveRunsMustYieldDifferentResults()
        {
            this.Given("Running two consecutive executions")
            .When(_ => RunTwiceOnDifferentInstances())
            .Then(_ => ResultsMustBeDifferent())

            .BDDfy();
        }

        private void PassingIn15AsParameter()
        {
            _classUnderTest = new UniqueNumbers();
            _result1 = _classUnderTest.GetUniqueNDigitNumberAsString(15);
        }

        private void RunTwiceOnDifferentInstances()
        {
            _classUnderTest = new UniqueNumbers();
            _result1 = _classUnderTest.GetUniqueNDigitNumberAsString(15);

            System.Threading.Thread.Sleep(1000); //Won't be called consecutavely under operatioanl conditions//

            _classUnderTest = new UniqueNumbers();
            _result2 = _classUnderTest.GetUniqueNDigitNumberAsString(15);
        }

        private void ResultsMustBeDifferent()
        {
            Assert.AreNotEqual(_result1, _result2);
        }

        private void ResultMustHave15Digits()
        {
            Assert.AreEqual(15, _result1.Length);
        }
    }
}