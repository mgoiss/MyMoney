using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MyMoney.Modelo
{
    class MascaraMoeda : Behavior<Entry>
    {
        //TODO verificar pq a mascara só suporta 22 caracteres
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {

                bool ehCallback = false;
                string texto = args.NewTextValue;
                double valor;


                if (!(Double.TryParse(texto.Replace("R$ ", "").Replace(".", "").ToString(), out valor))) //Analisando se foi informado um valor numerico
                {
                    ((Entry)sender).Text = ((Entry)sender).Text.Replace(texto, "");
                }
                else if ((texto == "0" || texto == " " || texto.Replace("R$ ", "") == "0,0") & texto.Replace("R$ ", "").Replace(".", "").Length <= 3) //Verificando se foi digitado 0 ou um valor nulo, pois caso tenha sido verdadeiro a mascara tbm colocara "R$ 0,00"
                {
                    ((Entry)sender).Text = "R$ 0,00";
                }
                else
                {
                    if (texto.Contains("R$"))
                    {
                        var valorNovoEmDecimal = ConverterReaisParaDecimal(texto);
                        var valorAntigoEmDecimal = args.OldTextValue.Contains("R$") ? ConverterReaisParaDecimal(args.OldTextValue) : int.Parse(args.OldTextValue);
                        ehCallback = valorNovoEmDecimal == valorAntigoEmDecimal;

                        texto = valorNovoEmDecimal.ToString();
                    }

                    if (!ehCallback)
                    {
                        if (!string.IsNullOrEmpty(texto))
                        {
                            var textoFormatadoEmReais = (Decimal.Parse(texto.Replace("R$ ", "").Replace(",", "").Replace(".", "")) / 100).ToString("C", CultureInfo.GetCultureInfo("pt-BR"));
                            texto = textoFormatadoEmReais;
                        }

                        ((Entry)sender).Text = texto;
                    }
                }

                //Verificando se o texto tem menos q 4 caracteres, caso seja verdadeiro a mascarar colocara "R$ 0,00" evitando assim que o usuário apague tudo
                //Verificando se foi digitado 0 ou um valor nulo, pois caso tenha sido verdadeiro a mascara tbm colocara "R$ 0,00"
                //if ((texto != "0" & texto != " ") || texto.Length < 3)                 

            }
        }

        private static double ConverterReaisParaDecimal(string valor)
        {
            var valorConvertido = Decimal.Parse(valor.Replace("R$ ", "").Replace(",", "").Replace(".", ""),
                CultureInfo.GetCultureInfo("pt-BR"));
            return (double)valorConvertido;
        }
    }
}
