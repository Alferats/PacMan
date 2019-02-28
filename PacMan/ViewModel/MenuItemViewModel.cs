using Core;
using PacMan.Model.GameModel;
using Services.Commands;
using System.Windows.Input;

namespace PacMan.ViewModel
{
    public class MenuItemViewModel
    {
        private readonly GameModel _game;

        private readonly IMovingAlgorithm _movingAlgorithm;

        public MenuItemViewModel(IMovingAlgorithm movingAlgorithm, GameModel game)
        {
            _game = game;
            Header = movingAlgorithm.Name;
            _movingAlgorithm = movingAlgorithm;
            if (_movingAlgorithm == _game.SelectedAlgorithm) IsChecked = true;
        }

        public string Header { get; }

        public bool IsChecked { get; set; }

        private RelayCommand _changeAlgorithm;
        public ICommand ChangeAlgorithm
        {
            get
            {
                return _changeAlgorithm ??
                       (_changeAlgorithm = new RelayCommand(obj =>
                       {
                           _game.ChangeAlgorithm(_movingAlgorithm);
                       }));
            }
        }
    }
}
