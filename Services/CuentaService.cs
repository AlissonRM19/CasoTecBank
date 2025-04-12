// Clase que gestiona las operaciones relacionadas con cuentas bancarias
public class CuentaService
{
    // Servicio de almacenamiento en formato JSON
    private readonly JsonStorageService _storage;

    // Lista de cuentas cargadas desde el almacenamiento
    private List<Cuenta> _cuentas;

    // Constructor que inicializa el servicio de almacenamiento y carga las cuentas
    public CuentaService(JsonStorageService storage)
    {
        _storage = storage;
        _cuentas = _storage.LoadData<List<Cuenta>>().Result ?? new List<Cuenta>(); // Carga las cuentas o crea una lista vacía si no hay datos
    }

    // Método para obtener todas las cuentas registradas
    public async Task<List<Cuenta>> ObtenerTodasCuentas() => await Task.FromResult(_cuentas);

    // Método para obtener todas las cuentas asociadas a un cliente específico
    public async Task<List<Cuenta>> ObtenerCuentasPorCliente(int clienteId) =>
        await Task.FromResult(_cuentas.Where(c => c.ClienteId == clienteId).ToList());

    // Método para crear una nueva cuenta si no existe otra con el mismo número
    public async Task<bool> CrearCuenta(Cuenta cuenta)
    {
        // Verifica si ya existe una cuenta con el mismo número
        if (_cuentas.Any(c => c.NumeroCuenta == cuenta.NumeroCuenta)) return false;

        // Asigna un nuevo ID a la cuenta de forma incremental
        cuenta.CuentaId = _cuentas.Any() ? _cuentas.Max(c => c.CuentaId) + 1 : 1;

        // Agrega la cuenta a la lista
        _cuentas.Add(cuenta);

        // Guarda los cambios en el almacenamiento
        return await GuardarCambios();
    }

    // Método para realizar una operación de depósito o retiro en una cuenta
    public async Task<decimal?> RealizarOperacion(int cuentaId, decimal monto, string tipo)
    {
        // Busca la cuenta por ID
        var cuenta = _cuentas.FirstOrDefault(c => c.CuentaId == cuentaId);
        if (cuenta == null) return null; // Si no existe, devuelve null

        // Verifica si el retiro es posible (fondos suficientes)
        if (tipo == "Retiro" && cuenta.Saldo < Math.Abs(monto)) return decimal.MinValue;

        // Aplica el monto (positivo o negativo) al saldo
        cuenta.Saldo += monto;

        // Guarda los cambios y retorna el nuevo saldo
        await GuardarCambios();
        return cuenta.Saldo;
    }

    // Método que realiza una transacción de débito asociada a una tarjeta de débito
    public async Task<bool> RealizarTransaccionDebito(string numeroTarjeta, decimal monto)
    {
        // Se obtiene la tarjeta correspondiente al número proporcionado
        var tarjeta = await _tarjetaService.ObtenerTarjeta(numeroTarjeta);

        // Se valida que la tarjeta exista y que sea del tipo "Débito"
        if (tarjeta?.Tipo != "Débito") return false;

        // Se obtiene la cuenta asociada a la tarjeta
        var cuenta = await _cuentaService.ObtenerCuenta(tarjeta.CuentaAsociada);

        // Se verifica que la cuenta exista y que tenga fondos suficientes
        if (cuenta?.Saldo < monto) return false;

        // Se descuenta el monto solicitado del saldo de la cuenta
        cuenta.Saldo -= monto;

        // Se actualiza el saldo de la tarjeta (aunque en débito, puede mantenerse por consistencia)
        await _tarjetaService.ActualizarSaldo(numeroTarjeta, monto);

        // Se confirma que la transacción fue realizada con éxito
        return true;
    }

    // Método para eliminar una cuenta por ID
    public async Task<bool> EliminarCuenta(int id)
    {
        // Busca la cuenta por ID
        var cuenta = _cuentas.FirstOrDefault(c => c.CuentaId == id);
        if (cuenta == null) return false; // Si no existe, retorna false

        // Elimina la cuenta de la lista
        _cuentas.Remove(cuenta);

        // Guarda los cambios en el almacenamiento
        return await GuardarCambios();
    }

    // Método privado para guardar los cambios en el almacenamiento JSON
    private async Task<bool> GuardarCambios()
    {
        try
        {
            await _storage.SaveData(_cuentas); // Intenta guardar los datos
            return true; // Éxito
        }
        catch
        {
            return false; // Fallo
        }
    }
}