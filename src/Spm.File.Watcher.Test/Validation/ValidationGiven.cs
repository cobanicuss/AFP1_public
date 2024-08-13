namespace Spm.File.Watcher.Test.Validation
{
    public partial class ValidationTest
    {
        #region GIVEN
        private void WhiteSpaceDateStringPassedIn()
        {
            _jdeDateString = "      ";
        }

        private void EmptyDateStringPassedIn()
        {
            _jdeDateString = string.Empty;
        }

        private void BadDateStringPassedIn()
        {
            _method = "method";
            _name1 = "name1";
            _jdeDateString = "BadDateString";
        }

        private void ValidDateStringPassedIn()
        {
            _method = "method";
            _name1 = "name1";
            _jdeDateString = "12/05/16";
        }

        private void EmptyStringIsPassedIn()
        {
            EmptyStringsArePassedIn();
        }

        private void EmptyStringsArePassedIn()
        {
            _value1 = string.Empty;
            _value2 = "";
            _value3 = string.Empty;
            _value4 = null;
        }

        private void WhitSpaceStringIsPassedIn()
        {
            WhitSpaceStringsArePassedIn();
        }

        private void WhitSpaceStringsArePassedIn()
        {
            _value1 = " ";
            _value2 = "     ";
            _value3 = "  ";
            _value4 = " ";
        }

        private void ValidStringIsPassedIn()
        {
            ValidStringsArePassedIn();
        }

        private void ValidStringsArePassedIn()
        {
            _method = "method";
            _name1 = "name1";
            _value1 = "value1";
            _name2 = "name2";
            _value2 = "value2";
            _name3 = "name3";
            _value3 = "value3";
            _name4 = "name4";
            _value4 = "value4";
        }

        private void OneValidFloatStringIsPassedIn()
        {
            _floatString1 = "123.456";
        }

        private void OneEmptyFloatStringIsPassedIn()
        {
            _name1 = "name1";
            _floatString1 = string.Empty;
        }

        private void OneBadFloatStringIsPassedIn()
        {
            _name1 = "name1";
            _floatString1 = "BadFloatString";
        }

        private void TwoValidFloatStringArePassedIn()
        {
            _floatString1 = "123.456";
            _floatString2 = "123.456";
        }

        private void OneWhiteSpaceFloatStringIsPassedIn()
        {
            _name1 = "name1";
            _floatString1 = "  ";
        }

        private void TwoEmptyFloatStringsArePassedIn()
        {
            _method = "method";

            _name1 = "name1";
            _floatString1 = string.Empty;

            _name2 = "name2";
            _floatString2 = null;
        }

        private void TwoBadFloatStringIsPassedIn()
        {
            _method = "method";

            _name1 = "name1";
            _floatString1 = "BadFloatString1";

            _name2 = "name2";
            _floatString2 = "BadFloatString2";
        }

        private void EmptyDoubleStringIsPassedIn()
        {
            _name1 = "name1";
            _doubleString = string.Empty;
        }

        private void BadDoubleStringIsPassedIn()
        {
            _name1 = "name1";
            _doubleString = "BadDoubleString";
        }

        private void ValidDoubleStringIsPassedIn()
        {
            _doubleString = "123.456";
        }

        private void ValidIntegerStringIsPassedIn()
        {
            _integerString = "123";
        }

        private void BadIntegerStringIsPassedIn()
        {
            _name1 = "name1";
            _integerString = "BadIntegerString";
        }

        private void EmptyIntegerStringIsPassedIn()
        {
            _name1 = "name1";
            _integerString = string.Empty;
        }
        #endregion
    }
}
