using System.Data;
using System.Data.SqlClient;

namespace PruebaTecnica.App.Database
{
    public class Conexion
    {
        private readonly string? _connectionString;

        public Conexion(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("UserRegistryApp");
        }

        public DataTable GetData(string query, SqlParameter[] parametros = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    if (parametros != null)
                    {
                        cmd.Parameters.AddRange(parametros);
                    }
                    connection.Open();
                    DataTable dataTable = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                    return dataTable;
                }
            }
        }

        public object GetScalar(string query, SqlParameter[] parametros = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                    {
                        cmd.Parameters.AddRange(parametros);
                    }
                    connection.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        public void ExecSP(string nombreSP, SqlParameter[] parametros = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(nombreSP, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                    {
                        cmd.Parameters.AddRange(parametros);
                    }
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataSet ExecSPWithOutput(string nombreSP, SqlParameter[] parametros = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(nombreSP, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                    {
                        cmd.Parameters.AddRange(parametros);
                    }

                    DataSet dataset = new DataSet();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        connection.Open();
                        adapter.Fill(dataset);
                    }

                    return dataset;
                }
            }
        }
    }
}
