using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pathfinding_Scripts
{
    //The T makes this class a Generic, allowing it to work work other classes as well
    //where T : IHeapItem - Guarantees T implements the IHeapItem interface
    public class Heap<T> where T : IHeapItem<T>
    {
        #region Variables
        /// <summary>
        /// An array of any Generic type (Nodes, Ints, floats).
        /// </summary>
        T[] items;
        /// <summary>
        /// A count of the current amount of items in this Heap.
        /// </summary>
        int currentItemCount;
        
        /// <summary>
        /// The count of items within this Heap.
        /// </summary>
        public int Count { get { return currentItemCount; } }
        #endregion

        /// <summary>
        /// Creates a new Heap.
        /// </summary>
        /// <param name="maxHeapSize">
        /// The maximum size of this Heap.
        /// </param>
        public Heap(int maxHeapSize)
        {
            items = new T[maxHeapSize];
        }

        /// <summary>
        /// Adds an item (T) to this Heap.
        /// </summary>
        /// <param name="item">
        /// The item being added.
        /// </param>
        public void Add(T item)
        {
            item.HeapIndex = currentItemCount;
            items[currentItemCount] = item;
            SortUp(item);
            currentItemCount++;
        }

        /// <summary>
        /// Removes the first item of the Heap.
        /// </summary>
        /// <returns>
        /// The item removed from the Heap.
        /// </returns>
        public T RemoveFirst()
        {
            T output = items[0];

            currentItemCount--;
            items[0] = items[currentItemCount];
            items[0].HeapIndex = 0;
            SortDown(items[0]);

            return output;
        }

        /// <summary>
        /// Changes the priority of a given item.
        /// </summary>
        /// <param name="item">
        /// The item whose prority is being changed.
        /// </param>
        public void UpdateItem(T item)
        {
            //Called in Pathfinding if the Node's priority has been increased
            //If I'm ever working with this Heap class with something else whose item's priority decreases, call SortDown
            SortUp(item);
        }

        /// <summary>
        /// Checks to see if this Heap contains the given item.
        /// </summary>
        /// <param name="item">
        /// The item that's being checked if the Heap contains.
        /// </param>
        /// <returns>
        /// Return true if the Heap does contain the given item, or false if not.
        /// </returns>
        public bool Contains(T item)
        {
            bool output = false;

            //Equals checks if two given objects are equal (their the same object)
            output = Equals(items[item.HeapIndex], item);

            return output;
        }

        /// <summary>
        /// Sorts the given item by comparing it to it's parents.
        /// </summary>
        /// <param name="item">
        /// The item being sorted.
        /// </param>
        void SortUp(T item)
        {
            int parentIndex = (item.HeapIndex - 1) / 2;

            while (true)
            {
                T parentItem = items[parentIndex];
                //CompareTo compares indexes.  If the given type has a higher priorty it returns 1, if same returns 0, if lower returns -1
                //This statement calls the CompareTo function in the item class that inherits IComparabe (in this case Node.CompareTo gets called)
                if (item.CompareTo(parentItem) > 0)
                {
                    Swap(item, parentItem);
                }
                else
                {
                    //Breaks out of the loop
                    break;
                }
            }
        }

        /// <summary>
        /// Sorts the given item by comparing it to it's children.
        /// </summary>
        /// <param name="item">
        /// The item being sorted.
        /// </param>
        void SortDown(T item)
        {
            while (true)
            {
                int childIndexLeft = item.HeapIndex * 2 + 1;
                int childIndexRight = item.HeapIndex * 2 + 2;
                int swapIndex = 0;

                if (childIndexLeft < currentItemCount)
                {
                    swapIndex = childIndexLeft;

                    if (childIndexRight < currentItemCount)
                    {
                        if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                        {
                            swapIndex = childIndexRight;
                        }
                    }

                    //If the child has a higher priority than it's parent.
                    if (item.CompareTo(items[swapIndex]) < 0)
                    {
                        Swap(item, items[swapIndex]);
                    }
                    else
                    {
                        //The parent has the higher priority, it's in it's correct place.
                        return;
                    }
                }
                else
                {
                    //If the parent doesn't have any children, it's in it's correct position
                    return;
                }
            }
        }

        /// <summary>
        /// Swaps the given items.
        /// </summary>
        /// <param name="itemA">
        /// A first item.
        /// </param>
        /// <param name="itemB">
        /// A second item.
        /// </param>
        void Swap (T itemA, T itemB)
        {
            items[itemA.HeapIndex] = itemB;
            items[itemB.HeapIndex] = itemA;

            int itemAIndex = itemA.HeapIndex;
            itemA.HeapIndex = itemB.HeapIndex;
            itemB.HeapIndex = itemAIndex;
        }
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    /// <summary>
    /// The index of this Heap item.
    /// </summary>
    int HeapIndex { get; set; }
}