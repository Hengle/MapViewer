using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ksu.Cis300.MapViewer
{
    public partial class Map : UserControl
    {
        private const int _maxZoom = 6;
        private int _scale;
        private int _zoom = 0;
        private QuadTree uxMap;
        public bool canZoomIn;
        public bool canZoomOut;

        public Map(List<StreetSegment> streets, RectangleF bounds, int sf)
        {
            int count = 0;
            try {
                foreach (StreetSegment street in streets)
                {
                    if (isWithinBounds(street.Start, bounds) && isWithinBounds(street.End, bounds))
                    {
                        InitializeComponent();
                        uxMap = new QuadTree(streets, bounds, _maxZoom);
                        _scale = sf;
                        Size size = new Size(Convert.ToInt32(bounds.Width) * sf, Convert.ToInt32(bounds.Height )* sf);
                        Size = size;
                    }
                    count++;
                }
            } catch(ArgumentException ae)
            {
                MessageBox.Show("Street " + count + "is not within the given bounds." + ae);
            }
            
        }

        private static bool isWithinBounds(PointF point, RectangleF area)
        {
            if(point.X >= area.Left && point.X <= area.Right && point.Y <= area.Top && point.Y >= area.Bottom)
            {
                return true;
            }
            return false;
        }

        public void ZoomIn()
        {
            if (canZoomIn)
            {
                _zoom++;
                _scale = _scale * 2;
                Size = new Size(Size.Width * 2, Size.Height * 2);
                Invalidate();
            }
        }

        public void ZoomOut()
        {
            if (canZoomOut)
            {
                _zoom--;
                _scale = _scale / 2;
                Size = new Size(Size.Width / 2, Size.Height / 2);
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RectangleF rec = e.ClipRectangle;
            Graphics draw = e.Graphics;
            Region r = new Region(rec);
            draw.Clip = r;
            uxMap.Draw(draw, _scale, _zoom);
        }

    }
}
