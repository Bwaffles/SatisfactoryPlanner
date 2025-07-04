﻿using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration.Processing.Inbox;
using SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration.Processing.InternalCommands;
using Serilog;
using System.Collections.Specialized;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration.Quartz
{
    internal static class QuartzStartup
    {
        private static IScheduler _scheduler = null!;

        internal static void Initialize(ILogger logger, TimeSpan internalProcessingExecutionInterval)
        {
            logger.Information("Quartz starting...");

            _scheduler = StartScheduler(logger);

            //ScheduleProcessOutboxJob(internalProcessingExecutionInterval);
            ScheduleProcessInboxJob(internalProcessingExecutionInterval);
            ScheduleProcessInternalCommandsJob(internalProcessingExecutionInterval);

            logger.Information("Quartz started.");
        }

        internal static void Shutdown() => _scheduler?.Shutdown().Wait();

        private static IScheduler StartScheduler(ILogger logger)
        {
            LogProvider.SetCurrentLogProvider(new SerilogLogProvider(logger));

            var schedulerConfiguration = new NameValueCollection
            {
                {
                    "quartz.scheduler.instanceName", "SatisfactoryPlanner.Warehouses"
                }
            };

            var schedulerFactory = new StdSchedulerFactory(schedulerConfiguration);
            var scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            scheduler.Start().GetAwaiter().GetResult();

            return scheduler;
        }

        private static void ScheduleProcessInternalCommandsJob(TimeSpan internalProcessingExecutionInterval)
        {
            var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();
            var triggerCommandsProcessing =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithSimpleSchedule(scheduleBuilder => scheduleBuilder
                        .WithInterval(internalProcessingExecutionInterval)
                        .RepeatForever())
                    .Build();

            _scheduler
                .ScheduleJob(processInternalCommandsJob, triggerCommandsProcessing)
                .GetAwaiter().GetResult();
        }

        private static void ScheduleProcessInboxJob(TimeSpan internalProcessingExecutionInterval)
        {
            var processInboxJob = JobBuilder.Create<ProcessInboxJob>().Build();
            var processInboxTrigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithSimpleSchedule(scheduleBuilder => scheduleBuilder
                        .WithInterval(internalProcessingExecutionInterval)
                        .RepeatForever())
                    .Build();

            _scheduler
                .ScheduleJob(processInboxJob, processInboxTrigger)
                .GetAwaiter().GetResult();
        }

        //private static void ScheduleProcessOutboxJob(TimeSpan internalProcessingExecutionInterval)
        //{
        //    var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
        //    var trigger =
        //        TriggerBuilder
        //            .Create()
        //            .StartNow()
        //            .WithSimpleSchedule(scheduleBuilder => scheduleBuilder
        //                .WithInterval(internalProcessingExecutionInterval)
        //                .RepeatForever())
        //            .Build();

        //    _scheduler
        //        .ScheduleJob(processOutboxJob, trigger)
        //        .GetAwaiter().GetResult();
        //}
    }
}