using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Vaja1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        public List<string> St_prop { get; set; }

        private void Odpri_Click(object sender, RoutedEventArgs e)
        {

            var dialog = new Microsoft.Win32.OpenFileDialog();
            if (dialog.ShowDialog()== true)
            {
                string ime = dialog.FileName;
                if (ime.Substring(ime.Length - 3) == "txt")
                {
                    try
                    {
                        List<string> string_stevilke = new List<string>();

                        using (StreamReader sr = File.OpenText(ime))
                        {
                            string vrstica;
                            while ((vrstica = sr.ReadLine()) != null)
                            {
                                Glavni_text.Content = "Datoteka odprta";
                                Glavni_text.Foreground = Brushes.Green;

                                foreach (string st in vrstica.Split(";")){
                                    string_stevilke.Add(st);
                                    Vrstice.Items.Add(st);
                                }
                            }
                        }
                        St_prop = string_stevilke;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Prišlo je do napake", ex.Message);
                    }

                }
                else
                {
                    Glavni_text.Content = "File ni cvs";
                    Glavni_text.Foreground = Brushes.Red;
                }
            }
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            List<int> stevila_last = Izbiranje(Pretvori(St_prop)) ;

            //foreach (int lol in stevila_last)
            //{
            //    MessageBox.Show("Prišlo je do napake" + lol);
                
            //}

            if ((bool)UI.IsChecked)
            {
                
                Izpis(stevila_last);
            }
            else if ((bool)Datoteka.IsChecked)
            {
                Izpis_File(stevila_last);
            }
            else if ((bool)UI_Datoteka.IsChecked)
            {
                Izpis(stevila_last);
                Izpis_File(stevila_last);


            }
            else {
                Glavni_text.Content = "Izberi izmed 3 moznosti";
                Glavni_text.Foreground = Brushes.Orange;
            }




        }

        private void Izpis(List<int> tabelca)
        {
            Vrstice.Items.Clear();
            foreach (int vrednost in tabelca)
            {
                Vrstice.Items.Add(vrednost);
            }
            Glavni_text.Content = "Sortirano";
            Glavni_text.Foreground = Brushes.Green;
        }

        private void Izpis_File(List<int> tabelca)
        {
            int zacasna = 1;
            string pot = "Stevilke.txt";
            while (true) {

                if (!File.Exists(pot))
                {
                    using (StreamWriter sw = File.CreateText(pot))
                    {
                        foreach (int tex in tabelca)
                        {
                            sw.WriteLine(tex.ToString());
                        }
                    }
                    break;
                }
                else {
                    pot = "Stevilke" + zacasna + ".txt";
                    zacasna++;
                }

            }
            
        }



        private List<int> Pretvori(List<string> list_st)
        {
            List<int> stevila = new List<int>();


            foreach (string str in list_st)
            {
                stevila.Add(Convert.ToInt32(str));
            }

            return stevila;
        }


        static List<int> Izbiranje(List<int> tabela)
        {
            for (int neurejeni = 0; neurejeni < tabela.Count; neurejeni++)
            {

                int najmanjsiPozicija = neurejeni;

                int najmanjsi = tabela[neurejeni];
                for (int i = neurejeni + 1; i < tabela.Count; i++)
                {
                    if (tabela[i] < najmanjsi)
                    {
                        najmanjsiPozicija = i;
                        najmanjsi = tabela[i];
                    }
                }

                tabela[najmanjsiPozicija] = tabela[neurejeni];
                tabela[neurejeni] = najmanjsi;
            }
            return tabela;
        }

    }
}
