using Core;
using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Services
{
    public class MapMovingControl
    {
        private readonly double _mapHeight;
        private readonly double _mapWidth;
        private readonly double _timeUpdateAnimation;
        private readonly bool _isTransitionState;
        private BitmapImage _defaultIm;
        private BitmapImage _upIm;
        private BitmapImage _leftIm;
        private BitmapImage _rightIm;
        private BitmapImage _downIm;
        private Direction _direction = Direction.None;
        private readonly bool[,] _map;

        public MapMovingControl(bool[,] map, double mapHeight, double mapWidth, int timeUpdateGame, string upIm, string leftIm, string rightIm, string downIm, string defaultIm, bool isTransitionState = true)
        {
            _isTransitionState = isTransitionState;
            _timeUpdateAnimation = (double) timeUpdateGame / 1000;
            _map = map;
            _mapHeight = mapHeight;
            _mapWidth = mapWidth;
            InitBitmapImages(upIm, leftIm, rightIm, downIm, defaultIm);
        }

        private void InitBitmapImages(string upIm, string leftIm, string rightIm, string downIm, string defaultIm)
        {
            _defaultIm = new BitmapImage();
            _defaultIm.BeginInit();
            _defaultIm.UriSource = new Uri(defaultIm, UriKind.Relative);
            _defaultIm.EndInit();

            _upIm = new BitmapImage();
            _upIm.BeginInit();
            _upIm.UriSource = new Uri(upIm, UriKind.Relative);
            _upIm.EndInit();

            _leftIm = new BitmapImage();
            _leftIm.BeginInit();
            _leftIm.UriSource = new Uri(leftIm, UriKind.Relative);
            _leftIm.EndInit();

            _rightIm = new BitmapImage();
            _rightIm.BeginInit();
            _rightIm.UriSource = new Uri(rightIm, UriKind.Relative);
            _rightIm.EndInit();

            _downIm = new BitmapImage();
            _downIm.BeginInit();
            _downIm.UriSource = new Uri(downIm, UriKind.Relative);
            _downIm.EndInit();
        }
  
        private bool _isSwitchImage;

        private void UpdateImage(Image image)
        {
            if (_isTransitionState)
            {
                if (!_isSwitchImage)
                {
                    image.Source = _defaultIm;
                    _isSwitchImage = true;
                    return;
                }

                _isSwitchImage = false;
            } 
            
            switch (_direction)
            {
                case Direction.Left:
                    if (image.Source != _leftIm)
                        image.Source = _leftIm;
                    break;
                case Direction.Right:
                    if (image.Source != _rightIm)
                        image.Source = _rightIm;
                    break;
                case Direction.Up:
                    if (image.Source != _upIm)
                        image.Source = _upIm;
                    break;
                case Direction.Down:
                    if (image.Source != _downIm)
                        image.Source = _downIm;
                    break;
                case Direction.None:
                    break;
            }
        }

        private bool IsCanMove(Direction direction, CanvasObject mapObject, int step)
        {
            var x = mapObject.X;
            var y = mapObject.Y;

            bool isXAxisTranslationCompleted = x % mapObject.Width == 0;
            bool isYAxisTranslationCompleted = y % mapObject.Height == 0;

            int xLeft = x == 0 ? 0 : (x - step) / mapObject.Width;
            int xRight = (x / mapObject.Width) + 1;
            int xUp = x / mapObject.Width;
            int xDown = x / mapObject.Width;

            int yLeft = y / mapObject.Height;
            int yRight = y / mapObject.Height;
            int yUp = y == 0 ? 0 : ((y - step) / mapObject.Height);
            int yDown = (y / mapObject.Height) + 1;

            switch (direction)
            {
                case Direction.Left:
                    if ((mapObject.X < step) || !isYAxisTranslationCompleted || !_map[yLeft,xLeft]) return false;

                    return true;

                case Direction.Right:
                    if (!(mapObject.X < _mapWidth - mapObject.Width) || !isYAxisTranslationCompleted || !_map[yRight,xRight]) return false;

                    return true;

                case Direction.Up:
                    if ((mapObject.Y < step) || !isXAxisTranslationCompleted || !_map[yUp,xUp]) return false;

                    return true;

                case Direction.Down:
                    if (!(mapObject.Y < _mapHeight - mapObject.Height) || !isXAxisTranslationCompleted || !_map[yDown,xDown]) return false;

                    return true;

                case Direction.None:
                    return true;

                default: return false;
            }
        }

        private void MoveObjectCoordinates(Direction direction, CanvasObject mapObject, int step)
        {
            switch (direction)
            {
                case Direction.Left:
                    mapObject.X = mapObject.X - step;
                    break;

                case Direction.Right:
                    mapObject.X = mapObject.X + step;
                    break;

                case Direction.Up:
                    mapObject.Y = mapObject.Y - step;
                    break;

                case Direction.Down:
                    mapObject.Y = mapObject.Y + step;
                    break;

                case Direction.None:
                    break;
            }
        }

        public void MoveMapObject(Image image, Direction direction, CanvasObject mapObject, int step)
        {
            var leftAnimation = new DoubleAnimation { From = mapObject.X };
            var topAnimation = new DoubleAnimation { From = mapObject.Y };

            var canMoveNewDirection = IsCanMove(direction, mapObject, step);

            if (!canMoveNewDirection)
            {
                var canMovePreviousDirection = IsCanMove(_direction, mapObject, step);
                if (!canMovePreviousDirection) return;
            }
            else
            {
                _direction = direction;
            }

            MoveObjectCoordinates(_direction, mapObject, step);

            leftAnimation.To = mapObject.X;
            topAnimation.To = mapObject.Y;

            leftAnimation.Duration = TimeSpan.FromSeconds(_timeUpdateAnimation);
            topAnimation.Duration = TimeSpan.FromSeconds(_timeUpdateAnimation);

            image.BeginAnimation(Canvas.TopProperty, topAnimation);
            image.BeginAnimation(Canvas.LeftProperty, leftAnimation);

            UpdateImage(image);
        }

        public void UpdatePosition(Image image, CanvasObject mapPointFrom, CanvasObject targetPointFrom)
        {
            var leftAnimation = new DoubleAnimation { From = mapPointFrom.X };
            var topAnimation = new DoubleAnimation { From = mapPointFrom.Y };

            mapPointFrom.X = targetPointFrom.X;
            mapPointFrom.Y = targetPointFrom.Y;
            
            leftAnimation.To = mapPointFrom.X;
            topAnimation.To = mapPointFrom.Y;

            leftAnimation.Duration = TimeSpan.FromSeconds(0.05);
            topAnimation.Duration = TimeSpan.FromSeconds(0.05);

            image.BeginAnimation(Canvas.TopProperty, topAnimation);
            image.BeginAnimation(Canvas.LeftProperty, leftAnimation);
        }

    }
}
