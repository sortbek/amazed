using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class MapManager : MonoBehaviour
{

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
