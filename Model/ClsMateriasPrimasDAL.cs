using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Entity;

namespace Model
{
    public class ClsMateriasPrimasDAL
    {
        public int Agregar(ClsMateriasPrimas pMateriasPrimas)
        {
            int retorno = 0;
            MySqlConnection conexion = BdConexion.ObtenerConexion();

            //MySqlCommand comando = new MySqlCommand(String.Format(
            //    "INSERT INTO clientes (identificacion, nombres, apellidos, sexo, direccion, telefono, telefonocel) VALUES ('{0}','{1}','{2}', '{3}', '{4}', '{5}', '{6}')",
            //    pCliente.Identificación, pCliente.Nombres, pCliente.Apellidos, pCliente.Sexo, pCliente.Dirección, pCliente.Telefono, pCliente.Celular), BdConexion.ObtenerConexion());

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "guardarMateriasPrimas";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@cod", pMateriasPrimas.Codigo);
            cmd.Parameters["@cod"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@nom", pMateriasPrimas.Nombre);
            cmd.Parameters["@nom"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@min", pMateriasPrimas.Stock_Minimo);
            cmd.Parameters["@min"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@max", pMateriasPrimas.Stock_Maximo);
            cmd.Parameters["@max"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@exis", pMateriasPrimas.Existencias);
            cmd.Parameters["@exis"].Direction = ParameterDirection.Input;           

            retorno = cmd.ExecuteNonQuery();
            return retorno;
        }

        public int Actualizar(ClsMateriasPrimas pMateriasPrimas)
        {
            int retorno = 0;
            MySqlConnection conexion = BdConexion.ObtenerConexion();
            //MySqlCommand comando = new MySqlCommand(String.Format("UPDATE clientes SET nombres='{0}', apellidos='{1}', sexo='{2}', direccion='{3}', telefono='{4}', telefonocel='{5}' WHERE identificacion={6}",
            // pCliente.Nombres, pCliente.Apellidos, pCliente.Sexo, pCliente.Dirección, pCliente.Telefono, pCliente.Celular, pCliente.Identificación), conexion);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "actualizarMateriasPrimas";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@cod", pMateriasPrimas.Codigo);
            cmd.Parameters["@cod"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@nom", pMateriasPrimas.Nombre);
            cmd.Parameters["@nom"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@min", pMateriasPrimas.Stock_Minimo);
            cmd.Parameters["@min"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@max", pMateriasPrimas.Stock_Maximo);
            cmd.Parameters["@max"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@exis", pMateriasPrimas.Existencias);
            cmd.Parameters["@exis"].Direction = ParameterDirection.Input; 

            retorno = cmd.ExecuteNonQuery();
            conexion.Close();
            return retorno;
        }

        public int Eliminar(string pCodigo)
        {
            int retorno = 0;
           
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "eliminarMateriasPrimas";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@cod", pCodigo);
            cmd.Parameters["@cod"].Direction = ParameterDirection.Input;

            retorno = cmd.ExecuteNonQuery();
            //conexion.Close();
            return retorno;
        }

        public List<ClsMateriasPrimas> cargarMaterias()
        {
            List<ClsMateriasPrimas> _lista = new List<ClsMateriasPrimas>();

            MySqlConnection conexion = BdConexion.ObtenerConexion();
            MySqlCommand comando = new MySqlCommand(string.Format("Select * from materiasprimas"), conexion);

            MySqlDataReader _reader = comando.ExecuteReader();
            while (_reader.Read())
            {
                ClsMateriasPrimas pMaterias = new ClsMateriasPrimas();
                pMaterias.Codigo = _reader.GetInt32(0);
                pMaterias.Nombre = _reader.GetString(1);
                pMaterias.Stock_Minimo = _reader.GetInt32(2);
                pMaterias.Stock_Maximo = _reader.GetInt32(3);
                pMaterias.Existencias = _reader.GetInt32(4);

                _lista.Add(pMaterias);
            }

            return _lista;
        }

        public List<ClsMateriasPrimas> Consultar(int Codigo)
        {
            List<ClsMateriasPrimas> _lista = new List<ClsMateriasPrimas>();

            MySqlConnection conexion = BdConexion.ObtenerConexion();
            MySqlCommand comando = new MySqlCommand(string.Format("Select * from materiasprimas where codigo ='{0}'", Codigo), conexion);

            //MySqlCommand cmd = new MySqlCommand();
            //cmd.Connection = BdConexion.ObtenerConexion();

            //cmd.CommandText = "consultar";
            //cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@id", Identificación);
            //cmd.Parameters["@id"].Direction = ParameterDirection.Input;

            MySqlDataReader _reader = comando.ExecuteReader();
            while (_reader.Read())
            {
                ClsMateriasPrimas pMaterias = new ClsMateriasPrimas();
                pMaterias.Codigo = _reader.GetInt32(0);
                pMaterias.Nombre = _reader.GetString(1);
                pMaterias.Stock_Minimo = _reader.GetInt32(2);
                pMaterias.Stock_Maximo = _reader.GetInt32(3);
                pMaterias.Existencias = _reader.GetInt32(4);


                _lista.Add(pMaterias);
            }

            return _lista;
        }      
        public static ClsMateriasPrimas ObtenerMateria(int Codigo)
        {
            ClsMateriasPrimas pMaterias = new ClsMateriasPrimas();

            MySqlConnection conexion = BdConexion.ObtenerConexion();
            MySqlCommand comando = new MySqlCommand(string.Format("Select * from materiasprimas where codigo = {0}", Codigo), conexion);

            //MySqlCommand cmd = new MySqlCommand();
            //cmd.Connection = BdConexion.ObtenerConexion();

            //cmd.CommandText = "consultar";
            //cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@id", Identificación);
            //cmd.Parameters["@id"].Direction = ParameterDirection.Input;

            MySqlDataReader _reader = comando.ExecuteReader();
            while (_reader.Read())
            {
                pMaterias.Codigo = _reader.GetInt32(0);
                pMaterias.Nombre = _reader.GetString(1);
                pMaterias.Stock_Minimo = _reader.GetInt32(2);
                pMaterias.Stock_Maximo = _reader.GetInt32(3);
                pMaterias.Existencias = _reader.GetInt32(4);
            }

            conexion.Close();
            return pMaterias;
        }
        public int MateriaRegistrada(string Nombre)
        {
            int resultado = 0;

            //MySqlConnection conexion = BdConexion.ObtenerConexion();
            //MySqlCommand _comando = new MySqlCommand(String.Format("SELECT * FROM clientes WHERE identificacion = {0}", pIdentificacion), conexion);

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = BdConexion.ObtenerConexion();

            cmd.CommandText = "materiaRegistrada";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nom", Nombre);
            cmd.Parameters["@nom"].Direction = ParameterDirection.Input;

            MySqlDataReader _reader = cmd.ExecuteReader();
            while (_reader.Read())
            {
                resultado++;
            }

            return resultado;
        }
      
    }
}
