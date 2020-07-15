using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MyMoney.Modelo
{
    [Table("Transacao")]
    public class Transacao
    {
        [PrimaryKey, AutoIncrement]
        public int CodTransacao { get; set; }
        public double ValorTransacao { get; set; }
        public string DescTransacao { get; set; }
        public DateTime DataTransacao { get; set; }
        public string TipoTransacao { get; set; }

        [Ignore]
        public char SimboloTransacao { get; set; }

        [Indexed]
        public int ContaId { get; set; }

        public Transacao()
        {
        }

        public Transacao(Transacao Trans)
        {
            CodTransacao = Trans.CodTransacao;
            ValorTransacao = Trans.ValorTransacao;
            DescTransacao = Trans.DescTransacao;
            DataTransacao = Trans.DataTransacao;
            TipoTransacao = Trans.TipoTransacao;
            ContaId = Trans.ContaId;
        }

        public Transacao(double valorTransacao, string descTransacao, DateTime dataTransacao, string tipoTransacao, int contaId)
        {
            ValorTransacao = valorTransacao;
            DescTransacao = descTransacao;
            DataTransacao = dataTransacao;
            TipoTransacao = tipoTransacao;
            ContaId = contaId;
        }
    }
}
