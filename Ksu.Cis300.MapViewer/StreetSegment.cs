using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Ksu.Cis300.MapViewer
{
    public struct StreetSegment
    {

        private PointF _start;
        private PointF _end;
        private Pen _pen;
        private int _visibleLevels;

        public PointF Start
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
            }
        }

        public PointF End
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
            }
        }

        public int VisibleLevels
        {
            get
            {
                return _visibleLevels;
            }
            set
            {
                _visibleLevels = value;
            }
        }

        public StreetSegment(PointF s, PointF e, Color c, int vl, float w)
        {
            _start = s;
            _end = e;
            _visibleLevels = vl;
            _pen = new Pen(c, w);
        }

        public void Draw(Graphics g)
        {
            g.DrawLine(_pen, _start.X, _start.Y, _end.X, _end.Y);
        }
    }
}
