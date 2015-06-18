using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Model;

namespace Controller
{
    public class CtrEmpleado
    {
        public int Guardar(string TipoDocumento, int Identificacion, string Nombres, string Apellidos, string Genero, string Telefono, string Celular, string Cargo)
        {
            ClsEmpleado eEmpleado = new ClsEmpleado();
            eEmpleado.TipoDocumento = TipoDocumento;
            eEmpleado.Identificación = Identificacion;
            eEmpleado.Nombres = Nombres;
            eEmpleado.Apellidos = Apellidos;
            eEmpleado.Sexo = Genero;
            eEmpleado.Telefono = Telefono;
            eEmpleado.Celular = Celular;
            eEmpleado.Cargo = Cargo;

            ClsEmpleadosDAL mEmpleado = new ClsEmpleadosDAL();
            int resultado = mEmpleado.Agregar(eEmpleado);
            return resultado;
        }

        public int Actualizar(string TipoDocumento, int Identificacion, string Nombres, string Apellidos, string Genero, string Telefono, string Celular, string Cargo)
        {
            ClsEmpleado eEmpleado = new ClsEmpleado();
            eEmpleado.TipoDocumento = TipoDocumento;
            eEmpleado.Identificación = Identificacion;
            eEmpleado.Nombres = Nombres;
            eEmpleado.Apellidos = Apellidos;
            eEmpleado.Sexo = Genero;
            eEmpleado.Telefono = Telefono;
            eEmpleado.Celular = Celular;
            eEmpleado.Cargo = Cargo;

            ClsEmpleadosDAL mEmpleado = new ClsEmpleadosDAL();
            int resultado = mEmpleado.Actualizar(eEmpleado);
            return resultado;
        }
        public List<ClsEmpleado> Listar(int Identificacion)
        {
            ClsEmpleadosDAL mEmpleado = new ClsEmpleadosDAL();
            return mEmpleado.Consultar(Identificacion);
        }
        public List<ClsEmpleado> cargarEmpleado()
        {
            ClsEmpleadosDAL mEmpleados = new ClsEmpleadosDAL();
            return mEmpleados.cargarEmpleado();
        }
        public int Eliminar(int Identificacion)
        {
            ClsEmpleadosDAL mEmpleado = new ClsEmpleadosDAL();
            return mEmpleado.Eliminar(Identificacion);
        }
        public int EmpleadoRegistrado(int Identificacion)
        {
            ClsEmpleadosDAL mEmpleado = new ClsEmpleadosDAL();
            return mEmpleado.EmpleadoRegistrado(Identificacion);
        }
    }
}
