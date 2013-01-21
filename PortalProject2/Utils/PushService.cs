using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack.ServiceClient.Web;
using RestSharp;
using JsonSerializer = ServiceStack.Text.JsonSerializer;


namespace PortalProject2.Utils
{
    public class PushService
    {
        public static string ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
        public static MessageResponse DoSendMessage(MessageOptions data)
        {
            JsonServiceClient client = new JsonServiceClient(ServiceUrl);
            //var client = new RestClient(ServiceUrl);
            
            var toSend = JsonSerializer.SerializeToString(data);
            MessageResponse response = client.Send<MessageResponse>("POST","createMessage",data);
            return response;
        }

        internal static MessageResponse SendPush(string method, JObject data)
        {
            Uri url = new Uri(ServiceUrl + method);
            JObject json = new JObject(new JProperty("request", data));
           return DoPostRequest(url, json);
        }

        private static MessageResponse DoPostRequest(Uri url, JObject json)
        {
            string responseText;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.ContentType = "text/json";
            req.Method = "POST";
            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                streamWriter.Write(json.ToString());
            }
            HttpWebResponse httpResponse;
            try
            {
                httpResponse = (HttpWebResponse)req.GetResponse();
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Problem with {0}, {1}", url, exc.Message));
            }
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                 responseText = streamReader.ReadToEnd();
                //Console.Response.Write(responseText);
            }
            return JsonConvert.DeserializeObject<MessageResponse>(responseText);
        }
    }
}