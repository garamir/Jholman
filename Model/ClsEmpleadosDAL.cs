using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Entity;
using System.Data;


namespace Model
{
    public class ClsEmpleadosDAL
    {
        public int Agregar(ClsEmpleado pEmpleado)
        {
            int retorno = 0;            

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = BdConexion.ObtenerConexion();

                cmd.CommandText = "guardarEmpleado";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@tipo", pEmpleado.TipoDocumento);
                cmd.Parameters["@tipo"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@id", pEmpleado.Identificación);
                cmd.Parameters["@id"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@nom", pEmpleado.Nombres);
                cmd.Parameters["@nom"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@ape", pEmpleado.Apellidos);
                cmd.Parameters["@ape"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@sex", pEmpleado.Sexo);
                cmd.Parameters["@sex"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@tel", pEmpleado.Telefono);
                cmd.Parameters["@tel"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@cel", pEmpleado.Celular);
                cmd.Parameters["@cel"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@car", pEmpleado.Cargo);
                cmd.Parameters["@car"].Direction = ParameterDirection.Input;

                retorno = cmd.ExecuteNonQuery();
                return retorno;  
            }

        public List<ClsEmpleado> Consultar(int Identificación)
        {
            List<ClsEmpleado> lista = new List<ClsEmpleado>();
           
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "consultarEmpleado1";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", Identificación);
            cmd.Parameters["@id"].Direction = ParameterDirection.Input;

            MySqlDataReader _reader = cmd.ExecuteReader();
            while (_reader.Read())
            {
                ClsEmpleado pEmpleado = new ClsEmpleado();
                pEmpleado.TipoDocumento = _reader.GetString(0);
                pEmpleado.Identificación = _reader.GetInt32(1);
                pEmpleado.Nombres = _reader.GetString(2);
                pEmpleado.Apellidos = _reader.GetString(3);
                pEmpleado.Sexo = _reader.GetString(4);
                pEmpleado.Telefono = _reader.GetString(5);
                pEmpleado.Celular = _reader.GetString(6);
                pEmpleado.Cargo = _reader.GetString(7);

                lista.Add(pEmpleado);
            }

            return lista;
        }

        public static ClsEmpleado ObtenerEmpleado(string TipoDocumento)
        {
            ClsEmpleado pEmpleado = new ClsEmpleado();

            //MySqlConnection conexion = BdConexion.ObtenerConexion();
            //MySqlCommand _comando = new MySqlCommand(String.Format("SELECT * FROM clientes WHERE identificacion = {0}", pIdentificación), conexion);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "consultarEmpleado2";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tipo", TipoDocumento);
            cmd.Parameters["@tipo"].Direction = ParameterDirection.Input;

            MySqlDataReader _reader = cmd.ExecuteReader();
            while (_reader.Read())
            {
                pEmpleado.TipoDocumento = _reader.GetString(0);
                pEmpleado.Identificación = _reader.GetInt32(1);
                pEmpleado.Nombres = _reader.GetString(2);
                pEmpleado.Apellidos = _reader.GetString(3);
                pEmpleado.Sexo = _reader.GetString(4);
                pEmpleado.Telefono = _reader.GetString(5);
                pEmpleado.Celular = _reader.GetString(6);
                pEmpleado.Cargo = _reader.GetString(7);
            }

            //conexion.Close();
            return pEmpleado;
        }        
        public int Actualizar(ClsEmpleado Empleado)
        {
            int retorno = 0;            
            
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "actualizarEmpleado";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tipo", Empleado.TipoDocumento);
            cmd.Parameters["@tipo"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@id", Empleado.Identificación);
            cmd.Parameters["@id"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@nom", Empleado.Nombres);
            cmd.Parameters["@nom"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@ape", Empleado.Apellidos);
            cmd.Parameters["@ape"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@sex", Empleado.Sexo);
            cmd.Parameters["@sex"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@tel", Empleado.Telefono);
            cmd.Parameters["@tel"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@cel", Empleado.Celular);
            cmd.Parameters["@cel"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@car", Empleado.Cargo);
            cmd.Parameters["@car"].Direction = ParameterDirection.Input;

            retorno = cmd.ExecuteNonQuery();
            return retorno;
        }

        public int Eliminar(int Identificación)
        {
            int retorno = 0;
           
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "eliminarEmpleado";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", Identificación);
            cmd.Parameters["@id"].Direction = ParameterDirection.Input;

            retorno = cmd.ExecuteNonQuery();
            //conexion.Close();
            return retorno;

        }
        public List<ClsEmpleado> cargarEmpleado()
        {
            List<ClsEmpleado> lista = new List<ClsEmpleado>();

            //MySqlConnection conexion = BdConexion.ObtenerConexion();
            //MySqlCommand comando = new MySqlCommand(string.Format("Select * from clientes"), conexion);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "cargarEmpleado";
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader _reader = cmd.ExecuteReader();
            while (_reader.Read())
            {
                ClsEmpleado eEmpleado = new ClsEmpleado();
                eEmpleado.TipoDocumento = _reader.GetString(0);
                eEmpleado.Identificación = _reader.GetInt32(1);
                eEmpleado.Nombres = _reader.GetString(2);
                eEmpleado.Apellidos = _reader.GetString(3);
                eEmpleado.Sexo = _reader.GetString(4);
                eEmpleado.Telefono = _reader.GetString(5);
                eEmpleado.Celular = _reader.GetString(6);
                eEmpleado.Cargo = _reader.GetString(7);

                lista.Add(eEmpleado);
            }

            return lista;
        }
        public int EmpleadoRegistrado(int Identificacion)
        {
            int resultado = 0;

            //MySqlConnection conexion = BdConexion.ObtenerConexion();
            //MySqlCommand _comando = new MySqlCommand(String.Format("SELECT * FROM clientes WHERE identificacion = {0}", pIdentificacion), conexion);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "empleadoRegistrado";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", Identificacion);
            cmd.Parameters["@id"].Direction = ParameterDirection.Input;

            MySqlDataReader _reader = cmd.ExecuteReader();
            while (_reader.Read())
            {
                resultado++;
            }

            return resultado;
        }

    }
}
