using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HughPathFinding
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private Transform startPosition;
        [SerializeField] private LayerMask obstacleLayerMask;
        [SerializeField] private Vector2 gridWorldSize;
        [SerializeField] private float nodeRadius;
        private float distanceBetweenNodes;

        private Node[,] nodeArray; //2차원 배열
        [HideInInspector] public List<Node> finalPath;

        private float nodeDiameter; //node의 지름
        private int gridSizeX;
        private int gridSizeY;

        private void Start()
        {
            nodeDiameter = nodeRadius * 2;
            gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
            gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

            DrawGrid();
        }

        private void DrawGrid()
        {
            nodeArray = new Node[gridSizeX, gridSizeY];
            Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

            for (int y = 0; y < gridSizeY; y++)
            {
                for (int x = 0; x < gridSizeX; x++)
                {
                    Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                    bool isWall = true;
                    if (Physics.CheckSphere(worldPoint, nodeRadius, obstacleLayerMask))
                    {
                        isWall = false;
                    }

                    nodeArray[x, y] = new Node(x, y, isWall, worldPoint);
                }
            }
        }

        public Node NodeFrowmWorldPosition(Vector3 worldPosition)
        {
            float xPosition = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
            float yPosition = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y; //wordPosition의 z값이 2차원에선 y값이다.

            xPosition = Mathf.Clamp01(xPosition);
            yPosition = Mathf.Clamp01(yPosition);

            int x = Mathf.RoundToInt((gridSizeX - 1) * xPosition);
            int y = Mathf.RoundToInt((gridSizeY - 1) * yPosition);

            return nodeArray[x, y];
        }

        public List<Node> GetNeighborhoodNodes(Node node)
        {
            List<Node> neighborhoodNodeList = new List<Node>();
            int xCheck;
            int yCheck;

            //top side
            xCheck = node.gridX;
            yCheck = node.gridY + 1;
            if (0 <= xCheck && xCheck < gridSizeX)
            {
                if (0 <= yCheck && yCheck < gridSizeY)
                {
                    neighborhoodNodeList.Add(nodeArray[xCheck, yCheck]);
                }
            }

            //bottom side
            xCheck = node.gridX;
            yCheck = node.gridY - 1;
            if (0 <= xCheck && xCheck < gridSizeX)
            {
                if (0 <= yCheck && yCheck < gridSizeY)
                {
                    neighborhoodNodeList.Add(nodeArray[xCheck, yCheck]);
                }
            }

            //right side
            xCheck = node.gridX + 1;
            yCheck = node.gridY;
            if (0 <= xCheck && xCheck < gridSizeX)
            {
                if (0 <= yCheck && yCheck < gridSizeY)
                {
                    neighborhoodNodeList.Add(nodeArray[xCheck, yCheck]);
                }
            }

            //left side
            xCheck = node.gridX - 1;
            yCheck = node.gridY;
            if (0 <= xCheck && xCheck < gridSizeX)
            {
                if (0 <= yCheck && yCheck < gridSizeY)
                {
                    neighborhoodNodeList.Add(nodeArray[xCheck, yCheck]);
                }
            }

            return neighborhoodNodeList;
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
            if (nodeArray != null)
            {
                foreach (Node n in nodeArray)
                {
                    if (n.isWall) //현재 노드가 벽이라면
                    {
                        Gizmos.color = Color.white;
                    }
                    else
                    {
                        Gizmos.color = Color.yellow;
                    }

                    if (finalPath != null)
                    {
                        if (finalPath.Contains(n)) //현재 노드가 최종 도착지라면
                        {
                            Gizmos.color = Color.green;
                        }
                    }
                    Gizmos.DrawCube(n.nodePosition, Vector3.one * (nodeDiameter - distanceBetweenNodes)); //현재 노드 위치에 노드를 그린다.
                }
            }
        }
    }
}