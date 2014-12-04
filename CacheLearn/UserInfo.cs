using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheLearn
{
    class UserInfo
    {
        public int ID { get; set; }
        public string UserName { get; set; }

        public override string ToString()
        {
            return "ID:" + this.ID + ",UserName:" + this.UserName;
        }
    }
}
