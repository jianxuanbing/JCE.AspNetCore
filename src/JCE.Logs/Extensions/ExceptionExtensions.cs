using System;
using System.Collections.Generic;
using System.Text;
using JCE.Utils.Exceptions;

namespace JCE.Logs.Extensions
{
    /// <summary>
    /// 异常扩展
    /// </summary>
    public static partial class ExceptionExtensions
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="exception">异常</param>
        /// <param name="log">日志</param>
        public static void Log(this Exception exception, ILog log)
        {
            if (!(exception is Warning warning))
            {
                log.Exception(exception).Error();
                return;
            }
            log.Exception(exception,warning.Code).Warn();
        }
    }
}
