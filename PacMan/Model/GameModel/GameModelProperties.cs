using Core;
using DataLayer.Concrete;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using DataLayer.Entities;

namespace PacMan.Model.GameModel
{
    public partial class GameModel : INotifyPropertyChanged
    {
        #region Notifier
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private const int TimeUpdateClock = 200;
        private const int TimeUpdatePucMan = 200;
        private const int TimeUpdateGhosts = 240;
        private const int TimeUpdateGame = 30;

        private const double ValueDecreaseClock = (double)TimeUpdateClock / 1000;

        private const int SizeObjectsOnMap = 24;
        private const int NumberVerticalBlocksMap = 21;
        private const int NumberHorizontalBlocksMap = 31;
        private const int StepMovingObjects = 8;

        private readonly System.Timers.Timer _timerClockUpdate = new System.Timers.Timer();
        private readonly System.Timers.Timer _timerGameUpdate = new System.Timers.Timer();
        private readonly System.Timers.Timer _timerPacManUpdate = new System.Timers.Timer();
        private readonly System.Timers.Timer _timerGhostsUpdate = new System.Timers.Timer();
        private readonly Random _pseudoRandom = new Random();
        
        private readonly TimeSpan _timeGameOver = new TimeSpan(0, 0, 0, 0, 500);

        private readonly CanvasObject _pacManStartPositionPoint = new CanvasObject { Width = SizeObjectsOnMap, Height = SizeObjectsOnMap, Y = 0, X = 0 };
        private readonly UnitOfWork _dataBase = new UnitOfWork();

        private PucMan _currentPucMan;

        private List<Ghost> _ghosts = new List<Ghost>();

        public Record CurrentRecord => new Record() { PlayerName = PlayerName, Score = Score };
        public List<Record> AllRecords => new List<Record>(_dataBase.Records.GetAll());

        private List<Fire> _fires = new List<Fire>();

        private Direction _selectPucManDirection = Direction.None;

        private readonly MatrixCreator _matrixCreator = new MatrixCreator(NumberHorizontalBlocksMap, NumberVerticalBlocksMap);

        public bool[,] MapMatrix { get; set; } = new bool[NumberVerticalBlocksMap,NumberHorizontalBlocksMap];

        public bool IsGameNotBegun { get; private set; } = true;

        public List<IMovingAlgorithm> GhostsMovingAlgorithms { get; private set; } = new List<IMovingAlgorithm>();

        private IMovingAlgorithm _selectedAlgorithm;
        public IMovingAlgorithm SelectedAlgorithm
        {
            get => _selectedAlgorithm;
            private set
            {
                _selectedAlgorithm = value;
                OnPropertyChanged();
            }
        }

        private Canvas _location;
        public Canvas Location
        {
            get => _location;
            private set
            {
                _location = value;
                OnPropertyChanged();
            }
        }

        private string _playerName = "New Player";
        public string PlayerName
        {
            get => _playerName;
            private set
            {
                _playerName = value;
                OnPropertyChanged();
            }
        }

        private int _score;
        public int Score
        {
            get => _score;
            private set
            {
                _score = value;
                OnPropertyChanged();
            }
        }

        private int _lives = 1;
        public int Lives
        {
            get => _lives;
            private set
            {
                _lives = value;
                OnPropertyChanged();
            }
        }

        private bool _isGameOver;
        public bool IsGameOver
        {
            get => _isGameOver;
            private set
            {
                _isGameOver = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _timeCurrentGameCurrentGame = new TimeSpan(0, 3, 0);
        public TimeSpan TimeCurrentGame
        {
            get => _timeCurrentGameCurrentGame;
            private set
            {
                _timeCurrentGameCurrentGame = value;
                OnPropertyChanged();
            }
        }
    }
}
