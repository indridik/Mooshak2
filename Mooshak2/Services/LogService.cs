using Mooshak2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mooshak2.Services
{
    public static class LogService
    {/// <summary>
    ///  Error log that sends error msg to Error Logger table in database
    /// </summary>

        public static void LogError(string method, Exception ex)
        {
            MooshakDataContext context = new MooshakDataContext();
            ErrorLog log = new ErrorLog()
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                TimeStamp = DateTime.Now,
                Method = method
            };
            context.ErrorLogs.InsertOnSubmit(log);
        }
    }
}