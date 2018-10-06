using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_04
{
    public partial class Form1 : Form
    {
        private List<CityInfo> initialCityListOne = new List<CityInfo>();
        private List<CityInfo> initialCityListTwo = new List<CityInfo>();
        private List<CityInfo> initialCityListThree = new List<CityInfo>();
        private List<CityInfo> initialCityListFour = new List<CityInfo>();

        private List<CityInfo>[] currentPopulation = new List<CityInfo>[4];
        private float[] CurrentDistances = new float[4];

        private Bitmap bmp = new Bitmap(1000, 1000);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitailizationHelper.CreateInitialGeneration(initialCityListOne, initialCityListTwo, initialCityListThree, initialCityListFour, CurrentDistances);
            RefreshPicBox(initialCityListFour);
        }

        private void RefreshPicBox(List<CityInfo> cityList)
        {
            bmp = DrawingHelper.DrawDotandLine(cityList);
            pictureBox1.Image = bmp;
        }
    }
}
