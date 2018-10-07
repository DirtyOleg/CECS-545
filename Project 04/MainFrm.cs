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
    public partial class Mainfrm : Form
    {
        private List<CityInfo> initialCityListOne = new List<CityInfo>();
        private List<CityInfo> initialCityListTwo = new List<CityInfo>();
        private List<CityInfo> initialCityListThree = new List<CityInfo>();
        private List<CityInfo> initialCityListFour = new List<CityInfo>();

        private List<CityInfo>[] currentPopulation = new List<CityInfo>[4];
        private float[] CurrentDistances = new float[4];

        private Bitmap bmp = new Bitmap(1000, 1000);

        public Mainfrm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitailizationHelper.CreateInitialGeneration(initialCityListOne, initialCityListTwo, initialCityListThree, initialCityListFour, CurrentDistances);
            currentPopulation[0] = initialCityListOne;
            currentPopulation[1] = initialCityListTwo;
            currentPopulation[2] = initialCityListThree;
            currentPopulation[3] = initialCityListFour;
            RefreshPicBox(initialCityListFour);
        }

        private void RefreshPicBox(List<CityInfo> cityList)
        {
            bmp = DrawingHelper.DrawDotandLine(cityList);
            pictureBox1.Image = bmp;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
           GeneticAlgorithm.Mate(ref currentPopulation, ref CurrentDistances, 1, 0.1f);
        }
    }
}
