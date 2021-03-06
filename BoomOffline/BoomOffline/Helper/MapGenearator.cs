﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BoomOffline.Entity;
using BoomOffline.Resource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BoomOffline.Helper
{
    class MapGenerator
    {

        private const int TYPE_OBSTACLE = 1;
        private const int TYPE_EMPTY = 0;

        public BasicEntity[,] map;
        private int[][] logicMap;

        public BasicEntity[,] Map
        {
            get { return map; }
        }

        public int[][] LogicMap
        {
            get { return logicMap; }
            set { logicMap = value; }
        }

        public Point GetRealPosition(int i, int j)
        {
            return map[i, j].Rect.Location;
        }

        private void GenerateLogicMap(string map)
        {
            List<int[]> grid = new List<int[]>();
            string[] lines = System.IO.File.ReadAllLines(@map);
            foreach (string line in lines)
            {
                string[] tokens = line.Split('\t');
                grid.Add(tokens.Select(token => Int32.Parse(token)).ToArray());
            }

            LogicMap = grid.ToArray();

        }

        public void GenerateMap(int startX, int startY)
        {
            GenerateLogicMap("map01.txt");
            map = new BasicEntity[LogicMap.Length, LogicMap[0].Length];
            var unit = Global.Instance.GameUnit;
            int startMapX = 25;
            int startMapY = 25;

            Texture2D obstacle = new Texture2D(Global.Instance.Graphics, 1, 1);
            obstacle.SetData(new Color[] { Color.Brown });
            Texture2D empty = new Texture2D(Global.Instance.Graphics, 1, 1);
            obstacle.SetData(new Color[] { Color.Green });

            for (int i = 0; i < LogicMap.Length; i++)
            {
                for (int j = 0; j < LogicMap[i].Length; j++)
                {
                    map[i, j] = new BasicEntity();
                    map[i, j].Load(LogicMap[i][j] == TYPE_OBSTACLE ? obstacle : empty, new Rectangle(startMapX + unit * j, startMapY + unit * i, unit, unit), Color.White);
                }
            }

        }

        public bool IsValidLocation(int i, int j)
        {
            return LogicMap[i][j] == TYPE_EMPTY;
        }

    }
}
