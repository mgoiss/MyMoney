﻿using MyMoney.Banco;
using MyMoney.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMoney.Telas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TelaCadastro : ContentPage
	{
		public TelaCadastro ()
		{
			InitializeComponent ();
        }

        private void VamosLa(object sender, EventArgs args)
        {            
            if(verificardados())
            {
                DataBase data = new DataBase();

                Usuario use = new Usuario(txtNome.Text, int.Parse(txtSenha.Text));

                double valor = 0.0;
                if(txtValor.Text != null || txtValor.Text != "") //Passando o valor para a variavel
                {
                    valor = double.Parse(txtValor.Text);
                }

                Conta cont = new Conta(txtNomeConta.Text, txtObjetivoConta.Text, valor);

                data.InserirUsuario(use, cont); //Criando o usuário

                App.Current.MainPage = new TelaLogin(); //Chamando a tela Login
            }            
        }

        private bool verificardados()
        {
            int senha = 0;
            double valor = 0;

            //Prencha todos os campos
            if (txtNome.Text == null || txtNome.Text == "" || txtSenha.Text == null || txtSenha.Text == "" || txtNomeConta.Text == null || txtNomeConta.Text == "" || txtObjetivoConta.Text == null || txtObjetivoConta.Text == "")
            {
                //TODO: Fazer uma verificação por campo, para exibir ao usuário o campo que dever ser preenchido
                DisplayAlert("Erro", "Preencha todos os dados obrigatorios", "OK");

                return false;
            }
            else if (!(Int32.TryParse(txtSenha.Text.ToString(), out senha))) //Analisando se foi informado um valor numerico
            {
                DisplayAlert("Erro", "O campo senha deve ser preenchido apenas com numero!", "OK");

                return false;
            }
            else if (txtValor.Text != null)
            {
                if(!(Double.TryParse(txtValor.Text.ToString(), out valor))) //Analisando se foi informado um valor numerico
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