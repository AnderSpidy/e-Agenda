using e_Agenda.ConsoleApp.Compartilhado.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ConsoleApp.ModuloContato
{
    public class TelaCadastroContato : TelaBase, ITelaCadastravel
    {
        private readonly Notificador notificador;
        private readonly IRepositorio<Contato> repositorioContato;
        public TelaCadastroContato(IRepositorio<Contato> repositorioContato, Notificador notificador) : base("Cadastro Contato")
        {
            this.repositorioContato = repositorioContato;
            this.notificador = notificador;
        }
        public void Inserir()
        {
            MostrarTitulo("Cadastro de Funcionário");

            Contato novoContato = ObterContato();

            repositorioContato.Inserir(novoContato);

            notificador.ApresentarMensagem("Contato cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        private Contato ObterContato()
        {
            Console.Write("Digite o nome do contato: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o email do contato: ");
            string email = Console.ReadLine();

            Console.Write("Digite o telefone do contato: ");
            string telefone = Console.ReadLine();

            Console.WriteLine("Digite a empresa do contato: ");
            string empresa = Console.ReadLine();

            Console.WriteLine("Digite o cargo do contato:");
            string cargo = Console.ReadLine();
            return new Contato(nome,email, telefone, empresa, cargo);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Contato");

            bool temContatos = VisualizarRegistros("Pesquisando");

            if (temContatos == false)
            {
                notificador.ApresentarMensagem("Nenhum contato cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroContato = ObterNumeroRegistro();

            Contato funcionarioAtualizado = ObterContato();

            bool conseguiuEditar = repositorioContato.Editar(numeroContato, funcionarioAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Funcionário editado com sucesso!", TipoMensagem.Sucesso);
        }

        private int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do Contato que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = repositorioContato.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    notificador.ApresentarMensagem("ID do Contato não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

        public void EspecialidadeCadaTela()
        {
            throw new NotImplementedException();
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Contato");

            bool temContato = VisualizarRegistros("Pesquisando");

            if (temContato == false)
            {
                notificador.ApresentarMensagem("Nenhum contato cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroCOntato = ObterNumeroRegistro();

            bool conseguiuExcluir = repositorioContato.Excluir(numeroCOntato);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Contato excluído com sucesso1", TipoMensagem.Sucesso);
        }

       

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Funcionários");

            List<Contato> contatos = repositorioContato.SelecionarTodos();

            if (contatos.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum funcionário disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Contato contato in contatos)
                Console.WriteLine(contato.ToString());

            Console.ReadLine();

            return true;
        }
    }
}
