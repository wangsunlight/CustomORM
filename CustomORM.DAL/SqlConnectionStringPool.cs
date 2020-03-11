using CustomORM.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.DAL
{
    public class SqlConnectionStringPool
    {
        internal static string GetConnectionString(DBOperateType dBOperateType)
        {
            if (!ConfigurationManager.GetEnableWriteRead)
            {
                return ConfigurationManager.SqlConnectionStringCustom;
            }
            switch (dBOperateType)
            {
                case DBOperateType.Write:
                    return ConfigurationManager.SqlConnectionStringWrite;
                case DBOperateType.Read:
                    return DispatcherConn();
                default:
                    throw new Exception("Wrong DBOperateType");
            }
        }

        private static long iIndex = 0;

        private static string DispatcherConn()
        {
            return ConfigurationManager.SqlConnectionStringRead[iIndex++ % ConfigurationManager.SqlConnectionStringRead.Length];
        }
        internal enum DBOperateType
        {
            Write,
            Read
        }
    }
}
