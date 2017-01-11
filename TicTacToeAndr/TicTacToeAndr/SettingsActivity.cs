using System;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;

namespace TicTacToeAndr
{
    [Activity(Label = "Tic Tac Toe", MainLauncher = false)]
    public class SettingsActivity : Activity
    {
        private char firstPlayer;
        private bool isSoundsEnabled;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            firstPlayer = Intent.Extras.GetChar("FirstPlayer");
            isSoundsEnabled = Intent.Extras.GetBoolean("Sounds");

            if(!"XO".Contains(firstPlayer))
                throw new ArgumentException("First player must be either X or O");

            SetContentView(Resource.Layout.Settings);

            FindViewById<Spinner>(Resource.Id.FirstPlayerSpinner)
                .SetSelection(firstPlayer == 'X' ? 0 : 1);

            FindViewById<Switch>(Resource.Id.SoundsSwitch).Checked = isSoundsEnabled;
        }

        public override void OnBackPressed()
        {
            Intent.RemoveExtra("FirstPlayer");
            Intent.RemoveExtra("Sounds");

            firstPlayer = FindViewById<Spinner>(Resource.Id.FirstPlayerSpinner).SelectedItemId == 0 ? 'X' : 'O';

            Intent.PutExtra("FirstPlayer", firstPlayer);
            Intent.PutExtra("Sounds", FindViewById<Switch>(Resource.Id.SoundsSwitch).Checked);

            SetResult(Result.Ok, Intent);
            Finish();
            base.OnBackPressed();
        }
    }
}