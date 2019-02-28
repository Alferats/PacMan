using Core;
using Services;
using System.Windows.Controls;

namespace PacMan.Model
{
    public class PucMan : MovingGameObject
    {
        public PucMan(MapMovingControl movingControl, int speed, string basicIm, CanvasObject mapPoint)
            :base(movingControl, speed, mapPoint)
        {
            InitImage(basicIm);

            Canvas.SetTop(Image, CanvasCoordinateY);
            Canvas.SetLeft(Image, CanvasCoordinateX);
        }

        
    }
}
