using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MyMoney.Modelo;
using SQLite;

namespace MyMoney.Banco
{
    class DataBase
    {
        private SQLiteConnection _conexao;

        public DataBase()
        {
            var dep = DependencyService.Get<ICaminho>();
            string caminho = dep.ObterCaminho("database.db");

            _conexao = new SQLiteConnection(caminho);

            //Criando a Tabela
            _conexao.CreateTable<Usuario>();
            _conexao.CreateTable<Conta>();
            _conexao.CreateTable<Transacao>();

            /*Usuario use = new Usuario();

            use.NomeUsuario = "Matheus";
            use.Senha = 1234567;

            var id = _conexao.Insert(use);

            Conta cont = new Conta();

            cont.NomeConta = "Teste";
            cont.Objetivo = "Testado testando testando";
            cont.ValorConta = 1000;
            cont.UsuarioId = id;

            _conexao.Insert(cont);*/
        }

        public bool VerificarExistenciaUsuario()
        {
            var dados = _conexao.Table<Usuario>().ToList();

            if( dados.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Login(int senha)
        {
            var dados = _conexao.Table<Usuario>().ToList();

            foreach (Usuario valor in dados)
            {
                if (valor.Senha == senha)
                {
                    return true;
                }
            }

            return false;
        }

        public double TotalDinheiro()
        {
            var dados = _conexao.Table<Conta>().ToList();

            double total = 0.0;

            foreach(Conta valor in dados)
            {
                total += valor.ValorConta;
            }

            return total;
        }

        public List<Conta> ListarContas()
        {
            return _conexao.Table<Conta>().ToList();
        }

        public void InserirUsuario( Usuario use, Conta cont)
        {
            var id = _conexao.Insert(use);

            InserirConta(cont, id);
        }

        public void InserirConta(Conta cont, int idUsuario)
        {
            cont.UsuarioId = idUsuario;
            _conexao.Insert(cont);
        }
    }
}
