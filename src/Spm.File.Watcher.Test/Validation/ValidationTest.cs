using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using Spm.File.Watcher.Service.Validation;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.Validation
{
    [TestFixture]
    public partial class ValidationTest
    {
        private IValidate _validate;
        private IDisplayErrors _error;

        private string _jdeDateString;

        private string _floatString1;
        private string _floatString2;

        private string _doubleString;

        private string _integerString;
        
        private string _method;
        private string _name1;
        private string _value1;
        private string _name2;
        private string _value2;
        private string _name3;
        private string _value3;
        private string _name4;
        private string _value4;

        private ResultDto _resultDto;

        [SetUp]
        public void Setup()
        {
            _error = new ErrorDisplay();
            _validate = new Validate(_error);
        }

        [Test]
        public void OneValidStringShouldPassValidation()
        {
            this.Given(_ => ValidStringIsPassedIn())
            .When(_ => ValidateOneString())
            .Then(_ => ValidationPassed())

            .BDDfy();
        }

        [Test]
        public void OneEmptyStringShouldFailValidation()
        {
            this.Given(_ => EmptyStringIsPassedIn())
            .When(_ => ValidateOneString())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }

        [Test]
        public void OneWhiteSpaceStringShouldFailValidation()
        {
            this.Given(_ => WhitSpaceStringIsPassedIn())
            .When(_ => ValidateOneString())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }

        [Test]
        public void TwoValidStringShouldPassValidation()
        {
            this.Given(_ => ValidStringsArePassedIn())
            .When(_ => ValidateTwoStrings())
            .Then(_ => ValidationPassed())

            .BDDfy();
        }

        [Test]
        public void OneOrMoreEmptyStringsFromTwoInputsShouldFailValidation()
        {
            this.Given(_ => EmptyStringsArePassedIn())
            .When(_ => ValidateTwoStrings())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }

        [Test]
        public void OneOrMoreWhiteSpaceStringsFromTwoInputsShouldFailValidation()
        {
            this.Given(_ => WhitSpaceStringsArePassedIn())
            .When(_ => ValidateTwoStrings())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }

        [Test]
        public void ThreeValidStringShouldPassValidation()
        {
            this.Given(_ => ValidStringsArePassedIn())
            .When(_ => ValidateThreeStrings())
            .Then(_ => ValidationPassed())

            .BDDfy();
        }

        [Test]
        public void OneOrMoreEmptyStringsFromThreeInputsShouldFailValidation()
        {
            this.Given(_ => EmptyStringsArePassedIn())
            .When(_ => ValidateThreeStrings())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }

        [Test]
        public void OneOrMoreWhiteSpaceStringsFromThreeInputsShouldFailValidation()
        {
            this.Given(_ => WhitSpaceStringsArePassedIn())
            .When(_ => ValidateThreeStrings())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }

        [Test]
        public void FourValidStringShouldPassValidation()
        {
            this.Given(_ => ValidStringsArePassedIn())
            .When(_ => ValidateFourStrings())
            .Then(_ => ValidationPassed())

            .BDDfy();
        }

        [Test]
        public void OneOrMoreEmptyStringsFromFourInputsShouldFailValidation()
        {
            this.Given(_ => EmptyStringsArePassedIn())
            .When(_ => ValidateFourStrings())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }

        [Test]
        public void OneOrMoreWhiteSpaceStringsFromFourInputsShouldFailValidation()
        {
            this.Given(_ => WhitSpaceStringsArePassedIn())
            .When(_ => ValidateFourStrings())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }

        [Test]
        public void ValidDateShouldPassValidation()
        {
            this.Given(_ => ValidDateStringPassedIn())
            .When(_ => ValidateDate())
            .Then(_ => ValidationPassed())

            .BDDfy();
        }
        
        [Test]
        public void BadDateShouldFailValidation()
        {
            this.Given(_ => BadDateStringPassedIn())
            .When(_ => ValidateDate())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }

        [Test]
        public void EmptyDateShouldFailValidation()
        {
            this.Given(_ => EmptyDateStringPassedIn())
            .When(_ => ValidateDate())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }

        [Test]
        public void WhiteSpaceDateShouldFailValidation()
        {
            this.Given(_ => WhiteSpaceDateStringPassedIn())
            .When(_ => ValidateDate())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }

        [Test]
        public void OneValidFloatStringShouldPassValidation()
        {
            this.Given(_ => OneValidFloatStringIsPassedIn())
            .When(_ => ValidateOneFloatString())
            .Then(_ => ValidationPassed())

            .BDDfy();
        }

        [Test]
        public void OneBadFloatStringShouldFailValidation()
        {
            this.Given(_ => OneBadFloatStringIsPassedIn())
            .When(_ => ValidateOneFloatString())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }

        [Test]
        public void OneEmptyFloatStringShouldFailValidation()
        {
            this.Given(_ => OneEmptyFloatStringIsPassedIn())
            .When(_ => ValidateOneFloatString())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }

        [Test]
        public void OneWhiteSpaceFloatStringShouldFailValidation()
        {
            this.Given(_ => OneWhiteSpaceFloatStringIsPassedIn())
            .When(_ => ValidateOneFloatString())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }

        [Test]
        public void TwoValidFloatStringsShouldPassValidation()
        {
            this.Given(_ => TwoValidFloatStringArePassedIn())
            .When(_ => ValidateTwoFloatStrings())
            .Then(_ => ValidationPassed())

            .BDDfy();
        }

        [Test]
        public void OneOrMoreBadFloatStringFromTwoInputsShouldFailValidation()
        {
            this.Given(_ => TwoBadFloatStringIsPassedIn())
            .When(_ => ValidateTwoFloatStrings())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }

        [Test]
        public void OneOrMoreEmptyFloatStringFromTwoInputsShouldFailValidation()
        {
            this.Given(_ => TwoEmptyFloatStringsArePassedIn())
            .When(_ => ValidateTwoFloatStrings())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }

        [Test]
        public void ValidDoubleStringShouldPassValidation()
        {
            this.Given(_ => ValidDoubleStringIsPassedIn())
            .When(_ => ValidateDoubleString())
            .Then(_ => ValidationPassed())

            .BDDfy();
        }

        [Test]
        public void BadDoubleStringShouldFailValidation()
        {
            this.Given(_ => BadDoubleStringIsPassedIn())
            .When(_ => ValidateDoubleString())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }

        [Test]
        public void EmptyDoubleStringShouldFailValidation()
        {
            this.Given(_ => EmptyDoubleStringIsPassedIn())
            .When(_ => ValidateDoubleString())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }

        [Test]
        public void ValidIntegerStringShouldPassValidation()
        {
            this.Given(_ => ValidIntegerStringIsPassedIn())
            .When(_ => ValidateIntegerString())
            .Then(_ => ValidationPassed())

            .BDDfy();
        }

        [Test]
        public void BadIntegerStringShouldFailValidation()
        {
            this.Given(_ => BadIntegerStringIsPassedIn())
            .When(_ => ValidateIntegerString())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }

        [Test]
        public void EmptyIntegerStringShouldFailValidation()
        {
            this.Given(_ => EmptyIntegerStringIsPassedIn())
            .When(_ => ValidateIntegerString())
            .Then(_ => ValidationFailed())

            .BDDfy();
        }
    }
}