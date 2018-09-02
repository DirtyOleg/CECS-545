using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_02
{
    class Program
    {
        static CityInfo[] cities;
        static double shortestDistance;
        static Queue<CityInfo> queue = new Queue<CityInfo>();
        static Stack<CityInfo> stack = new Stack<CityInfo>();

        static void Main(string[] args)
        {
            //BFS way 
            InitializeAllCityInfo();
            BFSMethod();

            //DFS way
            InitializeAllCityInfo();
            DFSMethod();
        }

        static void InitializeAllCityInfo()
        {
            cities = new CityInfo[11];
            shortestDistance = double.MaxValue;

            //read all lines from the .tsp file, each line will be one element in the following array
            //File.ReadAllLines method will ignore the last empty line
            string[] data = File.ReadAllLines("11PointDFSBFS.tsp");

            //since all the files follow the similar format, first 7 lines is description
            //so skip all those useless lines, and only read the useful data
            for (int i = 7; i < data.Length; i++)
            {
                //split the line by the space
                string[] temp = data[i].Split(' ');
                cities[i - 7] = new CityInfo() { ID = i - 6, X = Convert.ToDouble(temp[1]), Y = Convert.ToDouble(temp[2]), isVisited = false };
            }

            //manually assign connected cities
            cities[0].connectedCities = new CityInfo[] {
                new CityInfo() {ID = 2, isVisited = false},
                new CityInfo() {ID = 3, isVisited = false},
                new CityInfo() {ID = 4, isVisited = false}
            };
            cities[1].connectedCities = new CityInfo[] {
                new CityInfo() {ID = 3, isVisited = false}
            };
            cities[2].connectedCities = new CityInfo[] {
                new CityInfo() {ID = 4, isVisited = false},
                new CityInfo() {ID = 5, isVisited = false}
            };
            cities[3].connectedCities = new CityInfo[] {
                new CityInfo() {ID = 5, isVisited = false},
                new CityInfo() {ID = 6, isVisited = false},
                new CityInfo() {ID = 7, isVisited = false}
            };
            cities[4].connectedCities = new CityInfo[] {
                new CityInfo() {ID = 7, isVisited = false},
                new CityInfo() {ID = 8, isVisited = false}
            };
            cities[5].connectedCities = new CityInfo[] {
                new CityInfo() {ID = 8, isVisited = false}
            };
            cities[6].connectedCities = new CityInfo[] {
                new CityInfo() {ID = 9, isVisited = false},
                new CityInfo() {ID = 10, isVisited = false}
            };
            cities[7].connectedCities = new CityInfo[] {
                new CityInfo() {ID = 9, isVisited = false},
                new CityInfo() {ID = 10, isVisited = false},
                new CityInfo() {ID = 11, isVisited = false}
            };
            cities[8].connectedCities = new CityInfo[] {
                new CityInfo() {ID = 11, isVisited = false}
            };
            cities[9].connectedCities = new CityInfo[] {
                new CityInfo() {ID = 11, isVisited = false}
            };
            cities[10].connectedCities = new CityInfo[] {

            };
        }

        static double CalculateDistance(CityInfo cityOne, CityInfo cityTwo)
        {
            return Math.Sqrt(Math.Pow(cityOne.X - cityTwo.X, 2) + Math.Pow(cityOne.Y - cityTwo.Y, 2));
        }

        static void BFSMethod()
        {
            queue.Enqueue(cities[0]);

            while (queue.Count != 0)
            {
                CityInfo dequeuedCity = queue.Dequeue();
                foreach (CityInfo connectedCity in dequeuedCity.connectedCities)
                {
                    queue.Enqueue(connectedCity);
                }
            }
        }

        static void DFSMethod()
        {

        }
    }

    //store each city's ID and coordinate
    class CityInfo
    {
        public int ID { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public bool isVisited { get; set; }
        public CityInfo[] connectedCities { get; set; }

        ////used for deep copy : ICloneable
        //public object Clone()
        //{
        //    CityInfo newOne = new CityInfo();
        //    newOne.ID = this.ID;
        //    newOne.X = this.X;
        //    newOne.Y = this.Y;
        //    return newOne;
        //}
    }
}
