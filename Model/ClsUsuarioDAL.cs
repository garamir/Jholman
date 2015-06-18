using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using Entity;
using System.Data;

namespace Model
{
    public class ClsUsuarioDAL
    {
        public int CrearCuenta(ClsUsuario pUsuario)
        {
            int retorno = 0;         
              
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "guardarUsuario";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nom", pUsuario.Nombre);
            cmd.Parameters["@nom"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@user", pUsuario.Usuario);
            cmd.Parameters["@user"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@pass", pUsuario.Contraseña);
            cmd.Parameters["@pass"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@pre", pUsuario.Pregunta);
            cmd.Parameters["@pre"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@res", pUsuario.Respuesta);
            cmd.Parameters["@res"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@rol", pUsuario.Rol);
            cmd.Parameters["@rol"].Direction = ParameterDirection.Input;

            retorno = cmd.ExecuteNonQuery();
            return retorno;
        }
        public int Autentificar(ClsLogin pLogin)
        {
            int resultado = -1;
           
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "autentificar";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user", pLogin.Usuario);
            cmd.Parameters["@user"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@pass", pLogin.Contraseña);
            cmd.Parameters["@pass"].Direction = ParameterDirection.Input;


            MySqlDataReader _reader = cmd.ExecuteReader();
            while (_reader.Read())
            {
                resultado = 50;
            }

            return resultado;
        }
        public int AutentificarUsuario(ClsUsuario pUsuario)
        {
            int resultado = -1;
           
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "autentificarUsuario";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user", pUsuario.Usuario);
            cmd.Parameters["@user"].Direction = ParameterDirection.Input;

            MySqlDataReader _reader = cmd.ExecuteReader();
            while (_reader.Read())
            {
                resultado = 50;
            }

            return resultado;
        }
        public int Seguridad(string Pregunta, string Respuesta)
        {
            int resultado = -1;

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "seguridad";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@pre", Pregunta);
            cmd.Parameters["@pre"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@res", Respuesta);
            cmd.Parameters["@res"].Direction = ParameterDirection.Input;

            MySqlDataReader _reader = cmd.ExecuteReader();
            while (_reader.Read())
            {
                resultado = 50;
            }

            return resultado;
        }
        public int ContraseñaNueva(ClsUsuario pUsuario)
        {
            int retorno = 0;

            MySqlConnection conexion = BdConexion.ObtenerConexion();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "nuevoPassword";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user", pUsuario.Usuario);
            cmd.Parameters["@user"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@pass", pUsuario.Contraseña);
            cmd.Parameters["@pass"].Direction = ParameterDirection.Input;

            retorno = cmd.ExecuteNonQuery();
            conexion.Close();
            return retorno;
        }
        public int UsuarioRegistrado (string pUsuario)
        {
            int resultado = 0;

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "usuarioRegistrado";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user", pUsuario);
            cmd.Parameters["@user"].Direction = ParameterDirection.Input;

            MySqlDataReader _reader = cmd.ExecuteReader();
            while (_reader.Read())
            {
                resultado++;
            }

            return resultado;
        }
        public List<ClsUsuario> Consultar(string Nombre)
        {
            List<ClsUsuario> lista = new List<ClsUsuario>();

            MySqlConnection conexion = BdConexion.ObtenerConexion();
            MySqlCommand _comando = new MySqlCommand(String.Format("SELECT * FROM usuarios WHERE nombre ='{0}'", Nombre), BdConexion.ObtenerConexion());


            //MySqlCommand cmd = new MySqlCommand();
            //cmd.Connection = BdConexion.ObtenerConexion();

            //cmd.CommandText = "consultarUsuario1";
            //cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@user", pUsuario);
            //cmd.Parameters["@user"].Direction = ParameterDirection.Input;

            MySqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                ClsUsuario eUsuario = new ClsUsuario();
                eUsuario.Nombre = _reader.GetString(0);
                eUsuario.Usuario = _reader.GetString(1);
                eUsuario.Contraseña = _reader.GetString(2);
                eUsuario.Pregunta = _reader.GetString(3);
                eUsuario.Respuesta = _reader.GetString(4);
                eUsuario.Rol = _reader.GetInt32(5);

                lista.Add(eUsuario);
            }

            return lista;
        }
        public static ClsUsuario ObtenerUsuario(string Usuario)
        {
            ClsUsuario eUsuario = new ClsUsuario();
            MySqlConnection conexion = BdConexion.ObtenerConexion();
            MySqlCommand _comando = new MySqlCommand(String.Format("SELECT * FROM usuarios WHERE idusuario ='{0}'", Usuario), BdConexion.ObtenerConexion());

        
            //MySqlCommand cmd = new MySqlCommand();
            //cmd.Connection = BdConexion.ObtenerConexion();

            //cmd.CommandText = "consultarUsuario2";
            //cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@user", pUsuario);
            //cmd.Parameters["@user"].Direction = ParameterDirection.Input;

            MySqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                eUsuario.Nombre = _reader.GetString(0);
                eUsuario.Usuario = _reader.GetString(1);
                eUsuario.Contraseña = _reader.GetString(2);
                eUsuario.Pregunta = _reader.GetString(3);
                eUsuario.Respuesta = _reader.GetString(4);
                //eUsuario.Rol = _reader.GetInt32(4);
            }

            conexion.Close();
            return eUsuario;
        }
        public int Actualizar(ClsUsuario pUsuario)
        {
            int retorno = 0;
           
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "actualizarUsuario";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nom", pUsuario.Nombre);
            cmd.Parameters["@nom"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@user", pUsuario.Usuario);
            cmd.Parameters["@user"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@pass", pUsuario.Contraseña);
            cmd.Parameters["@pass"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@pre", pUsuario.Pregunta);
            cmd.Parameters["@pre"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@res", pUsuario.Respuesta);
            cmd.Parameters["@res"].Direction = ParameterDirection.Input;

            retorno = cmd.ExecuteNonQuery();
            //conexion.Close();
            return retorno;
        }
        public int Eliminar(string Usuario)
        {
            int retorno = 0;
          
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "eliminarUsuario";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user", Usuario);
            cmd.Parameters["@user"].Direction = ParameterDirection.Input;

            retorno = cmd.ExecuteNonQuery();
            //conexion.Close();
            return retorno;              
        }
        public List<ClsUsuario> cargarUsuario()
        {
            List<ClsUsuario> lista = new List<ClsUsuario>();

            //MySqlConnection conexion = BdConexion.ObtenerConexion();
            //MySqlCommand comando = new MySqlCommand(string.Format("Select * from clientes"), conexion);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "cargarUsuario";
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader _reader = cmd.ExecuteReader();
            while (_reader.Read())
            {
                ClsUsuario eUsuario = new ClsUsuario();
                eUsuario.Nombre = _reader.GetString(0);
                eUsuario.Usuario = _reader.GetString(1);
                eUsuario.Contraseña = _reader.GetString(2);
                eUsuario.Pregunta = _reader.GetString(3);
                eUsuario.Respuesta = _reader.GetString(4);
                eUsuario.Rol = _reader.GetInt32(5);

                lista.Add(eUsuario);
            }
            return lista;
        }
    }
}
