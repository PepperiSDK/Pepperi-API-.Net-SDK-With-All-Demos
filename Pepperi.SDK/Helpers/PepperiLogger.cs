using Pepperi.SDK.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Helpers
{
    /// <summary>
    /// Logger writing logs to Log file 
    /// </summary>
    public class PepperiLogger : ILogger
    {
        #region Properties

        private string LogFilePath { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logFilePath"></param>
        /// <remarks>
        /// 1. when coming from unit test, the unit test defines the  logFileName 
        /// </remarks>
        public PepperiLogger(string logFilePath = null)
        {
            //state
            if (logFilePath != null)
            {
                this.LogFilePath = logFilePath;
            }
            else
            {
                string AssemblyLocation = Assembly.GetExecutingAssembly().Location;
                string AssemblyPath = Path.GetDirectoryName(AssemblyLocation);
                string logDirectory = AssemblyPath;
                string logFileName = "Log.txt";

                this.LogFilePath = Path.Combine(logDirectory, logFileName);
            }


            //create empty or override existing file
            using (FileStream fs = System.IO.File.Create(LogFilePath))
            {
            }
        }

        #endregion

        #region ILogger

        /// <summary>
        /// Appends the given string to the Log file
        /// </summary>
        /// <param name="s"></param>
        /// <remarks>
        /// 1. The stream writer constrctor uses appand mode
        /// 2. If the file does not exist, the stream writer creates it.
        ///    If the file exists, the stream writer appands to it.
        /// 3. he using statement automatically flushes AND CLOSES the stream and calls IDisposable.Dispose on the stream object.
        /// </remarks>
        public void Log(string s)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(LogFilePath, true))
            {
                file.Write(s); ;
            }
        }

        #endregion


        #region private methods
        #endregion
    }
}
