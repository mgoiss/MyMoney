using MyMoney.Banco;
using MyMoney.Modelo;
using MyMoney.Servicos;
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
        public TelaCadastro()
        {
            InitializeComponent();
        }

        private void VamosLa(object sender, EventArgs args)
        {
            if (verificardados())
            {
                try
                {
                    DataBase data = new DataBase();

                    Usuario use = new Usuario(txtUsuario.Text, int.Parse(txtSenha.Text));

                    double valor = 0.0;
                    if (txtValor.Text != null & txtValor.Text != "") //Passando o valor para a variavel
                    {
                        valor = double.Parse(txtValor.Text.Replace("R", "").Replace("$", "").Replace(" ", "").Replace(".", ""));
                    }

                    Conta cont = new Conta(txtNomeConta.Text, txtObjetivoConta.Text, valor);

                    data.InserirUsuario(use, cont); //Criando o usuário

                    App.Current.MainPage = new TelaLogin(); //Chamando a tela Login
                }
                catch (Exception ex)
                {
                    DisplayAlert("Erro", ex.Message, "OK");
                }                
            }
        }

        private bool verificardados()
        {
            int senha = 0;
            double valor = 0;

            //Prencha todos os campos
            if (txtUsuario.Text == null || txtUsuario.Text == "" || txtSenha.Text == null || txtSenha.Text == "" || txtNomeConta.Text == null || txtNomeConta.Text == "" || txtObjetivoConta.Text == null || txtObjetivoConta.Text == "")
            {
                DisplayAlert("Erro", "Preencha todos os dados obrigatorios", "OK");

                return false;
            }
            else if (!(Int32.TryParse(txtSenha.Text.ToString(), out senha))) //Analisando se foi informado um valor numerico
            {
                DisplayAlert("Erro", "O campo senha deve ser preenchido apenas com numero!", "OK");

                return false;
            }
            else if ((txtValor.Text != null) & txtValor.Text != "") //Verificando se foi infoemado algum valor
            {
                if (!(Double.TryParse(txtValor.Text.Replace("R", "").Replace("$", "").Replace(" ", "").Replace(".", "").ToString(), out valor))) //Analisando se foi informado um valor numerico
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

        private void TirarTeclado(object sender, TextChangedEventArgs e)
        {
            //Pegando o valor completo
            string currentText = e.NewTextValue;
            //Variável para pegar o valor anterio ao novo caractere
            string lastText = "";

            //Analisando se existe um valor antes do novo caractere
            if (!String.IsNullOrEmpty(e.OldTextValue))
            {
                lastText = e.OldTextValue; //Pegano o valor antes do novo caractere
            }

            //Calculando o tamanho, aki ele vai calcular o tamanho de toda a string e caso tenha um quebra linha o valor do calculo vai ser 1
            var currentNumb = currentText.Length - currentText.Replace(Environment.NewLine, string.Empty).Length;
            //Aki vai realizar o mesmo calculo do anterio, porém com base nos caracteres digitando antes do novo caractere
            var lastNumb = lastText.Length - lastText.Replace(Environment.NewLine, string.Empty).Length;

            //Aki será verificado se o caculo do novo caractere é meior que o do anterio, pois isso significar que ele tem um quebra linha
            if (currentNumb > lastNumb) 
            {
                //descriptionText.Text = lastText;
                //descriptionText.Unfocus();
                DependencyService.Get<IKeyboardHelper>().HideKeyboard();
            }            
        }
    }
}