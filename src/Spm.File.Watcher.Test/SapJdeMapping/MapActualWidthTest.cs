using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;
using Constants = Spm.File.Watcher.Service.Constants;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapActualWidthTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of ActualWidth";
        private ResultDto _dto;
        private Dictionary<ActualWidthTestDto, string> _input;
        private Dictionary<ActualWidthTestDto, ResultDto> _output;

        [Test]
        public void AllLogicalCombinationsOfInputParametersShouldReturnTheCorrectResult()
        {
            this.Given(Scenario)
            .When(_ => DifferentCombinationsOfInputParamtersArePassedIn())
                .And(_ => ExecutingMappingForAllInputs())
            .Then(_ => AllMappingIsGood())
                .And(_ => CorrectValuesAreReturned())

            .BDDfy();
        }

        [Test]
        public void Sec1ValueShouldBeRoundedCorrectlyWhenReturned()
        {
            this.Given(Scenario)
            .When(_ => CorrectInputParamertsAreProvidedForSec1ForActualWidth())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => Sec1IsRoundedCorrectly(_dto))

            .BDDfy();
        }

        [Test]
        public void Sec2ValueShouldBeRoundedCorrectlyWhenReturned()
        {
            this.Given(Scenario)
            .When(_ => CorrectInputParamertsAreProvidedForSec2())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => Sec2IsRoundedCorrectly(_dto))

            .BDDfy();
        }

        private void DifferentCombinationsOfInputParamtersArePassedIn()
        {
            _output = new Dictionary<ActualWidthTestDto, ResultDto>();

            _input = new Dictionary<ActualWidthTestDto, string>
            {
                {new ActualWidthTestDto{Id = 1, Mcu = Constants.OsulivansBeach, Sec2 = string.Empty,   Sec1 = "2.12"},  "2.12"},
                {new ActualWidthTestDto{Id = 2, Mcu = Constants.OsulivansBeach, Sec2 = "1.12",         Sec1 = "2.12"},  "1.12"},
                {new ActualWidthTestDto{Id = 5, Mcu = Constants.Salisbury,      Sec2 = string.Empty,   Sec1 = "2.12"},  "2.12"},
                {new ActualWidthTestDto{Id = 7, Mcu = Constants.Salisbury,      Sec2 = "1.12",         Sec1 = "2.12"},  "1.12"}
            };
        }

        protected override void ExecutingMapping()
        {
            _dto = null;
            _dto = ClassUnderTest.MapActualWidth(Mcu, Sec1, Sec2);
        }

        private void ExecutingMappingForAllInputs()
        {
            foreach (var item in _input)
            {
                _dto = ClassUnderTest.MapActualWidth(item.Key.Mcu, item.Key.Sec1, item.Key.Sec2);

                _output.Add(item.Key, _dto);
            }
        }

        private void AllMappingIsGood()
        {
            foreach (var item in _output)
            {
                Assert.IsTrue(item.Value.IsOk);
            }
        }

        private void CorrectValuesAreReturned()
        {
            foreach (var item in _input)
            {
                var output = _output.First(x => x.Key.Id == item.Key.Id).Value.Output;

                Assert.AreEqual(item.Value, output);
            }
        }
    }

    public class ActualWidthTestDto
    {
        public int Id { get; set; }
        public string Mcu { get; set; }
        public string Sec1 { get; set; }
        public string Sec2 { get; set; }
    }
}