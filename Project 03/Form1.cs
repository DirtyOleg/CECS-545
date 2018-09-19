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

namespace Project_03
{
    public partial class mainFrm : Form
    {
        private List<CityInfo> cityList = new List<CityInfo>(); //City List
        private List<CityInfo> visitedCityList = new List<CityInfo>();
        private Bitmap bmp;
        private float totalDistance;

        public mainFrm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbb_Scale.Enabled = false;
            cbb_StartCity.Enabled = false;
            btn_Calculate.Enabled = false;
            panel1.Visible = false;
        }

        private void btn_LoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            ofd.Filter = "Data File (*.tsp)|*.tsp|All files (*.*)|*.*";
            ofd.Title = "Choose the .tsp file";
            ofd.ShowDialog();

            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                //Initialize City List based on the file user chosen
                InitializeCityInfoList(ofd.FileName);

                cbb_Scale.Enabled = true;
                cbb_Scale.SelectedIndex = 0;

                //Plot the cities based on their location
                DrawCities();

                //Enable WinForm component
                panel1.Visible = true;
                cbb_StartCity.Enabled = true;
                cbb_StartCity.SelectedIndex = 0;
                btn_Calculate.Enabled = true;
            }
        }

        private void cbb_Scale_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Plot the city location 
            DrawCities();
        }

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            btn_Calculate.Enabled = false;
            CalculatePath();
            btn_Calculate.Enabled = true;
        }

        private void cbb_StartCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Plot the city location 
            DrawCities();
        }

        private void DrawCities()
        {
            //Plot the city location 
            bmp = DrawingHelper.DrawDot(cityList, Convert.ToSingle(cbb_Scale.SelectedItem.ToString()));
            pictureBox1.Image = bmp;
        }

        private void DrawPath()
        {
            //draw line between two cities
            for (int i = 0; i < visitedCityList.Count - 1; i++)
            {
                bmp = DrawingHelper.DrawLine(visitedCityList[i], visitedCityList[i + 1], bmp, Convert.ToSingle(cbb_Scale.SelectedItem.ToString()));

                //draw the path from the last visited city to the first city
                if (i == visitedCityList.Count - 2)
                {
                    bmp = DrawingHelper.DrawLine(visitedCityList[0], visitedCityList[i + 1], bmp, Convert.ToSingle(cbb_Scale.SelectedItem.ToString()));
                }
            }

            pictureBox1.Image = bmp;
        }

        private void InitializeCityInfoList(string fileName)
        {
            cityList.Clear();
            cbb_StartCity.Items.Clear();

            //read all lines from the .tsp file, each line will be one element in the following array
            //File.ReadAllLines method will ignore the last empty line
            string[] data = File.ReadAllLines(fileName);

            //since all the files follow the similar format, first 7 lines is description
            //so skip all those useless lines, and only read the useful data
            for (int i = 7; i < data.Length; i++)
            {
                //split the line by the space
                string[] row = data[i].Split(' ');
                cityList.Add(new CityInfo()
                {
                    ID = i - 6,
                    X = Convert.ToSingle(row[1]),
                    Y = Convert.ToSingle(row[2]),
                    IsVisited = false
                });

                cbb_StartCity.Items.Add(i - 6);
            }
        }

        static float DistanceBetweenTwoCities(CityInfo cityOne, CityInfo cityTwo)
        {
            return Convert.ToSingle(Math.Sqrt(Math.Pow(cityOne.X - cityTwo.X, 2) + Math.Pow(cityOne.Y - cityTwo.Y, 2)));
        }

        static float DistanceFromPointToLine(CityInfo partialTourNodeOne, CityInfo partialTourNodeTwo, CityInfo city)
        {
            //the distance function copy from Wikipedia: https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line
            double numberator = Math.Abs((partialTourNodeTwo.Y - partialTourNodeOne.Y) * city.X - (partialTourNodeTwo.X - partialTourNodeOne.X) * city.Y + partialTourNodeTwo.X * partialTourNodeOne.Y - partialTourNodeTwo.Y * partialTourNodeOne.X);
            double denominator = Math.Sqrt(Math.Pow((partialTourNodeTwo.Y - partialTourNodeOne.Y), 2) + Math.Pow((partialTourNodeTwo.X - partialTourNodeOne.X), 2));

            return Convert.ToSingle(numberator / denominator);
        }

        private void CalculatePath()
        {
            visitedCityList.Clear();
            totalDistance = 0f;

            //firstly, find the first two cities taht salesman will visit
            //the fisrt city is based on the user choice, the second city is the city which is most closest to first city 
            CityInfo startCity = cityList.Find(city => city.ID == (Convert.ToInt32(cbb_StartCity.SelectedItem)));
            visitedCityList.Add(startCity);
            CityInfo secondCity = cityList[0];

            float distance = float.MaxValue;

            foreach (CityInfo city in cityList)
            {
                if (city.ID == startCity.ID)
                {
                    continue;
                }

                float temp = DistanceBetweenTwoCities(startCity, city);
                if (distance > temp)
                {
                    secondCity = city;
                    distance = temp;
                }
            }

            visitedCityList.Add(secondCity);

            //Closest Edge Insertion Heuristic
            while (visitedCityList.Count != cityList.Count)
            {
                //Determine whether adding this city to the partial tour based on Closest Edge Insertion Heuristic
                distance = float.MaxValue;
                CityInfo nextCity = null;
                int insertIndex = 0;

                foreach (CityInfo potentialNextCity in cityList)
                {
                    if (visitedCityList.Exists(c => c.ID == potentialNextCity.ID))
                    {
                        //if the city is already visited, skip it
                        continue;
                    }

                    //compute the distance between potentialNextCity with all edge in the partial tour
                    for (int i = 0; i < visitedCityList.Count - 1; i++)
                    {
                        float temp = DistanceFromPointToLine(visitedCityList[i], visitedCityList[i + 1], potentialNextCity);
                        if (temp < distance)
                        {
                            distance = temp;
                            nextCity = potentialNextCity;
                            insertIndex = i + 1;
                        }
                    }
                }
                visitedCityList.Insert(insertIndex, nextCity);
            }

            ShowResult();
        }

        private void ShowResult()
        {
            //draw the path
            DrawPath();

            //calculate and print the total travel distance
            for (int i = 0; i < visitedCityList.Count - 1; i++)
            {
                totalDistance += DistanceBetweenTwoCities(visitedCityList[i], visitedCityList[i + 1]);
            }
            totalDistance += DistanceBetweenTwoCities(visitedCityList[0], visitedCityList[visitedCityList.Count - 1]);
            lbl_Distance.Text = totalDistance.ToString();

            //print the path
            StringBuilder str = new StringBuilder();
            foreach (CityInfo city in visitedCityList)
            {
                str.Append(city.ID + "->");
            }
            str.Append(visitedCityList[0].ID);
            lbl_Path.Text = str.ToString();
        }
    }
}
