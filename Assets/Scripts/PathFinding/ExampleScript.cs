using System.Collections;
using Assets.Scripts.pathfinding;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.PathFinding
{
	public class ExampleScript : MonoBehaviour
	{
		private Vector3[] _path;

		// Use this for initialization
		void Start ()
		{
			StartCoroutine(Waiting(2));
		}

		private IEnumerator Waiting(int time)
		{
			yield return new WaitForSeconds(time);

			PathRequestManager.RequestPath(transform.position, GameManager.Instance.GetEndpoint(), OnPathFound);
		}

		public void OnPathFound(Vector3[] newPath, bool pathFound)
		{
			if (pathFound)
			{
				_path = newPath;
			}
		}

		public void OnDrawGizmos()
		{
			if (_path == null) return;
			for (var i = 0; i < _path.Length; i ++) {
				Gizmos.color = Color.red;
				_path[i].y = 1;
				Gizmos.DrawCube(_path[i], Vector3.one);

				Gizmos.DrawLine(i == 0 ? transform.position : _path[i - 1], _path[i]);
			}
		}

	}
}
