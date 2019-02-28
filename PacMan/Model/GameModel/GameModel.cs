using System;

namespace PacMan.Model.GameModel
{
    public partial class GameModel
    {
        public GameModel()
        {
            CreateTimers();
            CreateLocation();
            GetMovingAlgorithms();
            CreateWall();
            CreatePacMan();
            CreateGhosts();
            CreateFires();
        }

        public void CreateTimers()
        {
            _timerGameUpdate.Elapsed += timerGame_Elapsed;
            _timerGameUpdate.Interval = TimeUpdateGame;

            _timerPacManUpdate.Elapsed += timerPacMan_Elapsed;
            _timerPacManUpdate.Interval = TimeUpdatePucMan;

            _timerGhostsUpdate.Elapsed += timerGhosts_Elapsed;
            _timerGhostsUpdate.Interval = TimeUpdateGhosts;

            _timerClockUpdate.Elapsed += timerClock_Elapsed;
            _timerClockUpdate.Interval = TimeUpdateClock;
        }

        private void StopTimers()
        {
            _timerGameUpdate.Stop();
            _timerPacManUpdate.Stop();
            _timerGhostsUpdate.Stop();
            _timerClockUpdate.Stop();
        }

        private void timerGame_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            PacManSeekingFire();
            

            if (!IsWhetherCaught() && !IsTimeOut()) return;

            StopTimers();

            if (!CheckIsGameOver()) return;

            GameOver();

            void Action()
            {
                Location.Children.Clear();
            }
            CallApplicationDispatcherBeginInvoke(Action);
        }

        private void timerPacMan_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            void Action()
            {
                _currentPucMan.Move(_selectPucManDirection);
            }
            CallApplicationDispatcherBeginInvoke(Action);
        }

        private void timerGhosts_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            foreach (var ghost in _ghosts)
            {
                void Action()
                {
                    ghost.Move(_currentPucMan);
                }
                CallApplicationDispatcherBeginInvoke(Action);
            }
        }

        private void timerClock_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            TimeCurrentGame = TimeCurrentGame - TimeSpan.FromSeconds(ValueDecreaseClock);
        }


        
    }
}
