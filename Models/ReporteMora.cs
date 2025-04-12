public class ReporteMora
{
    #region Variables Privadas

    private int _ID_ReporteMora;
    private int _ced_Cliente;
    private int _Cedula_Cliente;
    private int _Numero_Prestamo;
    private int _Cuotas_Vencidas;
    private int _Monto_Adecuado;

    #endregion

    #region Metodos

    public ReporteMora(int iD_ReporteMora, int cliente, int cedula_Cliente, int numero_Prestamo, int cuotas_Vencidas, int monto_Adecuado)
    {
        _ID_ReporteMora = iD_ReporteMora;
        _ced_Cliente = cliente;
        _Cedula_Cliente = cedula_Cliente;
        _Numero_Prestamo = numero_Prestamo;
        _Cuotas_Vencidas = cuotas_Vencidas;
        _Monto_Adecuado = monto_Adecuado;
    }
    public ReporteMora()
    {

    }

    #endregion

    #region Variables Publicas

    public int ID_ReporteMora { get => _ID_ReporteMora; set => _ID_ReporteMora = value; }
    public int ced_Cliente { get => _ced_Cliente; set => _ced_Cliente = value; }
    public int Cedula_Cliente { get => _Cedula_Cliente; set => _Cedula_Cliente = value; }
    public int Numero_Prestamo { get => _Numero_Prestamo; set => _Numero_Prestamo = value; }
    public int Cuotas_Vencidas { get => _Cuotas_Vencidas; set => _Cuotas_Vencidas = value; }
    public int Monto_Adecuado { get => _Monto_Adecuado; set => _Monto_Adecuado = value; }

    #endregion
}