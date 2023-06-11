using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MFarm.N_AStar
{
    public class Node : IComparable<Node>
    {
        public int NodeX = 0;
        public int NodeY = 0;

        /// <summary>
        /// ��������Ȩ��
        /// </summary>
        public int gCost = 0;

        /// <summary>
        /// ���յ����Ȩ��
        /// </summary>
        public int hCost = 0;

        public int fCost => gCost + hCost;

        public Node parentNode;

        public bool isObstacle = false;

        public Node(int x, int y)
        {
            parentNode = null;

            NodeX = x;
            NodeY = y;
        }

        /// <summary>
        /// ����ֵԽС��Ȩ��ԽС
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Node other)
        {
            int result = this.fCost.CompareTo(other.fCost);
            if (result == 0)
            {
                result = this.hCost.CompareTo(other.hCost);
            }

            return result;
        }

    }
}