using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Entity;
using Model;

namespace Controller
{
    public class CtrUsuario
    {
        public int Crear(string Nombre, string Usuario, string Contraseña, string Pregunta, string Respuesta)
        {
            ClsUsuario eUsuario = new ClsUsuario();

            eUsuario.Usuario = Usuario;
            eUsuario.Nombre = Nombre;
            eUsuario.Contraseña = Contraseña;
            eUsuario.Pregunta = Pregunta;
            eUsuario.Respuesta = Respuesta;
            eUsuario.Rol = 2;

            ClsUsuarioDAL mUsario = new ClsUsuarioDAL();
            int resultado = mUsario.CrearCuenta(eUsuario);
            return resultado;
        }
        
        public int Actualizar(string Nombre, string Usuario, string Contraseña, string Pregunta, string Respuesta)
        {
            ClsUsuario eUsuario = new ClsUsuario();

            eUsuario.Usuario = Usuario;
            eUsuario.Nombre = Nombre;
            eUsuario.Contraseña = Contraseña;
            eUsuario.Pregunta = Pregunta;
            eUsuario.Respuesta = Respuesta;

            ClsUsuarioDAL mUsuario = new ClsUsuarioDAL();
            int resultado = mUsuario.Actualizar(eUsuario);
            return resultado;
        }
        public List<ClsUsuario> Listar(string Usuario)
        {
            ClsUsuarioDAL mUsario = new ClsUsuarioDAL();
            return mUsario.Consultar(Usuario);
        }
        public List<ClsUsuario> cargarUsuario()
        {
            ClsUsuarioDAL mUsario = new ClsUsuarioDAL();
            return mUsario.cargarUsuario();
        }

        public int Eliminar(string Usuario)
        {
            ClsUsuarioDAL mUsario = new ClsUsuarioDAL();
            return mUsario.Eliminar(Usuario);
        }

        public int ContraseñaNueva(string Usuario, string Contraseña)
        {
            ClsUsuario eUsuario = new ClsUsuario();

            eUsuario.Usuario = Usuario;
            eUsuario.Contraseña = Contraseña;

            ClsUsuarioDAL mUsuario = new ClsUsuarioDAL();
            int resultado = mUsuario.ContraseñaNueva(eUsuario);
            return resultado;
        }

        public int UsuarioRegistrado(string Usuario)
        {
            ClsUsuarioDAL mUsuario = new ClsUsuarioDAL();
            return mUsuario.UsuarioRegistrado(Usuario);
        }
    }
       
}
