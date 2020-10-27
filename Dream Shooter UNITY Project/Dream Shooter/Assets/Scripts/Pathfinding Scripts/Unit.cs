using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Pathfinding_Scripts
{
    public class Unit : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// This Unit's target.
        /// </summary>
        public Transform target;
        /// <summary>
        /// This Unit's movement speed.
        /// </summary>
        public float speed = 5;

        /// <summary>
        /// This Unit's path.
        /// </summary>
        Vector3[] path;
        /// <summary>
        /// The target's index.
        /// </summary>
        int targetIndex;
        #endregion

        private void Start()
        {
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        }

        private void OnDrawGizmos()
        {
            if (path != null)
            {
                for (int i = targetIndex; i < path.Length; i++)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(path[i], Vector3.one);

                    if (i == targetIndex)
                    {
                        Gizmos.DrawLine(transform.position, path[i]);
                    }
                    else
                    {
                        Gizmos.DrawLine(path[i-1], path[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Is called when a path is found for this Unit.
        /// </summary>
        /// <param name="newPath">
        /// The new path for this Unit.
        /// </param>
        /// <param name="pathSucessful">
        /// Returns true if finding the path was a success, or false if not.
        /// </param>
        public void OnPathFound(Vector3[] newPath, bool pathSucessful)
        {
            if (pathSucessful)
            {
                path = newPath;
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
        }

        /// <summary>
        /// Follows the path.
        /// </summary>
        /// <returns>
        /// Nothing.
        /// </returns>
        IEnumerator FollowPath()
        {
            Vector3 currentWaypoint = path[0];

            while (true)
            {
                if (transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }

                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}