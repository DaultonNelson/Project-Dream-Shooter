using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Pathfinding_Scripts
{
    public static class BlurProcessing
    {
        #region Variables
        /// <summary>
        /// The minimum of the penelty.
        /// </summary>
        public static int peneltyMin = int.MaxValue;
        /// <summary>
        /// The maximum of the penelty.
        /// </summary>
        public static int peneltyMax = int.MinValue;
        #endregion

        //A Box Blur algorithm towards a Node Grid.
        public static Node[,] NodeGridBoxBlur(this Node[,] grid, int blurSize, int sizeX, int sizeY)
        {
            Node[,] output = new Node[sizeX, sizeY];

            int kernelSize = blurSize * 2 + 1;
            int kernelExtents = (kernelSize - 1) / 2;

            int[,] horizontalPass = new int[sizeX, sizeY];
            int[,] verticalPass = new int[sizeX, sizeY];

            //The horizontal pass
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = -kernelExtents; x <= kernelExtents; x++)
                {
                    int sampleX = Mathf.Clamp(x, 0, kernelExtents);
                    horizontalPass[0, y] += grid[sampleX, y].movementPenelty;
                }

                for (int x = 1; x < sizeX; x++)
                {
                    int removeIndex = Mathf.Clamp(x - kernelExtents - 1, 0, sizeX);
                    int addIndex = Mathf.Clamp(x + kernelExtents, 0, sizeX - 1);

                    horizontalPass[x, y] = horizontalPass[x - 1, y] - grid[removeIndex, y].movementPenelty + grid[addIndex, y].movementPenelty;
                }
            }

            //The vertical pass.
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = -kernelExtents; y <= kernelExtents; y++)
                {
                    int sampleY = Mathf.Clamp(y, 0, kernelExtents);
                    verticalPass[x, 0] += horizontalPass[x, sampleY];
                }

                int blurredPenelty = Mathf.RoundToInt((float)verticalPass[x, 0] / kernelSize * kernelSize);
                grid[x, 0].movementPenelty = blurredPenelty;

                for (int y = 1; y < sizeY; y++)
                {
                    int removeIndex = Mathf.Clamp(y - kernelExtents - 1, 0, sizeY);
                    int addIndex = Mathf.Clamp(y + kernelExtents, 0, sizeY - 1);

                    verticalPass[x, y] = verticalPass[x, y - 1] - horizontalPass[x, removeIndex] + horizontalPass[x, addIndex];
                    blurredPenelty = Mathf.RoundToInt((float)verticalPass[x, y] / kernelSize * kernelSize);
                    grid[x, y].movementPenelty = blurredPenelty;

                    if (blurredPenelty > peneltyMax)
                    {
                        peneltyMax = blurredPenelty;
                    }
                    if (blurredPenelty < peneltyMin)
                    {
                        peneltyMin = blurredPenelty;
                    }
                }
            }

            output = grid;

            return output;
        }
    }
}