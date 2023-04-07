using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Ipaas
{
    internal enum eRunJob_ResultStatus
    {
        /// <summary>
        /// Operation Completed successfully
        /// </summary>
        OK,
        /// <summary>
        /// Operation Completed using default values
        /// </summary>
        Warning,
        /// <summary>
        /// Validation error (custom friendly exception indicating error at client level)
        /// </summary>
        Error,
        /// <summary>
        /// Un expected error (Un expected exception or custo fatal exception)
        /// </summary>
        Fatal
    }

    internal enum eScheduledJobRunStatus
    {
        Running = 1,
        OK = 2,
        Error = 3,
        Warning = 4,
        Aborted = 5,
        PageLoaded = 6
    }
}
