using System.Web;
using System.Web.Services;
using System.Web.SessionState;

using Bussiness;

namespace UserCenter.Services
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class checkcode : IHttpHandler,IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            ComplexCheckCode gen = new ComplexCheckCode();
            string verifyCode = gen.CreateVerifyCode(5, 1);
            context.Session["CheckCode"] = verifyCode.ToUpper();
            System.Drawing.Bitmap bitmap = gen.CreateImage(verifyCode);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            context.Response.Clear();
            context.Response.ContentType = "image/Png";
            context.Response.BinaryWrite(ms.GetBuffer());
            bitmap.Dispose();
            ms.Dispose();
            ms.Close();
            context.Response.End();
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
