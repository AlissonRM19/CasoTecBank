using TecBankApi.Models;
using TecBankApi.Services;

public class CuentaService
{
    private readonly JsonStorageService _storage;           // Servicio que maneja el almacenamiento de datos en JSON
    private readonly TarjetaService _tarjetaService;        // Servicio para manejar tarjetas asociadas a cuentas

    // Propiedad para acceder a la lista de cuentas desde el almacenamiento centralizado
    private List<Cuenta> Cuentas => _storage.Cuentas;

    // Constructor que inyecta dependencias: almacenamiento JSON y servicio de tarjetas
    public CuentaService(JsonStorageService storage, TarjetaService tarjetaService)
    {
        _storage = storage;
        _tarjetaService = tarjetaService;
    }

    /// <summary>
    /// Devuelve la lista completa de cuentas registradas
    /// </summary>
    public async Task<List<Cuenta>> ObtenerTodasCuentas() => await Task.FromResult(Cuentas);

    /// <summary>
    /// Devuelve todas las cuentas asociadas a un cliente específico
    /// </summary>
    public async Task<List<Cuenta>> ObtenerCuentasPorCliente(int clienteId) =>
        Cuentas.Where(c => c.ClienteId == clienteId).ToList();

    /// <summary>
    /// Crea una nueva cuenta si no existe otra con el mismo número de cuenta
    /// </summary>
    public async Task<bool> CrearCuenta(Cuenta cuenta)
    {
        // Verifica si ya existe una cuenta con ese número
        if (Cuentas.Any(c => c.NumeroCuenta == cuenta.NumeroCuenta))
            return false;

        // Asigna un nuevo ID único
        cuenta.CuentaId = Cuentas.Any() ? Cuentas.Max(c => c.CuentaId) + 1 : 1;

        // Agrega la cuenta a la lista y guarda los datos
        Cuentas.Add(cuenta);
        await _storage.GuardarDatos();
        return true;
    }

    /// <summary>
    /// Realiza una operación de ingreso o retiro sobre una cuenta específica
    /// </summary>
    public async Task<decimal?> RealizarOperacion(int cuentaId, decimal monto, string v)
    {
        var cuenta = Cuentas.FirstOrDefault(c => c.CuentaId == cuentaId);
        if (cuenta == null) return null;

        // Si el monto es negativo (retiro), verifica que haya fondos suficientes
        if (monto < 0 && cuenta.Saldo < Math.Abs(monto))
            return null; // Fondos insuficientes

        // Actualiza el saldo y guarda
        cuenta.Saldo += monto;
        await _storage.GuardarDatos();
        return cuenta.Saldo;
    }

    /// <summary>
    /// Elimina una cuenta del sistema por su ID
    /// </summary>
    public async Task<bool> EliminarCuenta(int id)
    {
        var cuenta = Cuentas.FirstOrDefault(c => c.CuentaId == id);
        if (cuenta == null) return false;

        Cuentas.Remove(cuenta);
        await _storage.GuardarDatos();
        return true ;
    }

    /// <summary>
    /// Obtiene una cuenta específica por su ID
    /// </summary>
    public async Task<Cuenta?> ObtenerCuentaPorId(int id) =>
        Cuentas.FirstOrDefault(c => c.CuentaId == id);

    /// <summary>
    /// Valida si una cuenta pertenece a un cliente específico
    /// </summary>
    public async Task<bool> ValidarPropiedadCuenta(int cuentaId, int clienteId)
    {
        var cuenta = await ObtenerCuentaPorId(cuentaId);
        return cuenta?.ClienteId == clienteId;
    }

    /// <summary>
    /// Obtiene el saldo de una cuenta específica
    /// </summary>
    public async Task<decimal> ObtenerSaldo(int cuentaId)
    {
        var cuenta = await ObtenerCuentaPorId(cuentaId);
        return cuenta?.Saldo ?? 0;
    }
}