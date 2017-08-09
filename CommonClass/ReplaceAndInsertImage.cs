
using Aspose.Words;


namespace Common
{
    public class ReplaceAndInsertImage: IReplacingCallback
    {
    /// <summary>
    /// 需要插入的图片路径
    /// </summary>
    public string url { get; set; }

    public ReplaceAndInsertImage(string url)
    {
        this.url = url;
    }

    public ReplaceAction Replacing(ReplacingArgs e)
    {
        //获取当前节点
        var node = e.MatchNode;
        //获取当前文档
        Document doc = node.Document as Document;
        DocumentBuilder builder = new DocumentBuilder(doc);
        //将光标移动到指定节点
        builder.MoveTo(node);
        //插入图片

        builder.InsertImage(url);
        return ReplaceAction.Replace;
    }
}
}