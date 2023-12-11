using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _2048
{
    class Bloc : Label
    {
        private int val = 0;    // Valor que tindrà el bloc.

        // Constructor de la classe que rep els paràmetres necessaris.
        public Bloc(int coordX, int coordY, int ample, int alt, int valor) : base()
        {
            base.Background = ObtenirColorDesDeValor(valor);
            base.Margin = new Thickness(coordX, coordY, 0, 0);
            base.HorizontalContentAlignment = HorizontalAlignment.Center;
            base.VerticalContentAlignment = VerticalAlignment.Center;
            base.Height = ample;
            base.Width = alt;
            base.Content = valor.ToString();
            base.FontSize = 28;
            val = valor;
        }

        /// <summary>
        /// Serveix per donar valor al bloc. Depenent del valor es pintarà d'un color determinat.
        /// </summary>
        /// <param name="nouValor">Valor que es vol donar al bloc.</param>
        private void EstablirValor(int nouValor)
        {
            if (nouValor == 0) base.Content = "";
            else base.Content = nouValor.ToString();
            base.Background = ObtenirColorDesDeValor(nouValor);
        }

        /// <summary>
        /// Obtén o posa el valor del bloc.
        /// </summary>
        public int Valor
        {
            get { return val; }
            set { val = value; EstablirValor(val); }
        }

        /// <summary>
        /// Aquesta funció retorna un valor de color en funció del número donat, que pot anar de 0 a 2048.
        /// </summary>
        /// <param name="v">Valor per obtenir-ne el color. Pot anar de 0 a 2048 on cada possible valor equival a 2^n, on n pot anar de 0 a 11.</param>
        /// <returns>Un color de la classe SolidColorBrush, en funció del valor donat.</returns>
        private SolidColorBrush ObtenirColorDesDeValor(int v)
        {
            switch (v)
            {
                case 0: return Brushes.LightGray;
                case 2: return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EEE4DA"));
                case 4: return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDE0C8"));
                case 8: return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F2B179"));
                case 16: return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F59563"));
                case 32: return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F67C5F"));
                case 64: return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F65E3B"));
                case 128: return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDCF72"));
                case 256: return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDCC61"));
                case 512: return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDC850"));
                case 1024: return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDC53F"));
                case 2048: return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDC22E"));
                default: return Brushes.Black;
            }
        }
    }
}
