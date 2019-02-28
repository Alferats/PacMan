using Core;
using Services;
using System.Windows.Controls;

namespace PacMan.Model
{
    public class Ghost : MovingGameObject
    {
        private IMovingAlgorithm _movingAlgorithm;
        private readonly Direction _aimFault;
        private readonly int _mapWidth;
        private readonly int _mapHeight;

        public Ghost(MapMovingControl movingControl, IMovingAlgorithm movingAlgorithm, int speed, string basicIm, CanvasObject mapObject, int mapWidth, int mapHeight, Direction aimFault = Direction.None)
            : base(movingControl, speed, mapObject)
        {
            _mapHeight = mapHeight;
            _mapWidth = mapWidth;
            InitImage(basicIm);
            _movingAlgorithm = movingAlgorithm;
            Canvas.SetTop(Image, CanvasCoordinateY);
            Canvas.SetLeft(Image, CanvasCoordinateX);
            _aimFault = aimFault;
        }

        public void ChangeAlgorithm(IMovingAlgorithm movingAlgorithm)
        {
            _movingAlgorithm = movingAlgorithm;
        }

        public void Move(MovingGameObject pucMan)
        {
            Move(_movingAlgorithm.FindDirect(Aiming(pucMan), CanvasPoint));
        }

        private CanvasObject Aiming(MovingGameObject target)
        {
            var newTarget = new CanvasObject()
            {
                X = target.CanvasCoordinateX,
                Y = target.CanvasCoordinateY,
                Width = target.CanvasWidth,
                Height = target.CanvasHeight
            };
            switch (_aimFault)
            {
                case Direction.Left:
                    newTarget.X = test(0, _mapWidth - target.CanvasWidth,
                        target.CanvasCoordinateX - target.CanvasWidth * 3);
                    break;
                case Direction.Right:
                    newTarget.X = test(0, _mapWidth - target.CanvasWidth,
                        target.CanvasCoordinateX + target.CanvasWidth * 3);
                    break;
                case Direction.Up:
                    newTarget.Y = test(0, _mapHeight - target.CanvasHeight,
                        target.CanvasCoordinateY - target.CanvasHeight * 3);
                    break;
                case Direction.Down:
                    newTarget.Y = test(0, _mapHeight - target.CanvasHeight,
                        target.CanvasCoordinateY + target.CanvasHeight * 3);
                    break;
                case Direction.None:
                    break;
            }
            return newTarget;
        }

        private int test(int minValue, int maxValue, int currentValue)
        {
            if (minValue - currentValue > 0)
                currentValue = minValue;
            if (maxValue - currentValue < 0)
                currentValue = maxValue;
            return currentValue;
        }

        
    }
}
