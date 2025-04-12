public class Tarjeta
{
    #region Variables Privadas

    private int _N_Tarjeta;
    private string? _Tipo_Tarjeta;
    private int _ced_Cliente;
    private DateTime _Fecha_Expira;
    private int _N_Seguridad;
    private int? _Credito;
    private int? _Saldo;
    private int _N_Cuenta;

    #endregion

    #region Metodos

    public Tarjeta() 
    {
        
    }
    public Tarjeta(int n_Tarjeta, string? tipo_Tarjeta, int ced_Cliente, DateTime fecha_Expira, int n_Seguridad, int? credito, int? saldo, int n_Cuenta)
    {
        _N_Tarjeta = n_Tarjeta;
        _Tipo_Tarjeta = tipo_Tarjeta;
        _ced_Cliente = ced_Cliente;
        _Fecha_Expira = fecha_Expira;
        _N_Seguridad = n_Seguridad;
        _Credito = credito;
        _Saldo = saldo;
        _N_Cuenta = n_Cuenta;
    }

    #endregion

    #region Variables Publicas

    public int N_Tarjeta { get => _N_Tarjeta; set => _N_Tarjeta = value; }
    public string Tipo_Tarjeta { get => _Tipo_Tarjeta; set => _Tipo_Tarjeta = value; }
    public DateTime Fecha_Expira { get => _Fecha_Expira; set => _Fecha_Expira = value; }
    public int N_Seguridad { get => _N_Seguridad; set => _N_Seguridad = value; }
    public int N_Cuenta { get => _N_Cuenta; set => _N_Cuenta = value; }
    public int? Credito { get => _Credito; set => _Credito = value; }
    public int? Saldo { get => _Saldo; set => _Saldo = value; }
    public int Ced_Cliente { get => _ced_Cliente; set => _ced_Cliente = value; }

    #endregion
}