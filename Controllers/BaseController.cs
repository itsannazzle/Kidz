using System.Text;
using System.Text.Json;
using Kidz.DatabaseConnection;
using Kidz.Models;
using Kidz.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Kidz.Controllers
{
    public class BaseController : ControllerBase
    {
        public UserQuery _userQuery;
        public HitHistoryQuery _hitHistoryQuery;
        public ResultHistoryQuery _resultHistoryQuery;
        public RequestHistoryQuery _requestHistoryQuery;
        public ResponseHistoryQuery _responseHistoryQuery;

        public BaseController(DatabaseContex databaseContex)
        {
            _userQuery = new UserQuery(databaseContex);
            _hitHistoryQuery = new HitHistoryQuery(databaseContex);
            _resultHistoryQuery = new ResultHistoryQuery(databaseContex);
            _requestHistoryQuery = new RequestHistoryQuery(databaseContex);
            _responseHistoryQuery = new ResponseHistoryQuery(databaseContex);

        }

         public static String callInternalService(string stringURL, string stringRequest, string stringUserToken)
        {
            string stringModelResponse= "";
            string stringEncodedRequest;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Token", stringUserToken);

            Uri uri = new Uri(stringURL);

            try
            {
                stringEncodedRequest = base64Encode(stringRequest);
                StringContent content = new StringContent(JsonSerializer.Serialize(stringEncodedRequest),UnicodeEncoding.UTF8,"application/json");

                HttpResponseMessage httpResponseMessage = client.PostAsync(uri, content).Result;

                Console.Write(httpResponseMessage.StatusCode);

                httpResponseMessage.EnsureSuccessStatusCode();

                string stringResult = httpResponseMessage.Content.ReadAsStringAsync().Result;

                JsonSerializerOptions serializerOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                stringModelResponse = stringResult;
            }
            catch (Exception exception)
            {
                Console.WriteLine("error message : "+exception.Message);
            }

            return stringModelResponse;
        }

        public static string base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string base64Encode(string stringPlainText)
        {
             var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(stringPlainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}