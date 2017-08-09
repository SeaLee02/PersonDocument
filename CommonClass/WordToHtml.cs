using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions; //正则表达式
using System.Web;
using Aspose.Words;


namespace Common
{

    /// <summary>
    /// 说明
    /// 此类是操作Word的辅助类
    /// 用这个是需要引用Aspose.Word.dll的
    /// https://github.com/SeaLee02/PersonDocument/tree/master/DLL   这个里面有这个插件
    /// </summary>
    public class WordToHtml
    {
        /// <summary>
        /// 把字符串转成文件流
        /// </summary>
        /// <param name="filestream">文件流</param>
        /// <param name="sHTML">字符串</param>
        public void WriteFile(FileStream filestream, string sHTML)
        {
            byte[] data = new UTF8Encoding().GetBytes(sHTML);
            filestream.Write(data, 0, data.Length);
            filestream.Flush();
            filestream.Close();
        }
        //转化
        public void HtmlToWordFile(string HtmlFilePath)
        {
            FileClass fc = new FileClass();
            string htmlbody = fc.GetFile(HtmlFilePath);
            MatchCollection imgMC = Regex.Matches(htmlbody, @"<u>\s*</u>");
            for (int k = 0; k < imgMC.Count; k++)
            {
                htmlbody = htmlbody.Replace(imgMC[k].Value, "<u>　　　　　　　　　　</u>");
            }
            fc.WirteFile(HtmlFilePath, htmlbody);
            try
            {
                string dfileName = System.IO.Path.ChangeExtension(HtmlFilePath, ".doc");
                File.Move(HtmlFilePath, dfileName);
            }
            catch (Exception e)
            {
            }
        }
        public void replaceWordStrToPicture(string filePath, List<string> picturesList, string imageServicePath)
        {
            Aspose.Words.Document doc = new Aspose.Words.Document(filePath);
            foreach (string pictruePath in picturesList)
            {
                string picfileName = imageServicePath + pictruePath.Replace("&", "");
                Regex reg = new Regex(pictruePath);
                doc.Range.Replace(reg, new ReplaceAndInsertImage(picfileName), false);
            }
            DocumentBuilder builder2 = new DocumentBuilder(doc);
            builder2.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            doc.Save(filePath);
        }
        public FileStream GetFileObject(string sFileDir)
        {
            FileStream filestream;
            if (File.Exists(sFileDir))
                filestream = new FileStream(sFileDir, FileMode.Create);
            else
                filestream = File.Create(sFileDir);
            return filestream;
        }
        /// <summary>
        /// 根据html内容及标题和文件名形成html完整代码
        /// </summary>
        /// <param name="sBody">body内html的代码内容</param>
        /// <param name="fileName">html文件名</param>
        /// <param name="title">html标题</param>
        /// <returns>html代码</returns>
        /// 
        public string GetOverall(string sBody, string title)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"zh-CN\">");
            sb.Append("<head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
            //
            sb.Append("<title>" + title + "</title>\r\n</head><body>");
            sb.Append(sBody);
            sb.Append("</body></html>");
            return sb.ToString();

        }
        /// 用Bookmark绑定word对应项的值
        /// <summary>
        /// 用Bookmark绑定word对应项的值
        /// </summary>
        /// <param name="parama">指定word</param>
        /// <param name="parama">word里面的标签名称</param>
        /// <param name="paramb">对应标签显示的值</param>
        public void UseBookmark(Document doc, string parama, string paramb)
        {
            if (doc.Range.Bookmarks[parama] != null)
            {
                Bookmark mark = doc.Range.Bookmarks[parama];
                mark.Text = paramb;
            }
        }
        /// 用Bookmark追加word对应项的值
        /// <summary>
        /// 用Bookmark追加word对应项的值
        /// </summary>
        /// <param name="parama">指定word</param>
        /// <param name="parama">word里面的标签名称</param>
        /// <param name="paramb">对应标签追加的值</param>
        public void UseBookmarkAdd(Document doc, string parama, string paramb)
        {
            if (doc.Range.Bookmarks[parama] != null)
            {
                Bookmark mark = doc.Range.Bookmarks[parama];
                mark.Text = mark.Text + "\n" + paramb;
            }
        }

        /// <summary>
        /// 获取服务器端已经保存的同名文件，返回新文件名
        /// </summary>
        /// <param name="FileDir">文件目录</param>
        /// <param name="fileName">文件名</param>
        /// <param name="i">同文件名数量</param>
        /// <returns>新文件名</returns>
        public string GetServerFileName(string FileDir, string fileName, int i)
        {
            DirectoryInfo info = new DirectoryInfo(HttpContext.Current.Server.MapPath(FileDir));
            FileInfo[] fi = info.GetFiles();
            string newFileName = "";
            if (i == 0)
            {
                newFileName = fileName;
            }
            else
            {
                newFileName = fileName.Split('.')[0] + i + "." + fileName.Split('.')[1];
            }
            foreach (FileInfo f in fi)
            {
                if (f.Name.Trim().Equals(newFileName))
                {
                    newFileName = GetServerFileName(FileDir, fileName, ++i);
                }
            }
            return newFileName;
        }
    }
}