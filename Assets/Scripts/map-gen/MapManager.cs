using System;
using Assets.Scripts;
using Assets.Scripts.World;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	public GameObject ground;

    private GridNode[,] _map;
    private Generator _generator;
    private GameObject _player;

    private GridNode _current;
    private GridNode _newCurrent;

    private bool _isLoaded;

	// Use this for initialization
	void Start ()
	{
	    _generator = gameObject.GetComponent<Generator>();
	    _player = GameObject.FindWithTag("player");

		SetGround();
	}
	
	// Update is called once per frame
	void Update () {
	    if (_generator.IsGenerated() && !_isLoaded)
	    {
	        _map = _generator.GetMap();
	        _newCurrent = _map[0, 0];
	        _isLoaded = true;
	    }
	    else
	    {
	        CurrentLocation();
	        if ( _newCurrent!= _current)
	        {
	            UpdateCulling();
	        }
	    }
	}

	private void SetGround()
	{
		var size = GameManager.Instance.Size * _generator.NodeSize + 100;
		ground.transform.position = new Vector3(size/2-50,0,size/2-50);
		ground.transform.localScale = new Vector3(size,.1f,size);
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
