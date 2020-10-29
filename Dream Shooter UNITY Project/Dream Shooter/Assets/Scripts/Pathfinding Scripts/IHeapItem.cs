using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pathfinding_Scripts
{
    public interface IHeapItem<T> : IComparable<T>
    {
        /// <summary>
        /// The index of this Heap item.
        /// </summary>
        int HeapIndex { get; set; }
    }
}
