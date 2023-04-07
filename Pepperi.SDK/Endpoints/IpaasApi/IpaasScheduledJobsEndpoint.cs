using Pepperi.SDK.Contracts;
using Pepperi.SDK.Helpers;
using Pepperi.SDK.Model.Fixed.Generic;
using Pepperi.SDK.Model.Ipaas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pepperi.SDK.Endpoints.ipaasApi
{
    public class IpaasScheduledJobsEndpoint
    {
        #region Properties

        private string IpaasBaseUri { get; set; }
        private IpaasAddonAuthentification IpaasAuth { get; set; }
        private ILogger Logger { get; set; }
        private IpaasHttpClient IpaasHttpClient { get; set; }

        #endregion

        #region Constructor
        internal IpaasScheduledJobsEndpoint(string ipaasBaseUrl, ILogger logger, IpaasAddonAuthentification IpaasAuth)
        {
            this.IpaasBaseUri = ipaasBaseUrl;
            this.Logger = logger;
            this.IpaasAuth = IpaasAuth;

            this.IpaasHttpClient = new IpaasHttpClient(ipaasBaseUrl, logger, IpaasAuth);
        }
        #endregion

        #region Public Methods

        public IpaasDataUrls RunJob(int jobId)
        {
            Log($"RunJob started, jobId - {jobId}");
            var jobLogId = CallRunJob(jobId);

            Log($"Call Run Job returned log with {jobLogId} Id, starting pooling...");
            var finalLog = JobStatusPooling(jobLogId);

            ValuesValidator.Validate(finalLog, $"Final Log is null!");
            ValuesValidator.Validate(finalLog.RunStatusId != (int)eScheduledJobRunStatus.Running, $"Max Pooling time was exided! Job is still in Running status!", false);
            ValuesValidator.Validate(finalLog?.RunStatusId == (int)eScheduledJobRunStatus.OK,
                $"Job finished with '{GetJobRunStatusNameById(finalLog.RunStatusId)}' status! Should be 'OK' status to get data URLs!");

            Log($"Retrieving last task data urls...");
            var dataUrls = GetScheduledJobLogLastTaskDataUrl(jobLogId);
            return dataUrls;
        }

        #endregion

        #region Private Methods

        private int CallRunJob(int jobId)
        {
            var url = $"ClientTask/CallRunJob?ScheduledJobId={jobId}";

            var response = IpaasHttpClient.Get<IEnumerable<IpaasStartScheduledJobResponse>>(url);
            var startJobResponse = response?.FirstOrDefault();
            ValuesValidator.Validate(startJobResponse, "Can't get correct start job response!");

            ValuesValidator.Validate(startJobResponse.ResultStatus == (int)eRunJob_ResultStatus.OK, "Start Job response is in not OK status!");

            var scheduledJobLogId = startJobResponse.ScheduledJobLogId;

            ValuesValidator.Validate(scheduledJobLogId, "Can't get correct scheduled job log ID!");
            return scheduledJobLogId;
        }

        private IpaasScheduledJobLogStatusApi JobStatusPooling(int jobLogId, PoolingConfig poolingConfig = null)
        {

            if (poolingConfig == null)
                poolingConfig = new PoolingConfig();

            var current = 0;
            var shouldPool = true;
            IpaasScheduledJobLogStatusApi finalLog = null;

            Log($"JobStatusPooling start");
            Log($"MaxMinutesToPool - {poolingConfig.MaxMinutesToPool}");
            Log($"PoolingInternvalInMs - {poolingConfig.PoolingInternvalInMs}");

            var startDateTime = DateTime.UtcNow;
            var endingDateTime = DateTime.UtcNow.AddMinutes(poolingConfig.MaxMinutesToPool);

            while (DateTime.UtcNow <= endingDateTime && shouldPool)
            {
                Thread.Sleep(poolingConfig.PoolingInternvalInMs);
                current++;

                finalLog = GetJobLogStatus(jobLogId, true);
                var passedSec = (int)(DateTime.UtcNow - startDateTime).TotalSeconds;
                Log($"#{current}, passed - {passedSec} sec, status - {GetJobRunStatusNameById(finalLog.RunStatusId)}");

                shouldPool = finalLog.RunStatusId == (int)eScheduledJobRunStatus.Running;
            }

            Log($"Pooling Finished with {GetJobRunStatusNameById(finalLog.RunStatusId)} status!");
            return finalLog;
        }

        private IpaasScheduledJobLogStatusApi GetJobLogStatus(int jobLogId, bool isMinifiedLog = false)
        {
            ValuesValidator.Validate(jobLogId, $"GetJobLogStatus validation failed! Incorrect jobLogId - {jobLogId}!");
            var url = $"ScheduledJob/ReadScheduledJobLogsStatusApiByIds?ScheduledJobLogsIds={jobLogId}";
            var response = IpaasHttpClient.Get<IEnumerable<IpaasScheduledJobLogStatusApi>>(url, isMinifiedLog: isMinifiedLog);

            var log = response?.FirstOrDefault();
            ValuesValidator.Validate(log, $"GetJobLogStatus can't get job log!");

            return log;
        }

        private IpaasDataUrls GetScheduledJobLogLastTaskDataUrl(int jobLogId)
        {
            ValuesValidator.Validate(jobLogId, $"GetScheduledJobLogLastTaskDataUrl validation failed! Incorrect jobLogId - {jobLogId}!");
            var url = $"ScheduledJob/CreateScheduledJobLogLastTaskUrls?scheduledJobLogId={jobLogId}";

            var response = IpaasHttpClient.PostJson<string, IpaasDataUrls>(url, null);

            return response;
        }

        private string GetJobRunStatusNameById(int statusId)
        {
            return ((eScheduledJobRunStatus)statusId).ToString() ?? "Unknown status";
        }

        private void Log(string message)
        {
            this.Logger.Log(message + "\r\n");
        }
        #endregion
    }
}
