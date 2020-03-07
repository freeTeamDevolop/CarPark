using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Framework.Utilities.HttpMethod
{
    public class HttpHelper
    {
        public static string HttpPost(string Url, Dictionary<string, string> valuePair, Encoding Coding = null)
        {
            StreamReader StReder = null;
            try
            {
                if (string.IsNullOrEmpty(Url.Trim()))
                {
                    throw new Exception("URL地址不能为空");
                }
                HttpWebRequest Requst = (HttpWebRequest)HttpWebRequest.Create(Url.Trim());
                Requst.Method = "POST";
                Requst.ContentType = "application/x-www-form-urlencoded";
                if (valuePair.Count > 0)
                {
                    #region 添加Post 参数  

                    StringBuilder builder = new StringBuilder();
                    foreach (KeyValuePair<string, string> temp in valuePair)
                    {
                        builder.Append(temp.Key + "=" + temp.Value + "&");
                    }
                    int nLen = builder.Length;
                    builder.Remove(nLen - 1, 1);
                    //post数据转换成byte
                    byte[] Postdata = Encoding.UTF8.GetBytes(builder.ToString());
                    Requst.ContentLength = Postdata.Length;
                    using (Stream reqStream = Requst.GetRequestStream())
                    {
                        reqStream.Write(Postdata, 0, Postdata.Length);
                        reqStream.Close();
                    }
                    #endregion
                }
                HttpWebResponse Respons = (HttpWebResponse)Requst.GetResponse();
                if (Coding != null)
                    StReder = new StreamReader(Respons.GetResponseStream(), Coding);
                else
                    StReder = new StreamReader(Respons.GetResponseStream(), Encoding.UTF8);
                string result = StReder.ReadToEnd();
                StReder.Close();
                return result;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return "";
            }
        }
    }
}
