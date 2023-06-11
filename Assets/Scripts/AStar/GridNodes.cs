using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MFarm.Map;

namespace MFarm.N_AStar
{
    /// <summary>
    /// ���𴢴��ͼ�е�����������
    /// </summary>
    public class GridNodes
    {
        [Header("��ͼ��Ϣ")]
        private int gridHeight;
        private int gridWidth;

        public Node[,] gridNodesArray;

        public GridNodes(int width, int height)
        {

            this.gridWidth = width;
            this.gridHeight = height;

            gridNodesArray = new Node[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    gridNodesArray[x, y] = new Node(x, y);
                }
            }
        }

        public Node getGridNode(int nodeX, int nodeY)
        {
            if (nodeX < gridWidth && nodeY < gridHeight)
            {
                return gridNodesArray[nodeX, nodeY];
            }
            Debug.Log("gridNode����Խ��");
            return null;
        }
    }
}