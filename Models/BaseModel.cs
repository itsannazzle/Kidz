namespace Kidz.Models;

public class BaseModel
{
    public DateTime CreatedOn { set; get; }
    public string RequestedBy { set;  get; }

    public BaseModel()
    {
        this.CreatedOn = DateTime.Now;
    }

}