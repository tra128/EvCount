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
        Pokemon vsPokemon;
        PowerItem GlobalPowerItem;
        public MainPage(Trainer trainer)
        {
            InitializeComponent();
            GlobalTrainer = trainer;
            UserIcon.Source = GlobalTrainer.Image;
            TrainerName.Text = GlobalTrainer.Name;
            //img_VsPokemon.Source = "https://img.pokemondb.net/artwork/" + trainer.Pokemon.ToLower()+".jpg";
            img_TrainPokemon.Source = "https://img.pokemondb.net/sprites/heartgold-soulsilver/back-normal/" + trainer.Pokemon.ToLower() + ".png";
        }

        public async void GenSelected(object sender, EventArgs args) {
            var lbl = sender as Label;
            var acI = (lbl.Parent as Grid).Children.ElementAt(1) as ActivityIndicator;
            lbl.IsVisible = false;
            acI.IsVisible = true;
            acI.IsRunning = true;
            List<Pokemon> pokemons;
            PokemonApi api = new PokemonApi();
            pokemons = await api.Pokemons(lbl.Text);
            lv_Pokemons.ItemsSource = pokemons;
            lbl.IsVisible = true;
            acI.IsVisible = false;
            acI.IsRunning = false;
            sl_Gens.IsVisible = false;
            sl_Pokemons.IsVisible = true;
            lv_Pokemons.IsVisible = true;
        }

        public void GoBackGenSelected (object sender, EventArgs args){
            sl_Gens.IsVisible = true;
            sl_Pokemons.IsVisible = false;
        }

        public async void  PokemonSelectionTapped(object sender, EventArgs e) {
            if (!frm_PokemonSelection.IsVisible) { 
            frm_PokemonSelection.IsVisible = true;
            await frm_PokemonSelection.ScaleTo(1, 250);
            }
            else
            {
                await frm_PokemonSelection.ScaleTo(.1, 250);
                frm_PokemonSelection.IsVisible = false;
            }

        }

        public async void PokemonSelected (object sender, EventArgs args) {
            vsPokemon = ((sender as ViewCell).Parent as ListView).SelectedItem as Pokemon;
            img_VsPokemon.Source = " https://img.pokemondb.net/artwork/vector/large/"+vsPokemon.Name.ToLower()+".png";
            await frm_PokemonSelection.ScaleTo(.1, 250);
            frm_PokemonSelection.IsVisible = false;
        }

        public async void addEvs(object sender, EventArgs args)
        {
            int PokerusMultiplayer = 1, SOSMultiplayer = 1;
            int anklet = 0, band = 0, lens = 0, brace = 1,belt=0,bracer=0,weight=0;
            if (!(int.Parse(lbl_Total.Text)>=510)) {
                switch (lbl_ItemSelectedName.Text)
                {
                    case "Power Weight":
                        if (swt_Gen7.IsToggled)
                        {
                            weight = 8;
                        }
                        else
                        {
                            weight = 4;
                        }
                        break;
                    case "Power Bracer":
                        if (swt_Gen7.IsToggled)
                        {
                            bracer = 8;
                        }
                        else
                        {
                            bracer = 4;
                        }
                        break;
                    case "Power Belt":
                        if (swt_Gen7.IsToggled)
                        {
                            belt = 8;
                        }
                        else
                        {
                            belt = 4;
                        }
                        break;
                    case "Power Lens":
                        if (swt_Gen7.IsToggled)
                        {
                            lens = 8;
                        }
                        else
                        {
                            lens = 4;
                        }
                        break;
                    case "Power Band":
                        if (swt_Gen7.IsToggled)
                        {
                            band = 8;
                        }
                        else
                        {
                            band = 4;
                        }
                        break;
                    case "Power Anklet":
                        if (swt_Gen7.IsToggled)
                        {
                            anklet = 8;
                        }
                        else
                        {
                            anklet = 4;
                        }
                        break;
                    case "Macho Brace":
                        brace = 2;
                        break;
                }
                if (swt_Pokerus.IsToggled) { PokerusMultiplayer = 2; }
                if (swt_SOS.IsToggled) { SOSMultiplayer = 2; }
                lbl_HP.Text = (int.Parse(lbl_HP.Text) + ((weight + vsPokemon.HP) * PokerusMultiplayer * SOSMultiplayer * brace)).ToString();
                lbl_Atk.Text = (int.Parse(lbl_Atk.Text) + ((bracer + vsPokemon.Atk) * PokerusMultiplayer * SOSMultiplayer * brace)).ToString();
                lbl_Def.Text = (int.Parse(lbl_Def.Text) + ((belt + vsPokemon.Def) * PokerusMultiplayer * SOSMultiplayer * brace)).ToString();
                lbl_SpAtk.Text = (int.Parse(lbl_SpAtk.Text) + ((lens + vsPokemon.SpAtk) * PokerusMultiplayer * SOSMultiplayer * brace)).ToString();
                lbl_SpDef.Text = (int.Parse(lbl_SpDef.Text) + ((band + vsPokemon.SpDef) * PokerusMultiplayer * SOSMultiplayer * brace)).ToString();
                lbl_Spd.Text = (int.Parse(lbl_Spd.Text) + ((anklet + vsPokemon.Spd) * PokerusMultiplayer * SOSMultiplayer * brace)).ToString();

                lbl_Total.Text = (int.Parse(lbl_HP.Text) + int.Parse(lbl_Atk.Text) + int.Parse(lbl_Def.Text) + int.Parse(lbl_SpAtk.Text) + int.Parse(lbl_SpDef.Text) + int.Parse(lbl_Spd.Text)).ToString();
            }
        }

        public async void DisplayPowerItems(object sender, EventArgs argss)
        {
            PokemonApi api = new PokemonApi();
            if (!frm_PowerItemSelection.IsVisible)
            {
                lv_PowerItems.ItemsSource = api.GetPowerItems();
                frm_PowerItemSelection.IsVisible = true;
                await frm_PowerItemSelection.ScaleTo(1, 250);
            }
            else
            {
                await frm_PowerItemSelection.ScaleTo(.1, 250);
                frm_PowerItemSelection.IsVisible = false;
            }
        }

        public async void PowerItemSelected(object sender, EventArgs args)
        {
            GlobalPowerItem = ((sender as ViewCell).Parent as ListView).SelectedItem as PowerItem;
            img_PowerItem.Source = GlobalPowerItem.Image;
            lbl_ItemSelectedName.Text = GlobalPowerItem.Name;
            await frm_PowerItemSelection.ScaleTo(.1, 250);
            frm_PowerItemSelection.IsVisible = false;
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
