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
        private Bitmap bmp;

        public mainFrm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbb_Scale.SelectedIndex = 0;
            cbb_Scale.Enabled = false;
            panel1.Visible = false;
            btn_CalcPath.Enabled = false;
        }

        private void btn_LoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            ofd.Filter = "Data File (*.tsp)|*.tsp|All files (*.*)|*.*";
            ofd.Title = "Choose the .tsp file";
            ofd.ShowDialog();

            //Initialize City List based on the file user chosen
            InitializeCityInfoList(ofd.FileName);

            cbb_Scale.Enabled = true;
            cbb_Scale.SelectedIndex = 0;

            //Plot the city location 
            DrawDot();

            //Enable WinForm component
            panel1.Visible = true;
            btn_CalcPath.Enabled = true;
        }

        private void cbb_Scale_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Plot the city location 
            DrawDot();
        }        

        private void btn_CalcPath_Click(object sender, EventArgs e)
        {
            
        }

        private void DrawDot()
        {
            //Plot the city location 
            bmp = DrawingHelper.DrawDot(cityList, Convert.ToSingle(cbb_Scale.SelectedItem.ToString()));
            pictureBox1.Image = bmp;
        }

        private void DrawLine(CityInfo firstCity, CityInfo secondCity)
        {
            //draw line between two cities
            bmp = DrawingHelper.DrawLine(firstCity, secondCity, bmp, Convert.ToSingle(cbb_Scale.SelectedItem.ToString()));
            pictureBox1.Image = bmp;
        }

        private void InitializeCityInfoList(string fileName)
        {
            cityList.Clear();

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
                    Y = Convert.ToSingle(row[2])
                });
            }
        }
    }
}
