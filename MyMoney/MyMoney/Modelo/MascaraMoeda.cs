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
        }

        private static double ConverterReaisParaDecimal(string valor)
        {
            var valorConvertido = Decimal.Parse(valor.Replace("R$ ", "").Replace(",", "").Replace(".", ""),
                CultureInfo.GetCultureInfo("pt-BR"));
            return (double)valorConvertido;
        }
    }
}
