using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
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
    }
}
