using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spm.File.Watcher.Service.JdeToSapMapping;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.Convert
{
    [TestFixture]
    public class ConvertCecimalTest
    {
        private const string Scenario = "Jde fractional value";
        private IConvertDecimal _convertDecimal;

        private readonly Dictionary<double, int> _ioCheck = new Dictionary<double, int>
        {
            {0.0000001d   ,1},
            {0.000001d    ,1},
            {0.00001d     ,99999},
            {0.0001d      ,10000},
            {0.001d       ,1000},
            {0.01d        ,100},
            {0.1d         ,10},
            {0.09d        ,100},
            {0.99d        ,100},
            {0.999d       ,1000},
            {0.9999d      ,10000},
            {0.99999d     ,1},
            {0.999999d    ,1},
            {0d           ,1},
            {9d           ,1},
            {99d          ,1},
            {999d         ,1},
            {9999d        ,1},
            {99998d       ,1},
            {99999d       ,0},
            {100000d      ,0},
            {99998.1d     ,1},
            {99998.99d    ,1},
            {99998.999d   ,1},
            {99998.9999d  ,1},
            {99998.99999d ,1},
            {99998.999999d,1},
            {743.545d     ,11},
            {1234.1234d   ,81},
            {12345.1234d  ,8},
            {12345.1299d  ,8},
            {12345.12345d ,8},
            {47.45d       ,20},
            {0.3d         ,10},
            {0.3333d      ,10000},
            {0.33333d     ,3},
            {0.25d        ,4},
            {0.75d        ,4},
            {123.123d     ,187}
        };

        private readonly Dictionary<double, int> _output = new Dictionary<double, int>();

        [Test]
        public void CorrectDenominatorShouldBeReturnOnFractionalInput()
        {
            this.Given(Scenario)
            .When(_ => FractionIsPassedIn())
                .And(_ => CalculatingDenominator())
            .Then(_ => CorrectDenominotorShouldBeReturned())

            .BDDfy();
        }

        private void FractionIsPassedIn()
        {
            _convertDecimal = new Service.JdeToSapMapping.ConvertDecimal();
        }

        private void CalculatingDenominator()
        {
            foreach (var item in _ioCheck)
            {
                var thisOutput = _convertDecimal.UpscaleDenominator(item.Key);
                _output.Add(item.Key, thisOutput);

                Console.WriteLine($"=> Input={item.Key:0.########},Output-Denom={thisOutput},Num={item.Key*thisOutput:0.########}");
            }
        }

        private void CorrectDenominotorShouldBeReturned()
        {
            foreach (var item in _ioCheck)
            {
                Assert.AreEqual(item.Value, _output.FirstOrDefault(x => x.Key.Equals(item.Key)).Value);
                Assert.AreEqual(item.Value, _output.FirstOrDefault(x => x.Key.Equals(item.Key)).Value);
            }
        }
    }
}