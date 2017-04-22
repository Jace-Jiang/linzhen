using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;

namespace XGhms.Web.App_Code
{
    public class ValidateInput
    {
        /// <summary>
        /// 屏蔽网络关键值 true 包含， false 不包含
        /// </summary>
        /// <param name="validatestring">查询字符串</param>
        public static bool ValidateKeyWord(string validatestring)
        {
            bool flag = false;

            List<string> strlist = new List<string>();
            if (HttpRuntime.Cache["strlist"] == null)
            {
                string path = new Page().Server.MapPath("../App_Data/Sensitive.txt");
                string[] filestring = File.ReadAllLines(path, Encoding.Default);

                foreach (string str in filestring)
                    strlist.Add(str);

                DataCache.Insert("strlist", strlist,1440);//添加进cache，过期时间1天
            }
            else
            {
                strlist = HttpRuntime.Cache["strlist"] as List<string>;//从cache取值
            }
            foreach (string i in strlist)
            {
                if (string.IsNullOrEmpty(i))
                    continue;
                if (validatestring.Contains(i))
                {
                    flag = true; break;
                }
            }
            return flag;
        }
    }
}