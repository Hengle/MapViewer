using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ksu.Cis300.MapViewer
{
    public partial class UserInterface : Form
    {
        private int _initialScale = 10;
        private Map uxMap;

        public UserInterface()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private static List<StreetSegment> ReadFile(string f, out RectangleF bounds)
        {
            using (StreamReader sr = new StreamReader(f))
            {
                List<StreetSegment> _streets = null;
                string[] lines = f.Split(',');
                bounds = new RectangleF();
                bounds.Width = Convert.ToSingle(lines[0]);
                bounds.Height = Convert.ToSingle(lines[1]);
                PointF start = new PointF(Convert.ToInt32(lines[2]), Convert.ToInt32(lines[3]));
                PointF end = new PointF(Convert.ToInt32(lines[4]), Convert.ToInt32(lines[5]));
                Color color = Color.FromArgb(Convert.ToInt32(lines[6]));
                int vl = Convert.ToInt32(lines[7]);
                float w = Convert.ToSingle(lines[8]);
                StreetSegment street = new StreetSegment(start, end, color, vl, w);
                _streets.Add(street);
                return _streets;
            }
        }

        private void uxOpenMap_Click(object sender, EventArgs e)
        {
            List<StreetSegment> _streets = null;
            RectangleF bounds;
            if(uxOpenDialog.ShowDialog() == DialogResult.OK)
            {
                try {
                    string fileName = uxOpenDialog.FileName;
                    _streets = ReadFile(fileName, out bounds);
                    uxMap = new Map(_streets, bounds, _initialScale);
                    uxMapContainer.Controls.Clear();
                    uxMapContainer.Controls.Add(uxMap);
                    uxZoomIn.Enabled = true;
                    uxZoomOut.Enabled = false;
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void uxZoomIn_Click(object sender, EventArgs e)
        {
            Point currentPos = uxMapContainer.AutoScrollPosition;
            currentPos.X *= -1;
            currentPos.Y *= -1;
            uxMap.ZoomIn();
            if (!uxMap.canZoomIn)
                uxZoomIn.Enabled = false;
            else
                uxZoomIn.Enabled = true;
            if (!uxMap.canZoomOut)
                uxZoomOut.Enabled = false;
            else
                uxZoomOut.Enabled = true;
            Size size = uxMapContainer.ClientSize;
            Point newPos = new Point((currentPos.X * 2) + (size.Width / 2), (currentPos.Y * 2) + (size.Height / 2));
            uxMapContainer.AutoScrollPosition = newPos;
        }

        private void uxZoomOut_Click(object sender, EventArgs e)
        {
            Point currentPos = uxMapContainer.AutoScrollPosition;
            currentPos.X *= -1;
            currentPos.Y *= -1;
            uxMap.ZoomOut();
            if (!uxMap.canZoomIn)
                uxZoomIn.Enabled = false;
            else
                uxZoomIn.Enabled = true;
            if (!uxMap.canZoomOut)
                uxZoomOut.Enabled = false;
            else
                uxZoomOut.Enabled = true;
            Size size = uxMapContainer.ClientSize;
            Point newPos = new Point((currentPos.X / 2) - (size.Width / 4), (currentPos.Y / 2) - (size.Height / 4));
            uxMapContainer.AutoScrollPosition = newPos;
        }
    }
}
