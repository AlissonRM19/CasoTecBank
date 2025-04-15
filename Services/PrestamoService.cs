using TecBankApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TecBankApi.Services
{
    public class PrestamoService
    {
        public List<Prestamo> Prestamos { get; set; }

        public PrestamoService()
        {
            Prestamos = new List<Prestamo>();
        }

        public Prestamo? ObtenerPorId(int id)
        {
            return Prestamos.FirstOrDefault(p => p.Id_Prestamo == id);
        }


        public void Add(Prestamo prestamo)
        {
            Prestamos.Add(prestamo);
        }

        public void Remove(Prestamo prestamo)
        {
            Prestamos.Remove(prestamo);
        }

        public void SaveChanges()
        {
            // Como no hay base de datos real, no se hace nada aquí.
            // En un proyecto real, aquí se guardarían los cambios al JSON o DB.
        }

        public void Entry(Prestamo prestamo)
        {
            // En EF esto marca un objeto como modificado.
            // Aquí no hace nada porque la lista es en memoria.
            // Esta función se deja para compatibilidad con el controlador.
        }

        public async Task<ReporteComisiones> GenerarReporteComisiones(int asesorId, int mes, int año)
        {
            var prestamos = Prestamos
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

        public void ActualizarPrestamo(Prestamo actualizado)
        {
            var existente = ObtenerPorId(actualizado.Id_Prestamo);
            if (existente != null)
            {
                existente.Saldo = actualizado.Saldo;
                existente.Ced_Cliente = actualizado.Ced_Cliente;
                existente.Interes = actualizado.Interes;
                existente.Ced_acesor = actualizado.Ced_acesor;
                existente.FechaAprobacion = actualizado.FechaAprobacion;
            }
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
