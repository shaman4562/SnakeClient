using System.Net;
using System.IO;


namespace Snake
{
    internal class Input
    {        
        readonly static string jsonLeft = @"{""direction"":""Left"",""token"":""5@U(4PP2^jJjEL*S=k*W""}";
        readonly static string jsonRight = @"{""direction"":""Right"",""token"":""5@U(4PP2^jJjEL*S=k*W""}";
        readonly static string jsonTop = @"{""direction"":""Top"",""token"":""5@U(4PP2^jJjEL*S=k*W""}";
        readonly static string jsonBottom = @"{""direction"":""Bottom"",""token"":""5@U(4PP2^jJjEL*S=k*W""}";


        public static void PostRequestAsync(string strReq)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create("http://safeboard.northeurope.cloudapp.azure.com/api/Player/direction");
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            using (var requestStream = httpRequest.GetRequestStream())
            using (var writer = new StreamWriter(requestStream))
            {
                switch (strReq)
                {
                    case "Left":
                        writer.Write(jsonLeft);
                        break;
                    case "Right":
                        writer.Write(jsonRight);
                        break;
                    case "Up":
                        writer.Write(jsonTop);
                        break;
                    case "Down":
                        writer.Write(jsonBottom);
                        break;
                    default:
                        writer.Write(jsonTop);
                        break;
                }
            }
            using (var httpResponse = httpRequest.GetResponse())
            using (var responseStream = httpResponse.GetResponseStream())
            using (var reader = new StreamReader(responseStream))
            {
                string response = reader.ReadToEnd();
            }
        }
    }
}
