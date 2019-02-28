using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;

namespace UnitTests.Service
{
    [TestClass]
    public class MapCreatorTest
    {
        [TestMethod]
        public void TestGenerateGeometry()
        {
            var creator = new MatrixCreator(13, 11);

            var result = creator.Generate();

            Assert.AreEqual(11, result.GetLength(0));
            Assert.AreEqual(13, result.GetLength(1));
        }

        [TestMethod]
        public void TestChangeMatrixGeometry()
        {
            var creator = new MatrixCreator(13, 11);

            creator.ChangeMatrixGeometry(11, 11);

            var result = creator.Generate();

            Assert.AreEqual(11, result.GetLength(1));
            Assert.AreEqual(11, result.GetLength(0));
        }

        [TestMethod]
        public void CheckFillingLess40PercentWalls()
        {
            var creator = new MatrixCreator(11, 11);

            var result = creator.Generate();

            var resultAllCount = result.GetLength(1) * result.GetLength(0);

            var resultWallCount = 0;
            
            foreach (var cell in result)
            {
                if (!cell) resultWallCount++;
            }

            Assert.AreEqual(121, resultAllCount);
            Assert.AreEqual(48, resultWallCount, 1);
        }
    }
}
