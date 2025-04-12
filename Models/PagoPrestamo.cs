public class PagoPrestamo
{
    #region Variables Privadas

    private int _id_pago;
    private int _monto_pagado;
    private DateTime _fecha;
    private string? _tipo_pago;
    private int _id_prestamo;

    #endregion

    #region Metodos

    public PagoPrestamo(int id_pago, int monto_pagado, DateTime fecha, string tipo_pago, int id_prestamo)
    {
        _id_pago = id_pago;
        _monto_pagado = monto_pagado;
        _fecha = fecha;
        _tipo_pago = tipo_pago;
        _id_prestamo = id_prestamo;
    }
    public PagoPrestamo()
    {

    }

    #endregion

    #region Variables Publicas

    public int Id_pago { get => _id_pago; set => _id_pago = value; }
    public int Monto_pagado { get => _monto_pagado; set => _monto_pagado = value; }
    public DateTime Fecha { get => _fecha; set => _fecha = value; }
    public string Tipo_pago { get => _tipo_pago; set => _tipo_pago = value; }
    public int Id_prestamo { get => _id_prestamo; set => _id_prestamo = value; }

    #endregion
}