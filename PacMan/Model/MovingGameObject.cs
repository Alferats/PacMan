using Core;
using Services;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PacMan.Model
{
    public class MovingGameObject : ICanvasObject
    {
        private readonly int _movementStep;

        public CanvasObject CanvasPoint { get; private set; }

        private readonly MapMovingControl _movingControl;

        public int CanvasCoordinateX => CanvasPoint.X;

        public int CanvasCoordinateY => CanvasPoint.Y;

        public int CanvasWidth => CanvasPoint.Width;

        public int CanvasHeight => CanvasPoint.Height;

        public Image Image { get; private set; }

        public MovingGameObject(MapMovingControl movingControl, int speed, CanvasObject mapPoint)
        {
            _movementStep = speed;
            CanvasPoint = mapPoint;
            _movingControl = movingControl;
        }

        protected void InitImage(string basicIm)
        {
            var defaultIm = new BitmapImage();
            defaultIm.BeginInit();
            defaultIm.UriSource = new Uri(basicIm, UriKind.Relative);
            defaultIm.EndInit();
            Image = new Image { Width = CanvasWidth, Height = CanvasHeight, Source = defaultIm };
        }

        public void Move(Direction direction)
        {
            _movingControl.MoveMapObject(Image, direction, CanvasPoint, _movementStep);
        }

        public void ToPosition(CanvasObject mapPoint)
        {
            _movingControl.UpdatePosition(Image, CanvasPoint, mapPoint);
        }

    }
}
