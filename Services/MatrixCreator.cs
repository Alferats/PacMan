using System;
using System.Collections.Generic;
using Core;

namespace Services
{
    public class MatrixCreator
    {
        private int _width;
        private int _height;

        public MatrixCreator(int width, int height)
        {
            _height = height;
            _width = width;
        }

        public void ChangeMatrixGeometry(int width, int height)
        {
            _height = height;
            _width = width;
        }

        public bool[,] Generate()
        {
            Random rand = new Random();
            if (_width < 10 || _height < 10) return null;

            //if the number is even, return an odd number that would not be a solid wall
            int localWidth = Round(_width);
            int localHeight = Round(_height);

            int x, y;

            var dir = new List<Direction>();
            var field = new Spot[localWidth, localHeight];

            field[0, 0].Path = true; // fork
            field[0, 0].Check = true; // pass

            while (true)
            {
                var finish = true;

                for (y = 0; y < localHeight; y++)
                {
                    for (x = 0; x < localWidth; x++)
                    {
                        if (field[x, y].Path)
                        {
                            finish = false;

                            //if the path is clear, add direction
                            if (x - 2 >= 0)
                                if (!field[x - 2, y].Check) dir.Add(Direction.Left); 

                            if (y + 2 < localHeight)
                                if (!field[x, y + 2].Check) dir.Add(Direction.Down);

                            if (x + 2 < localWidth)
                                if (!field[x + 2, y].Check) dir.Add(Direction.Right);

                            if (y - 2 >= 0)
                                if (!field[x, y - 2].Check) dir.Add(Direction.Up);

                            if (dir.Count > 0)
                            {
                                switch (dir[rand.Next(0, dir.Count)]) 
                                {
                                    case Direction.Left:
                                        field[x - 1, y].Check = true;
                                        field[x - 2, y].Check = true;
                                        field[x - 2, y].Path = true;
                                        break;

                                    case Direction.Down:
                                        field[x, y + 1].Check = true;
                                        field[x, y + 2].Check = true;
                                        field[x, y + 2].Path = true;
                                        break;

                                    case Direction.Right:
                                        field[x + 1, y].Check = true;
                                        field[x + 2, y].Check = true;
                                        field[x + 2, y].Path = true;
                                        break;

                                    case Direction.Up:
                                        field[x, y - 1].Check = true;
                                        field[x, y - 2].Check = true;
                                        field[x, y - 2].Path = true;
                                        break;
                                }
                            }
                            else // if the direction is impossible to add, remove the fork
                            {
                                field[x, y].Path = false;
                            }

                            dir.Clear(); 
                        }
                    }
                }

                if (finish) break; //if there was no fork, exit
            }

            var result = new bool[_height, _width];

            FillingMap(result, field);

            CheckFillRandomMap(result);

            return result;
        }

        private struct Spot
        {
            public bool Path, Check;
        }

        private int Round(int value)
        {
            if (value % 2 == 0)
            {
                return value - 1;
            }
            return value;

        }

        /// <summary>
        /// create a new array, where true is the pass, and false is the wall
        /// </summary>
        private void FillingMap(bool[,] map, Spot[,] data)
        {

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (data[x, y].Check)
                    {
                        map[y, x] = true;
                    }
                    else
                    {
                        map[y, x] = false;
                    }
                }
            }
        }

        private void OpenWay(bool[,] map)
        {
            var rnd = new Random();

            int x = rnd.Next(1, map.GetLength(1) - 2);
            int y = rnd.Next(1, map.GetLength(0) - 2);

            if (map[y, x]) return;

            bool isHorizontalLine = (!map[y,x + 1] && !map[y,x - 1]) &&
                                    (map[y + 1,x] && map[y - 1,x]);

            bool isVerticalLine = (!map[y+1,x] && !map[y-1,x]) &&
                                  (map[y,x+1] && map[y,x-1]);

            if (isHorizontalLine || isVerticalLine)
            {
                map[y,x] = true;
            }


        }

        private void CheckFillRandomMap(bool[,] map)
        {
            while (!IsCompleted(map))
            {
                OpenWay(map);
            }
        }

        private bool IsCompleted(bool[,] map)
        {
            int allCount = _width * _height;

            int openCount = 0;

            foreach (var cell in map)
            {
                if (cell) openCount++;
            }

            bool isFillingCompleted60Percent = openCount * 100 / allCount > 60;

            if (isFillingCompleted60Percent)
            {
                return true;
            }

            return false;
        }

    }
}