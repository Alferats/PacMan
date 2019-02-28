using Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;

namespace UnitTests.Service
{
    [TestClass]
    public class GhostMovingBasicAlgorithmTest
    {
        private readonly bool[,] _testMatrix = 
        {
            { true, false, true, true, true, true, true},
            { true, false, true, true, true, true, true },
            { true, false, true, true, true, true, true },
            { true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true }
        };

        private bool[,] GetTestMatrix()
        {
            return _testMatrix;
        }

        private int GetSizeObject()
        {
            return 10;
        }

        [TestMethod]
        public void FindingUpDirectionOnMap()
        {
            var sizeObject = GetSizeObject();

            var movingAlgorithm = new GhostMovingBasicAlgorithm(GetTestMatrix());

            var target = new CanvasObject()
            { Height = sizeObject, Width = sizeObject, Y = sizeObject * 2, X = sizeObject * 4 };

            var pursuer = new CanvasObject()
            { Height = sizeObject, Width = sizeObject, Y = sizeObject * 4, X = sizeObject * 4 };

            var result = movingAlgorithm.FindDirect(target, pursuer);

            Assert.AreEqual(Direction.Up, result);

        }

        [TestMethod]
        public void FindingDownDirectionOnMap()
        {
            var sizeObject = GetSizeObject();

            var movingAlgorithm = new GhostMovingBasicAlgorithm(GetTestMatrix());

            var target = new CanvasObject()
            { Height = sizeObject, Width = sizeObject, Y = sizeObject * 2, X = sizeObject * 4 };
            var pursuer = new CanvasObject()
            { Height = sizeObject, Width = sizeObject, Y = sizeObject * 0, X = sizeObject * 4 };

            var result = movingAlgorithm.FindDirect(target, pursuer);

            Assert.AreEqual(Direction.Down, result);
        }

        [TestMethod]
        public void FindingLeftDirectionOnMap()
        {
            var sizeObject = GetSizeObject();

            var movingAlgorithm = new GhostMovingBasicAlgorithm(GetTestMatrix());

            var target = new CanvasObject()
            { Height = sizeObject, Width = sizeObject, Y = sizeObject * 2, X = sizeObject * 4 };
            var pursuer = new CanvasObject()
            { Height = sizeObject, Width = sizeObject, Y = sizeObject * 2, X = sizeObject * 6 };

            var result = movingAlgorithm.FindDirect(target, pursuer);

            Assert.AreEqual(Direction.Left, result);
        }

        [TestMethod]
        public void FindingRightDirectionOnMap()
        {
            var sizeObject = GetSizeObject();

            var movingAlgorithm = new GhostMovingBasicAlgorithm(GetTestMatrix());

            var target = new CanvasObject()
            { Height = sizeObject, Width = sizeObject, Y = sizeObject * 2, X = sizeObject * 4 };
            var pursuer = new CanvasObject()
            { Height = sizeObject, Width = sizeObject, Y = sizeObject * 2, X = sizeObject * 2 };

            var result = movingAlgorithm.FindDirect(target, pursuer);

            Assert.AreEqual(Direction.Right, result);
        }

        [TestMethod]
        public void FindingWayThroughBarrier()
        {
            var sizeObject = GetSizeObject();

            var movingAlgorithm = new GhostMovingBasicAlgorithm(GetTestMatrix());

            var target = new CanvasObject()
                { Height = sizeObject, Width = sizeObject, Y = sizeObject * 0, X = sizeObject * 2 };
            var pursuer = new CanvasObject()
                { Height = sizeObject, Width = sizeObject, Y = sizeObject * 0, X = sizeObject * 0 };

            var result = movingAlgorithm.FindDirect(target, pursuer);

            Assert.AreEqual(Direction.Down, result);
        }
    }
}
