namespace Core
{
    public interface IMovingAlgorithm
    {
        string Name { get; set; }

        bool[,] MapMatrix { get;}

        Direction FindDirect(CanvasObject pucMan, CanvasObject pursuer);

    }
}
