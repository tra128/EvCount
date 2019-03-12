using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using EVCount.Clases;

namespace EVCount
{
    public partial class MainPage : ContentPage
    {
        Trainer GlobalTrainer;
        public MainPage(Trainer trainer)
        {
            InitializeComponent();
            GlobalTrainer = trainer;
            UserIcon.Source = GlobalTrainer.Image;
            TrainerName.Text = GlobalTrainer.Name;
            img_VsPokemon.Source = "https://img.pokemondb.net/artwork/" + trainer.Pokemon.ToLower()+".jpg";
            img_TrainPokemon.Source = "https://img.pokemondb.net/sprites/heartgold-soulsilver/back-normal/" + trainer.Pokemon.ToLower() + ".png";
        }

        public async void GenSelected(object sender, EventArgs args) {
            var lbl = sender as Label;
            List<Pokemon> pokemons;
            PokemonApi api = new PokemonApi();
            pokemons = await api.Pokemons(lbl.Text);
            lv_Pokemons.ItemsSource = pokemons;
            sl_Gens.IsVisible = false;
            lv_Pokemons.IsVisible = true;
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
