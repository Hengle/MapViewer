using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Ksu.Cis300.MapViewer
{
    class QuadTree
    {
        private QuadTree _southeastChild;
        private QuadTree _southwestChild;
        private QuadTree _northeastChild;
        private QuadTree _northwestChild;
        private RectangleF _bounds;
        private List<StreetSegment> _streets;

        /// <summary>
        /// Organizes Street Segments by visibility. 
        /// </summary>
        /// <param name="split">List of segments to split.</param>
        /// <param name="height">Height of current subtree.</param>
        /// <param name="visible">List of streets that are visible at the current height.</param>
        /// <param name="invisible">List of streets not visible at the current height.</param>
        private static void SplitHeights(List<StreetSegment> split, int height, List<StreetSegment> visible,
            List<StreetSegment> invisible)
        {
            for(int i = 0; i < split.Count; i++)
            {
                if(split[i].VisibleLevels > height)
                {
                    visible.Add(split[i]);
                } else
                {
                    invisible.Add(split[i]);
                }
            }
        }

        /// <summary>
        /// Splits the list of segments into 2 lists depening where they lay(West or East). If it's stuck between the
        /// two, it splits it in half and adds the halves to the according list.
        /// </summary>
        /// <param name="split">List of street segments being split.</param>
        /// <param name="x">Value of X on the graph.</param>
        /// <param name="east">list of all segments east.</param>
        /// <param name="west">List of all segments west.</param>
        private static void SplitEastWest(List<StreetSegment> split, float x, List<StreetSegment> east,
            List<StreetSegment> west)
        {
            for(int i = 0; i < split.Count; i++)
            {
                if(split[i].Start.X < x && split[i].End.X < x)
                {
                    west.Add(split[i]);
                } else if(split[i].Start.X > x && split[i].End.X > x)
                {
                    east.Add(split[i]);
                } else
                {
                    StreetSegment firstHalf = split[i];
                    StreetSegment secondHalf = split[i];
                    float y = (((firstHalf.End.Y - firstHalf.Start.Y) * (x - firstHalf.Start.X)) / (firstHalf.End.X - firstHalf.Start.X))
                        + firstHalf.Start.Y;
                    firstHalf.Start = new PointF(x, y);
                    secondHalf.End = new PointF(x, y);
                    west.Add(secondHalf);
                    east.Add(firstHalf);
                }
            }
        }

        /// <summary>
        /// Works similar to the above method. Splits the segments from the list into 2 new lists. If it's between the
        /// 2 directions, it splits it in half and adds them to the correct list.
        /// </summary>
        /// <param name="split">List of segments being split</param>
        /// <param name="y">value of Y on the graph.</param>
        /// <param name="north">List of segments north.</param>
        /// <param name="south">List of segments south.</param>
        private static void SplitNorthSouth(List<StreetSegment> split, float y, List<StreetSegment> north,
            List<StreetSegment> south)
        {
            for (int i = 0; i < split.Count; i++)
            {
                if (split[i].Start.Y < y && split[i].End.Y < y)
                {
                    south.Add(split[i]);
                }
                else if (split[i].Start.Y > y && split[i].End.Y > y)
                {
                    north.Add(split[i]);
                }
                else
                {
                    StreetSegment firstHalf = split[i];
                    StreetSegment secondHalf = split[i];
                    float x = (((firstHalf.End.X - firstHalf.Start.X) * (y - firstHalf.Start.Y)) / (firstHalf.End.Y - firstHalf.Start.Y))
                        + firstHalf.Start.X;
                    firstHalf.Start = new PointF(x, y);
                    secondHalf.End = new PointF(x, y);
                    south.Add(secondHalf);
                    north.Add(firstHalf);
                }
            }
        }

        public QuadTree(List<StreetSegment> segments, RectangleF area, int height)
        {
            if(height == 0)
            {
                _streets = segments;
            } else
            {
                float x = (area.Width / 2) + area.Left;
                float y = (area.Height / 2) - area.Top; //change to -area.Top later if this doesnt work.
                List<StreetSegment> north = null;
                List<StreetSegment> south = null;
                List<StreetSegment> nw = null;
                List<StreetSegment> sw = null;
                List<StreetSegment> se = null;
                List<StreetSegment> ne = null;
                List<StreetSegment> all = null;
                SplitNorthSouth(segments, y, north, south);
                SplitEastWest(north, x, ne, nw);
                SplitEastWest(south, x, se, sw);
                height = height - 1;
                _southeastChild = new QuadTree(se, new RectangleF(area.Left, area.Top, area.Width, area.Height) , height);
                _southwestChild = new QuadTree;

            }
        }
    }
}
