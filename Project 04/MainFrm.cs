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
        private float[] currentDistances = new float[4];

        private Bitmap bmp = new Bitmap(1000, 1000);

        private const int iteration = 30;

        public Mainfrm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitailizationHelper.CreateInitialGeneration(initialCityListOne, initialCityListTwo, initialCityListThree, initialCityListFour);
            RefreshList();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            Start(1, 0.1f);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start(1, 0.5f);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Start(2, 0.1f);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Start(2, 0.5f);
        }

        private void Start(int crossoverSelection, float mutationPossibility)
        {
            ResultStruct[] results = new ResultStruct[iteration];
            for (int i = 0; i < iteration; i++)
            {
                GeneticAlgorithm.Evolution(ref currentPopulation, ref currentDistances, crossoverSelection, mutationPossibility);
                results[i] = ResultHelper.GenerateResult(currentDistances);
            }

            RefreshPicBox(currentPopulation[0]);

            EvolutionFrm eFrm = new EvolutionFrm();
            eFrm.CreateChart(results);
            eFrm.ShowDialog();
            RefreshList();
        }

        private void RefreshPicBox(List<CityInfo> cityList)
        {
            bmp = DrawingHelper.DrawDotandLine(cityList);
            pictureBox1.Image = bmp;
        }

        private void RefreshList()
        {
            currentPopulation[0] = initialCityListOne;
            currentPopulation[1] = initialCityListTwo;
            currentPopulation[2] = initialCityListThree;
            currentPopulation[3] = initialCityListFour;
            currentDistances[0] = DistanceHelper.TotalDistance(initialCityListOne);
            currentDistances[1] = DistanceHelper.TotalDistance(initialCityListTwo);
            currentDistances[2] = DistanceHelper.TotalDistance(initialCityListThree);
            currentDistances[3] = DistanceHelper.TotalDistance(initialCityListFour);
            RefreshPicBox(initialCityListOne);
        }
    }
}
