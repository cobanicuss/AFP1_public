using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;
using Spm.File.Watcher.Service;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapSizeOneTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of MapSizeOne";
        private ResultDto _dto;
        private Dictionary<SizeOneTestDto, string> _input;
        private Dictionary<SizeOneTestDto, ResultDto> _output;

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
            .When(_ => CorrectInputParamertsAreProvidedForSec1ForSizeOne())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => Sec1IsRoundedCorrectly(_dto))

            .BDDfy();
        }

        [Test]
        public void Dsc1ValueShouldBeRoundedCorrectlyWhenReturned()
        {
            this.Given(Scenario)
            .When(_ => CorrectInputParamertsAreProvidedForDsc1ForSizeOne())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => Dsc1IsRoundedCorrectly(_dto))

            .BDDfy();
        }

        private void DifferentCombinationsOfInputParamtersArePassedIn()
        {
            _output = new Dictionary<SizeOneTestDto, ResultDto>();

            Dsc1 = $"1234.45{Constants.Dsc1SearchString}";
            const string dsc1Stripped = "1234.45";

            _input = new Dictionary<SizeOneTestDto, string>
            {
                {new SizeOneTestDto{Id = 1, Mcu = Constants.OsulivansBeach, Sec1 = "2.12",  Dsc1 = Dsc1},   "2.12"},
                {new SizeOneTestDto{Id = 2, Mcu = Constants.OsulivansBeach, Sec1 = "2.12",  Dsc1 = ""},     "2.12"},
                {new SizeOneTestDto{Id = 3, Mcu = Constants.Salisbury,      Sec1 = "2.12",  Dsc1 = Dsc1},   dsc1Stripped},
                {new SizeOneTestDto{Id = 4, Mcu = Constants.Salisbury,      Sec1 = "2.12",  Dsc1 = ""},     "2.12"}
            };
        }

        protected override void ExecutingMapping()
        {
            _dto = null;
            _dto = ClassUnderTest.MapSizeOne(Mcu, Sec1, Dsc1);
        }

        private void ExecutingMappingForAllInputs()
        {
            foreach (var item in _input)
            {
                _dto = ClassUnderTest.MapSizeOne(item.Key.Mcu, item.Key.Sec1, item.Key.Dsc1);

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

    public class SizeOneTestDto
    {
        public int Id { get; set; }
        public string Mcu { get; set; }
        public string Sec1 { get; set; }
        public string Dsc1 { get; set; }
    }
}