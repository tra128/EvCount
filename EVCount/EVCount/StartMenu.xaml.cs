using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVCount.Clases;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.SimpleAudioPlayer;
using System.IO;

namespace EVCount
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartMenu : ContentPage
	{
        ISimpleAudioPlayer IntroTheme;
        Frame MainCurrentFrame;
        String MainFileNumber;
		public StartMenu ()
		{
            try { 
			InitializeComponent ();
            }
            catch (Exception e){ }
            IntroTheme = CrossSimpleAudioPlayer.Current;
            IntroTheme.Load("IntroTheme.mp3");
            IntroTheme.Loop=true;
            IntroTheme.Play();
		}

        public async void StartBtnTapped(object sender, EventArgs args)
        {
            
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string File1 = Path.Combine(path, "SaveFile#1.txt");
            string File2 = Path.Combine(path, "SaveFile#2.txt");
            string File3 = Path.Combine(path, "SaveFile#3.txt");
            if (File.Exists(File1)){
                List<Trainer> trainer1 = new List<Trainer>();
                String[]Data = File.ReadAllLines(File1);
                trainer1.Add(new Trainer { Image = Data[0], Name = Data[1], Pokemon = Data[2], HP = int.Parse(Data[3]), Atk = int.Parse(Data[4]), Def = int.Parse(Data[5]), SpAtk = int.Parse(Data[6]), SpDef = int.Parse(Data[7]), Spd = int.Parse(Data[8]) });
                lv_SaveFile1.ItemsSource = trainer1;
                grd_SaveFile1.IsVisible = true;
                lb_NewFile1.IsVisible = false;
            }

            if (File.Exists(File2))
            {
                List<Trainer> trainer1 = new List<Trainer>();
                String[] Data = File.ReadAllLines(File2);
                trainer1.Add(new Trainer { Image = Data[0], Name = Data[1], Pokemon = Data[2], HP = int.Parse(Data[3]), Atk = int.Parse(Data[4]), Def = int.Parse(Data[5]), SpAtk = int.Parse(Data[6]), SpDef = int.Parse(Data[7]), Spd = int.Parse(Data[8]) });
                lv_SaveFile2.ItemsSource = trainer1;
                grd_SaveFile2.IsVisible = true;
                lb_NewFile2.IsVisible = false;
            }

            if (File.Exists(File3))
            {
                List<Trainer> trainer1 = new List<Trainer>();
                String[] Data = File.ReadAllLines(File3);
                trainer1.Add(new Trainer { Image = Data[0], Name = Data[1], Pokemon = Data[2], HP = int.Parse(Data[3]), Atk = int.Parse(Data[4]), Def = int.Parse(Data[5]), SpAtk = int.Parse(Data[6]), SpDef = int.Parse(Data[7]), Spd = int.Parse(Data[8]) });
                lv_SaveFile3.ItemsSource = trainer1;
                grd_SaveFile3.IsVisible = true;
                lb_NewFile3.IsVisible = false;
            }

            await stl_StartMenu.FadeTo(-1, 2500);
            stl_SaveDataMenu.IsVisible = true;

            stl_StartMenu.IsVisible = false;
            await stl_SaveDataMenu.FadeTo(1, 2500);

        }

        public async void NewFileTapped(object sender, EventArgs args)
        {
            MainCurrentFrame = ((sender as Label).Parent as Grid).Parent as Frame;
            switch((sender as Label).Text.ToLower())
            {
                case "new file 1":
                    MainFileNumber = "#1";
                    break;
                case "new file 2":
                    MainFileNumber = "#2";
                    break;
                case "new file 3":
                    MainFileNumber = "#3";
                    break;
            }
            frm_NewUser.IsVisible = true;
            await frm_NewUser.ScaleTo(1, 500);
        }

        public async void DeleteTapped(object sender, EventArgs args) {
            if(await DisplayAlert("Delete","The File Will Be Deleted, Are You Sure You Want To Do This?", "Delete", "Cancel")) {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var CurrentGrid = ((sender as Image).Parent as Grid);
                String SaveFileNumber =(CurrentGrid.Children.ElementAt(1) as Label).Text;
                string User_filename = Path.Combine(path, "SaveFile"+SaveFileNumber+".txt");
                if (File.Exists(User_filename)) {
                    File.Delete(User_filename);
                }
                var CurrentFrame = ((CurrentGrid.Parent as Grid).Parent as Frame);
                await CurrentFrame.ScaleTo(.1, 250);
                (CurrentGrid.Parent as Grid).Children.ElementAt(0).IsVisible = false;
                (CurrentGrid.Parent as Grid).Children.ElementAt(1).IsVisible = true;
                await CurrentFrame.ScaleTo(1, 250);
            }
        }

        public void SaveData()
        {

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string User_filename = Path.Combine(path, "UFSD.txt");
            string Config_filename = Path.Combine(path, "CFSD.txt");

            if (!File.Exists(User_filename))
            {
                //Sesion u_id u_nombre u_password u_email
                String[] User_Data = new string[] { "false", "null", "NoUser", "Name", "null", "null" };
                String[] Config_Data = new string[] { "false", "0", "null", };

                File.WriteAllLines(User_filename, User_Data);
                File.WriteAllLines(Config_filename, Config_Data);
                
            }
            else
            {
                string[] toTestBy = File.ReadAllLines(User_filename);
            }
        }

        private void ntry_Name_Completed(object sender, EventArgs e)
        {
            ntry_Pokemon.Focus();
        }

        public async void CreateSaveFile(object sender, EventArgs args) {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string User_filename = Path.Combine(path, "SaveFile" + MainFileNumber + ".txt");
            string SaveFileNumber = "SaveFile" + MainFileNumber + ".txt";
            string[] Data = new string[] {"SunIcon",ntry_Name.Text,ntry_Pokemon.Text, "0", "0", "0", "0", "0", "0" };
            File.WriteAllLines(User_filename, Data);
            await frm_NewUser.ScaleTo(0, 500);
            ntry_Name.Text = "";
            ntry_Pokemon.Text = "";
            frm_NewUser.IsVisible = false;
            List<Trainer> trainer1 = new List<Trainer>();
            switch (SaveFileNumber)
            {
                case "SaveFile#1.txt":
                    Data = File.ReadAllLines(User_filename);
                    trainer1.Clear();
                    trainer1.Add(new Trainer { Image=Data[0], Name = Data[1], Pokemon = Data[2], HP = int.Parse(Data[3]), Atk = int.Parse(Data[4]), Def = int.Parse(Data[5]), SpAtk = int.Parse(Data[6]), SpDef = int.Parse(Data[7]), Spd = int.Parse(Data[8]) });
                    lv_SaveFile1.ItemsSource = trainer1;
                    await MainCurrentFrame.ScaleTo(.1, 250);
                    grd_SaveFile1.IsVisible = true;
                    lb_NewFile1.IsVisible = false;
                    await MainCurrentFrame.ScaleTo(1, 250);
                    break;
                case "SaveFile#2.txt":
                    Data = File.ReadAllLines(User_filename);
                    trainer1.Clear();
                    trainer1.Add(new Trainer { Image = Data[0], Name = Data[1], Pokemon = Data[2], HP = int.Parse(Data[3]), Atk = int.Parse(Data[4]), Def = int.Parse(Data[5]), SpAtk = int.Parse(Data[6]), SpDef = int.Parse(Data[7]), Spd = int.Parse(Data[8]) });
                    lv_SaveFile2.ItemsSource = trainer1;
                    await MainCurrentFrame.ScaleTo(.1, 250);
                    grd_SaveFile2.IsVisible = true;
                    lb_NewFile2.IsVisible = false;
                    await MainCurrentFrame.ScaleTo(1, 250);
                    break;
                case "SaveFile#3.txt":
                    Data = File.ReadAllLines(User_filename);
                    trainer1.Clear();
                    trainer1.Add(new Trainer { Image = Data[0], Name = Data[1], Pokemon = Data[2], HP = int.Parse(Data[3]), Atk = int.Parse(Data[4]), Def = int.Parse(Data[5]), SpAtk = int.Parse(Data[6]), SpDef = int.Parse(Data[7]), Spd = int.Parse(Data[8]) });
                    lv_SaveFile3.ItemsSource = trainer1;
                    await MainCurrentFrame.ScaleTo(.1, 250);
                    grd_SaveFile3.IsVisible = true;
                    lb_NewFile3.IsVisible = false;
                    await MainCurrentFrame.ScaleTo(1, 250);
                    break;

            }

        }

        protected override bool OnBackButtonPressed()
        {
            if (!stl_StartMenu.IsVisible) { 
                stl_SaveDataMenu.IsVisible = false;
                stl_SaveDataMenu.Opacity = 0;
                frm_NewUser.IsVisible = false;
                frm_NewUser.Scale = .1;
                stl_StartMenu.Opacity = 1;
                stl_StartMenu.IsVisible = true;
                return true;
            }
            else
            {
                IntroTheme.Stop();
                return false;
            }
            
        }

        public async void CloseFileCreation(object sender, EventArgs args)
        {
            await frm_NewUser.ScaleTo(0, 500);
            frm_NewUser.IsVisible = false;
        }

        private void lv_SaveFile_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            
            var trainer = (sender as ListView).SelectedItem as Trainer;
             Navigation.PushAsync(new MainPage(trainer));
        }
    }
}