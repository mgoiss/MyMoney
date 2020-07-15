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

        /***********************************
         * Metodos Relacionados ao Usuário *
         * *********************************/

        //Metodo para pesquisar se existe um usuário cadastrado
        public bool VerificarExistenciaUsuario()
        {
            var dados = _conexao.Table<Usuario>().ToList(); //Pegando os usuário no banco

            if (dados.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Verificando se a senha informada está correta
        public bool Login(int senha)
        {
            var dados = _conexao.Table<Usuario>().ToList(); // Pegando os dados do usuário

            foreach (Usuario valor in dados) //Pecorrendo a lista de usuários
            {
                if (valor.Senha == senha) //Comparando a senha informada com a senha cadastrada no banco
                {
                    return true;
                }
            }

            return false;
        }
        //Inserindo usuário
        public void InserirUsuario(Usuario use, Conta cont)
        {
            var id = _conexao.Insert(use);

            InserirConta(cont, id);
        }


        /*********************************
         * Metodos Relacionados a Contas *
         * *******************************/

        //Pegando o total de dinheiro
        public double TotalDinheiro()
        {
            var dados = _conexao.Table<Conta>().ToList(); // Pegando todas as contas

            double total = 0.0;

            foreach (Conta valor in dados)
            {
                total += valor.ValorConta; //Incrementando todos os valores
            }

            return total;
        }
        //Função para listar todas as contas
        public List<Conta> ListarContas()
        {
            List<Conta> ListConta = _conexao.Table<Conta>().ToList(); //Pegando todas as contas

            foreach (Conta valor in ListConta) //Pecorrendo as lista de conta e definindo a ultima transação (DEPOSITO/SAQUE)
            {
                valor.UltimoDeposito = UltimoDepositoSaque(valor.Id, "deposito").ValorTransacao;
                valor.UltimoSaque = UltimoDepositoSaque(valor.Id, "saque").ValorTransacao;

                valor.DataDeposito = UltimoDepositoSaque(valor.Id, "deposito").DataTransacao;
                valor.DataSaque = UltimoDepositoSaque(valor.Id, "saque").DataTransacao;
            }

            return ListConta;
        }
        //Inserindo conta
        public void InserirConta(Conta cont, int idUsuario)
        {
            cont.UsuarioId = idUsuario;
            _conexao.Insert(cont);
        }
        //APAGAR CONTA
        public void ApagarConta(Conta conta)
        {
            _conexao.Delete(conta);
        }

        /************************************
         * Metodos Relacionados a Transação *
         * **********************************/

        //Função para listar todas as transações de uma conta
        public List<Transacao> ListarTransacaoConta(int idConta)
        {
            //Pegando todas as transações de um determinada conta e ordenando pela data
            List<Transacao> ListaTransacao = _conexao.Query<Transacao>("select * from Transacao where ContaId = ? ORDER BY DataTransacao DESC", idConta); //Listando apenas as transações da conta

            foreach (Transacao valor in ListaTransacao) //Laço para pecorrer a lista adcionando o simbolo para cada transação
            {
                if (valor.TipoTransacao == "deposito")
                {
                    valor.SimboloTransacao = '+';
                }
                else
                {
                    valor.SimboloTransacao = '-';
                }
            }

            return ListaTransacao;
        }
        //Função para retornar a ultima trasação pelo tipo dela
        public Transacao UltimoDepositoSaque(int idConta, string nomeCampo)
        {
            //Pegando todas as transações de um determinada conta e ordenando pela data
            List<Transacao> ListTransacao = _conexao.Query<Transacao>("select * from Transacao where ContaId = ? ORDER BY DataTransacao DESC", idConta); //Listando apenas as transações da conta
            Transacao trans;

            if (ListTransacao.Count > 0) //Analisando de existe transaçao na conta
            {
                int cont = 0; 
                foreach (var item in ListTransacao) //Pecorrendo a lista para pegar a ultima transação desejada
                {                    
                    if (item.TipoTransacao == nomeCampo)
                    {
                        trans = new Transacao(ListTransacao[cont]); //Instaciando a transação com o ultimo deposito

                        return trans;
                    }
                    cont++;
                }

                return trans = new Transacao(); //Retornando nullo pq pecorreu a lista e não foi encontrado nenhuma transação do tipo desejado
            }
            else
            {
                return trans = new Transacao(); //Retornando nulo pq a lista tá vazia
            }            
        }
        //Inserindo Transação
        public void InserirTransacao(Transacao trans, int idConta)
        {
            trans.ContaId = idConta;
            _conexao.Insert(trans);

            //TODO Criar metodo para alterar o valor total da conta e adicionar metodo a função de saque, depoisto e exclusão de transação            
        }

        public void AlterarValorTotal(int idConta, double valor)
        {

        }





    }
}
