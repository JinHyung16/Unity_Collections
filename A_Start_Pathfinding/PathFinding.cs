using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HughPathFinding
{
    public class PathFinding : MonoBehaviour
    {
        private Grid grid; //HughPathFinding의 Grid

        [SerializeField] private Transform startPosition;
        [SerializeField] private Transform targetPosition;

        private void Awake()
        {
            grid = GetComponent<Grid>();
        }

        private void Update()
        {
            FindPath(startPosition.position, targetPosition.position);
        }

        private void FindPath(Vector3 start, Vector3 target)
        {
            Node startNode = grid.NodeFrowmWorldPosition(start);
            Node targetNode = grid.NodeFrowmWorldPosition(target);

            List<Node> openList = new List<Node>();
            HashSet<Node> closeList = new HashSet<Node>();

            openList.Add(startNode);
            while (0 < openList.Count)
            {
                Node curNode = openList[0];
                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].totalCost < curNode.totalCost || openList[i].totalCost == curNode.totalCost && openList[i].disCost < curNode.disCost)
                    {
                        curNode = openList[i];
                    }
                }
                openList.Remove(curNode);
                closeList.Add(curNode);

                if (curNode == targetNode)
                {
                    GetFinalPath(startNode, targetNode);
                }

                //최단경로의 상하좌우 1칸씩을 보면서 최단 경로를 계속 찾아간다
                foreach (Node neighboorhod in grid.GetNeighborhoodNodes(curNode))
                {
                    if (!neighboorhod.isWall || closeList.Contains(neighboorhod))
                    {
                        continue;
                    }
                    int moveCost = curNode.moveCost + GetManhattenDistance(curNode, neighboorhod);
                    if (moveCost < neighboorhod.moveCost || !openList.Contains(neighboorhod))
                    {
                        neighboorhod.moveCost = moveCost;
                        neighboorhod.disCost = GetManhattenDistance(neighboorhod, targetNode);
                        neighboorhod.Parent = curNode;

                        if (!openList.Contains(neighboorhod))
                        {
                            openList.Add(neighboorhod);
                        }
                    }
                }
            }
        }

        private void GetFinalPath(Node startNode, Node endNode)
        {
            List<Node> finalPathList = new List<Node>();
            Node curNode = endNode;
            while (curNode != startNode)
            {
                finalPathList.Add(curNode);
                curNode = curNode.Parent;
            }
            finalPathList.Reverse();
            grid.finalPath = finalPathList;
        }

        private int GetManhattenDistance(Node aNode, Node bNode)
        {
            int x = Mathf.Abs(aNode.gridX - bNode.gridX);
            int y = Mathf.Abs(aNode.gridY - bNode.gridY);

            return x + y;
        }
    }
}