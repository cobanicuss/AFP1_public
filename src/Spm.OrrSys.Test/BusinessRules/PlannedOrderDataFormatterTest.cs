using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spm.OrrSys.Service.Domain;
using Spm.OrrSys.Service.Repositories;
using Spm.Shared;
using TestStack.BDDfy;

namespace Spm.OrrSys.Test.BusinessRules
{
    [TestFixture]
    public class PlannedOrderDataFormatterTest
    {
        private IFormatPlannedOrderData _classUnderTest;

        private IList<object[]> _readingOpenPlannedOrders;
        private IEnumerable<OrrSysF3460Z1> _castPlannedOrders;

        private IList<OrrSysF3460Z1> _openPlannedOrders;
        private IList<OrrSysF3460Z1> _groupedByPlannedOrders;

        private IList<OrrSysF3460Z1> _openPlannedOrdersWithNoLineNumbers;
        private IList<OrrSysF3460Z1> _openPlannedOrdersWithLineNumbers;

        private const string SomeStringValue = "a";
        private const double SomeDoubleValue = 1.1d;
        private const decimal SomeDecimalValue = 1.1m;
        private readonly decimal _todaysDateInJulian = ConvertDate.DateToJdeDate(DateTime.Today);

        private const int RowNumbersAreZero = 0;

        private const string Ftedbt = SomeStringValue;
        private const string Ftedtn = SomeStringValue;
        private const decimal Fteddt = SomeDecimalValue;

        private const string GroupByLitm = "litm1";
        private const string GroupByMcu = "mcu1";
        private const decimal GroupByDrqj = 111.11m;

        private const double SumUorg = 1.0d;
        private const double SumFqt = 2.0d;

        private const int IterationCount = 3;

        [Test]
        public void OpenPlannedOrdersShouldBeGroupedByCorrectly()
        {
            this.Given(_ => PlannedOrdersNeedsToBeCreated())
           .When(_ => PassingInOpenPlannedOrders())
                .And(_ => ExecutingGroupByQuery())
           .Then(_ => PlannedOrdersMustBeGroupedByCorrectly())
           .BDDfy();
        }

        [Test]
        public void ReadingDataForOpenPlannedOrdersShouldBeCastCorrectlyToList()
        {
            this.Given(_ => PlannedOrdersNeedsToBeCreated())
           .When(_ => ReadingOpenPlannedOrderData())
                .And(_ => CastingDataToList())
           .Then(_ => CastingMustBeDoneCorrectly())
           .BDDfy();
        }

        [Test]
        public void LineNumbersShouldIncrementInThousands()
        {
            this.Given(_ => PlannedOrdersNeedsToBeCreated())
           .When(_ => PassingInOpenPlannedOrdersWithNoLineNumbers())
                .And(_ => InsertingNewLineNumbers())
           .Then(_ => LineNumbersMustBeCorrect())
           .BDDfy();
        }

        private void PassingInOpenPlannedOrdersWithNoLineNumbers()
        {
            _openPlannedOrdersWithNoLineNumbers = CreateNewOpenPlannedOrders();
        }

        private void ReadingOpenPlannedOrderData()
        {
            _readingOpenPlannedOrders = new List<object[]>();

            for (var i = 0; i < IterationCount; i++)
            {
                var objectType = new object[]
                {
                    SomeStringValue, 
                    SomeStringValue, 
                    SomeStringValue, 
                    SomeStringValue, 
                    SomeStringValue, 
                    SomeDoubleValue, 
                    SomeStringValue,
                    SomeStringValue, 
                    SomeStringValue, 
                    _todaysDateInJulian, 
                    SomeDoubleValue, 
                    SomeDoubleValue, 
                    SomeStringValue, 
                    SomeStringValue
                };

                _readingOpenPlannedOrders.Add(objectType);
            }
        }

        private void PlannedOrdersNeedsToBeCreated()
        {
            _classUnderTest = new FormatPlannedOrderData();
        }

        private void PassingInOpenPlannedOrders()
        {
            _groupedByPlannedOrders = new List<OrrSysF3460Z1>();

            _openPlannedOrders = CreateNewOpenPlannedOrders();

            AddDuplicateDataToListToTestGroupByQuery();
        }

        private static List<OrrSysF3460Z1> CreateNewOpenPlannedOrders()
        {
            return new List<OrrSysF3460Z1>
            {
                new OrrSysF3460Z1
                {
                    FTEDUS = "aa",
                    FTEDBT = "bb",
                    FTEDTN = "cc",
                    FTEDLN = 0,
                    FTTYTN = "dd",
                    FTEDDT = 111.1m,
                    FTDRIN = "ee",
                    FTEDSP = "ff",
                    FTTNAC = "gg",
                    FTITM  = 222.2d,
                    FTLITM = "litm2",   
                    FTAITM = "hh",
                    FTMCU  = "mcu2",    
                    FTDRQJ = 222.22m,   
                    FTUORG = 1.0d,      
                    FTFQT  = 1.0d,      
                    FTTYPF = "ii",
                    FTDCTO = "jj"
                },
                new OrrSysF3460Z1
                {
                    FTEDUS = "aa",
                    FTEDBT = "bb",
                    FTEDTN = "cc",
                    FTEDLN = 0,
                    FTTYTN = "dd",
                    FTEDDT = 111.1m,
                    FTDRIN = "ee",
                    FTEDSP = "ff",
                    FTTNAC = "gg",
                    FTITM  = 222.2d,
                    FTLITM = "litm3",   
                    FTAITM = "hh",
                    FTMCU  = "mcu3",    
                    FTDRQJ = 333.33m,   
                    FTUORG = 1.0d,      
                    FTFQT  = 1.0d,     
                    FTTYPF = "ii",
                    FTDCTO = "jj"
                },
                new OrrSysF3460Z1
                {
                    FTEDUS = "aa",
                    FTEDBT = "bb",
                    FTEDTN = "cc",
                    FTEDLN = 0,
                    FTTYTN = "dd",
                    FTEDDT = 111.1m,
                    FTDRIN = "ee",
                    FTEDSP = "ff",
                    FTTNAC = "gg",
                    FTITM  = 222.2d,
                    FTLITM = "litm4",   
                    FTAITM = "hh",
                    FTMCU  = "mcu4",    
                    FTDRQJ = 444.44m,   
                    FTUORG = 1.0d,      
                    FTFQT  = 1.0d,     
                    FTTYPF = "ii",
                    FTDCTO = "jj"
                }
            };
        }

