using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MyMoney.Modelo
{
    [Table("Conta")]
    public class Conta
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NomeConta { get; set; }
        public string Objetivo { get; set; }
        public double ValorConta { get; set; }

        [Indexed]
        public int UsuarioId { get; set; }

        [Ignore]
        public double UltimoDeposito { get; set; }
        [Ignore]
        public DateTime DataDeposito { get; set; }
        [Ignore]
        public double UltimoSaque { get; set; }
        [Ignore]
        public DateTime DataSaque { get; set; }


        public Conta()
        {
        }

        public Conta(string nomeConta, string objetivo, double valorConta)
        {
            NomeConta = nomeConta;
            Objetivo = objetivo;
            ValorConta = valorConta;
        }
    }
}
