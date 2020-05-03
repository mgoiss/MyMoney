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

        [Indexed]
        public int ContaId { get; set; }
    }
}