        private void AddDuplicateDataToListToTestGroupByQuery()
        {
            for (var i = 0; i < IterationCount; i++)
            {
                var duplicateItem = new OrrSysF3460Z1
                {
                    FTEDUS = "aa",
                    FTEDBT = "bb",
                    FTEDTN = "cc",
                    FTEDLN = 0,
                    FTTYTN = "dd",
                    FTEDDT = 111.1m,
                    FTDRIN = "ee",
                    FTEDSP = "ff",
                    FTTNAC = "gg",
                    FTITM = 222.2d,
                    FTLITM = GroupByLitm,
                    FTAITM = "hh",
                    FTMCU = GroupByMcu,
                    FTDRQJ = GroupByDrqj,
                    FTUORG = SumUorg,
                    FTFQT = SumFqt,
                    FTTYPF = "ii",
                    FTDCTO = "jj"
                };

                _openPlannedOrders.Add(duplicateItem);
            }
        }

        private void ExecutingGroupByQuery()
        {
            _groupedByPlannedOrders = _classUnderTest.ExecuteGroupByQuery(_openPlannedOrders);
        }

        private void CastingDataToList()
        {
            _castPlannedOrders = _classUnderTest.CastObjectToOrrSysF3460Z1(Ftedbt, Ftedtn, Fteddt, _readingOpenPlannedOrders);
        }

        private void InsertingNewLineNumbers()
        {
            _openPlannedOrdersWithLineNumbers = _classUnderTest.InsertLineNumbers(_openPlannedOrdersWithNoLineNumbers);
        }

        private void CastingMustBeDoneCorrectly()
        {
            foreach (var item in _castPlannedOrders)
            {
                Assert.AreEqual(item.FTEDUS, SomeStringValue);
                Assert.AreEqual(item.FTEDBT, SomeStringValue);
                Assert.AreEqual(item.FTEDTN, SomeStringValue);
                Assert.AreEqual(item.FTEDLN, RowNumbersAreZero);
                Assert.AreEqual(item.FTTYTN, SomeStringValue);
                Assert.AreEqual(item.FTEDDT, SomeDecimalValue);
                Assert.AreEqual(item.FTDRIN, SomeStringValue);
                Assert.AreEqual(item.FTEDSP, SomeStringValue);
                Assert.AreEqual(item.FTTNAC, SomeStringValue);
                Assert.AreEqual(item.FTITM, SomeDoubleValue);
                Assert.AreEqual(item.FTLITM, SomeStringValue);
                Assert.AreEqual(item.FTAITM, SomeStringValue);
                Assert.AreEqual(item.FTMCU, SomeStringValue);
                Assert.AreEqual(item.FTDRQJ, _todaysDateInJulian); //Must be the same for Today's date//
                Assert.AreEqual(item.FTUORG, SomeDoubleValue);
                Assert.AreEqual(item.FTFQT, SomeDoubleValue);
                Assert.AreEqual(item.FTTYPF, SomeStringValue);
                Assert.AreEqual(item.FTDCTO, SomeStringValue);
            }
        }

        private void PlannedOrdersMustBeGroupedByCorrectly()
        {
            var groupByCount = _openPlannedOrders.Count - IterationCount + 1;
            const double sumOfUorg = SumUorg * IterationCount;
            const double sumOfFqt = SumFqt * IterationCount;

            var currentGroupedByItem = _groupedByPlannedOrders.First(x => string.Equals(x.FTLITM, GroupByLitm));
            var currentValueOfUorg = currentGroupedByItem.FTUORG;
            var currentValueOfFqt = currentGroupedByItem.FTFQT;

            Assert.AreNotEqual(_groupedByPlannedOrders.Count, _openPlannedOrders.Count);
            Assert.AreEqual(_groupedByPlannedOrders.Count, groupByCount);
            Assert.AreEqual(sumOfUorg, currentValueOfUorg);
            Assert.AreEqual(sumOfFqt, currentValueOfFqt);
        }

        private void LineNumbersMustBeCorrect()
        {
            var orderedByLineNr = _openPlannedOrdersWithLineNumbers.OrderBy(x => x.FTEDLN);

            var reference = 0;

            foreach (var item in orderedByLineNr)
            {
                if (reference == 0)
                {
                    Assert.AreEqual(item.FTEDLN, 1000);
                    reference = item.FTEDLN;

                    continue;
                }

                var delta = item.FTEDLN - reference;
                Assert.AreEqual(delta, 1000);

                reference = item.FTEDLN;
            }
        }
    }
}