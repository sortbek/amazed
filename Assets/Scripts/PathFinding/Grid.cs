using System.Collections.Generic;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.PathFinding
{
    public class Grid : MonoBehaviour
    {
        public bool DisplayGridGizmos;
        public LayerMask UnwalkableMask;
        public Vector2 GridWorldSize;
        public float NodeRadius;
        private Node[,] _grid;

        private float _nodeDiameter;
        private int _gridSizeX, _gridSizeY;

        public void Init()
        {
            // Set al the necessary variables we need for our grid
            GridWorldSize = new Vector2(GameManager.Instance.Size * 12 + 20, GameManager.Instance.Size * 12 + 20);

            _nodeDiameter = NodeRadius * 2;
            _gridSizeX = Mathf.RoundToInt(GridWorldSize.x / _nodeDiameter);
            _gridSizeY = Mathf.RoundToInt(GridWorldSize.y / _nodeDiameter);

            // Create the actual grid
            CreateGrid();
        }

        public int MaxSize
        {
            get
            {
                return _gridSizeX * _gridSizeY;
            }
        }

        private void CreateGrid()
        {
            _grid = new Node[_gridSizeX, _gridSizeY];
            var worldBottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 -
                                  Vector3.forward * GridWorldSize.y / 2;

            for (var x = 0; x < _gridSizeX; x++)
            {
                for (var y = 0; y < _gridSizeY; y++)
                {
                    var worldPoint = worldBottomLeft + Vector3.right * (x * _nodeDiameter + NodeRadius) +
                                     Vector3.forward * (y * _nodeDiameter + NodeRadius);
                    var walkable = !(Physics.CheckSphere(worldPoint, NodeRadius, UnwalkableMask));
                    _grid[x, y] = new Node(walkable, worldPoint, x, y);
                }
            }
        }

        public List<Node> GetNeighbours(Node node)
        {
            var neighbours = new List<Node>();

            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    var checkX = node.GridX + x;
                    var checkY = node.GridY + y;

                    if (checkX >= 0 && checkX < _gridSizeX && checkY >= 0 && checkY < _gridSizeY)
                    {
                        neighbours.Add(_grid[checkX, checkY]);
                    }
                }
            }

            return neighbours;
        }


        public Node NodeFromWorldPoint(Vector3 worldPosition)
        {
            var percentX = (worldPosition.x + GridWorldSize.x / 2) / GridWorldSize.x;
            var percentY = (worldPosition.z + GridWorldSize.y / 2) / GridWorldSize.y;
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            var x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
            var y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);
            return _grid[x, y];
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, 1, GridWorldSize.y));
            if (_grid == null || !DisplayGridGizmos) return;
            foreach (var n in _grid)
            {
                Gizmos.color = (n.Walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.WorldPosition, Vector3.one * (_nodeDiameter - .1f));
            }
        }
    }
}