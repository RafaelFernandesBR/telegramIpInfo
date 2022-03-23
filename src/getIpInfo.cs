using System.Net;
using Newtonsoft.Json.Linq;

namespace telegramIp
{
    public class getInfo
    {

        private string getIpInfo(string ip)
        {
            string url = "http://ip-api.com/json/" + ip + "?fields=1113821&lang=pt-BR";
            string json = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                json = reader.ReadToEnd();
                reader.Close();
                stream.Close();
            }
            catch (Exception ex)
            {
                json = ex.Message;
            }
            return json;
        }

        //tratar o json para retorno
        public string buscaInfoIp(string ip)
        {
            string json = getIpInfo(ip);
            string retorno = null;
            try
            {
                var obj = JObject.Parse(json);
                if (Convert.ToString(obj["status"]) == "fail")
                {
                    retorno = "Ip inválido, tente novamente.";
                }
                else
                {
                    retorno = "IP: " + obj["query"] + "\n";
                    retorno += "Cidade: " + obj["city"] + "\n";
                    retorno += "Região: " + obj["regionName"] + " " + obj["region"] + "\n";
                    retorno += "País: " + obj["country"] + "\n";
                    retorno += "Provedor: " + obj["isp"] + "\n";
                    retorno += "Fornecedora: " + obj["org"] + "\n";
                    retorno += "Latitude: " + obj["lat"] + "\n";
                    retorno += "Longitude: " + obj["lon"] + "\n";
                }
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            return retorno;
        }

    }
}
