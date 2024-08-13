namespace Spm.File.Watcher.Service.Validation
{
    public interface IDisplayErrors
    {
        string CannotFind(string methodName, string name1, string value1);

        string ProblemWith(string name1, string value1);
        string ProblemWith(string methodName, string name1, string value1);
        string ProblemWith(string methodName, string name1, string value1, string name2, string value2);
        string ProblemWith(string methodName, string name1, string value1, string name2, string value2, string name3, string value3);
        string ProblemWith(string methodName, string name1, string value1, string name2, string value2, string name3, string value3, string name4, string value4);

        string CannotParse(string name1, string value1);
        string CannotParse(string methodName, string name1, string value1);
        string CannotParse(string methodName, string name1, string value1, string name2, string value2);
    }
}