namespace Kidz.Models
{
    public class RequestModel : BaseModel
    {
        public string IPAddress { set; get; }
        public string ProjectCode { set; get; }
        public string ClientDeviceID { set; get; }
        public string Data { set; get; }

        public RequestModel()
        {

        }
    }
}