using e_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ConsoleApp.ModuloTarefa
{
    public class Tarefa : EntidadeBase
    {
        private readonly string titulo;
        internal readonly Prioridade prioridade;
        internal DateTime dataDeCriacao;
        internal readonly List<Item> itens;

        internal int percentualPorItem;
        internal int percentualDeConclusao;
        private DateTime dataDeConclusao;

       
        public string DataDeConclusao
        {
            get { 
                if(dataDeConclusao == DateTime.MinValue)
                {
                    return "TAREFA NÃO CONCLUIDA";
                }
                else
                {
                    return "CONCLUIDA: " + dataDeConclusao;
                }
               
            }
            
        }

        public Tarefa(string titulo, Prioridade prioridade,List<Item> itens)
        {
            this.titulo = titulo;
            this.prioridade = prioridade;
            this.itens = itens;
            percentualPorItem = 100/itens.Count();
        }
        public int AjustePercentual()
        {
            percentualDeConclusao = 0;
            int verificaConclusao=0;
            foreach (Item item in itens)
            {
                if (item.pendencia == true)
                {
                    verificaConclusao++;
                    percentualDeConclusao += percentualPorItem;
                }
            }
            if (verificaConclusao == itens.Count)
            {
                percentualDeConclusao = 100;
                dataDeConclusao = DateTime.Now;
            }
            return percentualDeConclusao;
        }

        public override string ToString()
        {
            return "ID: " + id + Environment.NewLine +
                "Titulo: " + titulo + Environment.NewLine +
                prioridade + Environment.NewLine +
                "Data de Criação: " + dataDeCriacao + Environment.NewLine +
                "Percentual de Conclusao: " + AjustePercentual() + "%" + Environment.NewLine + 
                "Data de Conclusão: " + DataDeConclusao + Environment.NewLine;
        }
    }
}
