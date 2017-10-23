﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Quaver.Graphics;

namespace Quaver.Utility
{
    internal class Util
    {
        /// <summary>
        /// Returns a 1-dimensional value for an object's alignment within the provided boundary.
        /// </summary>
        /// <param name="scale">The value (percentage) which the object will be aligned to (0=min, 0.5 =mid, 1.0 = max)</param>
        /// <param name="objectSize">The size of the object</param>
        /// <param name="boundary"></param>
        /// <returns></returns>
        internal static float Align(float scale, float objectSize, Vector2 boundary, float offset = 0)
        {
            float boundaryMin;
            float boundaryMax;

            // Sets the boundary min/max values
            if (boundary.X < boundary.Y)
            {
                boundaryMin = boundary.X;
                boundaryMax = boundary.Y;
            }
            else
            {
                boundaryMin = boundary.Y;
                boundaryMax = boundary.X;
            }

            // The alignment (Also used as a temporary boundary size value)
            var alignment = boundaryMax - boundaryMin;

            //If the object size is bigger than the boundary for some reason
            alignment = (objectSize > alignment) ? (scale * alignment) + boundaryMin : ((alignment - objectSize) * scale) + boundaryMin;

            return alignment + offset;
        }

        /// <summary>
        /// Returns an aligned rectangle within a boundary.
        /// </summary>
        /// <param name="objectAlignment">The alignment of the object.</param>
        /// <param name="objectRect">The size of the object.</param>
        /// <param name="boundary">The Rectangle of the boundary.</param>
        /// <returns></returns>
        internal static Rectangle DrawRect(Alignment objectAlignment, Rectangle objectRect, Rectangle boundary)
        {
            float alignX = 0;
            float alignY = 0;

            // Set the X-Alignment Scale
            switch (objectAlignment)
            {
                case Alignment.BotCenter:
                case Alignment.MidCenter:
                case Alignment.TopCenter:
                    alignX = 0.5f;
                    break;
                case Alignment.BotRight:
                case Alignment.MidRight:
                case Alignment.TopRight:
                    alignX = 1f;
                    break;
                default:
                    break;
            }

            // Set the Y-Alignment Scale
            switch (objectAlignment)
            {
                case Alignment.MidLeft:
                case Alignment.MidCenter:
                case Alignment.MidRight:
                    alignY = 0.5f;
                    break;
                case Alignment.BotLeft:
                case Alignment.BotCenter:
                case Alignment.BotRight:
                    alignY = 1f;
                    break;
                default:
                    break;
            }

            //Set X and Y Alignments
            alignX = Align(alignX, objectRect.Width, new Vector2(boundary.X, boundary.X + boundary.Width), objectRect.X);
            alignY = Align(alignY, objectRect.Height, new Vector2(boundary.Y, boundary.Y + boundary.Height), objectRect.Y);

            return new Rectangle((int)alignX, (int)alignY, (int)objectRect.Width, (int)objectRect.Height);
        }

        /// <summary>
        /// Generates A random float between 2 numbers.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        internal static float Random(float min, float max)
        {
            var  random = new Random();

            // If min > max for some reason
            if (min > max)
            {
                var temp = min;
                max = min;
                min = temp;
            }

            //Generate the random number
            var randNum = random.Next(0, 1000) / 1000f;

            //Return the random number in the given range
            return (randNum * (max - min)) + min;
        }

        /// <summary>
        /// This method is used for animation/tweening.
        /// </summary>
        /// <param name="target">The target value.</param>
        /// <param name="current">The current value.</param>
        /// <param name="scale">Make sure this value is between 0 and 1.</param>
        /// <returns></returns>
        internal static float Tween(float target, float current, double scale)
        {
            return (float)(current + ((target - current) * scale));
        }
    }
}