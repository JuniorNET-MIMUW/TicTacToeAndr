using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;

namespace TicTacToeAndr
{
    [Activity(Label = "Tic Tac Toe", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private const int SettingsCode = 42;

        private char firstPlayer = 'X';
        private bool isSoundsEnabled = true;

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
            FindViewById<Button>(Resource.Id.SettingsButton).Click += SettingsClick;
            
            /*delegate 
            {
                var builder = new AlertDialog.Builder(this);
                builder.SetMessage("Under development");
                builder.SetPositiveButton("OK", delegate { });
                builder.Show();
            };*/
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == SettingsCode && resultCode == Result.Ok)
            {
                firstPlayer = data.Extras.GetChar("FirstPlayer");
                isSoundsEnabled = data.Extras.GetBoolean("Sounds");
            }
        }

        private void SettingsClick(object sender, EventArgs e)
        {
            var intent = GetIntent(typeof(SettingsActivity));
            StartActivityForResult(intent, SettingsCode);
        }

        private void ContinueClick(object sender, EventArgs e)
        {
            var intent = GetIntent(typeof(GameActivity));
            StartActivity(intent);
        }

        private void NewGameClick(object sender, EventArgs e)
        {
            File.WriteAllText(Path, EmptyBoard);
            var intent = GetIntent(typeof(GameActivity));
            StartActivity(intent);
        }

        private Intent GetIntent(Type type)
        {
            var intent = new Intent(this, type);
            intent.PutExtra("FirstPlayer", firstPlayer);
            intent.PutExtra("Sounds", isSoundsEnabled);
            return intent;
        }
    }
}

