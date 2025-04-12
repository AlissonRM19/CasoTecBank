using static System.Runtime.InteropServices.JavaScript.JSType;

public class Rol
{
    #region Variables Privadas

    private string? _Nombre;
    private string? _Descripcion;

    #endregion

    #region Metodos

    public Rol(string nombre, string descripcion)
    {
        _Nombre = nombre;
        _Descripcion = descripcion;
    }

    public Rol()
    {
        _Nombre = string.Empty;
        _Descripcion = string.Empty;
    }

    #endregion

    #region Variables Publicas

    public string Nombre { get => _Nombre; set => _Nombre = value; }
    public string Descripcion { get => _Descripcion; set => _Descripcion = value; }

    #endregion
}