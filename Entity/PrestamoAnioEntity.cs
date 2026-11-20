namespace T1CAMPOSCRUZJHOSEPANTONY.Entity
{
    public class PrestamoAnioEntity
    {
        public int IdPrestamo { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public string ApellidosNombres { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string EstadoPrestamo { get; set; } = string.Empty;
        public decimal Multa { get; set; }
    }
}
