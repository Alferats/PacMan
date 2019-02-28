namespace Core
{
    public abstract class AbstractMovingAlgorithm : IMovingAlgorithm
    {
        protected AbstractMovingAlgorithm(bool[,] mapMatrix)
        {
            MapMatrix = mapMatrix;
        }

        public string Name { get; set; }

        public bool[,] MapMatrix { get; }

        public abstract Direction FindDirect(CanvasObject pucMan, CanvasObject pursuer);
    }
    
}
