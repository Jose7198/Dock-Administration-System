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
    public class D_OrdenDeArrendamiento
    {

        private SqlConnection DB = new SqlConnection(ConfigurationManager.ConnectionStrings["sitedb"].ConnectionString);

        public DataTable SelectOrdenesInfo()
        {
            SqlCommand command = new SqlCommand("sp_listarOrdenesInfo", DB);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SelectOrdenesCont()
        {
            SqlCommand command = new SqlCommand("sp_listarOrdenesCont", DB);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SelectRangoOrdenCont(DateTime fechaMin, DateTime fechaMax)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM ORDENES_CONT WHERE FECHAORDEN BETWEEN @fechaMin AND @fechaMax", DB);
            command.Parameters.AddWithValue("@fechaMin", fechaMin);
            command.Parameters.AddWithValue("@fechaMax", fechaMax);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SelectRangoOrdenInfo(DateTime fechaMin, DateTime fechaMax)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM ORDENES_INFO WHERE (FECHAINICIO BETWEEN @fechaMin AND @fechaMax) OR (FECHAFIN BETWEEN @fechaMin AND @fechaMax)", DB);
            command.Parameters.AddWithValue("@fechaMin", fechaMin);
            command.Parameters.AddWithValue("@fechaMax", fechaMax);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SelectOrdenesInfo(DateTime fecha)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM ORDENES_INFO WHERE FECHAINICIO = @fecha OR FECHAFIN = @fecha", DB);
            command.Parameters.AddWithValue("@fecha", fecha);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SelectOrdenesInfo(string criterio, string value)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM ORDENES_INFO WHERE UPPER("+criterio+") LIKE '%' + UPPER(@value) + '%'", DB);
            command.Parameters.AddWithValue("@value", value);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SelectOrdenesInfo(string criterio, double value)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM ORDENES_INFO WHERE " + criterio + " = @value", DB);
            command.Parameters.AddWithValue("@value", value);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SelectOrdenesCont(string criterio, double value)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM ORDENES_CONT WHERE " + criterio + " = @value", DB);
            command.Parameters.AddWithValue("@value", value);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SelectOrdenesCont(DateTime fecha)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM ORDENES_CONT WHERE FECHAORDEN = @fecha", DB);
            command.Parameters.AddWithValue("@fecha", fecha);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public bool InsertOrdenes(int ordenID, string clienteID, int puertoID, int puestoID, string empleadoID, DateTime fechaInicio, DateTime fechaFin, double cuota)
        {
            bool isSuccess = false;
            try
            {
                SqlCommand command = new SqlCommand("sp_insertarOrden", DB);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ordenID", ordenID);
                command.Parameters.AddWithValue("@clienteID", clienteID);
                command.Parameters.AddWithValue("@puertoID", puertoID);
                command.Parameters.AddWithValue("@puestoID", puestoID);
                command.Parameters.AddWithValue("@empleadoID", empleadoID);
                command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                command.Parameters.AddWithValue("@fechaFin", fechaFin);
                command.Parameters.AddWithValue("@cuota", cuota);
                DB.Open();
                int rows = command.ExecuteNonQuery();
                if (rows == 2)
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

        public bool DeleteOrdenes(int ordenID, int puertoID, int puestoID)
        {
            bool isSuccess = false;
            try
            {
                SqlCommand command = new SqlCommand("sp_eliminarOrden", DB);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ordenID", ordenID);
                command.Parameters.AddWithValue("@puertoID", puertoID);
                command.Parameters.AddWithValue("@puestoID", puestoID);
                DB.Open();
                int rows = command.ExecuteNonQuery();
                if (rows == 2)
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

        public bool UpdateOrdenes(int ordenID, string clienteID, int puertoID, int puestoID, string empleadoID, DateTime fechaInicio, DateTime fechaFin, double cuota)
        {
            bool isSuccess = false;
            try
            {
                SqlCommand command = new SqlCommand("sp_actualizarOrden", DB);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ordenID", ordenID);
                command.Parameters.AddWithValue("@clienteID", clienteID);
                command.Parameters.AddWithValue("@puertoID", puertoID);
                command.Parameters.AddWithValue("@puestoID", puestoID);
                command.Parameters.AddWithValue("@empleadoID", empleadoID);
                command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                command.Parameters.AddWithValue("@fechaFin", fechaFin);
                command.Parameters.AddWithValue("@cuota", cuota);
                DB.Open();
                int rows = command.ExecuteNonQuery();
                if (rows == 2)
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
