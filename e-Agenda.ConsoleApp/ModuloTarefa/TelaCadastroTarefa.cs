using e_Agenda.ConsoleApp.Compartilhado.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ConsoleApp.ModuloTarefa
{
    public class TelaCadastroTarefa : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Tarefa> repositorioTarefa;
        private readonly Notificador notificador;
        public TelaCadastroTarefa(IRepositorio<Tarefa> repositorioTarefa, Notificador notificador) : base("Cadastro de Tarefas")
        {
            this.repositorioTarefa = repositorioTarefa;
            this.notificador = notificador;
        }

        public override void MensagemOpcional()
        {
            base.MensagemOpcional();
            Console.WriteLine("Digite 5 para gerir itens de tarefa");
        }


        public void Inserir()
        {
            MostrarTitulo("Cadastro de Tarefas");
            Tarefa novaTarefa = ObterTarefa();
            repositorioTarefa.Inserir(novaTarefa);
            notificador.ApresentarMensagem("Tarefas cadastrado com Sucesso", TipoMensagem.Sucesso);

        }
        
        #region São os metodos para "montar" uma tarefa.(Tarefa = Itens + Prioridade)
        private Tarefa ObterTarefa()
        {
            Console.WriteLine("Digite o Titulo da tarefa:");
            string titulo = Console.ReadLine();
            Prioridade prioridade = ObterPrioridade();
            List<Item> itens = ObterItens();
            Tarefa novaTarefa = new Tarefa(titulo, prioridade, itens);
            
            return novaTarefa;
        }

        private List<Item> ObterItens()
        {
            List<Item> itens = new List<Item>();

            Console.WriteLine("Digite a quantidade de itens desta tarefa:");
            int quantidadeDeItens = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < quantidadeDeItens; i++)
            {
                Console.WriteLine("Digite a descrição do " + (i+1) + "º item:");
                string descricao = Console.ReadLine();
                Item novoItem = new Item(i+1,descricao);
                itens.Add(novoItem);
            }
            return itens;
        }

        private Prioridade ObterPrioridade()
        {
            Console.WriteLine("Digite o nivel de prioriadade da Tarefa:\n1-Baixa 2-Normal 3-Alta");
            string categoria = Console.ReadLine();
            Prioridade prioridade = new Prioridade();
            prioridade.Categoria = categoria;
            return prioridade;
        }
        #endregion
        public void Editar()
        {
            MostrarTitulo("Editando Tarefa");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhuma tarefa cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroTarefa = ObterNumeroRegistro();

            Tarefa tarefaAtualizada = ObterTarefa();

            bool conseguiuEditar = repositorioTarefa.Editar(numeroTarefa, tarefaAtualizada);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Tarefa editada com sucesso!", TipoMensagem.Sucesso);
        }

        private int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da tarefa que deseja: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = repositorioTarefa.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    notificador.ApresentarMensagem("ID da Tarefa não foi encontrada, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Tarefa");

            bool temTarefasCadastradas = VisualizarRegistros("Pesquisando");

            if (temTarefasCadastradas == false)
            {
                notificador.ApresentarMensagem("Nenhuma tarefa cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroTarefa = ObterNumeroRegistro();

            bool conseguiuExcluir = repositorioTarefa.Excluir(numeroTarefa);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Tarefa excluída com sucesso", TipoMensagem.Sucesso);
        }


        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Tarefas Cadastradas");

            Console.WriteLine("1 - Deseja ver Tarefas Pendetes");
            Console.WriteLine("2 - Deseja ver Tarefas Completas");
            Console.WriteLine();
            Console.Write("- ");
            string opcao = Console.ReadLine();
            List<Tarefa> tarefas = null;
            switch (opcao)
            {
                case "1":
                    tarefas = repositorioTarefa.Filtrar(x => x.AjustePercentual() < 95);
                    break;
                case "2":
                    tarefas = repositorioTarefa.Filtrar(x => x.AjustePercentual() >= 95);
                    break;
            }

            if (tarefas.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhuma tarefa cadastrada", TipoMensagem.Atencao);
                return false;
            }
           
            foreach (Tarefa tarefa in tarefas)
            {
                if(tarefa.prioridade.categoria == "Alta")
                Console.WriteLine(tarefa.ToString());
            }
            foreach (Tarefa tarefa in tarefas)
            {
                if (tarefa.prioridade.categoria == "Normal")
                    Console.WriteLine(tarefa.ToString());
            }
            foreach (Tarefa tarefa in tarefas)
            {
                if (tarefa.prioridade.categoria == "Baixa")
                    Console.WriteLine(tarefa.ToString());
            }

            Console.ReadLine();

            return true;
        }

        public void EspecialidadeCadaTela()
        {
            Console.WriteLine("Selecione o ID da Tarefa que deseja ver detalhes:");
            Console.WriteLine("");
            bool temTarefasCadastradas = VisualizarRegistros("Pesquisando");

            if (temTarefasCadastradas == false)
            {
                notificador.ApresentarMensagem("Nenhuma tarefa cadastrada para gerir.", TipoMensagem.Atencao);
                return;
            }
            int opcaoTarefa = ObterNumeroRegistro()-1;
            Console.Clear();
            List<Tarefa> tarefas = repositorioTarefa.SelecionarTodos();
            tarefas[opcaoTarefa].ToString();
            Console.WriteLine("");
            Console.Clear();
            Console.WriteLine("ITENS:\n");
            foreach (Item item in tarefas[opcaoTarefa].itens)
            {
                Console.WriteLine("Id:" + item.id + Environment.NewLine +
                "Descrição:" + item.Descricao + Environment.NewLine +
                "Status:" + item.pendencia + Environment.NewLine);
            }
            Console.WriteLine("Digite o Id do item que foi concluido:");
            int opcaoItemConcluido = Convert.ToInt32(Console.ReadLine())-1;
            tarefas[opcaoTarefa].itens[opcaoItemConcluido].pendencia = true;

            notificador.ApresentarMensagem("Alteração Concluida", TipoMensagem.Sucesso);

            Console.ReadLine();

        }
    }
}
