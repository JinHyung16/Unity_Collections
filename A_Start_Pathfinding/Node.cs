using UnityEngine;

namespace HughPathFinding
{
    public class Node
    {
        public int gridX;
        public int gridY;

        public bool isWall;
        public Vector3 nodePosition;

        public int moveCost; //다음 노드로 이동하는 비용
        public int disCost; //현재 노드에서 도착지점까지의 거리

        public int totalCost { get { return (this.moveCost + this.disCost); } }
        public Node Parent;

        public Node(int gX, int gY, bool iswall, Vector3 nodeposition)
        {
            this.gridX = gX;
            this.gridY = gY;
            this.isWall = iswall;
            this.nodePosition = nodeposition;
        }
    }
}
