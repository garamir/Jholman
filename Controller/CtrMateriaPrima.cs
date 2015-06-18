using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Entity;

namespace Controller
{
    public class CtrMateriaPrima
    {
        public int Guardar(int Codigo, string Nombre, int Stock_Minimo, int Stock_Maximo)
        {
            ClsMateriasPrimas eMateriasPrimas = new ClsMateriasPrimas();

            eMateriasPrimas.Codigo = Codigo;
            eMateriasPrimas.Nombre = Nombre;
            eMateriasPrimas.Stock_Minimo = Stock_Minimo;
            eMateriasPrimas.Stock_Maximo = Stock_Maximo;

            ClsMateriasPrimasDAL mMateriasPrimas = new ClsMateriasPrimasDAL();
            int resultado = mMateriasPrimas.Agregar(eMateriasPrimas);
            return resultado;
        }

        public int Actualizar(int Codigo, string Nombre, int Stock_Minimo, int Stock_Maximo)
        {
            ClsMateriasPrimas eMateriasPrimas = new ClsMateriasPrimas();

            eMateriasPrimas.Codigo = Codigo;
            eMateriasPrimas.Nombre = Nombre;
            eMateriasPrimas.Stock_Minimo = Stock_Minimo;
            eMateriasPrimas.Stock_Maximo = Stock_Maximo;

            ClsMateriasPrimasDAL mMateriasPrimas = new ClsMateriasPrimasDAL();
            int resultado = mMateriasPrimas.Actualizar(eMateriasPrimas);
            return resultado;
        }
        public int Eliminar(string Codigo)
        {
            ClsMateriasPrimasDAL mMateriasPrimas = new ClsMateriasPrimasDAL();
            return mMateriasPrimas.Eliminar(Codigo);
        }
        public List<ClsMateriasPrimas> cargarMaterias()
        {
            ClsMateriasPrimasDAL mMaterias = new ClsMateriasPrimasDAL();
            return mMaterias.cargarMaterias();
        }
        public List<ClsMateriasPrimas> Listar(int Codigo)
        {
            ClsMateriasPrimasDAL mMaterias = new ClsMateriasPrimasDAL();
            return mMaterias.Consultar(Codigo);
        }
        public int MateriaRegistrada(string Nombre)
        {
            ClsMateriasPrimasDAL mMaterias = new ClsMateriasPrimasDAL();
            return mMaterias.MateriaRegistrada(Nombre);
        }
    }
}
