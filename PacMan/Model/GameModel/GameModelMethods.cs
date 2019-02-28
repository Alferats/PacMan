using Core;
using PacMan.Model.Images;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PacMan.Model.GameModel
{
    public partial class GameModel
    {
        public void StartGame()
        {
            if(!IsGameNotBegun)
                return;
            Lives--;
            IsGameNotBegun = false;

            _timerGameUpdate.Start();
            _timerPacManUpdate.Start();
            _timerGhostsUpdate.Start();
            _timerClockUpdate.Start();
        }

        public void ReCreateGame()
        {
            IsGameNotBegun = true;
            IsGameOver = false;
            _timeCurrentGameCurrentGame = new TimeSpan(0, 3, 0);
            _score = 0;
            _lives = 3;
            Location = null;
            GhostsMovingAlgorithms = new List<IMovingAlgorithm>();
            _ghosts = new List<Ghost>();
            _fires = new List<Fire>();
            _currentPucMan = null;
            void Action()
            {
                CreateLocation();
                GetMovingAlgorithms();
                CreateWall();
                CreatePacMan();
                CreateGhosts();
                CreateFires();
                ToStartPosition();
            }
            CallApplicationDispatcherBeginInvoke(Action);
        }

        public void GameOver()
        {
            _dataBase.Records.Create(CurrentRecord);
            _dataBase.Save();
        }

        public void ChangeName(string newName)
        {
            PlayerName = newName;
        }

        public void GetMovingAlgorithms()
        {
            GhostsMovingAlgorithms.Clear();

            System.IO.Directory.CreateDirectory(StaticStrings.FolderPlugins);
            var catalog = new DirectoryCatalog(StaticStrings.FolderPlugins);
            var container = new CompositionContainer(catalog);
            var importer = new Importer();

            container.ComposeExportedValue(MapMatrix);
            container.ComposeParts(importer);

            foreach (var import in importer.ImportedMembers)
            {
                GhostsMovingAlgorithms.Add(import);
            }

            _selectedAlgorithm = new GhostMovingBasicAlgorithm(MapMatrix);
            GhostsMovingAlgorithms.Add(_selectedAlgorithm);
        }

        public void SetPucManDirection(Direction direction)
        {
            _selectPucManDirection = direction;
        }

        private void CreateLocation()
        {
            MapMatrix = _matrixCreator.Generate();

            Location = new Canvas { Background = new SolidColorBrush(Colors.Black), Width = SizeObjectsOnMap * NumberHorizontalBlocksMap, Height = SizeObjectsOnMap * NumberVerticalBlocksMap };

        }

        private void CreatePacMan()
        {
            var pacManMovingControl = new MapMovingControl(MapMatrix, NumberVerticalBlocksMap * SizeObjectsOnMap, NumberHorizontalBlocksMap * SizeObjectsOnMap, TimeUpdatePucMan,
                StaticStrings.PacManUpImg, StaticStrings.PacManLeftImg, StaticStrings.PacManRightImg, StaticStrings.PacManDownImg, StaticStrings.PacManBasicImg);

            _currentPucMan = new PucMan(pacManMovingControl, StepMovingObjects, StaticStrings.PacManBasicImg, _pacManStartPositionPoint);

            Location.Children.Add(_currentPucMan.Image);
        }

        private void CreateWall()
        {
            var wallIm = new BitmapImage();
            wallIm.BeginInit();
            wallIm.UriSource = new Uri(StaticStrings.WallImg, UriKind.Relative);
            wallIm.EndInit();

            for (var row = 0; row < NumberVerticalBlocksMap; row++)
            {
                for (var col = 0; col < NumberHorizontalBlocksMap; col++)
                {
                    if (MapMatrix[row,col])
                        continue;

                    var wall = new Image()
                    {
                        Width = SizeObjectsOnMap,
                        Height = SizeObjectsOnMap,
                        Source = wallIm
                    };

                    Canvas.SetTop(wall, row * SizeObjectsOnMap);
                    Canvas.SetLeft(wall, col * SizeObjectsOnMap);
                    Location.Children.Add(wall);
                }
            }
        }
        
        private void CreateGhosts()
        {
            var ghostPinkMovingControl = new MapMovingControl(MapMatrix, NumberVerticalBlocksMap * SizeObjectsOnMap, NumberHorizontalBlocksMap * SizeObjectsOnMap, TimeUpdateGhosts,
                StaticStrings.PinkGhostUpImg, StaticStrings.PinkGhostLeftImg, StaticStrings.PinkGhostRightImg, StaticStrings.PinkGhostDownImg, StaticStrings.PinkGhostBasicImg, false);
            var ghostRedMovingControl = new MapMovingControl(MapMatrix, NumberVerticalBlocksMap * SizeObjectsOnMap, NumberHorizontalBlocksMap * SizeObjectsOnMap, TimeUpdateGhosts,
                StaticStrings.RedGhostUpImg, StaticStrings.RedGhostLeftImg, StaticStrings.RedGhostRightImg, StaticStrings.RedGhostDownImg, StaticStrings.RedGhostBasicImg, false);
            var ghostBlueMovingControl = new MapMovingControl(MapMatrix, NumberVerticalBlocksMap * SizeObjectsOnMap, NumberHorizontalBlocksMap * SizeObjectsOnMap, TimeUpdateGhosts,
                StaticStrings.BlueGhostUpImg, StaticStrings.BlueGhostLeftImg, StaticStrings.BlueGhostRightImg, StaticStrings.BlueGhostDownImg, StaticStrings.BlueGhostBasicImg, false);
            var ghostYellowMovingControl = new MapMovingControl(MapMatrix, NumberVerticalBlocksMap * SizeObjectsOnMap, NumberHorizontalBlocksMap * SizeObjectsOnMap, TimeUpdateGhosts,
                StaticStrings.YellowGhostUpImg, StaticStrings.YellowGhostLeftImg, StaticStrings.YellowGhostRightImg, StaticStrings.YellowGhostDownImg, StaticStrings.YellowGhostBasicImg, false);

            var pinkGhost = new Ghost(ghostPinkMovingControl, SelectedAlgorithm, StepMovingObjects, StaticStrings.PinkGhostBasicImg,
                CreateGhostMapPoint(_pseudoRandom), SizeObjectsOnMap * NumberHorizontalBlocksMap, SizeObjectsOnMap * NumberVerticalBlocksMap);
            _ghosts.Add(pinkGhost);
            Location.Children.Add(pinkGhost.Image);

            var redGhost = new Ghost(ghostRedMovingControl, SelectedAlgorithm, StepMovingObjects, StaticStrings.RedGhostBasicImg,
                CreateGhostMapPoint(_pseudoRandom), SizeObjectsOnMap * NumberHorizontalBlocksMap, SizeObjectsOnMap * NumberVerticalBlocksMap,
                Direction.Down);
            _ghosts.Add(redGhost);
            Location.Children.Add(redGhost.Image);

            var blueGhost = new Ghost(ghostBlueMovingControl, SelectedAlgorithm, StepMovingObjects, StaticStrings.BlueGhostBasicImg,
                CreateGhostMapPoint(_pseudoRandom), SizeObjectsOnMap * NumberHorizontalBlocksMap, SizeObjectsOnMap * NumberVerticalBlocksMap,
                Direction.Left);
            _ghosts.Add(blueGhost);
            Location.Children.Add(blueGhost.Image);

            var yellowGhost = new Ghost(ghostYellowMovingControl, SelectedAlgorithm, StepMovingObjects, StaticStrings.YellowGhostBasicImg,
                CreateGhostMapPoint(_pseudoRandom), SizeObjectsOnMap * NumberHorizontalBlocksMap, SizeObjectsOnMap * NumberVerticalBlocksMap,
                Direction.Right);
            _ghosts.Add(yellowGhost);
            Location.Children.Add(yellowGhost.Image);
        }

        private CanvasObject CreateGhostMapPoint(Random pseudoRandom)
        {
            var spot = new CanvasObject();

            while (true)
            {
                var xMatrixCoordinate = pseudoRandom.Next(5, NumberHorizontalBlocksMap - 1);
                var yMatrixCoordinate = pseudoRandom.Next(5, NumberVerticalBlocksMap - 1);

                if (!MapMatrix[yMatrixCoordinate,xMatrixCoordinate]) continue;
                spot.X = xMatrixCoordinate * SizeObjectsOnMap;
                spot.Y = yMatrixCoordinate * SizeObjectsOnMap;
                break;
            }
            spot.Width = SizeObjectsOnMap;
            spot.Height = SizeObjectsOnMap;

            return spot;
        }

        public void ChangeAlgorithm(IMovingAlgorithm algorithm)
        {
            SelectedAlgorithm = algorithm;

            foreach (var ghost in _ghosts)
            {
                ghost.ChangeAlgorithm(SelectedAlgorithm);
            }
        }

        private bool IsWhetherCaught()
        {
            foreach (var ghost in _ghosts)
            {
                var distance = Math.Sqrt(Math.Pow(Math.Abs(ghost.CanvasCoordinateX - _currentPucMan.CanvasCoordinateX), 2) +
                                            Math.Pow(Math.Abs(ghost.CanvasCoordinateY - _currentPucMan.CanvasCoordinateY), 2));

                if (distance < _currentPucMan.CanvasHeight - StepMovingObjects || distance < _currentPucMan.CanvasWidth - StepMovingObjects)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsTimeOut()
        {
            return _timeCurrentGameCurrentGame < _timeGameOver;
        }


        private void CreateFires()
        {
            for (int i = 0; i < 25; i++)
            {
                var fire = new Fire(SizeObjectsOnMap, MapMatrix, _fires, _pseudoRandom);
                _fires.Add(fire);
                Location.Children.Add(fire.FireImage);
            }
        }

        private void PacManSeekingFire()
        {
            var fire = FindEatenFire();
            if (fire == null) return;

            void Action()
            {
                Location.Children.Remove(fire.FireImage);
            }
            CallApplicationDispatcherBeginInvoke(Action);

            _fires.Remove(fire);
            Score = _score + 100;
        }

        private Fire FindEatenFire()
        {
            foreach (var fire in _fires)
            {
                var distance = Math.Sqrt(Math.Pow(Math.Abs(fire.CanvasCoordinateX - _currentPucMan.CanvasCoordinateX), 2) +
                                         Math.Pow(Math.Abs(fire.CanvasCoordinateY - _currentPucMan.CanvasCoordinateY), 2));

                if (distance <= StepMovingObjects)
                {
                    return fire;
                }
            }
            return null;
        }

        private void CallApplicationDispatcherBeginInvoke(Action action)
        {
            try
            {
                Application.Current.Dispatcher.BeginInvoke(action);
            }
            catch (Exception e)
            {
                StopTimers();
            }
            
        }

        private bool CheckIsGameOver()
        {
            if (Lives > 0)
            {
                void Action()
                {
                    ToStartPosition();
                }
                CallApplicationDispatcherBeginInvoke(Action);

                return IsGameOver = false;
            }
            return IsGameOver = true;
        }

        private void ToStartPosition()
        {
            IsGameNotBegun = true;

            var co = new CanvasObject() { Height = SizeObjectsOnMap, Width = SizeObjectsOnMap, X = 0, Y = 0 };

            _currentPucMan.ToPosition(co);

            foreach (var ghost in _ghosts)
            {
                ghost.ToPosition(CreateGhostMapPoint(_pseudoRandom));
            }
        }

        
    }
}
