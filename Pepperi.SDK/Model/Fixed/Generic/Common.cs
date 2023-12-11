using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed.Generic
{
    public class FromToDateTime
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }

    public class PoolingConfig
    {
        public int PoolingInternvalInMs { get; set; } = 1000;
        public int NumberOfPoolingAttempts { get; set; } = 60 * 5;

        public PoolingConfig() { }

        public PoolingConfig(int poolingInternvalInMs = 1000, int numberOfPoolingAttempts = 60 * 5)
        {
            this.PoolingInternvalInMs = poolingInternvalInMs;
            this.NumberOfPoolingAttempts = numberOfPoolingAttempts;
        }
    }
}
