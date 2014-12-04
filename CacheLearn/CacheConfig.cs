using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheLearn
{
    class CacheConfig
    {
        public List<SQLCommand> SQLCommands { get; set; }
    }

    /// <summary>
    /// 缓存配置
    /// </summary>
    class SQLCommand
    {

        /// <summary>
        /// 缓存类名称
        /// </summary>
        private string listType;
        
        public string ListType {
            get { return listType; }
            set { listType = value; } 
        }

        /// <summary>
        /// 取数SQL
        /// </summary>
        private string commandString;

        public string CommandString
        {
            get { return commandString; }
            set { commandString = value; }
        }

        /// <summary>
        /// 超时时间
        /// </summary>
        private string cacheOverTime;

        public string CacheOverTime
        {
            get { return commandString; }
            set { commandString = value; }
        }
 
    }
}
