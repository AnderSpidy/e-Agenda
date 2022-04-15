using e_Agenda.ConsoleApp.Compartilhado;
using e_Agenda.ConsoleApp.ModuloContato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ConsoleApp.ModuloCompromisso
{
    public class Compromisso : EntidadeBase
    {
        private readonly string assunto;
        private readonly DateTime data;
        private readonly string local;
        private readonly string inicio;
        private readonly string termino;
        private readonly Contato contato;

        public Compromisso(string assunto, DateTime data, string local, string inicio, string termino, Contato contato)
        {
            this.assunto = assunto;
            this.data = data;
            this.local = local;
            this.inicio = inicio;
            this.termino = termino;
            this.contato = contato;
        }

        public override string ToString()
        {
            return "ID:" + id + Environment.NewLine +
                "Compromisso: " + assunto + Environment.NewLine +
                "Data: " + data + Environment.NewLine +
                "Local: " + local + Environment.NewLine +
                "Horário de Inicio: " + inicio + Environment.NewLine +
                "Horário de Término: " + termino + Environment.NewLine +
                "Com o Contato:\n" + contato.ToString();
        }
    }
}
