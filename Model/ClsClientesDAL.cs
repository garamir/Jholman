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
    public class ClsClientesDAL
    {
        public int Agregar(Clscliente pCliente)
        {
            int retorno = 0;
            MySqlConnection conexion = BdConexion.ObtenerConexion();

            //MySqlCommand comando = new MySqlCommand(String.Format(
            //    "INSERT INTO clientes (identificacion, nombres, apellidos, sexo, direccion, telefono, telefonocel) VALUES ('{0}','{1}','{2}', '{3}', '{4}', '{5}', '{6}')",
            //    pCliente.Identificación, pCliente.Nombres, pCliente.Apellidos, pCliente.Sexo, pCliente.Dirección, pCliente.Telefono, pCliente.Celular), BdConexion.ObtenerConexion());
                        
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "guardar";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tipo", pCliente.Tipo_documento);
            cmd.Parameters["@tipo"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@id", pCliente.Identificación);
            cmd.Parameters["@id"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@nom", pCliente.Nombres);
            cmd.Parameters["@nom"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@ape", pCliente.Apellidos);
            cmd.Parameters["@ape"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@sex", pCliente.Género);
            cmd.Parameters["@sex"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@direc", pCliente.Dirección);
            cmd.Parameters["@direc"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@tel", pCliente.Telefono);
            cmd.Parameters["@tel"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@cel", pCliente.Celular);
            cmd.Parameters["@cel"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@emp", pCliente.Empresa);
            cmd.Parameters["@emp"].Direction = ParameterDirection.Input;                   

            retorno = cmd.ExecuteNonQuery();
            return retorno;
        }       

        public List<Clscliente> Consultar(int Identificación)
        {
            List<Clscliente> _lista = new List<Clscliente>();

           // MySqlCommand _comando = new MySqlCommand(String.Format(
           //"SELECT * FROM clientes  WHERE identificacion ='{0}'", Identificación), BdConexion.ObtenerConexion());

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "consultar1";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", Identificación);
            cmd.Parameters["@id"].Direction = ParameterDirection.Input;

            MySqlDataReader _reader = cmd.ExecuteReader();
            while (_reader.Read())
            {
                Clscliente pCliente = new Clscliente();
                pCliente.Tipo_documento = _reader.GetString(0);
                pCliente.Identificación = _reader.GetInt32(1);
                pCliente.Nombres = _reader.GetString(2);
                pCliente.Apellidos = _reader.GetString(3);
                pCliente.Género = _reader.GetString(4);
                pCliente.Dirección = _reader.GetString(5);
                pCliente.Telefono = _reader.GetString(6);
                pCliente.Celular = _reader.GetString(7);
                pCliente.Empresa = _reader.GetString(8);


                _lista.Add(pCliente);
            }

            return _lista;
        }      
        public static Clscliente ObtenerCliente(string TipoDocumento)
        {
            Clscliente pCliente = new Clscliente();

            //MySqlConnection conexion = BdConexion.ObtenerConexion();
            //MySqlCommand _comando = new MySqlCommand(String.Format("SELECT * FROM clientes WHERE identificacion = {0}", Identificación), conexion);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "consultar2";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tipo", TipoDocumento);
            cmd.Parameters["@tipo"].Direction = ParameterDirection.Input;

            MySqlDataReader _reader = cmd.ExecuteReader();
            while (_reader.Read())
            {
                pCliente.Tipo_documento = _reader.GetString(0);
                pCliente.Identificación = _reader.GetInt32(1);
                pCliente.Nombres = _reader.GetString(2);
                pCliente.Apellidos = _reader.GetString(3);
                pCliente.Género = _reader.GetString(4);
                pCliente.Dirección = _reader.GetString(5);
                pCliente.Telefono = _reader.GetString(6);
                pCliente.Celular = _reader.GetString(7);
                pCliente.Empresa = _reader.GetString(8);
            }

            return pCliente;
        }

        public int ClienteRegistrado(int Identificacion)
        {
            int resultado = 0;

            //MySqlConnection conexion = BdConexion.ObtenerConexion();
            //MySqlCommand _comando = new MySqlCommand(String.Format("SELECT * FROM clientes WHERE identificacion = {0}", pIdentificacion), conexion);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "clienteRegistrado";
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

        public int Actualizar(Clscliente Cliente)
        {
            int retorno = 0;

            //MySqlConnection conexion = BdConexion.ObtenerConexion();
            //MySqlCommand comando = new MySqlCommand(String.Format("UPDATE clientes SET nombres='{0}', apellidos='{1}', sexo='{2}', direccion='{3}', telefono='{4}', telefonocel='{5}' WHERE identificacion={6}",
            // pCliente.Nombres, pCliente.Apellidos, pCliente.Sexo, pCliente.Dirección, pCliente.Telefono, pCliente.Celular, pCliente.Identificación), conexion);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "actualizar";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@tipo", Cliente.Tipo_documento);
            cmd.Parameters["@tipo"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@id", Cliente.Identificación);
            cmd.Parameters["@id"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@nom", Cliente.Nombres);
            cmd.Parameters["@nom"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@ape", Cliente.Apellidos);
            cmd.Parameters["@ape"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@sex", Cliente.Género);
            cmd.Parameters["@sex"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@direc", Cliente.Dirección);
            cmd.Parameters["@direc"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@tel", Cliente.Telefono);
            cmd.Parameters["@tel"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@cel", Cliente.Celular);
            cmd.Parameters["@cel"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@emp", Cliente.Empresa);
            cmd.Parameters["@emp"].Direction = ParameterDirection.Input;  

            retorno = cmd.ExecuteNonQuery();            
            return retorno;                       
        }

        public int Eliminar(int Identificación)
        {
            int retorno = 0;

            //MySqlConnection conexion = BdConexion.ObtenerConexion();
            //MySqlCommand comando = new MySqlCommand(string.Format("DELETE FROM clientes WHERE identificacion={0}", pIdentificación), conexion);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "eliminar";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", Identificación);
            cmd.Parameters["@id"].Direction = ParameterDirection.Input;                       

            retorno = cmd.ExecuteNonQuery();            
            return retorno;      

        }
        public List<Clscliente> cargarClientes()
        {
            List<Clscliente> _lista = new List<Clscliente>();

            //MySqlConnection conexion = BdConexion.ObtenerConexion();
            //MySqlCommand comando = new MySqlCommand(string.Format("Select * from clientes"), conexion);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "cargarClientes";
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataReader _reader = cmd.ExecuteReader();
            while (_reader.Read())
            {
                Clscliente pCliente = new Clscliente();
                pCliente.Tipo_documento = _reader.GetString(0);
                pCliente.Identificación = _reader.GetInt32(1);
                pCliente.Nombres = _reader.GetString(2);
                pCliente.Apellidos = _reader.GetString(3);
                pCliente.Género = _reader.GetString(4);
                pCliente.Dirección = _reader.GetString(5);
                pCliente.Telefono = _reader.GetString(6);
                pCliente.Celular = _reader.GetString(7);
                pCliente.Empresa = _reader.GetString(8);

                _lista.Add(pCliente);
            }

            return _lista;
        }
    }

}
