using TecBankApi.Models;

namespace TecBankApi.Services
{
    public class PagoPrestamoService
    {
        private readonly PrestamoService _prestamoService;
        private readonly List<PagoPrestamo> _pagos;

        public PagoPrestamoService(PrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
            _pagos = new List<PagoPrestamo>();
        }

        // Obtener todos los pagos
        public List<PagoPrestamo> GetAll()
        {
            return _pagos;
        }

        // Obtener un pago por ID
        public PagoPrestamo? GetById(int id)
        {
            return _pagos.FirstOrDefault(p => p.Id_pago == id);
        }

        // Crear un nuevo pago y aplicarlo al préstamo
        public bool Create(PagoPrestamo pago)
        {
            var prestamo = _prestamoService.ObtenerPorId(pago.Id_prestamo);
            if (prestamo == null) return false;

            // Aplicar pago
            prestamo.Saldo -= pago.Monto_pagado;
            if (prestamo.Saldo < 0) prestamo.Saldo = 0;

            // Generar ID de pago
            pago.Id_pago = _pagos.Any() ? _pagos.Max(p => p.Id_pago) + 1 : 1;
            pago.Fecha = DateTime.Now;

            // Guardar pago
            _pagos.Add(pago);
            _prestamoService.ActualizarPrestamo(prestamo);

            return true;
        }

        // Actualizar un pago
        public void Update(PagoPrestamo pagoActualizado)
        {
            var index = _pagos.FindIndex(p => p.Id_pago == pagoActualizado.Id_pago);
            if (index >= 0)
            {
                _pagos[index] = pagoActualizado;
            }
        }

        // Eliminar un pago
        public void Delete(int id)
        {
            var pago = GetById(id);
            if (pago != null)
            {
                _pagos.Remove(pago);
            }
        }
    }
}
