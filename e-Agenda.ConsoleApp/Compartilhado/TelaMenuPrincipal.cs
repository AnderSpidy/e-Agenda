using e_Agenda.ConsoleApp.Compartilhado.Interfaces;
using e_Agenda.ConsoleApp.ModuloCompromisso;
using e_Agenda.ConsoleApp.ModuloContato;
using e_Agenda.ConsoleApp.ModuloTarefa;
using System;

namespace e_Agenda.ConsoleApp
{
    public class TelaMenuPrincipal
    {
        //instancia de notificador para utiplizar "ApresentarMensagem" na tela principal
        private readonly Notificador notificador;

        private IRepositorio<Tarefa> repositorioTarefa;
        private TelaCadastroTarefa telaCadastroTarefa;

        private IRepositorio<Contato> repositorioContato;
        private TelaCadastroContato telaCadastroContato;

        private IRepositorio<Compromisso> repositorioCompromisso;
        private TelaCadastroCompromisso telaCadastroCompromisso;
        public TelaMenuPrincipal(Notificador notificador)
        {
            this.notificador = notificador;

            repositorioTarefa = new RepositorioTarefa();
            telaCadastroTarefa = new TelaCadastroTarefa(repositorioTarefa, notificador);

            repositorioContato = new RepositorioContato();
            telaCadastroContato = new TelaCadastroContato(repositorioContato, notificador);

            repositorioCompromisso = new RepositorioCompromisso();
            telaCadastroCompromisso = new TelaCadastroCompromisso(repositorioCompromisso, notificador,telaCadastroContato,repositorioContato);

        }


        public string MostrarOpcoes()
        {
            Console.Clear();
            Console.WriteLine("e-Agenda Menu Principal:");
            Console.WriteLine("Digite 1 para Gerenciar Tarefas");
            Console.WriteLine("Digite 2 para Gerenciar Contatos");
            Console.WriteLine("Digite 3 para Gerenciar Compromissos");
            Console.WriteLine("Digite s para sair");

            string opcaoSelecionada = Console.ReadLine();
            // hhhhg
            return opcaoSelecionada;
        }

        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            if (opcao == "1")
                tela = telaCadastroTarefa;

            else if (opcao == "2")
                tela = telaCadastroContato;

            else if (opcao == "3")
                tela = telaCadastroCompromisso;

            else if (opcao == "4")
                tela = null;

            else if (opcao == "5")
                tela = null;

            return tela;
        }
    }
}