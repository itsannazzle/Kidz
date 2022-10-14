using Kidz.Constants;

namespace Kidz.Models
{
    public class HitHistoryModel : BaseModel
    {
        public int ID { set; get; }
        public string URL { set; get; }
        public string Data { set; get; }
        public string ClientDeviceID { set; get; }
        public string IPAddress { set; get; }
        public DateTime? RequestedOn { set; get; }


        public HitHistoryModel()
        {
            this.ID = 0;
        }

        public void setHitHistoryFromRequest(RequestModel modelRequest, String stringUrl)
        {
            this.URL  = stringUrl;
            this.Data = modelRequest.Data;
            this.ClientDeviceID = modelRequest.ClientDeviceID;
            this.IPAddress = modelRequest.IPAddress;
            this.RequestedBy = modelRequest.RequestedBy;
            this.RequestedOn = DateTime.Now;

        }

    }

}