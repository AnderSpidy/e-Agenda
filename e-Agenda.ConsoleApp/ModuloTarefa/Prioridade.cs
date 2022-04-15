using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ConsoleApp.ModuloTarefa
{
    public class Prioridade
    {
        internal string categoria;

       public string Categoria
        {
            get { return "Prioridade: " + categoria; }
            set {
                if(Convert.ToInt32(value) == 1)
                {
                    categoria = "Baixa";
                }if(Convert.ToInt32(value) == 2)
                {
                    categoria = "Normal";
                }
                if (Convert.ToInt32(value) == 3)
                {
                    categoria = "Alta";
                }
            }
        }
        public override string ToString()
        {
            return Categoria;
        }
    }
}
