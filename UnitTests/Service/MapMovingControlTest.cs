using Core;
using Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Controls;

namespace UnitTests.Service
{
    [TestClass]
    public class MapMovingControlTest
    {
        private readonly bool[,] _testMatrix = 
        {
            { true, true, true},
            { true, true, true},
            { true, true, true}
        };

        private bool[,] GetTestMatrix()
        {
            return _testMatrix;
        }

        private int GetSizeObject()
        {
            return 10;
        }

        private int GetStep()
        {
            return 5;
        }

        [TestMethod]
        public void TestMoveDown()
        {
           var control = new MapMovingControl(GetTestMatrix(), _testMatrix.GetLength(0)* GetSizeObject(), _testMatrix.GetLength(1) * GetSizeObject(),
               10, "","","","","");
           
           var direction = Direction.Down;

           var canvasPoint = new CanvasObject()
               {X = 1* GetSizeObject(), Y = 1 * GetSizeObject(), Height = GetSizeObject(), Width = GetSizeObject()};

           var image = new Image();
           Canvas.SetTop(image, 0);
           Canvas.SetLeft(image, 0);

           control.MoveMapObject(image, direction, canvasPoint, GetStep());

           Assert.AreEqual(10, canvasPoint.X);
           Assert.AreEqual(15, canvasPoint.Y);
        }

        [TestMethod]
        public void TestMoveUp()
        {
            var control = new MapMovingControl(GetTestMatrix(), GetTestMatrix().GetLength(0) * GetSizeObject(), GetTestMatrix().GetLength(1) * GetSizeObject(),
                10, "", "", "", "", "");

            var direction = Direction.Up;

            var canvasPoint = new CanvasObject()
                { X = 1 * GetSizeObject(), Y = 1 * GetSizeObject(), Height = GetSizeObject(), Width = GetSizeObject() };

            var image = new Image();
            Canvas.SetTop(image, 0);
            Canvas.SetLeft(image, 0);

            control.MoveMapObject(image, direction, canvasPoint, GetStep());

            Assert.AreEqual(10, canvasPoint.X);
            Assert.AreEqual(5, canvasPoint.Y);
        }

        [TestMethod]
        public void TestMoveLeft()
        {
            var control = new MapMovingControl(GetTestMatrix(), GetTestMatrix().GetLength(0) * GetSizeObject(), GetTestMatrix().GetLength(1) * GetSizeObject(),
                10, "", "", "", "", "");

            var direction = Direction.Left;

            var canvasPoint = new CanvasObject()
                { X = 1 * GetSizeObject(), Y = 1 * GetSizeObject(), Height = GetSizeObject(), Width = GetSizeObject() };

            var image = new Image();
            Canvas.SetTop(image, 0);
            Canvas.SetLeft(image, 0);

            control.MoveMapObject(image, direction, canvasPoint, GetStep());

            Assert.AreEqual(5, canvasPoint.X);
            Assert.AreEqual(10, canvasPoint.Y);
        }

        [TestMethod]
        public void TestMoveRight()
        {
            var control = new MapMovingControl(GetTestMatrix(), GetTestMatrix().GetLength(0) * GetSizeObject(), GetTestMatrix().GetLength(1) * GetSizeObject(),
                10, "", "", "", "", "");

            var direction = Direction.Right;

            var canvasPoint = new CanvasObject()
                { X = 1 * GetSizeObject(), Y = 1 * GetSizeObject(), Height = GetSizeObject(), Width = GetSizeObject() };

            var image = new Image();
            Canvas.SetTop(image, 0);
            Canvas.SetLeft(image, 0);

            control.MoveMapObject(image, direction, canvasPoint, GetStep());

            Assert.AreEqual(15, canvasPoint.X);
            Assert.AreEqual(10, canvasPoint.Y);
        }

        [TestMethod]
        public void TestMovementCheckThroughWall()
        {
            var control = new MapMovingControl(GetTestMatrix(), GetTestMatrix().GetLength(0) * GetSizeObject(), GetTestMatrix().GetLength(1) * GetSizeObject(),
                10, "", "", "", "", "");

            var direction = Direction.Up;

            var canvasPoint = new CanvasObject()
                { X = 0, Y = 0, Height = GetSizeObject(), Width = GetSizeObject() };

            var image = new Image();
            Canvas.SetTop(image, 0);
            Canvas.SetLeft(image, 0);

            control.MoveMapObject(image, direction, canvasPoint, GetStep());

            Assert.AreEqual(0, canvasPoint.X);
            Assert.AreEqual(0, canvasPoint.Y);
        }
    }
}
