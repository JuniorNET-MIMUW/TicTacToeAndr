using System;
using System.IO;
using System.Linq;

using Android.App;
using Android.Media;
using Android.OS;
using Android.Widget;

namespace TicTacToeAndr
{
    [Activity(Label = "Tic Tac Toe", MainLauncher = false)]
    public class GameActivity : Activity
    {
        private bool isSoundsEnabled;
        private char firstPlayer = 'X';

        private static readonly string Path = 
            System.IO.Path.Combine(System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.LocalApplicationData), "data.txt");

        private bool _last;
        private int _counter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            string[] data = File.ReadAllLines(Path);
            var array = data.Select(line => Parse(line.Split(' '))).ToArray();

            if (Intent.Extras.GetChar("FirstPlayer") == 'O')
            {
                _last = !_last;
                firstPlayer = 'O';
            }

            isSoundsEnabled = Intent.Extras.GetBoolean("Sounds");

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Game);

            SetButtonValue(Resource.Id.button11, array[0][0]);
            SetButtonValue(Resource.Id.button12, array[0][1]);
            SetButtonValue(Resource.Id.button13, array[0][2]);
            SetButtonValue(Resource.Id.button21, array[1][0]);
            SetButtonValue(Resource.Id.button22, array[1][1]);
            SetButtonValue(Resource.Id.button23, array[1][2]);
            SetButtonValue(Resource.Id.button31, array[2][0]);
            SetButtonValue(Resource.Id.button32, array[2][1]);
            SetButtonValue(Resource.Id.button33, array[2][2]);

            FindViewById<Button>(Resource.Id.button11).Click += GetEventHandler(Resource.Id.button11);
            FindViewById<Button>(Resource.Id.button12).Click += GetEventHandler(Resource.Id.button12);
            FindViewById<Button>(Resource.Id.button13).Click += GetEventHandler(Resource.Id.button13);
            FindViewById<Button>(Resource.Id.button21).Click += GetEventHandler(Resource.Id.button21);
            FindViewById<Button>(Resource.Id.button22).Click += GetEventHandler(Resource.Id.button22);
            FindViewById<Button>(Resource.Id.button23).Click += GetEventHandler(Resource.Id.button23);
            FindViewById<Button>(Resource.Id.button31).Click += GetEventHandler(Resource.Id.button31);
            FindViewById<Button>(Resource.Id.button32).Click += GetEventHandler(Resource.Id.button32);
            FindViewById<Button>(Resource.Id.button33).Click += GetEventHandler(Resource.Id.button33);

            FindViewById<Button>(Resource.Id.ResetButton).Click += ResetButtonClick;
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutString("1,1", FindViewById<Button>(Resource.Id.button11).Text);
            outState.PutString("1,2", FindViewById<Button>(Resource.Id.button12).Text);
            outState.PutString("1,3", FindViewById<Button>(Resource.Id.button13).Text);
            outState.PutString("2,1", FindViewById<Button>(Resource.Id.button21).Text);
            outState.PutString("2,2", FindViewById<Button>(Resource.Id.button22).Text);
            outState.PutString("2,3", FindViewById<Button>(Resource.Id.button23).Text);
            outState.PutString("3,1", FindViewById<Button>(Resource.Id.button31).Text);
            outState.PutString("3,2", FindViewById<Button>(Resource.Id.button32).Text);
            outState.PutString("3,3", FindViewById<Button>(Resource.Id.button33).Text);

            base.OnSaveInstanceState(outState);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            _last = false;

            SetButtonValue(Resource.Id.button11, savedInstanceState.GetString("1,1"));
            SetButtonValue(Resource.Id.button12, savedInstanceState.GetString("1,2"));
            SetButtonValue(Resource.Id.button13, savedInstanceState.GetString("1,3"));
            SetButtonValue(Resource.Id.button21, savedInstanceState.GetString("2,1"));
            SetButtonValue(Resource.Id.button22, savedInstanceState.GetString("2,2"));
            SetButtonValue(Resource.Id.button23, savedInstanceState.GetString("2,3"));
            SetButtonValue(Resource.Id.button31, savedInstanceState.GetString("3,1"));
            SetButtonValue(Resource.Id.button32, savedInstanceState.GetString("3,2"));
            SetButtonValue(Resource.Id.button33, savedInstanceState.GetString("3,3"));
        }

        private void ResetButtonClick(object sender, EventArgs e)
        {
            FindViewById<Button>(Resource.Id.button11).Text = string.Empty;
            FindViewById<Button>(Resource.Id.button12).Text = string.Empty;
            FindViewById<Button>(Resource.Id.button13).Text = string.Empty;
            FindViewById<Button>(Resource.Id.button21).Text = string.Empty;
            FindViewById<Button>(Resource.Id.button22).Text = string.Empty;
            FindViewById<Button>(Resource.Id.button23).Text = string.Empty;
            FindViewById<Button>(Resource.Id.button31).Text = string.Empty;
            FindViewById<Button>(Resource.Id.button32).Text = string.Empty;
            FindViewById<Button>(Resource.Id.button33).Text = string.Empty;
            _last = false;
            _counter = 0;
        }

        public override void OnBackPressed()
        {
            AlertDialog.Builder exitDialogBuilder = new AlertDialog.Builder(this);
            exitDialogBuilder.SetMessage("Do you want to save your game?");
            exitDialogBuilder.SetPositiveButton("Save", delegate
            {
                var toWrite = string.Empty;
                toWrite += ButtonToString(Resource.Id.button11) + " ";
                toWrite += ButtonToString(Resource.Id.button12) + " ";
                toWrite += ButtonToString(Resource.Id.button13) + "\n";
                toWrite += ButtonToString(Resource.Id.button21) + " ";
                toWrite += ButtonToString(Resource.Id.button22) + " ";
                toWrite += ButtonToString(Resource.Id.button23) + "\n";
                toWrite += ButtonToString(Resource.Id.button31) + " ";
                toWrite += ButtonToString(Resource.Id.button32) + " ";
                toWrite += ButtonToString(Resource.Id.button33);

                File.WriteAllText(Path, toWrite);
                base.OnBackPressed();
            });
            exitDialogBuilder.SetNegativeButton("Don't save", delegate
            {
                base.OnBackPressed();
            });
            exitDialogBuilder.SetNeutralButton("Cancel", delegate { });
            exitDialogBuilder.Show();
        }

        private void SetButtonValue(int buttonId, string value)
        {
            if (value != string.Empty)
            {
                _last = !_last;
                _counter++;
            }

            FindViewById<Button>(buttonId).Text = value;
        }

