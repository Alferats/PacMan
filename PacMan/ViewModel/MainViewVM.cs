using Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using Services.Commands;
using PacMan.Model.GameModel;
using PacMan.View;

namespace PacMan.ViewModel
{
    public class MainViewVm : INotifyPropertyChanged
    {
        #region Notifier
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private readonly GameModel _game;

        public bool IsGameNotBegun => _game.IsGameNotBegun;

        public string Time => "Time: "+ _game.TimeCurrentGame.ToString(@"mm\:ss");

        public string Score => "Score: " +_game.Score;

        public string Lives => "Lives: x" + _game.Lives;

        public bool IsGameOver => _game.IsGameOver;

        private List<MenuItemViewModel> GetPlugins()
        {
            var plugins = new List<MenuItemViewModel>();

            foreach (var algorithm in _game.GhostsMovingAlgorithms)
            {
                var mi = new MenuItemViewModel(algorithm, _game);
                plugins.Add(mi);
            }
            return plugins;
        }

        
        public List<MenuItemViewModel> GhostsMovingAlgorithms => GetPlugins();

        public Canvas Location => _game.Location;

        public MainViewVm()
        {
            _game = new GameModel();
            _game.PropertyChanged += (s, a) => { OnPropertyChanged(nameof(Time)); };
            _game.PropertyChanged += (s, a) => { OnPropertyChanged(nameof(Location)); };
            _game.PropertyChanged += (s, a) => { OnPropertyChanged(nameof(GhostsMovingAlgorithms)); };
            _game.PropertyChanged += (s, a) => { OnPropertyChanged(nameof(IsGameNotBegun)); };
            _game.PropertyChanged += (s, a) => { OnPropertyChanged(nameof(Score)); };
            _game.PropertyChanged += (s, a) => { OnPropertyChanged(nameof(Lives)); };
            _game.PropertyChanged += (s, a) => { OnPropertyChanged(nameof(IsGameOver)); };
        }
 
        private RelayCommand _start;
        public ICommand Start
        {
            get
            {
                return _start ??
                       (_start = new RelayCommand(obj =>
                       {
                           _game.StartGame();
                       }));
            }
        }

        private RelayCommand _recreate;
        public ICommand Recreate
        {
            get
            {
                return _recreate ??
                       (_recreate = new RelayCommand(obj =>
                       {
                           _game.ReCreateGame();
                       }));
            }
        }

        private RelayCommand _showRecords;
        public ICommand ShowRecords
        {
            get
            {
                return _showRecords ??
                       (_showRecords = new RelayCommand(obj =>
                       {
                           RecordsView records = new RecordsView(_game.AllRecords, _game.CurrentRecord);
                           records.ShowDialog();
                       }));
            }
        }

        private RelayCommand _enterName;
        public ICommand EnterName
        {
            get
            {
                return _enterName ??
                       (_enterName = new RelayCommand(obj =>
                       {
                           RenameView rename = new RenameView(_game.PlayerName);
                           rename.ShowDialog();
                           if (rename.DialogResult==true)
                           {
                               _game.ChangeName(rename.PlayerName);
                           }
                           
                       }));
            }
        }

        private RelayCommand _pressDown;
        public ICommand PressDown
        {
            get
            {
                return _pressDown ??
                       (_pressDown = new RelayCommand(obj =>
                       {
                           _game.SetPucManDirection(Direction.Down);
                       }));
            }
        }

        private RelayCommand _pressUp;
        public ICommand PressUp
        {
            get
            {
                return _pressUp ??
                       (_pressUp = new RelayCommand(obj =>
                       {
                           _game.SetPucManDirection(Direction.Up);
                       }));
            }
        }

        private RelayCommand _pressLeft;
        public ICommand PressLeft
        {
            get
            {
                return _pressLeft ??
                       (_pressLeft = new RelayCommand(obj =>
                       {
                           _game.SetPucManDirection(Direction.Left);
                       }));
            }
        }

        private RelayCommand _pressRight;
        public ICommand PressRight
        {
            get
            {
                return _pressRight ??
                       (_pressRight = new RelayCommand(obj =>
                       {
                           _game.SetPucManDirection(Direction.Right);
                       }));
            }
        }
    }
}
