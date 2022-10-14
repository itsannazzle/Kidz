namespace Kidz.Models;

abstract public class BaseBangunRuangModel
{
    public string Nama { get; set; }
    public int JumlahSisi { get; set; }
    public int JariJari { get; set; }

    public double Result { get; set; }

    //Constructor
    public BaseBangunRuangModel()
    {
        this.Nama = "";
        this.JumlahSisi = 0;
    }

    //Overloading
    public BaseBangunRuangModel(int intJariJari)
    {
        this.JariJari = intJariJari;
    }

    public string detailBangunan()
    {
        return string.Format("Nama bangunan {0} , Jumlah sisi : {1} ",Nama,JumlahSisi);
    }

    //Abstraction
    public abstract double countLuasBangunan();

    
}