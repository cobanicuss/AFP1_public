using System;
using System.Collections.Generic;
using System.IO;
using Moq;
using Spm.File.Watcher.Service.Downloader;

namespace Spm.File.Watcher.Test.FileIo
{
    public abstract class FileDataTestBase
    {
        protected const string Path = @"C:\";
        
        protected abstract string TestFileName { get;}
        protected abstract int Columns { get;}

        protected Mock<IWorkWithFiles> FileManipulator;

        private string FullFilePath => $@"{Path}{TestFileName}";

        protected string[] Data;
        
        public abstract void DataShouldBeExtractedFromCorrecltyFormattedFile();
        public abstract void DataShouldBeEmptyForEmptyFile();
        public abstract void DtoShouldBeEmptyForFileWithOnlyEmptyStringValues();
        public abstract void AddedEndCommaInFileLineShouldBeIgnored();
        public abstract void WrongColumnsInFileMustCreateErrorCondition();
        
        protected abstract void ExtractingDataFromFile();
        protected abstract void DataMustContainDataFromFile();
        protected abstract void DataMustContainItemWithAllEmptyString();
        protected abstract void MethodUnderTestWhichThrowsException();
        protected abstract void DataMustBeEmptyButNotNull();
        protected abstract void DataMustBeNull();

        protected void ErrorFileMustBeCreated()
        {
            FileManipulator.Verify(x => x.CreateErrorFileForIssue(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        }

        protected void CorrectlyFormatedFile()
        {
            if (System.IO.File.Exists(FullFilePath)) System.IO.File.Delete(FullFilePath);

            using (var fs = new StreamWriter(FullFilePath))
            {
                var heading = CreateHeadingLine();
                var data = CreateDataLine();

                fs.WriteLine(heading);
                fs.WriteLine(data);
                fs.Flush();
            }
        }

        protected void FileWithExtraCommaAtEndOfLine()
        {
            if (System.IO.File.Exists(FullFilePath)) System.IO.File.Delete(FullFilePath);

            using (var fs = new StreamWriter(FullFilePath))
            {
                var heading = CreateHeadingLine();
                var data = CreateDataLine();
                data = data + ",";

                fs.WriteLine(heading);
                fs.WriteLine(data);
                fs.Flush();
            }
        }

        protected void FileWithLessColumns()
        {
            if (System.IO.File.Exists(FullFilePath)) System.IO.File.Delete(FullFilePath);

            using (var fs = new StreamWriter(FullFilePath))
            {
                var heading = CreateHeadingLine();
                var data = CreateDataLineWithLessColunms();

                fs.WriteLine(heading);
                fs.WriteLine(data);
                fs.Flush();
            }
        }

        protected void FileWithHeadingOnlyAndNoData()
        {
            if (System.IO.File.Exists(FullFilePath)) System.IO.File.Delete(FullFilePath);

            using (var fs = new StreamWriter(FullFilePath))
            {
                var heading = CreateHeadingLine();

                fs.WriteLine(heading);
                fs.Flush();
            }
        }

        protected void DataIsEmptyString()
        {
            if (System.IO.File.Exists(FullFilePath)) System.IO.File.Delete(FullFilePath);

            using (var fs = new StreamWriter(FullFilePath))
            {
                var heading = CreateHeadingLine();
                var data = CreateEmptyStringDataLine();

                fs.WriteLine(heading);
                fs.WriteLine(data);
                fs.Flush();
            }
        }

        protected void RemoveTestFile()
        {
            if (System.IO.File.Exists(FullFilePath)) System.IO.File.Delete(FullFilePath);
        }

        protected Exception ExecuteActionThatThrows(Action action)
        {
            Exception exception = null;
            try
            {
                action();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return exception;
        }

        private string CreateHeadingLine()
        {
            var heading = new List<string>();

            for (var i = 0; i < Columns; i++)
            {
                heading.Add($"h{i + 1}");
            }

            return string.Join(",", heading.ToArray());
        }

        private string CreateDataLine()
        {
            var heading = new List<string>();

            for (var i = 0; i < Columns; i++)
            {
                heading.Add($"{i + 1}");
            }

            Data = heading.ToArray();

            return string.Join(",", Data);
        }

        private string CreateDataLineWithLessColunms()
        {
            var heading = new List<string>();

            for (var i = 0; i < (Columns - 2); i++)
            {
                heading.Add($"{i + 1}");
            }

            Data = heading.ToArray();

            return string.Join(",", Data);
        }

        private string CreateEmptyStringDataLine()
        {
            var heading = new List<string>();

            for (var i = 0; i < Columns; i++)
            {
                heading.Add(string.Empty);
            }

            Data = heading.ToArray();

            return string.Join(",", Data);
        }
    }
}