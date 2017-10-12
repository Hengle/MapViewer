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
        private QuadTree _southeastChild = null;
        private QuadTree _southwestChild = null;
        private QuadTree _northeastChild = null;
        private QuadTree _northwestChild = null;
        private RectangleF _bounds = new RectangleF();
        private List<StreetSegment> _streets = new List<StreetSegment>();

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
            foreach(StreetSegment street in split)
            {
                if(street.VisibleLevels > height)
                {
                    visible.Add(street);
                } else
                {
                    invisible.Add(street);
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
            foreach(StreetSegment street in split)
            {
                if(street.Start.X <= x && street.End.X <= x)
                {
                    west.Add(street);
                } else if(street.Start.X > x && street.End.X > x)
                {
                    east.Add(street);
                } else
                {
                    StreetSegment firstHalf = street;
                    StreetSegment secondHalf = street;
                    float y = (((street.End.Y - street.Start.Y) * (x - street.Start.X)) / (street.End.X - street.Start.X))
                        + street.Start.Y;
                    firstHalf.Start = new PointF(x, y);
                    secondHalf.End = new PointF(x, y);
                    if (firstHalf.End.X > x)
                    {
                        east.Add(firstHalf);
                        west.Add(secondHalf);
                    }
                    else
                    {
                        east.Add(secondHalf);
                        west.Add(firstHalf);
                    }
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
            foreach(StreetSegment street in split)
            {
                if (street.Start.Y <= y && street.End.Y <= y)
                {
                    north.Add(street);
                }
                else if (street.Start.Y > y && street.End.Y > y)
                {
                    south.Add(street);
                }
                else
                {
                    StreetSegment firstHalf = street;
                    StreetSegment secondHalf = street;
                    float x = (((street.End.X - street.Start.X) * (y - street.Start.Y)) / (street.End.Y - street.Start.Y))
                        + street.Start.X;
                    firstHalf.End = new PointF(x, y);
                    secondHalf.Start = new PointF(x, y);
                    if (firstHalf.End.Y > y)
                    {
                        south.Add(firstHalf);
                        north.Add(secondHalf);
                    }
                    else
                    {
                        south.Add(secondHalf);
                        north.Add(firstHalf);
                    }
                }
            }
        }

        /// <summary>
        /// Contructs a QuadTree node.
        /// </summary>
        /// <param name="segments">List of street segments.</param>
        /// <param name="area">Area you're drawing in.</param>
        /// <param name="height">HEight of tree to be constructed.</param>
        public QuadTree(List<StreetSegment> segments, RectangleF area, int height)
        {
            _bounds = area;

            if (height == 0)
            {
                _streets = segments;
            }
            else
            {
                List<StreetSegment> northSide = new List<StreetSegment>();
                List<StreetSegment> southSide = new List<StreetSegment>();
                List<StreetSegment> nw = new List<StreetSegment>();
                List<StreetSegment> ne = new List<StreetSegment>();
                List<StreetSegment> sw = new List<StreetSegment>();
                List<StreetSegment> se = new List<StreetSegment>();
                List<StreetSegment> nonVisible = new List<StreetSegment>();
                List<StreetSegment> visible = new List<StreetSegment>();
                float x = (area.Width / 2) + area.Left;
                float y = (area.Height / 2) + area.Top;

                SplitHeights(segments, height, visible, nonVisible);
                _streets = visible;
                SplitNorthSouth(nonVisible, y, northSide, southSide);
                SplitEastWest(northSide, x, ne, nw);
                SplitEastWest(southSide, x, se, sw);

                float width = area.Width / 2;
                float length = area.Height / 2;
                height--;
                _northeastChild = new QuadTree(ne, new RectangleF(x, area.Top, width, length), height);
                _northwestChild = new QuadTree(nw, new RectangleF(area.Left, area.Top, width, length), height);
                _southeastChild = new QuadTree(se, new RectangleF(x, y, width, length), height);
                _southwestChild = new QuadTree(sw, new RectangleF(area.Left, y, width, length), height);
            }
        }


        /// <summary>
        /// Draws the contents of the tree. Gives each quadrant something.
        /// </summary>
        /// <param name="g">Graphics being used to draw.</param>
        /// <param name="sf">Scale Factor to make the coordinates map coordinates.</param>
        /// <param name="maxDepth">Max of tree nodes to be drawn.</param>
        public void Draw(Graphics g, int sf, int maxDepth)
        {

            RectangleF area = new RectangleF(g.ClipBounds.X / sf, g.ClipBounds.Y / sf, g.ClipBounds.Width / sf, g.ClipBounds.Height / sf);
            if (area.IntersectsWith(_bounds)){
                foreach (StreetSegment street in _streets)
                {
                    street.Draw(g, sf);
                }
                if(maxDepth > 0)
                {
                    maxDepth--;
                    _northeastChild.Draw(g, sf, maxDepth);
                    _northwestChild.Draw(g, sf, maxDepth);
                    _southeastChild.Draw(g, sf, maxDepth);
                    _southwestChild.Draw(g, sf, maxDepth);
                }
                
            }
        }
    }
}
