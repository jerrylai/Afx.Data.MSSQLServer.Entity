using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Afx.Data.Schema;
#if NETCOREAPP || NETSTANDARD
using Microsoft.Data.SqlClient;
#else
using System.Data.SqlClient;
#endif
using Afx.Data;

namespace Afx.Data.MSSQLServer
{
    /// <summary>
    /// 获取数据库结构信息
    /// </summary>
    public class MsSqlDatabase : Database
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public MsSqlDatabase(string connectionString) : base(connectionString, SqlClientFactory.Instance)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connection">数据库链接</param>
        /// <param name="isOwnsConnection">释放资源时是否释放链接</param>
        public MsSqlDatabase(SqlConnection connection, bool isOwnsConnection = true)
            : base(connection, isOwnsConnection)
        {
        }

        public override string EncodeColumn(string column)
        {
            if (string.IsNullOrEmpty(column)) throw new ArgumentNullException("column");
            return string.Format("[{0}]", column);
        }

        public override string EncodeParameterName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            return string.Format("@{0}", name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override ITableSchema GeTableSchema()
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(this.ConnectionString);
            return new MsSqlServerTableSchema(new MsSqlDatabase(this.ConnectionString), connectionStringBuilder.InitialCatalog);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IDatabaseSchema GetDatabaseSchema()
        {
            //Data Source=127.0.0.1;Initial Catalog=master;User ID=sa;Password=123;Pooling=False;Min Pool Size=1;Max Pool Size=100;Load Balance Timeout=30;Application Name=Afx.Data
            var connectionStringBuilder = new SqlConnectionStringBuilder(this.ConnectionString);
            var database = connectionStringBuilder.InitialCatalog;
            connectionStringBuilder.InitialCatalog = "master";
            connectionStringBuilder.Remove("Min Pool Size");
            connectionStringBuilder.Remove("Max Pool Size");
            connectionStringBuilder.Remove("Load Balance Timeoute");
            connectionStringBuilder.Pooling = false;
            return new MsSqlServerDatabaseSchema(new MsSqlDatabase(connectionStringBuilder.ConnectionString), database);
        }
    }
}
