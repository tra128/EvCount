using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EVCount
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }



        private void Reads(object sender, EventArgs e)
        {
            try
            {

                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string Efilename = Path.Combine(path, "EvC.txt");

                if (!File.Exists(Efilename))
                {
                    //Sesion u_id u_nombre u_password u_email
                    String[] EData = new string[] { "0" };
                    File.WriteAllLines(Efilename, EData);
                    DisplayAlert("Completed", "Data Not Found So it has been Created", "Ok");
                }
                else
                {
                    Debug.Write("-----------------------We Made it!!!!----------");
                    string[] toTestBy = File.ReadAllLines(Efilename);
                    DisplayCount.Text = toTestBy[0];
                    DisplayAlert("Completed", "Data Retrived", "Ok");

                }
            }
            catch (Exception Ex)
            {
                Debug.Write(Ex.Message);
                DisplayAlert("Failed", "Something Went Wrong\nError: " + Ex.Message, "Ok");
            }
        }

        private void Saves(object sender, EventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string Efilename = Path.Combine(path, "EvC.txt");
                //Sesion u_id u_nombre u_password u_email
                String[] EData = new string[] { DisplayCount.Text };
                File.WriteAllLines(Efilename, EData);
                DisplayAlert("Completed", "Data Saved", "Ok");
            }
            catch (Exception Ex) { Debug.Write(Ex.Message);
                DisplayAlert("Failed", "Something Went Wrong\nError: " + Ex.Message, "Ok");
            }
        }

        private void Rise1(object sender, EventArgs e)
        {
            DisplayCount.Text = (int.Parse(DisplayCount.Text) + 1) + "";
            Test();
        }
        public async void Test(){
            Clases.PokemonApi pokemonApi = new Clases.PokemonApi();
            pokemonApi.Generate();
        }
        private void Rise4(object sender, EventArgs e)
        {
            DisplayCount.Text = (int.Parse(DisplayCount.Text) + 4) + "";
        }

        private void Rise8(object sender, EventArgs e)
        {
            DisplayCount.Text = (int.Parse(DisplayCount.Text) + 8) + "";
        }

        private void Rise12(object sender, EventArgs e)
        {
            DisplayCount.Text = (int.Parse(DisplayCount.Text) + 12) + "";
        }
        private void Restart(object sender, EventArgs e)
        {
            DisplayCount.Text = "0";

        }
    }
}
