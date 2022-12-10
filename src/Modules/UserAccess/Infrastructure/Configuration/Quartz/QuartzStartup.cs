using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Processing.Inbox;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Processing.InternalCommands;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox;
using Serilog;
using System.Collections.Specialized;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Quartz
{
    internal static class QuartzStartup
    {
        private static IScheduler _scheduler = null!;

        internal static void Initialize(ILogger logger)
        {
            logger.Information("Quartz starting...");

            _scheduler = StartScheduler(logger);

            ScheduleProcessOutboxJob();
            ScheduleProcessInboxJob();
            ScheduleProcessInternalCommandsJob();

            logger.Information("Quartz started.");
        }

        internal static void Shutdown() => _scheduler.Shutdown();

        private static IScheduler StartScheduler(ILogger logger)
        {
            var schedulerConfiguration = new NameValueCollection
            {
                {
                    "quartz.scheduler.instanceName", "SatisfactoryPlanner"
                }
            };

            var schedulerFactory = new StdSchedulerFactory(schedulerConfiguration);
            var scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            LogProvider.SetCurrentLogProvider(new SerilogLogProvider(logger));

            scheduler.Start().GetAwaiter().GetResult();

            return scheduler;
        }

        private static void ScheduleProcessInternalCommandsJob()
        {
            var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();
            var triggerCommandsProcessing =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/2 * * ? * *")
                    .Build();

            _scheduler
                .ScheduleJob(processInternalCommandsJob, triggerCommandsProcessing)
                .GetAwaiter().GetResult();
        }

        private static void ScheduleProcessInboxJob()
        {
            var processInboxJob = JobBuilder.Create<ProcessInboxJob>().Build();
            var processInboxTrigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/2 * * ? * *")
                    .Build();

            _scheduler
                .ScheduleJob(processInboxJob, processInboxTrigger)
                .GetAwaiter().GetResult();
        }

        private static void ScheduleProcessOutboxJob()
        {
            var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
            var trigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/2 * * ? * *")
                    .Build();

            _scheduler
                .ScheduleJob(processOutboxJob, trigger)
                .GetAwaiter().GetResult();
        }
    }
}