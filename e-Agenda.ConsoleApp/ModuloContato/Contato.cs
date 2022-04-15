using e_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ConsoleApp.ModuloContato
{
    public class Contato : EntidadeBase
    {
        private readonly string nome;
        private readonly string email;
        private readonly string telefone;
        private readonly string empresa;
        private readonly string cargo;

        public Contato(string nome, string email, string telefone, string empresa, string cargo)
        {
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.empresa = empresa;
            this.cargo = cargo;
        }

        public override string ToString()
        {
            return "ID: " + id + Environment.NewLine +
                   "Nome: " + nome + Environment.NewLine +
                   "Email: " + email + Environment.NewLine +
                   "Telefone: " + telefone + Environment.NewLine +
                   "Empresa: " + empresa + Environment.NewLine +
                   "Cargo: " + cargo + Environment.NewLine;
        }
    }
}
