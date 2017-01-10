using Android.App;
using Android.OS;

namespace TicTacToeAndr
{
    [Activity(Label = "Tic Tac Toe", MainLauncher = false)]
    public class SettingsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Settings);
        }
    }
}