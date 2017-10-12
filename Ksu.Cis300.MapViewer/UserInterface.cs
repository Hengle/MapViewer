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


        /// <summary>
        /// Reads the file inputted and puts the values into a list of street segments.
        /// </summary>
        /// <param name="f">String of street data.</param>
        /// <param name="bounds">Area of the streets.</param>
        /// <returns></returns>
        private static List<StreetSegment> ReadFile(string f, out RectangleF bounds)
        {
            /*List<StreetSegment> _streets = new List<StreetSegment>();

            using (StreamReader sr = new StreamReader(f))
            {
                string[] coords = sr.ReadLine().Split(',');
                bounds = new RectangleF(0, 0, Convert.ToSingle(coords[0]), Convert.ToSingle(coords[1]));

                while (!sr.EndOfStream)
                {
                    string[] lines = sr.ReadLine().Split(',');
                    PointF start = new PointF(Convert.ToInt32(lines[0]), Convert.ToInt32(lines[1]));
                    PointF end = new PointF(Convert.ToInt32(lines[2]), Convert.ToInt32(lines[3]));
                    Color color = Color.FromArgb(Convert.ToInt32(lines[4]));
                    int vl = Convert.ToInt32(lines[5]);
                    float w = Convert.ToSingle(lines[6]);
                    StreetSegment street = new StreetSegment(start, end, color, w, vl);
                    _streets.Add(street);
                }
            }
            return _streets;*/

            List<StreetSegment> list = new List<StreetSegment>();


            using (StreamReader input = new StreamReader(f))
            {
                string[] conttents = input.ReadLine().Split(',');
                bounds = new RectangleF(0, 0, Convert.ToSingle(conttents[0]), Convert.ToSingle(conttents[1]));
                while (!input.EndOfStream)
                {
                    string[] contents = input.ReadLine().Split(',');

                    PointF start = new PointF(Convert.ToSingle(contents[0]), Convert.ToSingle(contents[1]));
                    PointF end = new PointF(Convert.ToSingle(contents[2]), Convert.ToSingle(contents[3]));
                    StreetSegment str = new StreetSegment(start, end, Color.FromArgb(Convert.ToInt32(contents[4])), Convert.ToSingle(contents[5]), Convert.ToInt32(contents[6]));
                    list.Add(str);

                }
            }

            return list;
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

        /// <summary>
        /// Zooms in on a spot on the map when clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxZoomIn_Click(object sender, EventArgs e)
        {
            Point currentPos = uxMapContainer.AutoScrollPosition;
            currentPos.X *= -1;
            currentPos.Y *= -1;
            uxMap.ZoomIn();
            uxZoomIn.Enabled = uxMap.canZoomIn();
            uxZoomOut.Enabled = uxMap.canZoomOut();
            Size size = uxMapContainer.ClientSize;
            Point newPos = new Point((currentPos.X * 2) + (uxMapContainer.ClientSize.Width / 2), (currentPos.Y * 2) + (uxMapContainer.ClientSize.Height / 2));
            uxMapContainer.AutoScrollPosition = newPos;
        }

        /// <summary>
        /// Zooms out on a spot on the map when clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxZoomOut_Click(object sender, EventArgs e)
        {
            Point currentPos = uxMapContainer.AutoScrollPosition;
            currentPos.X *= -1;
            currentPos.Y *= -1;
            uxMap.ZoomOut();
            uxZoomIn.Enabled = uxMap.canZoomIn();
            uxZoomOut.Enabled = uxMap.canZoomOut();
            Size size = uxMapContainer.ClientSize;
            Point newPos = new Point((currentPos.X / 2) - (uxMapContainer.ClientSize.Width / 4), (currentPos.Y / 2) - (uxMapContainer.ClientSize.Height / 4));
            uxMapContainer.AutoScrollPosition = newPos;
        }
    }
}
