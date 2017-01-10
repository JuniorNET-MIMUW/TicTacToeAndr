using System;
using System.Collections;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Widget;
using Android.OS;

namespace TicTacToeAndr
{
    [Activity(Label = "TicTacToeAndr", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private readonly Button[,] _board;

        private bool _last;
        private int _counter;
        
        
        public MainActivity()
        {
            _board = new[,]
            {
                {
                    FindViewById<Button>(Resource.Id.button11),
                    FindViewById<Button>(Resource.Id.button12),
                    FindViewById<Button>(Resource.Id.button13)
                },
                {
                    FindViewById<Button>(Resource.Id.button21),
                    FindViewById<Button>(Resource.Id.button22),
                    FindViewById<Button>(Resource.Id.button23)
                },
                {
                    FindViewById<Button>(Resource.Id.button31),
                    FindViewById<Button>(Resource.Id.button32),
                    FindViewById<Button>(Resource.Id.button33)
                }
            };
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            /*
            var button11 = FindViewById<Button>(Resource.Id.button11);
            var button12 = FindViewById<Button>(Resource.Id.button12);
            var button13 = FindViewById<Button>(Resource.Id.button13);
            var button21 = FindViewById<Button>(Resource.Id.button21);
            var button22 = FindViewById<Button>(Resource.Id.button22);
            var button23 = FindViewById<Button>(Resource.Id.button23);
            var button31 = FindViewById<Button>(Resource.Id.button31);
            var button32 = FindViewById<Button>(Resource.Id.button32);
            var button33 = FindViewById<Button>(Resource.Id.button33);*/


            _board[0,0].Click += GetEventHandler(0, 0);
            _board[0,1].Click += GetEventHandler(0, 1);
            _board[0,2].Click += GetEventHandler(0, 2);
            _board[1,0].Click += GetEventHandler(1, 0);
            _board[1,1].Click += GetEventHandler(1, 1);
            _board[1,2].Click += GetEventHandler(1, 2);
            _board[2,0].Click += GetEventHandler(2, 0);
            _board[2,1].Click += GetEventHandler(2, 1);
            _board[2,2].Click += GetEventHandler(2, 2);

            _board[0,0].Text = ButtonsValues.Buttons[0,0];
            _board[0,1].Text = ButtonsValues.Buttons[0,1];
            _board[0,2].Text = ButtonsValues.Buttons[0,2];
            _board[1,0].Text = ButtonsValues.Buttons[1,0];
            _board[1,1].Text = ButtonsValues.Buttons[1,1];
            _board[1,2].Text = ButtonsValues.Buttons[1,2];
            _board[2,0].Text = ButtonsValues.Buttons[2,0];
            _board[2,1].Text = ButtonsValues.Buttons[2,1];
            _board[2,2].Text = ButtonsValues.Buttons[2,2];

            FindViewById<Button>(Resource.Id.ResetButton).Click += ResetClickHandler;
        }

        private void ResetClickHandler(object sender, EventArgs e)
        {
            _board[0,0].Text = ButtonsValues.Buttons[0,0] = string.Empty;
            _board[0,1].Text = ButtonsValues.Buttons[0,1] = string.Empty;
            _board[0,2].Text = ButtonsValues.Buttons[0,2] = string.Empty;
            _board[1,0].Text = ButtonsValues.Buttons[1,0] = string.Empty;
            _board[1,1].Text = ButtonsValues.Buttons[1,1] = string.Empty;
            _board[1,2].Text = ButtonsValues.Buttons[1,2] = string.Empty;
            _board[2,0].Text = ButtonsValues.Buttons[2,0] = string.Empty;
            _board[2,1].Text = ButtonsValues.Buttons[2,1] = string.Empty;
            _board[2,2].Text = ButtonsValues.Buttons[2,2] = string.Empty;
        }

        private EventHandler GetEventHandler(int i, int j)
        {
            var button = _board[i, j];
            Func<bool> doesAnyoneWins = delegate 
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
                }
                else dialog.SetMessage("Game is over, draw");
                dialog.SetPositiveButton("OK", delegate { });
                dialog.Show();
            };

            return (sender, args) =>
            {
                if(button.Text != string.Empty || doesAnyoneWins()) return;
                button.Text = _last ? "O" : "X";
                _last = !_last;
                _counter++;
                if (_counter == 9 || doesAnyoneWins())
                    showDialog();
            };
        }
    }
}

