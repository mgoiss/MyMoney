using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLitePCL;

namespace MyMoney.Modelo
{
    [Table("Usuario")]
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string NomeUsuario { get; set; }
        public int Senha { get; set; }
        
        [Ignore]
        public List<Conta> Contas { get; set;}

        public Usuario()
        {
        }

        public Usuario(string nomeUsuario, int senha)
        {
            NomeUsuario = nomeUsuario;
            Senha = senha;
        }
    }
}
