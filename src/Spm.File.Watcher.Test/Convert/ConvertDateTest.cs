using System;
using NUnit.Framework;
using Spm.File.Watcher.Service;
using TestStack.BDDfy;
using Spm.File.Watcher.Service.JdeToSapMapping;

namespace Spm.File.Watcher.Test.Convert
{
    [TestFixture]
    public class ConvertDateTest
    {
        private IConvertDate _classUnderTest;

        private string _input;
        private string _output;

        private const string FourDigitYear = "dd/MM/yyyy";

        [Test]
        public void ValidDateBiggerThanOrEqualToTodaysDateWith2DigitYearMustBeConvertedToBiggerSapDateCorrectly()
        {
            this.Given(_ => ValidDotNetDateIsProvided())
                    .And(_ => InputDateWithTwoDigitYearIsBiggerThanTodaysDate())
                .When(_ => ConvertDateForSapToTodayIfSmallerElseBigger())
                .Then(_ => InputDateIsConvertedToEquivalentInputSapDateFormat())

            .BDDfy();
        }

        [Test]
        public void ValidDateSmallerThanTodaysDateWith2DigitYearMustBeConvertedToTodaysSapDate()
        {
            this.Given(_ => ValidDotNetDateIsProvided())
                    .And(_ => InputDateWithTwoDigitYearIsSmallerThanTodaysDate())
                .When(_ => ConvertDateForSapToTodayIfSmallerElseBigger())
                .Then(_ => InputDateIsConvertedToTodaysSapDateFormat())

            .BDDfy();
        }
        
        [Test]
        public void ValidDateBiggerThanOrEqualToTodaysDateWith4DigitYearMustBeConvertedToBiggerSapDateCorrectly()
        {
            this.Given(_ => ValidDotNetDateIsProvided())
                    .And(_ => InputDateWith4DigitYearIsBiggerThanTodaysDate())
                .When(_ => ConvertDateForSapToTodayIfSmallerElseBigger())
                .Then(_ => InputDateIsConvertedToEquivalentInputSapDateFormat())

            .BDDfy();
        }

        [Test]
        public void ValidDateSmallerThanTodaysDateWith4DigitYearMustBeConvertedToTodaysSapDate()
        {
            this.Given(_ => ValidDotNetDateIsProvided())
                    .And(_ => InputDateWith4DigitYearIsSmallerThanTodaysDate())
                .When(_ => ConvertDateForSapToTodayIfSmallerElseBigger())
                .Then(_ => InputDateIsConvertedToTodaysSapDateFormat())

            .BDDfy();
        }

        [Test]
        public void ValidDateWith4DititYearMustBeConvertedToSapDate()
        {
            this.Given(_ => ValidDotNetDateIsProvided())
                    .And(_ => InputIsAnyDateWith4DigitYear())
                .When(_ => ConvertDateForSap())
                .Then(_ => InputDateIsConvertedToEquivalentInputSapDateFormat())

            .BDDfy();
        }

        [Test]
        public void ValidDateWith2DititYearMustBeConvertedToSapDate()
        {
            this.Given(_ => ValidDotNetDateIsProvided())
                    .And(_ => InputIsAnyDateWith2DigitYear())
                .When(_ => ConvertDateForSap())
                .Then(_ => InputDateIsConvertedToEquivalentInputSapDateFormat())

            .BDDfy();
        }

        private void InputDateIsConvertedToTodaysSapDateFormat()
        {
            var sapExpecedOutput = DateTime.Today.ToString(Constants.SapDateFormat);

            Assert.AreEqual(_output, sapExpecedOutput);
        }

        private void InputDateIsConvertedToEquivalentInputSapDateFormat()
        {
            var inputDate = DateTime.Parse(_input);

            var sapExpecedOutput = inputDate.ToString(Constants.SapDateFormat);

            Assert.AreEqual(_output, sapExpecedOutput);
        }

        private void ConvertDateForSapToTodayIfSmallerElseBigger()
        {
            _output = _classUnderTest.ConvertDateForSapToTodayIfSmaller(_input);
        }

        private void ConvertDateForSap()
        {
            _output = _classUnderTest.ConvertDateForSap(_input);
        }

        private void InputIsAnyDateWith2DigitYear()
        {
            _input = DateTime.Today.AddDays(-2).ToString(Constants.JdeExtractFileDateFormat);
        }

        private void InputDateWithTwoDigitYearIsSmallerThanTodaysDate()
        {
            _input = DateTime.Today.AddDays(-5).ToString(Constants.JdeExtractFileDateFormat);
        }

        private void InputDateWithTwoDigitYearIsBiggerThanTodaysDate()
        {
            _input = DateTime.Today.AddDays(5).ToString(Constants.JdeExtractFileDateFormat);
        }

        private void InputIsAnyDateWith4DigitYear()
        {
            _input = DateTime.Today.AddDays(-2).ToString(FourDigitYear);
        }

        private void InputDateWith4DigitYearIsBiggerThanTodaysDate()
        {
            _input = DateTime.Today.AddDays(5).ToString(FourDigitYear);
        }

        private void InputDateWith4DigitYearIsSmallerThanTodaysDate()
        {
            _input = DateTime.Today.AddDays(-5).ToString(FourDigitYear);
        }

        private void ValidDotNetDateIsProvided()
        {
            _classUnderTest = new ConvertDate();
        }
    }
}