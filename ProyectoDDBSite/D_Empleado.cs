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
    public class D_Empleado
    {

        private SqlConnection DB = new SqlConnection(ConfigurationManager.ConnectionStrings["sitedb"].ConnectionString);

        public DataTable ListarEmpleadosInfo()
        {
            SqlCommand command = new SqlCommand("sp_listarEmpleadosInfo", DB);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable ListarEmpleadosCont()
        {
            SqlCommand command = new SqlCommand("sp_listarEmpleadosCont", DB);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable BuscarPorPuerto(int puerto)
        {
            SqlCommand command = new SqlCommand("sp_listarPorPuerto", DB);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@idPuerto", SqlDbType.Int).Value =puerto;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SelectRangoEmpleadoInfo(DateTime fechaMin, DateTime fechaMax)
        {
            SqlCommand command = new SqlCommand("sp_listarEmpleadoPorRangoInf", DB);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechaMin", fechaMin);
            command.Parameters.AddWithValue("@fechaMax", fechaMax);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SelectEmpleadoInfo(string criterioDeBusqueda, string value)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM EMPLEADOS_INFO WHERE UPPER(" + criterioDeBusqueda + ") LIKE '%' + UPPER(@valor) + '%' ", DB);
            command.Parameters.AddWithValue("@valor", value);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SelectEmpleadoCont(string criterioDeBusqueda, string value)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM EMPLEADOS_CONT WHERE UPPER(" + criterioDeBusqueda + ") LIKE '%' + UPPER(@valor) + '%' ", DB);
            command.Parameters.AddWithValue("@valor", value);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SelectEmpleadoInfo(string criterioDeBusqueda, int value)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM EMPLEADOS_INFO WHERE UPPER(" + criterioDeBusqueda + ") LIKE '%' + UPPER(@valor) + '%' ", DB);
            command.Parameters.AddWithValue("@valor", value);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable SelectEmpleadoCont(string criterioDeBusqueda, double value)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM EMPLEADOS_CONT WHERE UPPER(" + criterioDeBusqueda + ") LIKE '%' + UPPER(@valor) + '%' ", DB);
            command.Parameters.AddWithValue("@valor", value);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public bool InsertEmpleado(string idEmpleado, int idPuerto, string nombres, string apellidos, DateTime fechaNac, string superior, double salario, string cargo)
        {
            bool isSuccess = false;
            try
            {
                SqlCommand command = new SqlCommand("sp_insertarEmpleado", DB);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_Empleado", idEmpleado);
                command.Parameters.AddWithValue("@id_Puerto", idPuerto);
                command.Parameters.AddWithValue("@nombre", nombres);
                command.Parameters.AddWithValue("@apellido", apellidos);
                command.Parameters.AddWithValue("@fecha_Nac", fechaNac);
                command.Parameters.AddWithValue("@superior", superior);
                command.Parameters.AddWithValue("@salario", salario);
                command.Parameters.AddWithValue("@cargo", cargo);
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

        public bool UpdateEmpleado(string idEmpleado, int idPuerto, string nombres, string apellidos, DateTime fechaNac, string superior, double salario, string cargo)
        {
            bool isSuccess = false;
            try
            {
                SqlCommand command = new SqlCommand("sp_actualizarEmpleado", DB);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_Empleado", idEmpleado);
                command.Parameters.AddWithValue("@id_Puerto", idPuerto);
                command.Parameters.AddWithValue("@nombre", nombres);
                command.Parameters.AddWithValue("@apellido", apellidos);
                command.Parameters.AddWithValue("@fecha_Nac", fechaNac);
                command.Parameters.AddWithValue("@superior", superior);
                command.Parameters.AddWithValue("@salario", salario);
                command.Parameters.AddWithValue("@cargo", cargo);
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

        public bool DeleteEmpleado(string empleadoID, int puertoID)
        {
            bool isSuccess = false;
            try
            {
                SqlCommand command = new SqlCommand("sp_eliminarEmpleado", DB);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_Empleado", empleadoID);
                command.Parameters.AddWithValue("@id_Puerto", puertoID);
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
