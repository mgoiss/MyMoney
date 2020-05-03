﻿using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyMoney.Banco;
using MyMoney.Modelo;

namespace MyMoney.Telas.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupCriarConta
    {
        public PopupCriarConta()
        {
            InitializeComponent();
        }

        private void Criar(Object sender, EventArgs args)
        {
            if(verificardados()) //Verificando se todos os dados foram informados
            {
                DataBase data = new DataBase();

                //Analisando se o valor inicial da conta foi informado
                double valor = 0.0;
                if (txtValor.Text != null)
                {
                    valor = double.Parse(txtValor.Text);
                }

                //Criando um objeto do tipo conta
                Conta cont = new Conta(txtNomeConta.Text, txtObjetivoConta.Text, valor);

                //Inserindo a conta no banco
                data.InserirConta(cont, 1);

                //Metodos para voltar a pagina e recarregar o valor total
                App.Current.MainPage = new NavigationPage(new Abas()) { BarBackgroundColor = Color.FromHex("#E02041") }; //Chamando novamente a aba para que seja recarregado o valor total

                PopupNavigation.PopAsync(); //Por algum motivo precisou dessa parte para voltar a tela inicial
            }
        }

        private async void Voltar(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync();
        }

        private bool verificardados()
        {
            int valor = 0;

            if (txtNomeConta.Text == null || txtObjetivoConta.Text == null)
            {
                DisplayAlert("Erro", "Preencha todos os dados obrigatorios", "OK");

                return false;
            }
            else if (txtValor.Text != null)
            {
                if (!(Int32.TryParse(txtValor.Text.ToString(), out valor)))
                {
                    DisplayAlert("Erro", "O campo valor deve ser preenchido apenas com numero!", "OK");

                    return false;
                }

                return true;
            }
            else
            {
                return true;
            }

        }
    }
}