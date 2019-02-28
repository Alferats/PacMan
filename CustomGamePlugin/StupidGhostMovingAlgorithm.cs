using Core;
using System.ComponentModel.Composition;

namespace CustomGamePlugin
{
    [Export(typeof(IMovingAlgorithm))]
    public class StupidGhostMovingAlgorithm : AbstractMovingAlgorithm
    {
        [ImportingConstructor]
        public StupidGhostMovingAlgorithm(bool[,] mapMatrix) : base(mapMatrix)
        {
            Name = "StupidGhost";
        }

        public override Direction FindDirect(CanvasObject pucMan, CanvasObject pursuer)
        {
            return Direction.None;
        }
    }
}
