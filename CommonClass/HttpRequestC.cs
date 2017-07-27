using System;
using System.Web;//这个命名空间不可少，如果你引用了这个里面的类还是调用不出来请去添加引用


/// <summary>
/// 说明
/// 这个类是写在一个类库里面的，到时候是在页面里面进行调用，所以必须为公共的，全部带上public
/// 此类作为公共类，为了方便调用，方法全部定义为静态的
/// 可以根据你的需求进行补充方法
/// 在你的网站里面添加引用，找到这个命名空间为Common的,引用到你的项目里面，就可以调用次类
/// </summary>



namespace Common
{
    /// <summary>
    /// 读取HTTP值
    /// </summary>
    public class HttpRequestC
    {
        //先接受HttpRequest类，都是围绕这个进行处理的
        private static HttpRequest hr = HttpContext.Current.Request;


        /// <summary>
        /// 获取get传值
        /// </summary>
        /// <param name="key">参数名</param>
        /// <param name="iDefault">默认值</param>
        /// <returns>返回整型</returns>
        public static int iQ(string key, int iDefault = 0)
        {
            if (hr.QueryString[key] != null)
            {
                iDefault = Convert.ToInt32(hr.QueryString[key]);
            }
            return iDefault;
        }

        /// <summary>
        /// 获取get传值
        /// </summary>
        /// <param name="key">参数名</param>
        /// <param name="iDefault">默认值</param>
        /// <returns>返回字符串</returns>
        public static string sQ(string key, string sDefault = "")
        {
            if (hr.QueryString[key] != null)
            {
                sDefault = hr.QueryString[key];
            }
            return sDefault;
        }


        /// <summary>
        /// 获取post传值
        /// </summary>
        /// <param name="key">参数名</param>
        /// <param name="iDefault">默认值</param>
        /// <returns>返回整型</returns>
        public static int iF(string key, int iDefault = 0)
        {
            if (hr.Form[key] != null)
            {
                iDefault = Convert.ToInt32(hr.Form[key]);
            }
            return iDefault;
        }

        /// <summary>
        /// 获取post传值
        /// </summary>
        /// <param name="key">参数名</param>
        /// <param name="iDefault">默认值</param>
        /// <returns>返回字符串</returns>
        public static string sF(string key, string sDefault = "")
        {
            if (hr.Form[key] != null)
            {
                sDefault = hr.Form[key];
            }
            return sDefault;
        }


      /// <summary>
      /// 设置Cookie值
      /// </summary>
      /// <param name="Name">名字</param>
      /// <param name="strValue">值</param>
      /// <param name="key">键，这是个可选参数</param>
        public static void WriteCookie(string Name, string strValue, string key = "")
        {
            HttpCookie cookie = hr.Cookies[Name];
            if (cookie == null)
            {
                cookie = new HttpCookie(Name);
            }
            if (key!="")
            {
                cookie.Values[key] = HttpContext.Current.Server.UrlEncode(strValue);
            }
            else
            {
                cookie.Value = HttpContext.Current.Server.UrlEncode(strValue);  //中文需要编码
            }         
            HttpContext.Current.Response.Cookies.Add(cookie);
        }



       /// <summary>
       /// 获取Cookies值
       /// </summary>
       /// <param name="Name">名字</param>
       /// <param name="key">键</param>
       /// <returns>返回cookies值</returns>
        public static string GetCookie(string Name,string key="")
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[Name] != null)
            {
                if (key!="")
                {
                    return HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies[Name][key]);
                }
                else
                {
                    return HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies[Name].Value); //编码需要解码
                }               
            }
            return "";
        }


     



        /// <summary>
        /// 判断当前页面是否接受到了POST请求
        /// </summary>
        /// <returns></returns>
        public static bool IsPost()
        {
            return hr.HttpMethod.Equals("POST");
        }

        /// <summary>
        /// 判断当前页面是否接受到了Get请求
        /// </summary>
        /// <returns></returns>
        public static bool IsGet()
        {
            return hr.HttpMethod.Equals("GET");
        }


        /// <summary>
        /// 返回指定的服务器变量信息
        /// </summary>
        /// <param name="strName">服务器变量名</param>
        /// <returns>服务器变量信息</returns>
        public static string GetServerString(string strName)
        {
            if (hr.ServerVariables[strName] == null)
                return "";

            return hr.ServerVariables[strName].ToString();
        }

        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns></returns>
        public static string GetUrlReferrer()
        {
            string retVal = "";
            if (hr.UrlReferrer != null)
            {
                retVal = hr.UrlReferrer.ToString();
            }
            return retVal;
        }

        /// <summary>
        /// 得到当前完整主机头
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentFullHost()
        {
            if (!hr.Url.IsDefaultPort)
                return string.Format("{0}:{1}", hr.Url.Host, hr.Url.Port.ToString());

            return hr.Url.Host;
        }

        /// <summary>
        /// 得到主机头
        /// </summary>
        public static string GetHost()
        {
            return hr.Url.Host;
        }


        /// <summary>
        /// 获取当前请求的原始 URL(URL 中域信息之后的部分,包括查询字符串(如果存在))
        /// </summary>
        /// <returns></returns>
        public static string GetRawUrl()
        {
            return hr.RawUrl;
        }

        /// <summary>
        /// 判断当前访问是否来自浏览器软件
        /// </summary>
        /// <returns></returns>
        public static bool IsBrowserGet()
        {
            //把浏览器的名字写进集合里面
            string[] BrowserName = { "chrome", "ie", "opera", "netscape", "mozilla", "konqueror", "firefox" };
            string curBrowser = hr.Browser.Type.ToLower();
            for (int i = 0; i < BrowserName.Length; i++)
            {
                if (curBrowser.IndexOf(BrowserName[i]) >= 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是否来自搜索引擎链接
        /// </summary>
        /// <returns></returns>
        public static bool IsSearchEnginesGet()
        {
            if (hr.UrlReferrer == null)
                return false;

            string[] SearchEngine = { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou" };
            string tmpReferrer = hr.UrlReferrer.ToString().ToLower();
            for (int i = 0; i < SearchEngine.Length; i++)
            {
                if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string GetUrl()
        {
            return hr.Url.ToString();
        }
    }
}
