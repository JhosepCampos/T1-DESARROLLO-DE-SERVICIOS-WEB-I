using Microsoft.Data.SqlClient;

namespace T1CAMPOSCRUZJHOSEPANTONY.Data
{
    public class ConexionBD
    {
        private readonly string _cnn;        
        public ConexionBD(IConfiguration configuration)
        {
            _cnn = configuration.GetConnectionString("BDBIBLIOTECA2026") ?? throw new InvalidOperationException("No se encontró la cadena 'BDBIBLIOTECA2026'.");
        }

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_cnn);
        }
        
        private static bool _ultimoEstado = false;
        private static string _ultimoMensaje = "Verificando...";
        private static DateTime _ultimaRevision = DateTime.MinValue;
        public bool ProbarConexion(out string mensaje)
        {
            if ((DateTime.Now - _ultimaRevision).TotalSeconds < 10)
            {
                mensaje = _ultimoMensaje;
                return _ultimoEstado;
            }

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(_cnn) { ConnectTimeout = 1 };
                using (SqlConnection ctx = new SqlConnection(builder.ConnectionString))
                {
                    ctx.Open();
                    _ultimoEstado = true;
                    _ultimoMensaje = "Conexión exitosa con SQL Server";
                }
            }
            catch (Exception ex)
            {
                _ultimoEstado = false;
                _ultimoMensaje = "Sin conexión a BD: " + ex.Message;
            }

            _ultimaRevision = DateTime.Now;
            mensaje = _ultimoMensaje;
            return _ultimoEstado;
        }
    }
}
