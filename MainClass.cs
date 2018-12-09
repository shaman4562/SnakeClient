using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;

namespace Snake
{
    static class MainClass
    {
        static readonly string urlName = "http://safeboard.northeurope.cloudapp.azure.com/api/Player/name?token=5@U(4PP2^jJjEL*S=k*W";

        static void Main()
        {                       
            if (FirstConnect() == "Shaman")
            {                
                Application.Run(new Form1());
            }
            else
            {
                MessageBox.Show("Ошибка соединения");
            }
        }

        static string FirstConnect()
        {
            string name = "";
            using (var webClient = new WebClient())
            {               
                string response = webClient.DownloadString(urlName);
                NameClass str = JsonConvert.DeserializeObject<NameClass>(response);
                name = str.Name;                
            }
            return name;
        }


    }
}
