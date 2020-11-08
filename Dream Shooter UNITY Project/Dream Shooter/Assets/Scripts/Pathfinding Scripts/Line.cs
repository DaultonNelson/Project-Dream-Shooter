using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Pathfinding_Scripts
{
    public struct Line
    {
        #region Variables
        const float verticalLineGradient = 1e5f;

        float gradient;
        float y_intercept;
        float gradientPerpendicular;
        bool approachSide;
        Vector2 pointOnLine_1;
        Vector2 pointOnLine_2;
        #endregion

        public Line(Vector2 pointOnLine, Vector2 pointPerpendicularToLine)
        {
            float dx = pointOnLine.x - pointPerpendicularToLine.x;
            float dy = pointOnLine.y - pointPerpendicularToLine.y;

            //Calculating Perpendicular Gradient.
            if (dx == 0)
            {
                gradientPerpendicular = verticalLineGradient;
            }
            else
            {
                gradient = dy / dx;
            }

            if (gradientPerpendicular == 0)
            {
                gradient = verticalLineGradient;
            }
            else
            {
                gradient = -1 / gradientPerpendicular;
            }

            y_intercept = pointOnLine.y - gradient * pointOnLine.x;
            pointOnLine_1 = pointOnLine;
            pointOnLine_2 = pointOnLine + new Vector2(1, gradient);

            approachSide = false;
            approachSide = GetSide(pointPerpendicularToLine);
        }

        //Returns true if the given point p is on one side of the line defined by gradient and y_intercept, and false if it's on the other side.
        bool GetSide(Vector2 p)
        {
            bool output = false;

            if ((p.x - pointOnLine_1.x) * (pointOnLine_2.y - pointOnLine_1.y) > (p.y - pointOnLine_1.y) * (pointOnLine_2.x = pointOnLine_1.x))
            {
                output = true;
            }

            return output;
        }

        //We're going to see if the given point is on the other side of the line than the perpendicular point that were given in the ctor.
        public bool HasCrossedLine(Vector2 p)
        {
            bool output = false;

            output = GetSide(p) != approachSide;

            return output;
        }
    }
}