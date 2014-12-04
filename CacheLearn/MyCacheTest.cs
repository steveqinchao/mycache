using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

namespace CacheLearn
{

    class MyCacheTest
    {
        public static void Main()
        {
            MyCache<UserInfo> mycache = new MyCache<UserInfo>();
            
            //5秒更新一次缓存
            mycache.timeoutSecond = 5000;
            mycache.sql = "";
            //启动缓存
            mycache.StartCache();


            for (int i = 0; i < 100; i++)
            {
                //读取缓存
                Console.WriteLine("_______________读取缓存,缓存内容如下：");
                List<UserInfo> userList = mycache.GetData();
                foreach (UserInfo userInfo in userList)
                {
                    Console.WriteLine(userInfo.ToString());
                }

                //1秒读取一次缓存
                Thread.Sleep(1000);
 
            }
               
        }

    }
}
