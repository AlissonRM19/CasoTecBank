public class ReporteAsesoresCredito
{
    #region Variables Publicas

    private int _id;
    private int _ced_Asesor;
    private int _Meta_Colones;
    private int _meta_Dolares;
    private int _Total_Colones;
    private int _Total_Dolares;
    private int _Comicion_Colones;
    private int _Comicion_Dolares;

    #endregion

    #region Metodos

    public ReporteAsesoresCredito(int id, int asesor, int meta_Colones, int meta_Dolares, int total_Colones, int total_Dolares, int comicion_Colones, int comicion_Dolares)
    {
        _id = id;
        _ced_Asesor = asesor;
        _Meta_Colones = meta_Colones;
        _meta_Dolares = meta_Dolares;
        _Total_Colones = total_Colones;
        _Total_Dolares = total_Dolares;
        _Comicion_Colones = comicion_Colones;
        _Comicion_Dolares = comicion_Dolares;
    }

    public ReporteAsesoresCredito()
    {

    }

    #endregion

    #region Variables Publicas

    public int Id { get => _id; set => _id = value; }
    public int Asesor { get => _ced_Asesor; set => _ced_Asesor = value; }
    public int Meta_Colones { get => _Meta_Colones; set => _Meta_Colones = value; }
    public int Meta_Dolares { get => _meta_Dolares; set => _meta_Dolares = value; }
    public int Total_Colones { get => _Total_Colones; set => _Total_Colones = value; }
    public int Total_Dolares { get => _Total_Dolares; set => _Total_Dolares = value; }
    public int Comicion_Colones { get => _Comicion_Colones; set => _Comicion_Colones = value; }
    public int Comicion_Dolares { get => _Comicion_Dolares; set => _Comicion_Dolares = value; }

    #endregion
}