using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            //shortest path considering the least number of nodes
            Console.WriteLine("Shortest path considering the least number of nodes: \n");

            //DFS way
            Console.WriteLine("the result for using DFS alogithm:");
            DFSMethodForLeastNodes();
            Console.WriteLine();

            //BFS way
            Console.WriteLine("the result for using BFS algorithm:");
            BFSMethodForLeastNodes();
            Console.WriteLine();  

            //shortest path considering the weighted edges
            Console.WriteLine("Shortest path considering the weighted edges: \n");

            //DFS way
            Console.WriteLine("the result for using DFS alogithm:");
            DFSMethodForWeightedEdges();
            Console.WriteLine();

            //BFS way
            Console.WriteLine("the result for using BFS algorithm:");
            BFSMethodForWeightedEdges();
            Console.WriteLine();         

            Console.Read();
        }

        static double CalculateDistance(CityInfo cityOne, CityInfo cityTwo)
        {
            return Math.Sqrt(Math.Pow(cityOne.X - cityTwo.X, 2) + Math.Pow(cityOne.Y - cityTwo.Y, 2));
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
                    Y = Convert.ToDouble(row[2]),
                    pathFromSource = new List<int>()
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

        static void ResetDistanceAndPathForWeightedEdge()
        {
            foreach (CityInfo city in cities)
            {
                city.pathFromSource.Clear();
                city.distanceFromSource = double.MaxValue;
            }

            queue.Clear();
            stack.Clear();

            //manually assign the information of the starting city
            cities[0].distanceFromSource = 0;
            cities[0].pathFromSource.Add(1);
        }

        static void BFSMethodForWeightedEdges()
        {
            ResetDistanceAndPathForWeightedEdge();

            //start timer
            Stopwatch sw = new Stopwatch();
            sw.Start();

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

            sw.Stop();
            Console.WriteLine($"total time used (measured in milliseconds): {sw.Elapsed.TotalMilliseconds}");

            Console.WriteLine("the shortest distance is: " + cities[10].distanceFromSource);
            Console.Write("the shortest path is: ");
            foreach (int cityId in cities[10].pathFromSource)
            {
                Console.Write(cityId + " ");
            }
            Console.WriteLine();
        }

        static void DFSMethodForWeightedEdges()
        {
            ResetDistanceAndPathForWeightedEdge();

            //start timer
            Stopwatch sw = new Stopwatch();
            sw.Start();

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

            sw.Stop();
            Console.WriteLine($"total time used (measured in milliseconds): {sw.Elapsed.TotalMilliseconds}");

            Console.WriteLine("the shortest distance is: " + cities[10].distanceFromSource);
            Console.Write("the shortest path is: ");
            foreach (int cityId in cities[10].pathFromSource)
            {
                Console.Write(cityId + " ");
            }
            Console.WriteLine();
        }

        static void ResetPathForLeaseNodes()
        {
            foreach (CityInfo city in cities)
            {
                city.pathFromSource.Clear();
                city.IsVisited = false;
            }

            cities[0].pathFromSource.Add(1);
            cities[0].IsVisited = true;

            queue.Clear();
            stack.Clear();
        }

        static void BFSMethodForLeastNodes()
        {
            ResetPathForLeaseNodes();

            //start timer
            Stopwatch sw = new Stopwatch();
            sw.Start();

            queue.Enqueue(cities[0]);

            while (queue.Count != 0)
            {
                CityInfo dequeuedCity = queue.Dequeue();
                foreach (CityInfo connectedCity in dequeuedCity.connectedCities)
                {
                    //if the connectedCity is already in the queue, skip it
                    if (connectedCity.IsVisited == true)
                    {
                        continue;
                    }

                    //otherwise, add it to the queue and update the pathFromSource
                    queue.Enqueue(connectedCity);
                    connectedCity.IsVisited = true;

                    connectedCity.pathFromSource.AddRange(dequeuedCity.pathFromSource);
                    connectedCity.pathFromSource.Add(connectedCity.ID);
                }// end foreach
            }// end while()

            sw.Stop();
            Console.WriteLine($"total time used (measured in milliseconds): {sw.Elapsed.TotalMilliseconds}");

            Console.Write("the shortest path is: ");
            foreach (int cityId in cities[10].pathFromSource)
            {
                Console.Write(cityId + " ");
            }
            Console.WriteLine();
        }

        static void DFSMethodForLeastNodes()
        {
            ResetPathForLeaseNodes();

            //start timer
            Stopwatch sw = new Stopwatch();
            sw.Start();

            stack.Push(cities[0]);

            while (stack.Count != 0)
            {
                CityInfo popCity = stack.Pop();
                foreach (CityInfo connectedCity in popCity.connectedCities)
                {
                    //if the connectedCity is already in the stack, skip it
                    if (connectedCity.IsVisited == true)
                    {
                        continue;
                    }

                    //otherwise, add it to the stack and update the pathFromSource
                    stack.Push(connectedCity);
                    connectedCity.IsVisited = true;

                    connectedCity.pathFromSource.AddRange(popCity.pathFromSource);
                    connectedCity.pathFromSource.Add(connectedCity.ID);
                }// end foreach
            }// end while()

            sw.Stop();
            Console.WriteLine($"total time used (measured in milliseconds): {sw.Elapsed.TotalMilliseconds}");

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
        public bool IsVisited { get; set; }//Used only for considering least nodes case. Store if the city is visited.
        public CityInfo[] connectedCities { get; set; } //Used for both cases. Store the connected cities, this one is manually assigned
        public double distanceFromSource { get; set; } //Used only for considering weighted deges case. Store the shortest distance from the first city 
        public List<int> pathFromSource { get; set; } //Used for both cases. Store the shortest path from the first city
    }
}
