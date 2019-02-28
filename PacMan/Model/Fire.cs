using Core;
using PacMan.Model.Images;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PacMan.Model
{
    public class Fire
    {
        private readonly BitmapImage _defaultIm = new BitmapImage();
        private readonly CanvasObject _canvasObject = new CanvasObject();
        public Image FireImage { get; private set; }
        private readonly int _sizeOnCanvas;
        private int _matrixCoordinateX;
        private int _matrixCoordinateY;
        private readonly int _widthMatrix;
        private readonly int _heightMatrix;
        public int CanvasCoordinateX => _canvasObject.X;
        public int CanvasCoordinateY => _canvasObject.Y;

        public Fire(int size, bool[,] mapMatrix, List<Fire> fires, Random pseudoRandom)
        {
            if (mapMatrix == null) return;
            _heightMatrix = mapMatrix.GetLength(0);
            _widthMatrix = mapMatrix.GetLength(1);
            _sizeOnCanvas = size;
            CreateBitmapImage();
            CreateCanvasObject(mapMatrix, fires, pseudoRandom);
            CreateImage();
            SetCanvas();
        }

        private void CreateBitmapImage()
        {
            _defaultIm.BeginInit();
            _defaultIm.UriSource = new Uri(StaticStrings.FireImg, UriKind.Relative);
            _defaultIm.EndInit();
        }

        private void CreateCanvasObject(bool[,] mapMatrix, List<Fire> fires, Random pseudoRandom)
        {
            while (true)
            {
                var xMatrixCoordinate = pseudoRandom.Next(1, _widthMatrix);
                var yMatrixCoordinate = pseudoRandom.Next(1, _heightMatrix);

                if ((!mapMatrix[yMatrixCoordinate,xMatrixCoordinate])
                    || Contains(fires, xMatrixCoordinate, yMatrixCoordinate)) continue;


                _canvasObject.X = xMatrixCoordinate * _sizeOnCanvas;
                _canvasObject.Y = yMatrixCoordinate * _sizeOnCanvas;

                _matrixCoordinateX = xMatrixCoordinate;
                _matrixCoordinateY = yMatrixCoordinate;
                break;
            }
            _canvasObject.Width = _sizeOnCanvas;
            _canvasObject.Height = _sizeOnCanvas;
        }
        
        private bool Contains(List<Fire> fires, int x, int y)
        {
            foreach (var fire in fires)
            {
                if (fire._matrixCoordinateX == x && fire._matrixCoordinateY == y)
                {
                    return true;
                }
            }
            return false;
        }

        private void CreateImage()
        {
            FireImage = new Image()
            {
                Width = _sizeOnCanvas,
                Height = _sizeOnCanvas,
                Source = _defaultIm
            };
        }

        private void SetCanvas()
        {
            Canvas.SetTop(FireImage, _canvasObject.Y);
            Canvas.SetLeft(FireImage, _canvasObject.X);
        }


    }
}
