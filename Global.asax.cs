using System;
using System.Text;
using System.Web.Http;

namespace TournamaticBot
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            var log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
            try
            {
                GlobalConfiguration.Configure(WebApiConfig.Register);
            }
            catch (Exception e)
            {
                var sb = new StringBuilder();
                var inExp = e;
                do
                {
                    sb.AppendLine(inExp.Message);
                    inExp = e.InnerException;
                }
                while (inExp != null);
                log.Error(sb.ToString());
            }
        }
    }
}
