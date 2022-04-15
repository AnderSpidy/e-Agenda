﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ConsoleApp.Compartilhado.Interfaces
{
    public interface IRepositorio<T> where T : EntidadeBase
    {
        void Inserir(T entidade);
        bool Editar(int idSelecionado, T novaEntidade);
        bool Excluir(int idSelecionado);
        bool ExisteRegistro(int idSelecionado);
        T SelecionarRegistro(int idSelecionado);
        List<T> Filtrar(Predicate<T> condicao);
        List<T> SelecionarTodos();
        T SelecionarRegistro(Predicate<T> condicao);
    }
}
