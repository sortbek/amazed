using System;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts
{
	public class MapManager : MonoBehaviour
	{
		public GameObject Ground;

		private GridNode[,] _map;
		private Generator _generator;
		private GameObject _player;

		private GridNode _current;
		private GridNode _newCurrent;

		// Use this for initialization
		void Start ()
		{
			_generator = GetComponent<Generator>();
			_player = GameObject.FindWithTag("player");

			SetGround();
		}

		// Update is called once per frame
		void Update () {
			CurrentLocation();
			if ( _newCurrent!= _current)
			{
				UpdateCulling();
			}
		}

		public void Init()
		{
			_map = _generator.GetMap();
			_newCurrent = _map[0, 0];
		}

		private void SetGround()
		{
			var size = GameManager.Instance.Size * _generator.NodeSize + 100;
			Ground.transform.position = new Vector3(size/2-50,0,size/2-50);
			Ground.transform.localScale = new Vector3(size,.1f,size);
		}

		public void EnableCulling()
		{
			foreach (var m in _map)
			{
				m.SetActive(false);
			}
		}

		private void CurrentLocation()
		{
			var x = (int)Math.Round(_player.transform.position.x/_generator.NodeSize);
			var y = (int)Math.Round(_player.transform.position.z/_generator.NodeSize);

			if (_current == null || x != _current.X || y != _current.Y)
			{
				_newCurrent = _map[x, y];
			}
		}

		private void UpdateCulling()
		{
//        // Disable the previous enabled baked
			if (_current != null)
			{
				foreach (var node in _current.BakedList)
				{
					node.SetActive(false);
				}
			}

			_current = _newCurrent;
			_current.SetActive(true);

			foreach (var node in _current.BakedList)
			{
				node.SetActive(true);
			}
		}
	}
}
