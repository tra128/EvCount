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
        int hp = 0, atk = 0, def = 0, spatk = 0, spdef = 0, spd = 0;
        public MainPage(Trainer trainer)
        {
            InitializeComponent();
            GlobalTrainer = trainer;
            UserIcon.Source = GlobalTrainer.Image;
            TrainerName.Text = GlobalTrainer.Name;
            lbl_HP.Text = trainer.HP.ToString();
            lbl_Atk.Text = trainer.Atk.ToString();
            lbl_Def.Text = trainer.Def.ToString();
            lbl_SpAtk.Text = trainer.SpAtk.ToString();
            lbl_SpDef.Text = trainer.SpDef.ToString();
            lbl_Spd.Text = trainer.Spd.ToString();
            lbl_Total.Text = (int.Parse(lbl_HP.Text) + int.Parse(lbl_Atk.Text) + int.Parse(lbl_Def.Text) + int.Parse(lbl_SpAtk.Text) + int.Parse(lbl_SpDef.Text) + int.Parse(lbl_Spd.Text)).ToString();
            lbl_Left.Text = (510 - int.Parse(lbl_Total.Text)).ToString();
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

        public void saveData(object sender, EventArgs args)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string UserSaveData = Path.Combine(path, GlobalTrainer.SaveID);
            string[] ToSaveData = new string[] { GlobalTrainer.Image, GlobalTrainer.Name, GlobalTrainer.Pokemon, lbl_HP.Text, lbl_Atk.Text, lbl_Def.Text, lbl_SpAtk.Text, lbl_SpDef.Text, lbl_Spd.Text};
            File.WriteAllLines(UserSaveData, ToSaveData);
            img_SaveIcon.Source = "SaveIconSaved";

        }

        public void DisplayUsableItem(object sender, EventArgs e)
        {
            var lbl = sender as Label;
            var acI = (lbl.Parent as Grid).Children.ElementAt(1) as ActivityIndicator;
            lbl.IsVisible = false;
            acI.IsVisible = true;
            acI.IsRunning = true;
            List<UsableItems> usableItems;
            PokemonApi api = new PokemonApi();
            switch (lbl.Text)
            {
                case "Vitamin":
                    usableItems = api.GetVitamins();
                    lv_Items.ItemsSource = usableItems;
                    break;
                case "Wing":
                    usableItems = api.GetWings();
                    lv_Items.ItemsSource = usableItems;
                    break;
                case "Berry":
                    usableItems = api.GetBerries();
                    lv_Items.ItemsSource = usableItems;
                    break;
            }
            
            lbl.IsVisible = true;
            acI.IsVisible = false;
            acI.IsRunning = false;
            sl_UsableItems.IsVisible = false;
            sl_Items.IsVisible = true;
            lv_Items.IsVisible = true;
        }

        public void GoBackGenSelected (object sender, EventArgs args){
            sl_Gens.IsVisible = true;
            sl_Pokemons.IsVisible = false;
        }

        public void GoBackDisplayItems(object sender, EventArgs args)
        {
            sl_UsableItems.IsVisible = true;
            sl_Items.IsVisible = false;
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

        public async void UsableItemsTapped(object sender, EventArgs e)
        {
            if (!frm_UsableItems.IsVisible)
            {
                frm_UsableItems.IsVisible = true;
                await frm_UsableItems.ScaleTo(1, 250);
            }
            else
            {
                await frm_UsableItems.ScaleTo(.1, 250);
                frm_UsableItems.IsVisible = false;
            }
        }

        public async void PokemonSelected (object sender, EventArgs args) {
            vsPokemon = ((sender as ViewCell).Parent as ListView).SelectedItem as Pokemon;
            img_VsPokemon.Source = " https://img.pokemondb.net/artwork/vector/large/"+vsPokemon.Name.ToLower()+".png";
            img_Pluse.IsEnabled = true;
            await frm_PokemonSelection.ScaleTo(.1, 250);
            frm_PokemonSelection.IsVisible = false;
        }

        public async void UsableItemSelected(object sender,EventArgs e)
        {
            UsableItems item = ((sender as ViewCell).Parent as ListView).SelectedItem as UsableItems;

            if (true/*!(int.Parse(lbl_Total.Text) >= 510)*/)
            {
                                
                switch (item.Name)
                {
                    case "HP Up":
                        if ((int.Parse(lbl_HP.Text) + item.Value) >= 252) { lbl_HP.Text = "252"; }
                        else
                        {
                            if (item.Value >= int.Parse(lbl_Left.Text)) { item.Value = int.Parse(lbl_Left.Text); lbl_Left.Text = "0"; }
                            lbl_HP.Text = (int.Parse(lbl_HP.Text) + item.Value).ToString();
                        }
                        break;
                    case "Protein":
                        if ((int.Parse(lbl_Atk.Text) + item.Value) >= 252) { lbl_Atk.Text = "252"; }
                        else
                        {
                            if (item.Value >= int.Parse(lbl_Left.Text)) { item.Value = int.Parse(lbl_Left.Text); lbl_Left.Text = "0"; }
                            lbl_Atk.Text = (int.Parse(lbl_Atk.Text) + item.Value).ToString();
                        }
                        break;
                    case "Iron":
                        if ((int.Parse(lbl_Def.Text) + item.Value) >= 252) { lbl_Def.Text = "252"; }
                        else
                        {
                            if (item.Value >= int.Parse(lbl_Left.Text)) { item.Value = int.Parse(lbl_Left.Text); lbl_Left.Text = "0"; }
                            lbl_Def.Text = (int.Parse(lbl_Def.Text) + item.Value).ToString();
                        }
                        break;
                    case "Calcium":
                        if ((int.Parse(lbl_SpAtk.Text) + item.Value) >= 252) { lbl_SpAtk.Text = "252"; }
                        else
                        {
                            if (item.Value >= int.Parse(lbl_Left.Text)) { item.Value = int.Parse(lbl_Left.Text); lbl_Left.Text = "0"; }
                            lbl_SpAtk.Text = (int.Parse(lbl_SpAtk.Text) + item.Value).ToString();
                        }
                        break;
                    case "Zinc":
                        if ((int.Parse(lbl_SpDef.Text) + item.Value) >= 252) { lbl_SpDef.Text = "252"; }
                        else
                        {
                            if (item.Value >= int.Parse(lbl_Left.Text)) { item.Value = int.Parse(lbl_Left.Text); lbl_Left.Text = "0"; }
                            lbl_SpDef.Text = (int.Parse(lbl_SpDef.Text) + item.Value).ToString();
                        }
                        break;
                    case "Carbos":
                        if ((int.Parse(lbl_Spd.Text) + item.Value) >= 252) { lbl_Spd.Text = "252"; }
                        else
                        {
                            if (item.Value >= int.Parse(lbl_Left.Text)) { item.Value = int.Parse(lbl_Left.Text); lbl_Left.Text = "0"; }
                            lbl_Spd.Text = (int.Parse(lbl_Spd.Text) + item.Value).ToString();
                        }
                        break;
                    case "Health Wing":
                        if ((int.Parse(lbl_HP.Text) + item.Value) >= 252) { lbl_HP.Text = "252"; }
                        else
                        {
                            if (item.Value >= int.Parse(lbl_Left.Text)) { item.Value = int.Parse(lbl_Left.Text); lbl_Left.Text = "0"; }
                            lbl_HP.Text = (int.Parse(lbl_HP.Text) + item.Value).ToString();
                        }
                        break;
                    case "Muscle Wing":
                        if ((int.Parse(lbl_Atk.Text) + item.Value) >= 252) { lbl_Atk.Text = "252"; }
                        else
                        {
                            if (item.Value >= int.Parse(lbl_Left.Text)) { item.Value = int.Parse(lbl_Left.Text); lbl_Left.Text = "0"; }
                            lbl_Atk.Text = (int.Parse(lbl_Atk.Text) + item.Value).ToString();
                        }
                        break;
                    case "Resist Wing":
                        if ((int.Parse(lbl_Def.Text) + item.Value) >= 252) { lbl_Def.Text = "252"; }
                        else
                        {
                            if (item.Value >= int.Parse(lbl_Left.Text)) { item.Value = int.Parse(lbl_Left.Text); lbl_Left.Text = "0"; }
                            lbl_Def.Text = (int.Parse(lbl_Def.Text) + item.Value).ToString();
                        }
                        break;
                    case "Genius Wing":
                        if ((int.Parse(lbl_SpAtk.Text) + item.Value) >= 252) { lbl_SpAtk.Text = "252"; }
                        else
                        {
                            if (item.Value >= int.Parse(lbl_Left.Text)) { item.Value = int.Parse(lbl_Left.Text); lbl_Left.Text = "0"; }
                            lbl_SpAtk.Text = (int.Parse(lbl_SpAtk.Text) + item.Value).ToString();
                        }
                        break;
                    case "Clever Wing":
                        if ((int.Parse(lbl_SpDef.Text) + item.Value) >= 252) { lbl_SpDef.Text = "252"; }
                        else
                        {
                            if (item.Value >= int.Parse(lbl_Left.Text)) { item.Value = int.Parse(lbl_Left.Text); lbl_Left.Text = "0"; }
                            lbl_SpDef.Text = (int.Parse(lbl_SpDef.Text) + item.Value).ToString();
                        }
                        break;
                    case "Swift Wing":
                        if ((int.Parse(lbl_Spd.Text) + item.Value) >= 252) { lbl_Spd.Text = "252"; }
                        else
                        {
                            if (item.Value >= int.Parse(lbl_Left.Text)) { item.Value = int.Parse(lbl_Left.Text); lbl_Left.Text = "0"; }
                            lbl_Spd.Text = (int.Parse(lbl_Spd.Text) + item.Value).ToString();
                        }
                        break;
                    case "Pomeg Berry":
                        if ((int.Parse(lbl_HP.Text) - item.Value) <= 0)
                        {
                            lbl_HP.Text = "0";
                        }
                        else
                        {
                            if (item.Value <= (510 - int.Parse(lbl_Left.Text)) * -1)
                            {
                                item.Value = (510 - int.Parse(lbl_Left.Text)) * -1;
                                lbl_Left.Text = (item.Value * -1).ToString();
                            }
                            lbl_HP.Text = (int.Parse(lbl_HP.Text) + item.Value).ToString();
                        }
                        break;
                    case "Kelpsy Berry":
                        if ((int.Parse(lbl_Atk.Text) - item.Value) <= 0)
                        {
                            lbl_Atk.Text = "0";
                        }
                        else
                        {
                            if (item.Value <= (510 - int.Parse(lbl_Left.Text)) * -1)
                            {
                                item.Value = (510 - int.Parse(lbl_Left.Text)) * -1;
                                lbl_Left.Text = (item.Value * -1).ToString();
                            }
                            lbl_Atk.Text = (int.Parse(lbl_Atk.Text) + item.Value).ToString();
                        }
                        break;
                    case "Qualot Berry":
                        if ((int.Parse(lbl_Def.Text) - item.Value) <= 0)
                        {
                            lbl_Def.Text = "0";
                        }
                        else
                        {
                            if (item.Value <= (510 - int.Parse(lbl_Left.Text)) * -1)
                            {
                                item.Value = (510 - int.Parse(lbl_Left.Text)) * -1;
                                lbl_Left.Text = (item.Value * -1).ToString();
                            }
                            lbl_Def.Text = (int.Parse(lbl_Def.Text) + item.Value).ToString();
                        }
                        break;
                    case "Hondew Berry":
                        if ((int.Parse(lbl_SpAtk.Text) - item.Value) <= 0)
                        {
                            lbl_SpAtk.Text = "0";
                        }
                        else
                        {
                            if (item.Value <= (510 - int.Parse(lbl_Left.Text)) * -1)
                            {
                                item.Value = (510 - int.Parse(lbl_Left.Text)) * -1;
                                lbl_Left.Text = (item.Value * -1).ToString();
                            }
                            lbl_SpAtk.Text = (int.Parse(lbl_SpAtk.Text) + item.Value).ToString();
                        }
                        break;
                    case "Grepa Berry":
                        if ((int.Parse(lbl_SpDef.Text) - item.Value) <= 0)
                        {
                            lbl_SpDef.Text = "0";
                        }
                        else
                        {
                            if (item.Value <= (510 - int.Parse(lbl_Left.Text)) * -1)
                            {
                                item.Value = (510 - int.Parse(lbl_Left.Text)) * -1;
                                lbl_Left.Text = (item.Value * -1).ToString();
                            }
                            lbl_SpDef.Text = (int.Parse(lbl_SpDef.Text) + item.Value).ToString();
                        }
                        break;
                    case "Tamato Berry":
                        if ((int.Parse(lbl_Spd.Text) - item.Value) <= 0) {
                            lbl_Spd.Text = "0"; }
                        else
                        {
                            if (item.Value <= (510-int.Parse(lbl_Left.Text))*-1) {
                                item.Value = (510-int.Parse(lbl_Left.Text))*-1;
                                lbl_Left.Text = (item.Value*-1).ToString(); }
                            lbl_Spd.Text = (int.Parse(lbl_Spd.Text) + item.Value).ToString();
                        }
                        break;
                }

                lbl_Total.Text = (int.Parse(lbl_HP.Text) + int.Parse(lbl_Atk.Text) + int.Parse(lbl_Def.Text) + int.Parse(lbl_SpAtk.Text) + int.Parse(lbl_SpDef.Text) + int.Parse(lbl_Spd.Text)).ToString();
                lbl_Left.Text = (510 - int.Parse(lbl_Total.Text)).ToString();
            }

            await frm_UsableItems.ScaleTo(.1, 250);
            frm_UsableItems.IsVisible = false;
        }

        public void addEvs(object sender, EventArgs args)
        {
            int PokerusMultiplayer = 1, SOSMultiplayer = 1;
            int anklet = 0, band = 0, lens = 0, brace = 1,belt=0,bracer=0,weight=0;
            hp = 0; atk = 0; def = 0; spatk = 0; spdef = 0; spd = 0;
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

                hp =  ((weight + vsPokemon.HP) * PokerusMultiplayer * SOSMultiplayer * brace);
                atk = ((bracer + vsPokemon.Atk) * PokerusMultiplayer * SOSMultiplayer * brace);
                def = ((belt + vsPokemon.Def) * PokerusMultiplayer * SOSMultiplayer * brace);
                spatk = ((lens + vsPokemon.SpAtk) * PokerusMultiplayer * SOSMultiplayer * brace);
                spdef = ((band + vsPokemon.SpDef) * PokerusMultiplayer * SOSMultiplayer * brace);
                spd = ((anklet + vsPokemon.Spd) * PokerusMultiplayer * SOSMultiplayer * brace);

                if((int.Parse(lbl_HP.Text) + hp) >= 252) { lbl_HP.Text = "252"; }
                else { if(hp>=int.Parse(lbl_Left.Text)) { hp = int.Parse(lbl_Left.Text); lbl_Left.Text = "0"; }
                    lbl_HP.Text = (int.Parse(lbl_HP.Text) + hp).ToString();
                }

                if ((int.Parse(lbl_Atk.Text) + atk) >= 252) { lbl_Atk.Text = "252"; }
                else {
                    if (atk >= int.Parse(lbl_Left.Text)) { atk = int.Parse(lbl_Left.Text); lbl_Left.Text = "0"; }
                    lbl_Atk.Text = (int.Parse(lbl_Atk.Text) + atk).ToString(); }

                if ((int.Parse(lbl_Def.Text) + def) >= 252) { lbl_Def.Text = "252"; }
                else {
                    if (def >= int.Parse(lbl_Left.Text)) { def = int.Parse(lbl_Left.Text); lbl_Left.Text = "0"; }
                    lbl_Def.Text = (int.Parse(lbl_Def.Text) + def).ToString(); }

                if ((int.Parse(lbl_SpAtk.Text) + spatk) >= 252) { lbl_SpAtk.Text = "252"; }
                else {
                    if (spatk >= int.Parse(lbl_Left.Text)) { spatk = int.Parse(lbl_Left.Text); lbl_Left.Text = "0"; }
                    lbl_SpAtk.Text = (int.Parse(lbl_SpAtk.Text) + spatk).ToString(); }

                if ((int.Parse(lbl_SpDef.Text) + spdef) >= 252) { lbl_SpDef.Text = "252"; }
                else {
                    if (spdef >= int.Parse(lbl_Left.Text)) { spdef = int.Parse(lbl_Left.Text); lbl_Left.Text = "0"; }
                    lbl_SpDef.Text = (int.Parse(lbl_SpDef.Text) + spdef).ToString(); }

                if ((int.Parse(lbl_Spd.Text) + spd) >= 252) { lbl_Spd.Text = "252"; }
                else {
                    if (spd >= int.Parse(lbl_Left.Text)) { spd = int.Parse(lbl_Left.Text); lbl_Left.Text = "0"; }
                    lbl_Spd.Text = (int.Parse(lbl_Spd.Text) + spd).ToString(); }

                lbl_Total.Text = (int.Parse(lbl_HP.Text) + int.Parse(lbl_Atk.Text) + int.Parse(lbl_Def.Text) + int.Parse(lbl_SpAtk.Text) + int.Parse(lbl_SpDef.Text) + int.Parse(lbl_Spd.Text)).ToString();
                lbl_Left.Text = (510 - int.Parse(lbl_Total.Text)).ToString();
                lbl_defeated.Text = (int.Parse(lbl_defeated.Text) + 1)+"";
                img_Minus.IsEnabled = true;
                img_Pluse.IsEnabled = true;
            }
            img_SaveIcon.Source = "SaveIcon";
        }

        public void removeEvs(object sender, EventArgs args)
        {
            if ((int.Parse(lbl_HP.Text) - hp) <= 0) { lbl_HP.Text = "0"; }
            else { lbl_HP.Text = (int.Parse(lbl_HP.Text) - hp).ToString(); }

            if ((int.Parse(lbl_Atk.Text) - atk) <= 0) { lbl_Atk.Text = "0"; }
            else { lbl_Atk.Text = (int.Parse(lbl_Atk.Text) - atk).ToString(); }

            if ((int.Parse(lbl_Def.Text) - def) <= 0) { lbl_Def.Text = "0"; }
            else { lbl_Def.Text = (int.Parse(lbl_Def.Text) - def).ToString(); }

            if ((int.Parse(lbl_SpAtk.Text) - spatk) <= 0) { lbl_SpAtk.Text = "0"; }
            else { lbl_SpAtk.Text = (int.Parse(lbl_SpAtk.Text) - spatk).ToString(); }

            if ((int.Parse(lbl_SpDef.Text) - spdef) <= 0) { lbl_SpDef.Text = "0"; }
            else { lbl_SpDef.Text = (int.Parse(lbl_SpDef.Text) - spdef).ToString(); }

            if ((int.Parse(lbl_Spd.Text) - spd) <= 0) { lbl_Spd.Text = "0"; }
            else { lbl_Spd.Text = (int.Parse(lbl_Spd.Text) - spd).ToString(); }

            lbl_Total.Text = (int.Parse(lbl_HP.Text) + int.Parse(lbl_Atk.Text) + int.Parse(lbl_Def.Text) + int.Parse(lbl_SpAtk.Text) + int.Parse(lbl_SpDef.Text) + int.Parse(lbl_Spd.Text)).ToString();
            lbl_Left.Text = (510 - int.Parse(lbl_Total.Text)).ToString();
            lbl_defeated.Text = (int.Parse(lbl_defeated.Text) - 1) + "";
            img_Minus.IsEnabled = false;
            img_SaveIcon.Source = "SaveIcon";
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
