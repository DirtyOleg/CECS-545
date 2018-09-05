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
        static CityInfo[] cities = new CityInfo[11];
        static Queue<CityInfo> queue = new Queue<CityInfo>();
        static Stack<CityInfo> stack = new Stack<CityInfo>();

        static void Main(string[] args)
        {
            InitializeAllCityInfo();

            //BFS way
            Console.WriteLine("the result for using BFS algorithm:");
            ResetDistanceAndPath();
            BFSMethod();

            Console.WriteLine();

            //DFS way
            Console.WriteLine("the result for using DFS alogithm:");
            ResetDistanceAndPath();
            DFSMethod();

            Console.Write("\nPress any key to exit...");
            Console.Read();
        }

        static void InitializeAllCityInfo()
        {
            //read all lines from the .tsp file, each line will be one element in the following array
            //File.ReadAllLines method will ignore the last empty line
            string[] data = File.ReadAllLines("11PointDFSBFS.tsp");

            //since all the files follow the similar format, first 7 lines is description
            //so skip all those useless lines, and only read the useful data
            for (int i = 7; i < data.Length; i++)
            {
                //split the line by the space
                string[] row = data[i].Split(' ');
                cities[i - 7] = new CityInfo()
                {
                    ID = i - 6,
                    X = Convert.ToDouble(row[1]),
                    Y = Convert.ToDouble(row[2])
                };
            }

            //manually assign connected cities
            cities[0].connectedCities = new CityInfo[] { cities[1], cities[2], cities[3] };
            cities[1].connectedCities = new CityInfo[] { cities[2] };
            cities[2].connectedCities = new CityInfo[] { cities[3], cities[4] };
            cities[3].connectedCities = new CityInfo[] { cities[4], cities[5], cities[6] };
            cities[4].connectedCities = new CityInfo[] { cities[6], cities[7] };
            cities[5].connectedCities = new CityInfo[] { cities[7] };
            cities[6].connectedCities = new CityInfo[] { cities[8], cities[9] };
            cities[7].connectedCities = new CityInfo[] { cities[8], cities[9], cities[10] };
            cities[8].connectedCities = new CityInfo[] { cities[10] };
            cities[9].connectedCities = new CityInfo[] { cities[10] };
            cities[10].connectedCities = new CityInfo[] { };
        }

        static void ResetDistanceAndPath()
        {
            foreach (CityInfo city in cities)
            {
                city.distanceFromSource = double.MaxValue;
                city.pathFromSource = new List<int>();
            }

            //manually assign the information of the starting city
            cities[0].distanceFromSource = 0;
            cities[0].pathFromSource.Add(1);
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

                    //calculate the distance from the first city based on the current path
                    double distanceFromsource = dequeuedCity.distanceFromSource + CalculateDistance(dequeuedCity, connectedCity);

                    //compare the current calculated distance with the shortest distance calculated from previous calculation
                    if (distanceFromsource < connectedCity.distanceFromSource)
                    {
                        //assign new shortest distance and path
                        connectedCity.distanceFromSource = distanceFromsource;

                        connectedCity.pathFromSource.Clear();
                        foreach (int cityId in dequeuedCity.pathFromSource)
                        {
                            connectedCity.pathFromSource.Add(cityId);
                        }
                        connectedCity.pathFromSource.Add(connectedCity.ID);
                    }
                }// end foreach
            }// end while()

            Console.WriteLine("the shortest distance is: " + cities[10].distanceFromSource);
            Console.Write("the shortest path is: ");
            foreach (int cityId in cities[10].pathFromSource)
            {
                Console.Write(cityId + " ");
            }
            Console.WriteLine();
        }

        static void DFSMethod()
        {
            stack.Push(cities[0]);

            while (stack.Count != 0)
            {
                CityInfo popCity = stack.Pop();
                foreach (CityInfo connectedCity in popCity.connectedCities)
                {
                    stack.Push(connectedCity);

                    //calculate the distance from the first city based on the current path
                    double distanceFromsource = popCity.distanceFromSource + CalculateDistance(popCity, connectedCity);

                    //compare the current calculated distance with the shortest distance calculated from previous calculation
                    if (distanceFromsource < connectedCity.distanceFromSource)
                    {
                        //assign new shortest distance and path
                        connectedCity.distanceFromSource = distanceFromsource;

                        connectedCity.pathFromSource.Clear();
                        foreach (int cityId in popCity.pathFromSource)
                        {
                            connectedCity.pathFromSource.Add(cityId);
                        }
                        connectedCity.pathFromSource.Add(connectedCity.ID);
                    }
                }// end foreach
            }// end while

            Console.WriteLine("the shortest distance is: " + cities[10].distanceFromSource);
            Console.Write("the shortest path is: ");
            foreach (int cityId in cities[10].pathFromSource)
            {
                Console.Write(cityId + " ");
            }
            Console.WriteLine();
        }
    }

    //store each city's ID and coordinate
    class CityInfo
    {
        public int ID { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public CityInfo[] connectedCities { get; set; } //this one is manually assigned
        public double distanceFromSource { get; set; } //store the shortest distance from the first city 
        public List<int> pathFromSource { get; set; } //store the shortest path from the first city
    }
}
