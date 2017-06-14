using System;
using System.Collections.Generic;
using UnityEngine;

// Created by:
// Jeffrey Wienen
// S1079065
namespace Assets.Scripts.pathfinding {
    public class PathRequestManager : MonoBehaviour {
        private static PathRequestManager _instance;
        private readonly Queue<PathRequest> _pathRequestQueue = new Queue<PathRequest>();
        private PathRequest _currentPathRequest;

        private bool _isProcessingPath;
        private Pathfinding _pathfinding;

        private void Awake() {
            _instance = this;
            _pathfinding = GetComponent<Pathfinding>();
        }

        public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback) {
            var newRequest = new PathRequest(pathStart, pathEnd, callback);
            _instance._pathRequestQueue.Enqueue(newRequest);
            _instance.TryProcessNext();
        }

        private void TryProcessNext() {
            if (_isProcessingPath || _pathRequestQueue.Count <= 0)
                return;
            _currentPathRequest = _pathRequestQueue.Dequeue();
            _isProcessingPath = true;
            _pathfinding.StartFindPath(_currentPathRequest.PathStart, _currentPathRequest.PathEnd);
        }

        public void FinishedProcessingPath(Vector3[] path, bool success) {
            _currentPathRequest.Callback(path, success);
            _isProcessingPath = false;
            TryProcessNext();
        }

        private struct PathRequest {
            public readonly Vector3 PathStart;
            public readonly Vector3 PathEnd;
            public readonly Action<Vector3[], bool> Callback;

            public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool> callback) {
                if (callback == null) throw new ArgumentNullException("callback");
                PathStart = start;
                PathEnd = end;
                Callback = callback;
            }
        }
    }
}