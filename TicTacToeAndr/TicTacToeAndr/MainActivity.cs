using System;
using System.IO;
using Android.App;
using Android.Widget;
using Android.OS;

namespace TicTacToeAndr
{
    [Activity(Label = "Tic Tac Toe", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private static readonly string Path =
            System.IO.Path.Combine(System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.LocalApplicationData), "data.txt");

        private static readonly string EmptyBoard = "_ _ _\n_ _ _\n_ _ _";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            if(!File.Exists(Path))
                FindViewById<Button>(Resource.Id.ContinueButton).Enabled = false;

            FindViewById<Button>(Resource.Id.NewGameButton).Click += NewGameClick;
            FindViewById<Button>(Resource.Id.ContinueButton).Click += ContinueClick;
            FindViewById<Button>(Resource.Id.SettingsButton).Click += delegate 
            {
                var builder = new AlertDialog.Builder(this);
                builder.SetMessage("Under development");
                builder.SetPositiveButton("OK", delegate { });
                builder.Show();
            };
        }

        private void ContinueClick(object sender, EventArgs e)
        {
            StartActivity(typeof(GameActivity));
        }

        private void NewGameClick(object sender, EventArgs e)
        {
            File.WriteAllText(Path, EmptyBoard);
            StartActivity(typeof(GameActivity));
        }
    }
}

