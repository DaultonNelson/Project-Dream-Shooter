using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Pathfinding_Scripts
{
    public class PathRequestManager : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// A Queue (first in, first out) of all the Path Requests.
        /// </summary>
        Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
        /// <summary>
        /// The current Path Request;
        /// </summary>
        PathRequest currentPathRequest;
        /// <summary>
        /// The pathfinding class reference.
        /// </summary>
        Pathfinding pathfinding;
        /// <summary>
        /// Returns true if we're processing a path, or false if not.
        /// </summary>
        bool isProcessingPath;

        /// <summary>
        /// The instance of this PathRequestManager
        /// </summary>
        static PathRequestManager instance;
        #endregion

        private void Awake()
        {
            instance = this;
            pathfinding = GetComponent<Pathfinding>();
        }

        /// <summary>
        /// Requests a path.
        /// </summary>
        /// <param name="pathStart">
        /// The start of the path.
        /// </param>
        /// <param name="pathEnd">
        /// The end of the path.
        /// </param>
        /// <param name="callback">
        /// A stored method which will be called once the path is calculated.
        /// </param>
        //Since the unit that's going to call this method isn't going to get a response immediately (we're spreading the request out amongst a few frames so that system isn't taxed all that much)
        //It's instead going to pass in a method like OnPathRecieved(), and we'll store and call that method once we have calculated it's path
        //Thus we'll be storing that method as an Action which will take in two parameters: the path, and a bool representing whether the path request was succesful
        public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
        {
            PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
            instance.pathRequestQueue.Enqueue(newRequest);
            instance.TryProcessNext();

        }

        /// <summary>
        /// Sees if we're currently processing the path, and if we're not it will ask the Pathfinding class to process the next one.
        /// </summary>
        private void TryProcessNext()
        {
            if (!isProcessingPath && pathRequestQueue.Count > 0)
            {
                currentPathRequest = pathRequestQueue.Dequeue();
                isProcessingPath = true;
                pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
            }
        }

        /// <summary>
        /// This will be called once the Pathfinding class has calculated it's path.
        /// </summary>
        /// <param name="path">
        /// The path calculated.
        /// </param>
        /// <param name="success">
        /// Return true if calculating the path was a success, or false if not.
        /// </param>
        public void FinishedProcessingPath(Vector3[] path, bool success)
        {
            currentPathRequest.callback(path, success);
            isProcessingPath = false;
            TryProcessNext();
        }

        //A data structure that will hold the Request Path information
        struct PathRequest
        {
            /// <summary>
            /// The start of the path.
            /// </summary>
            public Vector3 pathStart;
            /// <summary>
            /// The end of the path.
            /// </summary>
            public Vector3 pathEnd;
            /// <summary>
            /// A stored method which will be called once the path is calculated.
            /// </summary>
            public Action<Vector3[], bool> callback;

            /// <summary>
            /// Creates a new PathRequest
            /// </summary>
            /// <param name="_start">
            /// The start of the path.
            /// </param>
            /// <param name="_end">
            /// The end of the path.
            /// </param>
            /// <param name="_callback">
            /// A stored method which will be called once the path is calculated.
            /// </param>
            public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
            {
                pathStart = _start;
                pathEnd = _end;
                callback = _callback;
            }
        }
    }
}