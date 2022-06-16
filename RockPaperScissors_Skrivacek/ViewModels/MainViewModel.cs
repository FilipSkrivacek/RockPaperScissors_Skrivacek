using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfRPS.ViewModels;

namespace RockPaperScissors_Skrivacek.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
       
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _running;
        private int _userscore;
        private int _pcscore;
        private Random _rnd = new Random();
        private Item _user;
        private Item _pc;

        public MainViewModel()
        {
            PcScore = 0;
            UserScore = 0;
            User = Item.Nothing;
            Pc = Item.Nothing;        
            Running = false;

            StartButtonCommand = new RelayCommand(
                () =>
                {
                    PcScore = 0;
                    UserScore = 0;
                    User = Item.Nothing;
                    Pc = Item.Nothing;
                    Running = true;
                    StartButtonCommand.RaiseCanExecuteChanged();
                    GameCommand.RaiseCanExecuteChanged();

                },
                () =>
                {
                    return !Running;
                }
              );

            GameCommand = new ParametrizedRelayCommand<string>(
                (parameter) =>
                {                  
                    Pc = (Item)_rnd.Next(1, Enum.GetNames(typeof(Item)).Length);
                    User = (Item)Convert.ToInt32(parameter);

                    if (Pc == Item.Paper && User == Item.Rock || Pc == Item.Scissors && User == Item.Paper || Pc == Item.Rock && User == Item.Scissors)
                    {
                        PcScore++;
                    }
                    else if ( User == Item.Paper && Pc == Item.Rock || User == Item.Scissors && Pc == Item.Paper || User == Item.Rock && Pc == Item.Scissors)
                    {
                        UserScore++;
                    }

                    if (UserScore == 5 || PcScore == 5)
                    {
                        Running = false;
                    }

                    StartButtonCommand.RaiseCanExecuteChanged();
                    GameCommand.RaiseCanExecuteChanged();
                },
                (parameter) =>
                {
                    return Running;
                }
              );
                 

        }

        public Item User { get { return _user; } set { _user = value; NotifyPropertyChanged(); } }
        public int UserScore { get { return _userscore; } set { _userscore = value; NotifyPropertyChanged(); } }
        public Item Pc { get { return _pc; } set { _pc = value; NotifyPropertyChanged(); } }
        public int PcScore { get { return _pcscore; } set { _pcscore = value; NotifyPropertyChanged(); } }
        public bool Running { get { return _running; } set { _running = value; NotifyPropertyChanged(); } }
        public RelayCommand StartButtonCommand { get; set; }
        public ParametrizedRelayCommand<string> GameCommand { get; set; }

        internal enum Item
        {
            Nothing = 0,
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }

    }
}
