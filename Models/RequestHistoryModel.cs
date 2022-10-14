namespace Kidz.Models
{
    public class RequestHistoryModel : BaseModel
    {
        public int ID { set; get; }
        public string URL { set; get; }
        public string RequestPlain { set; get; }
        public string Data { set; get; }
        public string DeviceID { set; get; }
        public string IPAddress { set; get; }
        public DateTime RequestedOn { set; get; }

        public RequestHistoryModel()
        {
            this.ID = 0;
        }

        public void setRequestHistoryModel(RequestModel modelRequest, string stringURL)
        {
            this.URL = stringURL;
            this.Data = modelRequest.Data;
            this.IPAddress = modelRequest.IPAddress;
            this.RequestedOn = DateTime.Now;
        }
    }
}