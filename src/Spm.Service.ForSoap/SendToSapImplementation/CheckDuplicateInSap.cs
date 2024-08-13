using System;

namespace Spm.Service.ForSoap.SendToSapImplementation
{
    public class DuplicateSetupInSap : IDuplicateSetupInSap
    {
        public string Setup(string sagaReferenceId, string queryString, string absoluteUri)
        {
            var sapDuplicateCheckVariable = Constants.DuplicateCheck;

            if (string.IsNullOrEmpty(queryString))
                sapDuplicateCheckVariable = sapDuplicateCheckVariable.Replace('&', '?');

            var currentUri = absoluteUri;

            if (currentUri.Contains(Constants.DuplicateCheck))
                currentUri = currentUri.Substring(0, currentUri.IndexOf(Constants.DuplicateCheck, StringComparison.InvariantCulture));

            if (currentUri.Contains(sapDuplicateCheckVariable))
                currentUri = currentUri.Substring(0, currentUri.IndexOf(sapDuplicateCheckVariable, StringComparison.InvariantCulture));

            var newUri = $"{currentUri}{sapDuplicateCheckVariable}{sagaReferenceId}";

            return newUri;
        }
    }
}