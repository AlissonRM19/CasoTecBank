public class Prestamo
{
    #region Variable Publica

    private int _id_Prestamo;
    private int _monto_original;
    private int _saldo;
    private int _ced_Cliente;
    private int _interes;
    private int _ced_acesor;

    #endregion

    #region Metodos

    public Prestamo(int id_Prestamo, int monto_original, int saldo, int ced_Cliente, int interes, int ced_acesor)
    {
        _id_Prestamo = id_Prestamo;
        _monto_original = monto_original;
        _saldo = saldo;
        _ced_Cliente = ced_Cliente;
        _interes = interes;
        _ced_acesor = ced_acesor;
    }
    public Prestamo() 
    {

    }

    #endregion

    #region Variables Publicas

    public int Id_Prestamo { get => _id_Prestamo; set => _id_Prestamo = value; }
    public int Monto_original { get => _monto_original; set => _monto_original = value; }
    public int Saldo { get => _saldo; set => _saldo = value; }
    public int Ced_Cliente { get => _ced_Cliente; set => _ced_Cliente = value; }
    public int Interes { get => _interes; set => _interes = value; }
    public int Ced_acesor { get => _ced_acesor; set => _ced_acesor = value; }

    #endregion
}