using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;


namespace CacheLearn
{
    public class MyCache 
    {
        //缓存的数据
        private Dictionary<string,List<object>> data;

        private void AddData(string listType , List<object> data)
        {
            this.data.Add(listType , data);
        }

        //根据key获取缓存
        public List<object> GetData(string listType)
        {
            return this.data[listType];
        }



        /// <summary>
        /// 启动缓存
        /// </summary>
        public void StartCache()
        {
            //读取配置文件
            XmlDocument doc = new XmlDocument();
            doc.Load(@"cacheconfig.xml");
            XmlNode xn = doc.SelectSingleNode("SQLCommands");
            XmlNodeList sqlCommandNodeList = xn.ChildNodes;

            List<SQLCommand> sqlCommandList = new List<SQLCommand>();
            foreach (XmlNode sqlCommandNode in sqlCommandNodeList)
            {
                SQLCommand sqlCommand = new SQLCommand();
                XmlElement sqlCommandElement = (XmlElement)sqlCommandNode;

                sqlCommand.ListType = sqlCommandElement.GetAttribute("ListType").ToString();
                sqlCommand.CommandString = sqlCommandElement.GetAttribute("CommandString").ToString();
                sqlCommand.CacheOverTime = sqlCommandElement.GetAttribute("CacheOverTime").ToString();
                sqlCommandList.Add(sqlCommand);
            }
            Console.WriteLine("缓存启动，读取配置文件完成...");


            Console.WriteLine("缓存启动，开始初始化缓存数据...");
            foreach (SQLCommand sqlCommand in sqlCommandList)
            {
                DataTable dt = GetDataFromDB(sqlCommand.CommandString);

                //
                SetData(ConvertHelper<T>.ConvertToList(dt));
            }
            

            Console.WriteLine("缓存启动，初始化数据完成.");

            //定时更新缓存
            TimerCallback tcb = new TimerCallback(UpdateCache);
            Timer tmr = new Timer(tcb, null, timeoutSecond, timeoutSecond);

        }

        /// <summary>
        /// 根据配置的SQL从数据库取得数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private DataTable GetDataFromDB(string sql)
        {
            //TODO:根据SQL从数据库取得数据
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID", typeof(Int32)));
            dt.Columns.Add(new DataColumn("UserName", typeof(String)));
            DataRow dr;

            int n = new Random().Next(6);
            for (int i = 0; i < n; i++)
            {
                dr = dt.NewRow();
                dr[0] = i;
                dr[1] = "第" + i.ToString() + "个记录的内容";
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="data"></param>
        private void UpdateCache(object data)
        {
            DataTable dt = GetDataFromDB(this.sql);
            List<T> newList= ConvertHelper<T>.ConvertToList(dt);
            SetData(newList);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("缓存更新完成...");
            foreach (T t in newList)
            {
                Console.WriteLine(t.ToString());
            }
        }
    }
}
