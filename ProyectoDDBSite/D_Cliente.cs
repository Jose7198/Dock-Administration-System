using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class D_Cliente
    {

        private SqlConnection DB = new SqlConnection(ConfigurationManager.ConnectionStrings["sitedb"].ConnectionString);

        public DataTable SelectCliente()
        {
            SqlCommand command = new SqlCommand("sp_listarClientes", DB);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public int PuertoActual()
        {
            SqlCommand command = new SqlCommand("sp_puertoID", DB);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@idPuerto", SqlDbType.Int).Direction = ParameterDirection.Output;
            DB.Open();
            command.ExecuteNonQuery();
            int puertoID = Convert.ToInt16(command.Parameters["@idPuerto"].Value);
            DB.Close();
            return puertoID;
        }

        public DataTable SelectRangoCliente(DateTime fechaMin, DateTime fechaMax)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM CLIENTE WHERE FECHA_NAC BETWEEN @min AND @max", DB);
            command.Parameters.AddWithValue("@min", fechaMin);
            command.Parameters.AddWithValue("@max", fechaMax);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SelectTextoCliente(string criterioDeBusqueda, string atributo)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM CLIENTE WHERE UPPER(" + criterioDeBusqueda + ") LIKE '%' + UPPER(@pam) + '%'", DB);
            command.Parameters.AddWithValue("@pam", atributo);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public bool InsertCliente(string idCliente, string idPuerto, string nombres, string apellidos, string direccion, string telefono, string correo, DateTime fecha)
        {
            bool isSuccess = false;
            try
            {
                SqlCommand command = new SqlCommand("sp_insertarCliente", DB);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_Cliente", idCliente);
                command.Parameters.AddWithValue("@id_Puerto", idPuerto);
                command.Parameters.AddWithValue("@nombre", nombres);
                command.Parameters.AddWithValue("@apellido", apellidos);
                command.Parameters.AddWithValue("@direccion", direccion);
                command.Parameters.AddWithValue("@telefono", telefono);
                command.Parameters.AddWithValue("@correo", correo);
                command.Parameters.AddWithValue("@fecha", fecha);
                DB.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }

            }
            catch
            {

            }
            finally
            {
                DB.Close();
            }
            return isSuccess;
        }

        public bool UpdateCliente(string idCliente, string idPuerto, string nombres, string apellidos, string direccion, string telefono, string correo, DateTime fecha)
        {
            bool isSuccess = false;
            try
            {
                SqlCommand command = new SqlCommand("sp_actualizarCliente", DB);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_Cliente", idCliente);
                command.Parameters.AddWithValue("@id_Puerto", idPuerto);
                command.Parameters.AddWithValue("@nombre", nombres);
                command.Parameters.AddWithValue("@apellido", apellidos);
                command.Parameters.AddWithValue("@direccion", direccion);
                command.Parameters.AddWithValue("@telefono", telefono);
                command.Parameters.AddWithValue("@correo", correo);
                command.Parameters.AddWithValue("@fecha", fecha);
                DB.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }

            }
            catch
            {

            }
            finally
            {
                DB.Close();
            }
            return isSuccess;
        }

        public bool DeleteCliente(int puertoID, string clienteID)
        {
            bool isSuccess = false;
            try
            {
                SqlCommand command = new SqlCommand("sp_eliminarCliente", DB);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idPuerto", puertoID);
                command.Parameters.AddWithValue("@idCliente", clienteID);
                DB.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
            }
            catch
            {

            }
            finally
            {
                DB.Close();
            }
            return isSuccess;
        }

    }
}