//        public override void OnConfigurationChanged(Configuration newConfig)
//        {
//            if (newConfig.Orientation == Orientation.Portrait)
//            {
//                FindViewById<LinearLayout>(Resource.Id.RootLayout).RemoveView(resetButton);
//                FindViewById<TableRow>(Resource.Id.ResetTableRow).AddView(resetButton);
//            }
//            if (newConfig.Orientation == Orientation.Landscape)
//            {
//                FindViewById<TableRow>(Resource.Id.ResetTableRow).RemoveView(resetButton);
//                FindViewById<LinearLayout>(Resource.Id.RootLayout).AddView(resetButton);
//            }
//
//            base.OnConfigurationChanged(newConfig);
//        }

        private string ButtonToString(int buttonId)
        {
            var button = FindViewById<Button>(buttonId);
            return button.Text == string.Empty ? "_" : button.Text;
        }

        private static string[] Parse(string[] array)
        {
            return array.Select(str => str == "_" ? "" : str).ToArray();
        }

        private EventHandler GetEventHandler(int id)
        {
            var button = FindViewById<Button>(id);
            Func<bool> doesAnyoneWins = () =>
            {
                var button11 = FindViewById<Button>(Resource.Id.button11).Text;
                var button12 = FindViewById<Button>(Resource.Id.button12).Text;
                var button13 = FindViewById<Button>(Resource.Id.button13).Text;
                var button21 = FindViewById<Button>(Resource.Id.button21).Text;
                var button22 = FindViewById<Button>(Resource.Id.button22).Text;
                var button23 = FindViewById<Button>(Resource.Id.button23).Text;
                var button31 = FindViewById<Button>(Resource.Id.button31).Text;
                var button32 = FindViewById<Button>(Resource.Id.button32).Text;
                var button33 = FindViewById<Button>(Resource.Id.button33).Text;



                return (button11 == button12 && button12 == button13 && button13 != string.Empty)
                       || (button21 == button22 && button22 == button23 && button23 != string.Empty)
                       || (button31 == button32 && button32 == button33 && button33 != string.Empty)
                       || (button11 == button21 && button21 == button31 && button31 != string.Empty)
                       || (button12 == button22 && button22 == button32 && button32 != string.Empty)
                       || (button13 == button23 && button23 == button33 && button33 != string.Empty)
                       || (button11 == button22 && button22 == button33 && button33 != string.Empty)
                       || (button31 == button22 && button22 == button13 && button13 != string.Empty);
            };

            Action showDialog = () =>
            {
                var dialog = new AlertDialog.Builder(this);
                if (doesAnyoneWins())
                {
                    string whoWins = _last ? "X" : "O";
                    dialog.SetMessage($"Game is over, {whoWins} winning!");
                    if (isSoundsEnabled)
                    {
                        var player = MediaPlayer.Create(this, Resource.Raw.Tada);
                        player.Start();
                    }
                }
                else dialog.SetMessage("Game is over, draw");
                dialog.SetNeutralButton("OK", delegate { });
                dialog.Show();
            };

            return (sender, args) =>
            {
                if (button.Text != string.Empty || doesAnyoneWins()) return;
                button.Text = _last ? "O" : "X";
                _last = !_last;
                _counter++;
                if (_counter == 9)
                {
                    showDialog();
                    return;
                }

                if (doesAnyoneWins())
                    showDialog();
            };
        }
    }
}