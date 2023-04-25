## A_Start_Pathfinding
- unity 3D에서 A_Start algorithm을 기반으로 Pathfinding 기능 구현 참고  
- [알고리즘 참고한 Github주소](https://github.com/danielmccluskey/A-Star-Pathfinding-Tutorial)  

## Usage
- 각 Script를 참고하여 프로젝트에 맞게 변형하면 된다  
- unity_EFU_TheClue 프로젝트에 ThemeThird 부분에 이 부분을 참고하여 응용하고 있다  
- Heap.cs는 List<Node>OpenSet을 Heap으로 대체하여 OpenSet의 들어갈 노드를 찾는 행위를 줄여줄 수 있는 최적화 Heap Sort다  
- PathManager.cs는 EnemyAI.cs 에서 CallBack 구조로 실시간으로 Path에 맞춰 움직일 수 있도록 구현  
- EnemyAI.cs 는 경로를 입력받아 쫓아가는 부분을 보여주고 있다  