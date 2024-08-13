using Spm.File.Watcher.Service.Dto;
using Spm.Shared;

namespace Spm.File.Watcher.Service.Validation
{
    public interface IValidate : IMarkAsValidator
    {
        ResultDto ParseAsDate(string methodName, string inputName, string inputValue);

        ResultDto AsString(string inputName, string inputValue);
        ResultDto AsString(string methodName, string inputName, string inputValue);
        ResultDto AsString(string methodName, string inputName1, string inputValue1, string inputName2, string inputValue2);
        ResultDto AsString(string methodName, string inputName1, string inputValue1, string inputName2, string inputValue2, string inputName3, string inputValue3);
        ResultDto AsString(string methodName, string inputName1, string inputValue1, string inputName2, string inputValue2, string inputName3, string inputValue3, string inputName4, string inputValue4);

        ResultDto ParseAsFloat(string inputName, string inputValue);
        ResultDto ParseAsFloat(string methodName, string inputName1, string inputValue1, string inputName2, string inputValue2);

        ResultDto ParseAsDouble(string inputName, string inputValue);

        ResultDto ParseAsInteger(string inputName, string inputValue);
    }
}