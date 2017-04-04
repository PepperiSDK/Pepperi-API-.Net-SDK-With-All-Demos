using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Contracts
{
    public interface ILogger
    {
        void Log(string s); 
    }

    public enum eLogLevel
    {
        Debug,          
        Warning,        
        Info,           
        Error,          
        Fatal               
    }
}
