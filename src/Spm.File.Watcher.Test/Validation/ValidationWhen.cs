namespace Spm.File.Watcher.Test.Validation
{
    public partial class ValidationTest
    {
        #region WHEN
        private void ValidateOneString()
        {
            _resultDto = _validate.AsString(_name1, _value1);
        }

        private void ValidateTwoStrings()
        {
            _resultDto = _validate.AsString(_method, _name1, _value1, _name2, _value2);
        }

        private void ValidateThreeStrings()
        {
            _resultDto = _validate.AsString(_method, _name1, _value1, _name2, _value2, _name3, _value3);
        }

        private void ValidateFourStrings()
        {
            _resultDto = _validate.AsString(_method, _name1, _value1, _name2, _value2, _name3, _value3, _name4, _value4);
        }

        private void ValidateDate()
        {
            _resultDto = _validate.ParseAsDate(_method, _name1, _jdeDateString);
        }

        private void ValidateOneFloatString()
        {
            _resultDto = _validate.ParseAsFloat(_name1, _floatString1);
        }

        private void ValidateTwoFloatStrings()
        {
            _resultDto = _validate.ParseAsFloat(_method, _name1, _floatString1, _name2, _floatString2);
        }

        private void ValidateDoubleString()
        {
            _resultDto = _validate.ParseAsDouble(_name1, _doubleString);
        }

        private void ValidateIntegerString()
        {
            _resultDto = _validate.ParseAsInteger(_name1, _integerString);
        }
        #endregion
    }
}