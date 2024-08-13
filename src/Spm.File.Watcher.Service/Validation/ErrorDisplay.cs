namespace Spm.File.Watcher.Service.Validation
{
    public class ErrorDisplay : IDisplayErrors
    {
        public string CannotFind(string methodName, string name1, string value1)
        {
            return $"Cannot find {name1}='{value1}', in {methodName}. Cannot proceed!";
        }

        public string ProblemWith(string name1, string value1)
        {
            return $"There is a problem with {name1}='{value1}'. Cannot proceed!!!!";
        }

        public string ProblemWith(string methodName, string name1, string value1)
        {
            return $"There is a problem with {name1}='{value1}' when dealing with {methodName}. Cannot proceed!!!!";
        }

        public string ProblemWith(string methodName, 
            string name1, string value1, 
            string name2, string value2)
        {
            return $"There are problems with one or more of these: {name1}='{value1}', {name2}='{value2}' when dealing with {methodName}. Cannot proceed!!!!";
        }

        public string ProblemWith(string methodName, 
            string name1, string value1, 
            string name2, string value2,
            string name3, string value3)
        {
            return $"There are problems with one or more of these: {name1}='{value1}', {name2}='{value2}', {name3}='{value3}' when dealing with {methodName}. Cannot proceed!!!!";
        }

        public string ProblemWith(string methodName, 
            string name1, string value1, 
            string name2, string value2, 
            string name3, string value3, 
            string name4, string value4)
        {
            return $"There are problems with one or more of these: {name1}='{value1}', {name2}='{value2}', {name3}='{value3}', {name4}='{value4}' when dealing with {methodName}. Cannot proceed!!!!";
        }

        public string CannotParse(string name1, string value1)
        {
            return $"Cannot parse to type: {name1}='{value1}'. Cannot proceed!!!!";
        }

        public string CannotParse(string methodName, string name1, string value1)
        {
            return $"Cannot parse to type: {name1}='{value1}' when dealing with {methodName}. Cannot proceed!!!!";
        }

        public string CannotParse(string methodName, string name1, string value1, string name2, string value2)
        {
            return $"Cannot parse one or more to type: {name1}='{value1}', {name2}='{value2}' when dealing with {methodName}. Cannot proceed!!!!";
        }
    }
}