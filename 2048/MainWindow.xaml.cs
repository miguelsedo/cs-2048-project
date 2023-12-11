using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Microsoft.VisualBasic;

namespace _2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // variables globals
        Bloc[,] blocs = new Bloc[4, 4];
        Random atzar = new Random();
        int  punts = 0, record = 0, dquadrat = (335 - 10 * 5) / 4, coordX, coordY, valor;
        bool repetir, moviments = false, canvis, contadorheperdut = true;
        SoundPlayer Punt = new SoundPlayer(Properties.Resources.Punt1);
        SoundPlayer Win = new SoundPlayer(Properties.Resources.Aplaudiments);
        SoundPlayer GameOver = new SoundPlayer(Properties.Resources.GameOver);



        public MainWindow()
        {
            InitializeComponent();
        }
        

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            
            switch (e.Key)
                {
                    case Key.Down:avall();
                    
                    break;
                    case Key.Right: dreta();
                    
                    break;
                    case Key.Left: esquerra();
                   
                    break;
                    case Key.Up:amunt();
                    
                    break;
                    default: break;
                }

                ComprovarGuanyatoPerdut();
                //després de cada moviment comprova si ha guanyat o ha perdut               
            
            if (punts >= record) record = punts;

            labelPunts1.Content = "Punts: " + "\n"+ punts;
            labelRecord1.Content= "Rècord: " + "\n" + record; 
            //els punts s'actualitzaen i el rècord si es supera
        }

        private void ButtonReiniciar_Click(object sender, RoutedEventArgs e)
        {
            Inicialitza();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            labelPunts1.Content = "Punts: " + "\n" +  punts;
            labelRecord1.Content = "Rècord: " + "\n" +  record;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)//creació dels blocs amb 10 píxels de marge
                {   
                    coordX = (10 + j * (dquadrat + 10));
                    coordY = (10 + i * (dquadrat + 10));

                    blocs[i, j] = new Bloc(coordX, coordY, dquadrat, dquadrat, valor = 0);
                    canvas2048.Children.Add(blocs[i, j]);
                    blocs[i, j].Valor = 0;
                }

            }

            for (int k = 0; k < 2; k++)
            {
                SortejaNouBloc();
                //bucle per sorjetar dos blocs            
            }

            //Situacions.EstabelixSituacio(blocs, 3);


        }


        private void SortejaNouBloc()
        {

            do
            {
                int sortejaI = sortejaValor(0, 4); //se sortejen kes posicions x(i) i y(j) del bloc
                int sortejaJ = sortejaValor(0, 4);

                if (blocs[sortejaI, sortejaJ].Valor == 0)
                {
                    blocs[sortejaI, sortejaJ].Valor = 2;
                    repetir = false;
                }else { repetir = true; }

            } while (repetir == true);
            //en cas de que se sorteji en una posició ocupada, es repeteix el bulce 
        }

        private int sortejaValor(int minim, int maxim)
        {
            int valorSortejat = atzar.Next(minim, maxim);
            return valorSortejat;
        }

        private void avall()
        {
            moviments = false;
            do
            {
                canvis = false;
                for (int i = 2; i >= 0; i--)
                {
                    for (int j = 0; j < 4; j++)
                    {

                        if (blocs[i, j].Valor != 0)
                        {
                            if (blocs[i, j].Valor == blocs[i + 1, j].Valor)
                            {
                                blocs[i + 1, j].Valor = blocs[i, j].Valor + blocs[i + 1, j].Valor;
                                blocs[i, j].Valor = 0;
                                canvis = true;                              
                                punts = punts + blocs[i + 1, j].Valor;
                                Punt.Play();


                            }
                            else
                            {
                                if (blocs[i + 1, j].Valor == 0)
                                {
                                    blocs[i + 1, j].Valor = blocs[i, j].Valor;
                                    blocs[i, j].Valor = 0;
                                    canvis = true;
                                    moviments = true;

                                }
                            }
                        }


                    }
                }
            } while (canvis == true);

            if (moviments == true) SortejaNouBloc();//si hi ha algun moviment se sorteja nou bloc
            else { HePerdut(); }//si no n'hi ha movimnt comporva si ha perdut
        }
        private void amunt()
        {
            moviments = false;

            do
            {
                canvis = false;
                for (int i = 1; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {

                        if (blocs[i, j].Valor != 0)
                        {
                            if (blocs[i, j].Valor == blocs[i  -1, j].Valor)
                            {
                                blocs[i  -1, j].Valor = blocs[i, j].Valor + blocs[i  -1, j].Valor;
                                blocs[i, j].Valor = 0;
                                canvis = true;
                                moviments = true;
                                punts = punts + blocs[i - 1, j].Valor;
                                Punt.Play();
                            }
                            else
                            {
                                if (blocs[i -1, j].Valor == 0)
                                {
                                    blocs[i -1, j].Valor = blocs[i, j].Valor;
                                    blocs[i, j].Valor = 0;
                                    moviments = true;
                                    canvis = true;

                                }
                            }
                        }


                    }
                }
            } while (canvis == true);
            if (moviments == true) SortejaNouBloc();
            else { HePerdut(); }
        }
        private void dreta()
        {
            moviments = false;
            do
            {
                canvis = false;
                for (int j = 2; j >= 0; j--)
                {
                    for (int i = 0; i < 4; i++)
                    {

                        if (blocs[i, j].Valor != 0)
                        {
                            if (blocs[i, j].Valor == blocs[i, j + 1].Valor)
                            {
                                blocs[i, j + 1].Valor = blocs[i, j].Valor + blocs[i, j + 1].Valor;
                                blocs[i, j].Valor = 0;
                                canvis = true;
                                moviments = true;
                                punts = punts + blocs[i, j + 1].Valor;
                                Punt.Play();

                            }
                            else
                            {
                                if (blocs[i, j + 1].Valor == 0)
                                {
                                    blocs[i, j + 1].Valor = blocs[i, j].Valor;
                                    blocs[i, j].Valor = 0;
                                    canvis = true;
                                    moviments = true;

                                }
                            }
                        }


                    }
                }
            } while (canvis == true);

            if (moviments == true) SortejaNouBloc();                     
            else { HePerdut(); }
        }

        private void esquerra()
        {
            moviments = false;
            do
            {
                canvis = false;
                for (int j = 1; j < 4; j++)
                {
                    for (int i = 0; i < 4; i++)
                    {

                        if (blocs[i, j].Valor != 0)
                        {
                            if (blocs[i, j].Valor == blocs[i, j - 1].Valor)
                            {
                                blocs[i, j - 1].Valor = blocs[i, j].Valor + blocs[i, j - 1].Valor;
                                blocs[i, j].Valor = 0;
                                canvis = true;
                                moviments = true;
                                Punt.Play();
                                punts = punts + blocs[i, j - 1].Valor;

                            }
                            else
                            {
                                if (blocs[i, j - 1].Valor == 0)
                                {
                                    blocs[i, j - 1].Valor = blocs[i, j].Valor;
                                    blocs[i, j].Valor = 0;
                                    canvis = true;
                                    moviments = true;

                                }
                            }
                        }
                    }
                }
            } while (canvis == true);

            if (moviments == true) SortejaNouBloc();
            else { HePerdut(); }



        }
        private void Inicialitza()
        {
            punts = 0;
            labelPunts1.Content = "Punts: " + "\n" + punts;
            labelRecord1.Content = "Rècord: " + "\n" + record;

            //tots els blocs es posena  valor=0

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    blocs[i, j].Valor = 0;
                }
            }

            for (int k = 0; k < 2; k++)
            {
                SortejaNouBloc();
            }
            LabelResolucio.Content = "";

        }
        private bool HeGuanyat()
        {
            for (int i=0; i<4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (blocs[i, j].Valor == 2048) return true;
                }
            }
            return false;
        }

        private void ComprovarGuanyatoPerdut()
        {
            
            //si guanya, apareix un inputbox
            if (HeGuanyat()==true)
            {
                Win.Play();
                LabelResolucio.Content="W"+"\n"+"I"+ "\n" + "N";
                string Guanyar = Interaction.InputBox("Enhorabona, has guanyat! Vols tornar a jugar?" + "\n" + "\n"+"En cas afirmatiu, escriu 'SI'");

                if (Guanyar=="SI" || Guanyar == "Si"| Guanyar == "si"|| Guanyar == "sI"|| Guanyar == "SÍ" || Guanyar == "Sí" | Guanyar == "sí" || Guanyar == "sÍ")
                {
                    Inicialitza();
                }
                else
                {
                    this.Close();
                }
            }

            //si s'ha perdut, apareix un inputbox
            if (HePerdut() == true)
            {
                GameOver.Play();
                LabelResolucio.Content = "L" + "\n" + "O" + "\n" + "S" + "\n" + "E";
                
                string Perdut = Interaction.InputBox("Has perdut! Vols tornar a jugar?" + "\n" + "\n" + "Introdueix 'SI'");
                if (Perdut == "SI" || Perdut == "si" || Perdut == "sI" || Perdut == "Si" || Perdut == "Sí" || Perdut == "SÍ" || Perdut == "sí") Inicialitza();
                else { this.Close(); }
            }


        }
        private bool HePerdut()
        {
            //comprova si hi ha zeros a la matriu

            contadorheperdut = false;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (blocs[i, j].Valor == 0)
                    {
                        contadorheperdut = true;
                        j = 4;
                        i = 4;
                    }
                }
            }

            //si no hi han zeros comrpova vertica i horitzontalment si es poden fer moviments
            if (contadorheperdut == false)
            {
                if (comprovarVertical() == true && comprovarHoritzontal() == true) return true;

            }//si no es poden, es perd
            return false;
        }


        private bool comprovarHoritzontal()
        {
            //comproba per columnes, el bloc de la posició de la dreta amb respecte al que està al bucle
            for (int j = 2; j >= 0; j--)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (blocs[i, j].Valor == blocs[i, j + 1].Valor) return false;
                }
            }
            return true;



        }
        private bool comprovarVertical()
        {
            //comrpova per files, si el bloc amb respecte al que hi ha a sota és igual al bloc que es troba a la posició corresponent al bucle
            for (int i = 2; i >= 0; i--)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (blocs[i, j].Valor == blocs[i + 1, j].Valor) return false;
                }
            }
            return true;    



        }

    }


}