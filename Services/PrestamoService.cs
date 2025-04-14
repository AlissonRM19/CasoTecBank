
using TecBankApi.Models;

namespace TecBankApi.Services
{
    public class PrestamoService
    {
        private readonly List<Prestamo> _prestamos;

        public PrestamoService()
        {
            // Ejemplo de lista provisional. En producción esto vendría de una base de datos o JSON.
            _prestamos = new List<Prestamo>();
        }

        /// <summary>
        /// Genera un reporte de comisiones para un asesor en un mes y año específicos.
        /// </summary>
        public ReporteComisiones GenerarReporteComisiones(int asesorId, int mes, int año)
        {
            var prestamos = _prestamos
                .Where(p => p.Ced_acesor == asesorId &&
                            p.FechaAprobacion.Month == mes &&
                            p.FechaAprobacion.Year == año)
                .ToList();

            return new ReporteComisiones
            {
                TotalColones = prestamos.Where(p => p.Moneda == "CRC").Sum(p => p.Monto_Original),
                TotalDolares = prestamos.Where(p => p.Moneda == "USD").Sum(p => p.Monto_Original),
                ComisionesColones = prestamos.Where(p => p.Moneda == "CRC").Sum(p => p.Monto_Original * 0.03m),
                ComisionesDolares = prestamos.Where(p => p.Moneda == "USD").Sum(p => p.Monto_Original * 0.03m)
            };
        }

        /// <summary>
        /// Agrega un nuevo préstamo (para pruebas o para persistencia si no hay base de datos).
        /// </summary>
        public void AgregarPrestamo(Prestamo prestamo)
        {
            _prestamos.Add(prestamo);
        }

        public List<Prestamo> ObtenerTodos()
        {
            return _prestamos;
        }

        public Prestamo? ObtenerPorId(int id)
        {
            return _prestamos.FirstOrDefault(p => p.Id_Prestamo == id);
        }

        public void EliminarPrestamo(int id)
        {
            var prestamo = ObtenerPorId(id);
            if (prestamo != null)
                _prestamos.Remove(prestamo);
        }

        public void ActualizarPrestamo(Prestamo actualizado)
        {
            var existente = ObtenerPorId(actualizado.Id_Prestamo);
            if (existente != null)
            {
                //existente.Monto_original = actualizado.Monto_original;
                existente.Saldo = actualizado.Saldo;
                existente.Ced_Cliente = actualizado.Ced_Cliente;
                existente.Interes = actualizado.Interes;
                existente.Ced_acesor = actualizado.Ced_acesor;
                //existente.Moneda = actualizado.Moneda;
                existente.FechaAprobacion = actualizado.FechaAprobacion;
            }
        }

        public class ReporteComisiones
        {
            public decimal TotalColones { get; set; }
            public decimal TotalDolares { get; set; }
            public decimal ComisionesColones { get; set; }
            public decimal ComisionesDolares { get; set; }
        }
    }
}
