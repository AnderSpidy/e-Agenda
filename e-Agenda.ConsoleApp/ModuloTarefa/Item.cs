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
        private string descricao;
        internal bool pendencia;

        public Item(int id,string descricao)
        {
            this.id = id;   
            this.descricao = descricao;
        }

        public string Descricao => descricao;

        public override string ToString()
        {
            return "Id:" + id + Environment.NewLine +
                "Descrição:" + Descricao + Environment.NewLine +
                "Status:" + pendencia + Environment.NewLine;
        }
    }
}
