using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class D_Puerto
    {
        private SqlConnection DB = new SqlConnection(ConfigurationManager.ConnectionStrings["sitedb"].ConnectionString);

        public DataTable ListarPuertos()
        {
            SqlCommand command = new SqlCommand("sp_listarPuertos", DB);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public bool DeletePuerto(int puertoID)
        {
            bool isSuccess = false;
            try
            {
                SqlCommand command = new SqlCommand("sp_eliminarPuerto", DB);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idPuerto", puertoID);
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

        public bool InsertPuerto(int idPuerto, string nombre, int capacidad, string ciudad, string direccion)
        {
            bool isSuccess = false;
            try
            {
                SqlCommand command = new SqlCommand("sp_ingresarPuerto", DB);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_Puerto",idPuerto);
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@capacidad", capacidad);
                command.Parameters.AddWithValue("@ciudad", ciudad);
                command.Parameters.AddWithValue("@direccion", direccion);
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

        public bool UpdatePuerto(int idPuerto, string nombre, int capacidad, string ciudad, string direccion)
        {
            bool isSuccess = false;
            try
            {
                SqlCommand command = new SqlCommand("sp_actualizarPuerto", DB);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_Puerto", idPuerto);
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@capacidad", capacidad);
                command.Parameters.AddWithValue("@ciudad", ciudad);
                command.Parameters.AddWithValue("@direccion", direccion);
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

        public DataTable SelectTextPuerto(string criterioDeBusqueda, string parametro)
        {
            SqlCommand command= new SqlCommand("SELECT * FROM PUERTO WHERE UPPER(" + criterioDeBusqueda + ") LIKE '%' + UPPER(@pam) + '%'", DB);
            command.Parameters.AddWithValue("@pam", parametro);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SelectNumeroPuerto(string criterioDeBusqueda, int parametro)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM PUERTO WHERE " + criterioDeBusqueda + " = @pam", DB);
            command.Parameters.AddWithValue("@pam", parametro);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SelectRangoPuerto(int min, int max)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM PUERTO WHERE CAPACIDAD BETWEEN @min AND @max", DB);
            command.Parameters.AddWithValue("@min", min);
            command.Parameters.AddWithValue("@max", max);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

    }
}
