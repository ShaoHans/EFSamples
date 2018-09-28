using NLog;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.NetFramework.Interceptors
{
    /// <summary>
    /// 构造拦截器通过NLog记录sql
    /// </summary>
    public class NLogCommandInterceptor : IDbCommandInterceptor
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private void LogIfNonAsync<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
            if(!interceptionContext.IsAsync)
            {
                _logger.Warn($"非异步执行sql：{command.CommandText}");
            }
        }

        private void LogIfError<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
            if (interceptionContext.Exception != null)
            {
                _logger.Error($"sql语句：{command.CommandText} 执行出现异常：{interceptionContext.Exception}");
            }
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            LogIfError(command, interceptionContext);
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            LogIfNonAsync(command, interceptionContext);
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            LogIfError(command, interceptionContext);
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            LogIfNonAsync(command, interceptionContext);
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            LogIfError(command, interceptionContext);
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            LogIfNonAsync(command, interceptionContext);
        }
    }
}
