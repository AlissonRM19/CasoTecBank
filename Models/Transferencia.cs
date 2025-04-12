public class Transferencia
{
    #region Variables Privadas

    private int _ID_transferencia;
    private int _cuenta_destino;
    private int _cuenta_Origen;
    private int _monto;
    private DateTime _fecha;

    #endregion

    #region Metodos

    public Transferencia(int iD_transferencia, int cuenta_destino, int cuenta_Origen, int monto, DateTime fecha)
    {
        _ID_transferencia = iD_transferencia;
        _cuenta_destino = cuenta_destino;
        _cuenta_Origen = cuenta_Origen;
        _monto = monto;
        _fecha = fecha;
    }
    public Transferencia()
    {

    }

    #endregion

    #region Varibles publicas

    public int ID_transferencia { get => _ID_transferencia; set => _ID_transferencia = value; }
    public int Cuenta_destino { get => _cuenta_destino; set => _cuenta_destino = value; }
    public int Cuenta_Origen { get => _cuenta_Origen; set => _cuenta_Origen = value; }
    public int Monto { get => _monto; set => _monto = value; }
    public DateTime Fecha { get => _fecha; set => _fecha = value; }

    #endregion
}