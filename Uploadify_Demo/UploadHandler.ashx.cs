using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;


namespace Uploadify_Demo
{
    /// <summary>
    /// UploadHandler 文件上传后台
    /// </summary>
    public class UploadHandler : IHttpHandler 
    {
       
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";
            HttpPostedFile file = context.Request.Files["Filedata"];
            string uploadPath = HttpContext.Current.Server.MapPath("~/upload/");
            if (file != null)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string fileName = file.FileName.Substring(0, file.FileName.IndexOf('.')) +
                                  DateTime.Now.ToString("yyyyMMddHHmmssfff") +
                                  file.FileName.Substring(file.FileName.IndexOf('.'),file.FileName.Length - file.FileName.IndexOf('.'));
                file.SaveAs(uploadPath + fileName);
                //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失，前端同样可以控制是否保留队列
               
                context.Response.Write("1");
            }
            else
            {
                context.Response.Write("0");
            }  
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}