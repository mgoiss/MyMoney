using System;
using System.Collections.Generic;
using System.Text;

namespace MyMoney.Banco
{
    public interface ICaminho
    {
        string ObterCaminho(string NomeArquivoBanco);
    }
}
