using Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace Services
{
    class PathNode
    {
        public Point Position { get; set; }
        public int PathLengthFromStart { get; set; }
        public PathNode CameFrom { get; set; }
        public int HeuristicEstimatePathLength { get; set; }
        public int EstimateFullPathLength => PathLengthFromStart + HeuristicEstimatePathLength;
    }

    public class GhostMovingBasicAlgorithm : IMovingAlgorithm
    {
        private Direction _direction = Direction.None;
        public string Name { get; set; } = "Default Ghost Algorithm";

        public bool[,] MapMatrix { get; }
        private readonly int _widthMatrix;
        private readonly int _heightMatrix;

        public GhostMovingBasicAlgorithm(bool[,] mapMatrix)
        {
            _heightMatrix = mapMatrix.GetLength(0);
            _widthMatrix = mapMatrix.GetLength(1);
            MapMatrix = mapMatrix;
        }

        private int GetCoordinate(int coordinate, int size)
        {
            return coordinate / size;
        }

        public Direction FindDirect(CanvasObject pucMan, CanvasObject pursuer)
        {
            var ghostPoint = new Point(
                GetCoordinate(pursuer.X, pursuer.Width),
                GetCoordinate(pursuer.Y, pursuer.Height));

            var pacManPoint = new Point(
                GetCoordinate(pucMan.X, pucMan.Width),
                GetCoordinate(pucMan.Y, pucMan.Height));

            var pointsWay = FindPath(ghostPoint, pacManPoint);


            if (pointsWay.Count == 0) return _direction;

            if (pointsWay.Contains(new Point(ghostPoint.X + 1, ghostPoint.Y))) return _direction = Direction.Right;
            if (pointsWay.Contains(new Point(ghostPoint.X - 1, ghostPoint.Y))) return _direction = Direction.Left;
            if (pointsWay.Contains(new Point(ghostPoint.X, ghostPoint.Y + 1))) return _direction = Direction.Down;
            if (pointsWay.Contains(new Point(ghostPoint.X, ghostPoint.Y - 1))) return _direction = Direction.Up;

            return _direction;
        }

        private List<Point> FindPath(Point start, Point goal)
        {
            var closedSet = new Collection<PathNode>();
            var openSet = new Collection<PathNode>();

            PathNode startNode = new PathNode()
            {
                Position = start,
                CameFrom = null,
                PathLengthFromStart = 0,
                HeuristicEstimatePathLength = GetHeuristicPathLength(start, goal)
            };
            openSet.Add(startNode);
            while (openSet.Count > 0)
            {
                var currentNode = openSet.OrderBy(node =>
                    node.EstimateFullPathLength).First();

                if (currentNode.Position == goal)
                    return GetPathForNode(currentNode);

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                foreach (var neighborNode in GetNeighbors(currentNode, goal))
                {
                    if (closedSet.Count(node => node.Position == neighborNode.Position) > 0)
                        continue;
                    var openNode = openSet.FirstOrDefault(node => node.Position == neighborNode.Position);

                    if (openNode == null)
                        openSet.Add(neighborNode);
                    else if (openNode.PathLengthFromStart > neighborNode.PathLengthFromStart)
                    {
                        openNode.CameFrom = currentNode;
                        openNode.PathLengthFromStart = neighborNode.PathLengthFromStart;
                    }
                }
            }
            return new List<Point>();
        }

        private int GetDistanceBetweenNeighbors()
        {
            return 1;
        }

        private int GetHeuristicPathLength(Point from, Point to)
        {
            return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
        }

        private Collection<PathNode> GetNeighbors(PathNode pathNode,
            Point goal)
        {
            var result = new Collection<PathNode>();

            var neighborPoints = new Point[4];
            neighborPoints[0] = new Point(pathNode.Position.X + 1, pathNode.Position.Y);
            neighborPoints[1] = new Point(pathNode.Position.X - 1, pathNode.Position.Y);
            neighborPoints[2] = new Point(pathNode.Position.X, pathNode.Position.Y + 1);
            neighborPoints[3] = new Point(pathNode.Position.X, pathNode.Position.Y - 1);

            foreach (var point in neighborPoints)
            {
                if (point.X < 0 || point.X >= _widthMatrix)
                    continue;
                if (point.Y < 0 || point.Y >= _heightMatrix)
                    continue;

                if (!MapMatrix[point.Y, point.X])
                    continue;

                var neighborNode = new PathNode()
                {
                    Position = point,
                    CameFrom = pathNode,
                    PathLengthFromStart = pathNode.PathLengthFromStart +
                                          GetDistanceBetweenNeighbors(),
                    HeuristicEstimatePathLength = GetHeuristicPathLength(point, goal)
                };
                result.Add(neighborNode);
            }
            return result;
        }

        private static List<Point> GetPathForNode(PathNode pathNode)
        {
            var result = new List<Point>();
            var currentNode = pathNode;
            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                currentNode = currentNode.CameFrom;
            }
            result.Reverse();
            return result;
        }
    }
}
