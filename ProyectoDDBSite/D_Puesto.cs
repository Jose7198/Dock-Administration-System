using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class D_Puesto
    {

        private SqlConnection DB = new SqlConnection(ConfigurationManager.ConnectionStrings["sitedb"].ConnectionString);
        
        public DataTable ListarPuestos()
        {
            SqlCommand command = new SqlCommand("sp_listarPuestos", DB);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable ListarPuestosDisponibles()
        {
            SqlCommand command = new SqlCommand("sp_listarPuestosDisponibles", DB);
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

        public DataTable SelectEstadoPuesto(string criterioDeBusqueda, int parametro)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM PUESTO WHERE ESTADO = @pam", DB);
            command.Parameters.AddWithValue("@pam", parametro);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SelectNumeroPuesto(string criterioDeBusqueda, double parametro)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM PUESTO WHERE " + criterioDeBusqueda + " = @pam", DB);
            command.Parameters.AddWithValue("@pam", parametro);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SelectRangoPuesto(double minAncho, double maxAncho, double minLargo, double maxLargo)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM PUESTO WHERE ANCHO BETWEEN @minAncho AND @maxAncho AND LARGO BETWEEN @minLargo AND @maxAncho", DB);
            command.Parameters.AddWithValue("@minAncho", minAncho);
            command.Parameters.AddWithValue("@maxAncho", maxAncho);
            command.Parameters.AddWithValue("@minLargo", minLargo);
            command.Parameters.AddWithValue("@maxLargo", maxLargo);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public bool UpdateEstadoPuesto(int idPuerto, int idPuesto)
        {
            bool isSuccess = false;
            try
            {
                SqlCommand command = new SqlCommand("sp_cambiarEstadoPuesto", DB);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@puertoID", idPuerto);
                command.Parameters.AddWithValue("@puestoID", idPuesto);
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
