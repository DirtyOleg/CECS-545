using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_05
{
    public partial class EvolutionFrm : Form
    {
        public EvolutionFrm()
        {
            InitializeComponent();
        }

        public void CreateChart(ResultStruct[] results)
        {
            int index = 1;
            foreach (ResultStruct result in results)
            {
                this.chart1.Series["Max"].Points.AddXY(index, result.max);
                this.chart1.Series["Min"].Points.AddXY(index, result.min);
                this.chart1.Series["Avg"].Points.AddXY(index, result.avg);
                this.chart1.Series["STD"].Points.AddXY(index, result.std);
                index++;
            }
        }
    }
}
