using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace ShLinkerApp
{
    public class BitlyShortenerProvider:ShortenerProvider
    {
        public override string GetShortUrl(string longUrl, string userName, string apiKey)
        {
            userName = "o_4opfom8sio";
            apiKey = "R_151d1af9200c413c8df1b345f1410422";
            var sbUrl = new StringBuilder("http://api.bitly.com/v3/shorten?");
            sbUrl.AppendFormat("&format=xml");
            sbUrl.Append("&longUrl=");
            sbUrl.Append(HttpUtility.UrlEncode(longUrl));
            sbUrl.Append("&login=");
            sbUrl.Append(HttpUtility.UrlEncode(userName));
            sbUrl.Append("&apiKey=");
            sbUrl.Append(HttpUtility.UrlEncode(apiKey));

            var request = WebRequest.Create(sbUrl.ToString()) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ServicePoint.Expect100Continue = false;
            request.ContentLength = 0;
            
            try
            {
                var req = request.GetResponse();
                //req.Wait();
                var response = req;
                var myXML = new StreamReader(response.GetResponseStream());
                dynamic xr = XmlReader.Create(myXML);

                var xDoc = new XmlDocument();
                xDoc.Load(xr);

                string status = xDoc.ChildNodes[1].ChildNodes[1].ChildNodes[0].Value;
                return status == "OK" ? xDoc.ChildNodes[1].ChildNodes[2].ChildNodes[0].ChildNodes[0].Value : status;
            }
            catch
            {
                return "Some error happened!";
            }
        }
    }
}
