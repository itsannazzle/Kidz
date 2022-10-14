namespace Kidz.Models;

public class PersegiModel : BaseBangunRuangModel
{
    public int Panjang { set; get; }
    public int Lebar { set; get; }

    public PersegiModel()
    {
        this.Panjang = 0;
        this.Lebar = 0;
    }

     //Overriding
    public override double countLuasBangunan()
    {
        Result = Panjang*Lebar;
        return Panjang*Lebar;
    }
}