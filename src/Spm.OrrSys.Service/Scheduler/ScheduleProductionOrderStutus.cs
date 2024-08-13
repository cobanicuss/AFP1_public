using System;
using Spm.OrrSys.Service.Repositories;

namespace Spm.OrrSys.Service.Scheduler
{
    public interface IScheduleProductionOrderStutus
    {
        DateTime SetNextTimeoutForLongSequence();
        uint SetNextTimeoutForShortSequence();
    }

    public class ScheduleProductionOrderStutus : IScheduleProductionOrderStutus
    {
        private readonly IOrrSysProcessVariableRepository _processVariable;

        public ScheduleProductionOrderStutus(IOrrSysProcessVariableRepository processVariable)
        {
            _processVariable = processVariable;
        }

        public DateTime SetNextTimeoutForLongSequence()
        {
            var runAt = DateTime.Now;

            var nextRunTimeSpan = _processVariable.RunProductinoOrderStutusOnTime().TimeOfDay;
            var currentTimeSpan = runAt.TimeOfDay;

            var nextRunTimeHour = nextRunTimeSpan.Hours;
            var nextRunTimeMinutes = nextRunTimeSpan.Minutes;

            if (currentTimeSpan.Ticks > nextRunTimeSpan.Ticks) runAt = runAt.AddDays(1);

            var nextDateTimeToRun = new DateTime(
                runAt.Year,
                runAt.Month,
                runAt.Day,
                nextRunTimeHour,
                nextRunTimeMinutes,
                0, 
                DateTimeKind.Local);

            return nextDateTimeToRun;
        }

        public uint SetNextTimeoutForShortSequence()
        {
            /* Keeping ALL sheduler trigger-values together in ONE type as well as future proofing */
            const uint runEveryNMinutes = Constants.OrrSysSchedulerSendPeriodMinutes;

            return runEveryNMinutes;
        }
    }
}