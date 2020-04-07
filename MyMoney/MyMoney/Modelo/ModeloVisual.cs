using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyMoney.Modelo
{
    public class ModeloVisual : INotifyPropertyChanged
    {
        #region Property

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion

        public ObservableCollection<ItensCarrossel> Itens { get; }

        private int _position;
        public int Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }

        public ModeloVisual()
        {
            Itens = new ObservableCollection<ItensCarrossel>();

            ItensCarrossel item = new ItensCarrossel
            {
                Titulo = "Bem Vindo!",
                Detalhe = "Aqui temos algumas funções para ",
                DetalheNegrito1 = "VOCÊ",
                DetalheNegrito2 = "DINHEIRO",
                DetalheContinua = " cuidar do seu ",
                Imagem = "TelaInicial1.png",
                Cor = "#E02041",
                TituloCor = "#FFFFFF",
                DetalheCor = "#000000",
            };

            Itens.Add(item);

            item = new ItensCarrossel
            {
                Titulo = "Seu Dinheiro é Importante",
                Detalhe = "Aqui ",
                DetalheNegrito1 = "VOCÊ",
                DetalheNegrito2 = "DINHEIRO",
                DetalheContinua = " vai conseguir gerenciar o seu ",
                Detalhe2 = "Controle todo o dinheiro que ",
                Detalhe2Negrito1 = "ENTRA",
                Detalhe2Negrito2 = "SAI",
                Detalhe2Continua = " e ",
                Imagem = "TelaInicial2.png",
                Cor = "#FFFFFF",
                TituloCor = "#E02041",
                DetalheCor = "#000000"
            };

            Itens.Add(item);


        }
    }
}
