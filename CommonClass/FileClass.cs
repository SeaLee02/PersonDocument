using System;
using System.Text;
using System.IO;

namespace Common
{
    public class FileClass
    {
        public string GetFile(string sFileDir)
        {
            string s = File.ReadAllText(sFileDir, Encoding.GetEncoding("gb2312"));
            if (s.Contains("charset=utf-8"))
            {
                s = File.ReadAllText(sFileDir, Encoding.UTF8);
            }
            return s;
        }
        public void WirteFile(string path, string str)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(str);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }
    }
}