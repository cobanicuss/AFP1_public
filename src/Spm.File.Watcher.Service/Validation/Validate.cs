using System;
using Spm.File.Watcher.Service.Dto;

namespace Spm.File.Watcher.Service.Validation
{
    public class Validate : IValidate
    {
        private readonly IDisplayErrors _error;

        public Validate(IDisplayErrors error)
        {
            _error = error;
        }

        public ResultDto ParseAsDate(string methodName, string inputName, string inputValue)
        {
            DateTime outDateTime;
            var isDate = DateTime.TryParse(inputValue, out outDateTime);

            var isOk = !string.IsNullOrEmpty(inputValue) && !string.IsNullOrWhiteSpace(inputValue) && isDate;

            if (isOk) return new ResultDto(true);

            return new ResultDto
            {
                IsOk = false,
                Output = _error.ProblemWith(methodName, inputName, inputValue)
            };
        }

        public ResultDto AsString(string inputName, string inputValue)
        {
            var isOk = !string.IsNullOrEmpty(inputValue) && !string.IsNullOrWhiteSpace(inputValue);

            if (isOk) return new ResultDto(true);

            return new ResultDto
            {
                IsOk = false,
                Output = _error.ProblemWith(inputName, inputValue)
            };
        }

        public ResultDto AsString(string methodName,
            string inputName, string inputValue)
        {
            var isOk = !string.IsNullOrEmpty(inputValue) && !string.IsNullOrWhiteSpace(inputValue);

            if (isOk) return new ResultDto(true);

            return new ResultDto
            {
                IsOk = false,
                Output = _error.ProblemWith(methodName, inputName, inputValue)
            };
        }

        public ResultDto AsString(string methodName,
            string inputName1, string inputValue1,
            string inputName2, string inputValue2)
        {
            var isOk = !string.IsNullOrEmpty(inputValue1) &&
                       !string.IsNullOrWhiteSpace(inputValue1) &&
                       !string.IsNullOrEmpty(inputValue2) &&
                       !string.IsNullOrWhiteSpace(inputValue2);

            if (isOk) return new ResultDto(true);

            return new ResultDto
            {
                IsOk = false,
                Output = _error.ProblemWith(methodName,
                                inputName1, inputValue1,
                                inputName2, inputValue2)
            };
        }

        public ResultDto AsString(string methodName,
            string inputName1, string inputValue1,
            string inputName2, string inputValue2,
            string inputName3, string inputValue3)
        {
            var isOk = !string.IsNullOrEmpty(inputValue1) &&
                       !string.IsNullOrWhiteSpace(inputValue1) &&
                       !string.IsNullOrEmpty(inputValue2) &&
                       !string.IsNullOrWhiteSpace(inputValue2) &&
                       !string.IsNullOrEmpty(inputValue3) &&
                       !string.IsNullOrWhiteSpace(inputValue3);

            if (isOk) return new ResultDto(true);

            return new ResultDto
            {
                IsOk = false,
                Output = _error.ProblemWith(methodName,
                                inputName1, inputValue1,
                                inputName2, inputValue2,
                                inputName3, inputValue3)
            };
        }

        public ResultDto AsString(string methodName,
            string inputName1, string inputValue1,
            string inputName2, string inputValue2,
            string inputName3, string inputValue3,
            string inputName4, string inputValue4)
        {
            var isOk = !string.IsNullOrEmpty(inputValue1) &&
                       !string.IsNullOrWhiteSpace(inputValue1) &&
                       !string.IsNullOrEmpty(inputValue2) &&
                       !string.IsNullOrWhiteSpace(inputValue2) &&
                       !string.IsNullOrEmpty(inputValue3) &&
                       !string.IsNullOrWhiteSpace(inputValue3) &&
                       !string.IsNullOrEmpty(inputValue4) &&
                       !string.IsNullOrWhiteSpace(inputValue4);

            if (isOk) return new ResultDto(true);

            return new ResultDto
            {
                IsOk = false,
                Output = _error.ProblemWith(methodName,
                                inputName1, inputValue1,
                                inputName2, inputValue2,
                                inputName3, inputValue3,
                                inputName4, inputValue4)
            };
        }

        public ResultDto ParseAsFloat(string inputName, string inputValue)
        {
            bool canParse;

            var isOk = !string.IsNullOrEmpty(inputValue) && !string.IsNullOrWhiteSpace(inputValue);

            if (isOk)
            {
                float floatOut;
                canParse = float.TryParse(inputValue, out floatOut);
            }
            else return new ResultDto
                {
                    IsOk = false,
                    Output = _error.ProblemWith(inputName, inputValue)
                };

            if (canParse)
                return new ResultDto(true);

            return new ResultDto
            {
                IsOk = false,
                Output = _error.CannotParse(inputName, inputValue)
            };
        }

        public ResultDto ParseAsFloat(string methodName, string inputName1, string inputValue1, string inputName2, string inputValue2)
        {
            bool canParse1;
            bool canParse2;

            var isOk = !string.IsNullOrEmpty(inputValue1) && 
                !string.IsNullOrWhiteSpace(inputValue1) &&
                !string.IsNullOrEmpty(inputValue2) && 
                !string.IsNullOrWhiteSpace(inputValue2);

            if (isOk)
            {
                float floatOut;
                canParse1 = float.TryParse(inputValue1, out floatOut);
                canParse2 = float.TryParse(inputValue1, out floatOut);
            }
            else return new ResultDto
            {
                IsOk = false,
                Output = _error.ProblemWith(methodName, inputName1, inputValue1, inputName2, inputValue2)
            };

            if (canParse1 && canParse2) return new ResultDto(true);

            return new ResultDto
            {
                IsOk = false,
                Output = _error.CannotParse(methodName, inputName1, inputValue1, inputName2, inputValue2)
            };
        }

        public ResultDto ParseAsDouble(string inputName, string inputValue)
        {
            bool canParse;

            var isOk = !string.IsNullOrEmpty(inputValue) && !string.IsNullOrWhiteSpace(inputValue);

            if (isOk)
            {
                double doubleOut;
                canParse = double.TryParse(inputValue, out doubleOut);
            }
            else return new ResultDto
            {
                IsOk = false,
                Output = _error.ProblemWith(inputName, inputValue)
            };

            if (canParse)
                return new ResultDto(true);

            return new ResultDto
            {
                IsOk = false,
                Output = _error.CannotParse(inputName, inputValue)
            };
        }

        public ResultDto ParseAsInteger(string inputName, string inputValue)
        {
            bool canParse;

            var isOk = !string.IsNullOrEmpty(inputValue) && !string.IsNullOrWhiteSpace(inputValue);

            if (isOk)
            {
                int intOut;
                canParse = int.TryParse(inputValue, out intOut);
            }
            else return new ResultDto
            {
                IsOk = false,
                Output = _error.ProblemWith(inputName, inputValue)
            };

            if (canParse)
                return new ResultDto(true);

            return new ResultDto
            {
                IsOk = false,
                Output = _error.CannotParse(inputName, inputValue)
            };
        }
    }
}
