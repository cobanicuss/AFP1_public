using System;
using Moq;
using NUnit.Framework;
using Spm.OrrSys.Service;
using Spm.OrrSys.Service.Repositories;
using Spm.OrrSys.Service.Scheduler;
using TestStack.BDDfy;

namespace Spm.OrrSys.Test
{
    [TestFixture]
    public class ProductionOrderStatusTimeSequenceTest
    {
        private IScheduleProductionOrderStutus _classUnderTest;
        private Mock<IOrrSysProcessVariableRepository> _repository;
        private DateTime _sheduledRunDateTime;
        private DateTime _nextRunDateTime;
        private DateTime _nowDateTime;
        private uint _duration;

        [SetUp]
        public void Setup()
        {
            _nowDateTime = DateTime.Now;

            _repository = new Mock<IOrrSysProcessVariableRepository>();

            _classUnderTest = new ScheduleProductionOrderStutus(_repository.Object);
        }

        [Test]
        public void LongSequenceForWhenScheduledTimeIsInTheFuture()
        {
            this.Given(_ => GivenScheduledTimeIsInNearFuture())
                .When(_ => NextRunTimeIsCaculatedForLongSequence())
                .Then(_ => TodaysDateAndScheduledFutureTimeMustBeUsed())

                .BDDfy();
        }

        [Test]
        public void LongSequenceForWhenScheduledTimeIsInThePast()
        {
            this.Given(_ => GivenScheduledTimeIsInNearPast())
                .When(_ => NextRunTimeIsCaculatedForLongSequence())
                .Then(_ => TomorrowsDateAndScheduledFutureTimeMustBeUsed())

                .BDDfy();
        }

        [Test]
        public void ShortSequenceMustRunEveryXMinutes()
        {
            this.Given("Short sequence time period.")
                .When(_ => NextRunTimeIsCaculatedForShortSequence())
                .Then(_ => MustBeCorrectConstantValue())

                .BDDfy();
        }

        private void MustBeCorrectConstantValue()
        {
            Assert.AreEqual(_duration, Service.Constants.OrrSysSchedulerSendPeriodMinutes);
        }
        
        private void TomorrowsDateAndScheduledFutureTimeMustBeUsed()
        {
            Assert.AreEqual(_nextRunDateTime.Day,_nowDateTime.AddDays(1).Day);

            Assert.AreEqual(_nextRunDateTime.Year, _nowDateTime.AddDays(1).Year);
            Assert.AreEqual(_nextRunDateTime.Month, _nowDateTime.AddDays(1).Month);
            Assert.AreEqual(_nextRunDateTime.Hour, _sheduledRunDateTime.AddDays(1).Hour);
            Assert.AreEqual(_nextRunDateTime.Minute, _sheduledRunDateTime.AddDays(1).Minute);
            Assert.AreEqual(_nextRunDateTime.Minute, 0);
            Assert.AreEqual(_nextRunDateTime.Second, _sheduledRunDateTime.AddDays(1).Second);
            Assert.AreEqual(_nextRunDateTime.Second, 0);
        }

        private void GivenScheduledTimeIsInNearPast()
        {
            var pastHour = _nowDateTime.AddHours(-2).Hour;

            _sheduledRunDateTime = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, pastHour, 0, 0);

            _repository.Setup(x => x.RunProductinoOrderStutusOnTime()).Returns(_sheduledRunDateTime);
        }

        private void GivenScheduledTimeIsInNearFuture()
        {
            var futureHour = _nowDateTime.AddHours(2).Hour;

            _sheduledRunDateTime = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, futureHour, 0, 0);

            _repository.Setup(x => x.RunProductinoOrderStutusOnTime()).Returns(_sheduledRunDateTime);
        }

        private void TodaysDateAndScheduledFutureTimeMustBeUsed()
        {
            Assert.AreEqual(_nextRunDateTime.Day, _nowDateTime.Day);

            Assert.AreEqual(_nextRunDateTime.Year, _nowDateTime.Year);
            Assert.AreEqual(_nextRunDateTime.Month, _nowDateTime.Month);
            Assert.AreEqual(_nextRunDateTime.Hour, _sheduledRunDateTime.Hour);
            Assert.AreEqual(_nextRunDateTime.Minute, _sheduledRunDateTime.Minute);
            Assert.AreEqual(_nextRunDateTime.Minute, 0);
            Assert.AreEqual(_nextRunDateTime.Second, _sheduledRunDateTime.Second);
            Assert.AreEqual(_nextRunDateTime.Second, 0);
        }

        private void NextRunTimeIsCaculatedForLongSequence()
        {
            _nextRunDateTime = _classUnderTest.SetNextTimeoutForLongSequence();
        }

        private void NextRunTimeIsCaculatedForShortSequence()
        {
            _duration = _classUnderTest.SetNextTimeoutForShortSequence();
        }
    }
}