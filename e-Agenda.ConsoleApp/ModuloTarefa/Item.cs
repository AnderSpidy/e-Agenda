using e_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ConsoleApp.ModuloTarefa
{
    public class Item 
    {
        public int id;
        internal string descricao;
        internal bool pendencia;

        public Item(int id,string descricao)
        {
            this.id = id;   
            this.descricao = descricao;
        }

        public override string ToString()
        {
            string pendenciaString;
            if(pendencia == true)
            {
                pendenciaString = "CONCLUÍDO";
            }
            else
            {
                pendenciaString = "PENDENTE";
            }
            return "Id:" + id + Environment.NewLine +
                "Descrição:" + descricao + Environment.NewLine +
                "Status:" + pendenciaString + Environment.NewLine;
        }

    }
}
