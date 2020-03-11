using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Framework
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
                    return ConfigurationManager.SqlConnectionStringRead[0];
                default:
                    throw new Exception("Wrong DBOperateType");
            }
        }
        internal enum DBOperateType
        {
            Write,
            Read
        }
    }
}
