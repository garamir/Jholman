using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Model;

namespace Controller
{
    public class CtrCliente
    {
        public int Guardar(string TipoDocumento, int Identificacion, string Nombres, string Apellidos, string Genero, string Direccion, string Telefono, string Celular, string Empresa)
        {
            Clscliente eCliente = new Clscliente();
            eCliente.Tipo_documento = TipoDocumento;
            eCliente.Identificación = Identificacion;
            eCliente.Nombres = Nombres;
            eCliente.Apellidos = Apellidos;
            eCliente.Género = Genero;
            //pCliente.Fecha_Nac = dtpFechaNacimiento.Value.Year + "/" + dtpFechaNacimiento.Value.Month + "/" + dtpFechaNacimiento.Value.Day;
            eCliente.Dirección = Direccion;
            eCliente.Telefono = Telefono;
            eCliente.Celular = Celular;
            eCliente.Empresa = Empresa;

            ClsClientesDAL mcliente = new ClsClientesDAL();
            int resultado = mcliente.Agregar(eCliente);
            return resultado;
        }
        public int Actualizar(string TipoDocumento, int Identificacion, string Nombres, string Apellidos, string Genero, string Direccion, string Telefono, string Celular, string Empresa)
        {
            Clscliente eCliente = new Clscliente();
            eCliente.Tipo_documento = TipoDocumento;
            eCliente.Identificación = Identificacion;
            eCliente.Nombres = Nombres;
            eCliente.Apellidos = Apellidos;
            eCliente.Género = Genero;
            //pCliente.Fecha_Nac = dtpFechaNacimiento.Value.Year + "/" + dtpFechaNacimiento.Value.Month + "/" + dtpFechaNacimiento.Value.Day;
            eCliente.Dirección = Direccion;
            eCliente.Telefono = Telefono;
            eCliente.Celular = Celular;
            eCliente.Empresa = Empresa;

            ClsClientesDAL mcliente = new ClsClientesDAL();
            int resultado = mcliente.Actualizar(eCliente);
            return resultado;
        }
        public List<Clscliente> Listar (int Identificacion)
        {
            ClsClientesDAL mClientes = new ClsClientesDAL();
            return mClientes.Consultar(Identificacion);
        }
        public List<Clscliente> cargarClientes()
        {
            ClsClientesDAL mClientes = new ClsClientesDAL();
            return mClientes.cargarClientes();
        }
        public int Eliminar (int Identificacion)
        {
            ClsClientesDAL mClientes = new ClsClientesDAL();
            return mClientes.Eliminar(Identificacion);
        }
        public int ClienteRegistrado(int Identificacion)
        {
            ClsClientesDAL mClientes = new ClsClientesDAL();
            return mClientes.ClienteRegistrado(Identificacion);
        }

       
    }
}
