using System.Text;
using System.Text.Json;
using Kidz.Constants;
using Kidz.Models;

namespace Kidz.Function
{
    public class UtilityFunction
    {
        public static ResponseModel callInternalService(string stringURL, RequestModel modelRequest, string stringUserToken)
        {
            ResponseModel modelResponse = new ResponseModel();

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Token", stringUserToken);

                Uri uri = new Uri(stringURL);

                using StringContent content = new StringContent(JsonSerializer.Serialize(modelRequest),UnicodeEncoding.UTF8,"application/json");

                HttpResponseMessage httpResponseMessage = client.PostAsync(uri, content).Result;

                Console.Write(httpResponseMessage.StatusCode);

                httpResponseMessage.EnsureSuccessStatusCode();

                string stringResult = httpResponseMessage.Content.ReadAsStringAsync().Result;

                JsonSerializerOptions serializerOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                modelResponse = JsonSerializer.Deserialize<ResponseModel>(stringResult, serializerOptions);
            }
            catch (Exception exception)
            {
                Console.WriteLine("error message : "+exception.Message);
            }

            return modelResponse;
        }
    }
}