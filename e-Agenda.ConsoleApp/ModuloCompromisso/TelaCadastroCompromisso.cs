using e_Agenda.ConsoleApp.Compartilhado.Interfaces;
using e_Agenda.ConsoleApp.ModuloContato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ConsoleApp.ModuloCompromisso
{
    public class TelaCadastroCompromisso :TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Compromisso> repositorioCompromisso;
        private readonly Notificador notificador;

        private readonly TelaCadastroContato telaCadastroContato;
        private readonly IRepositorio<Contato> repositorioContato;

        public TelaCadastroCompromisso(IRepositorio<Compromisso> repositorioCompromisso, Notificador notificador, TelaCadastroContato telaCadastroContato, IRepositorio<Contato> repositorioContato) : base("Cadastro de Compromissos")
        {
            this.repositorioCompromisso = repositorioCompromisso;
            this.notificador = notificador;
            this.telaCadastroContato = telaCadastroContato;
            this.repositorioContato = repositorioContato;
        }

        public void Editar()
        {
            MostrarTitulo("Editando Filme");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhum compromisso cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroContato = ObterNumeroRegistro();
            Contato contatoSelecionado = ObtemContato();
            Compromisso compromissoAtualizado = ObterCompromisso(contatoSelecionado);

            bool conseguiuEditar = repositorioCompromisso.Editar(numeroContato, compromissoAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Contato editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void EspecialidadeCadaTela()
        {
            throw new NotImplementedException();
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Compromisso");

            bool temCompromissosCadastrados = VisualizarRegistros("Pesquisando");

            if (temCompromissosCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhum Compromisso cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroCompromisso = ObterNumeroRegistro();

            bool conseguiuExcluir = repositorioCompromisso.Excluir(numeroCompromisso);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Compromisso excluído com sucesso1", TipoMensagem.Sucesso);
        }

        private int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do filme que deseja: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = repositorioCompromisso.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    notificador.ApresentarMensagem("ID da Sala não foi encontrada, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Novo Compromisso:");

            Contato contatoSelecionado = ObtemContato();
            if (contatoSelecionado == null)
            {
                notificador.ApresentarMensagem("Cadastre um contato antes de cadastrar um compromisso!", TipoMensagem.Atencao);
                return;
            }
            Compromisso novoCompromisso = ObterCompromisso(contatoSelecionado);
            repositorioCompromisso.Inserir(novoCompromisso);

            notificador.ApresentarMensagem("Gênero de Filme cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        private Compromisso ObterCompromisso(Contato contatoSelecionado)
        {
            Console.WriteLine("Digite qual é o compromisso:");
            string compromisso = Console.ReadLine();
            Console.WriteLine("Digite a data do compromisso:");
            DateTime date = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Digite o local do compromisso");
            string local = Console.ReadLine();
            Console.WriteLine("Digite o horário de inicio do compromisso:");
            string inicio = Console.ReadLine();
            Console.WriteLine("Figite o horario de término do compromisso:");
            string termino = Console.ReadLine();

            return new Compromisso(compromisso, date, local, inicio, termino, contatoSelecionado);
        }

        private Contato ObtemContato()
        {
            bool existeContato = telaCadastroContato.VisualizarRegistros("");

            if (!existeContato)
            {
                notificador.ApresentarMensagem("Não há nenhum contato disponivel disponivel para conseguir ter um compromisso!!!", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o Id do contato que irá ter o compromisso: ");
            int idContatoSelecionado = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Contato contatoSelecionado = repositorioContato.SelecionarRegistro(x => x.id == idContatoSelecionado);

            return contatoSelecionado;
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização dos Compromissos Cadastrados");

            List<Compromisso> compromissos = repositorioCompromisso.SelecionarTodos();

            if (compromissos.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum compromisso cadastrado", TipoMensagem.Atencao);
                return false;
            }

            foreach (Compromisso compromisso in compromissos)
                Console.WriteLine(compromisso.ToString());

            Console.ReadLine();

            return true;
        }
    }
}
