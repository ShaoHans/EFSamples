using Infrastructure.NetFramework.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.NetFramework.Interceptors
{
    public class PerformanceMonitoringInterceptor: DbCommandInterceptor
    {
        /// <summary>
        /// 执行时间：毫秒
        /// </summary>
        private long _duration = 0;
        public PerformanceMonitoringInterceptor(long duration)
        {
            _duration = duration;
        }

        private void OnExecuting<T>(DbCommand command, DbCommandInterceptionContext<T> interceptionContext)
        {
            var sw = new Stopwatch();
            interceptionContext.SetUserState("sw", sw);
            sw.Start();
        }

        private void OnExecuted<T>(DbCommand command, DbCommandInterceptionContext<T> interceptionContext)
        {
            var sw = (Stopwatch)interceptionContext.FindUserState("sw");
            sw.Stop();

            if (sw.ElapsedMilliseconds < _duration || interceptionContext.Exception == null)
            {
                return;
            }

            // 有异常的sql或者执行时间很长的sql记录到数据库
            StringBuilder parameters = new StringBuilder();
            foreach (DbParameter param in command.Parameters)
            {
                parameters.AppendLine($"{param.ParameterName} {param.DbType} = {param.Value}");
            }

            using (EfDbContext dbContext = new EfDbContext())
            {
                SqlLog sqlLog = new SqlLog();
                sqlLog.Sql = command.CommandText;
                sqlLog.Parameters = parameters.ToString();
                sqlLog.CommandType = command.CommandType.ToString();
                sqlLog.Milliseconds = sw.ElapsedMilliseconds;
                sqlLog.Exception = interceptionContext.Exception == null ? string.Empty : interceptionContext.Exception.ToString();
                sqlLog.CreateTime = DateTime.Now;

                dbContext.SqlLogs.Add(sqlLog);
                dbContext.SaveChanges();
            }
        }

        public override void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            OnExecuted(command, interceptionContext);
            base.NonQueryExecuted(command, interceptionContext);
        }

        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            OnExecuting(command, interceptionContext);
            base.NonQueryExecuting(command, interceptionContext);
        }

        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            OnExecuted(command, interceptionContext);
            base.ReaderExecuted(command, interceptionContext);
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            OnExecuting(command, interceptionContext);
            base.ReaderExecuting(command, interceptionContext);
        }

        public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            OnExecuted(command, interceptionContext);
            base.ScalarExecuted(command, interceptionContext);
        }

        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            OnExecuting(command, interceptionContext);
            base.ScalarExecuting(command, interceptionContext);
        }


    }
}
