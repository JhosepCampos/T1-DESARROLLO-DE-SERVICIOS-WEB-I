namespace T1CAMPOSCRUZJHOSEPANTONY.Entity
{
    public class LibroEntity
    {
        public string CodLibro { get; set; } = string.Empty;
        public string CodCategoria { get; set; } = string.Empty;
        public string NombreCategoria { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int AnioPublicacion { get; set; }
        public int Stock { get; set; }
        public byte Estado { get; set; }
    }
}
