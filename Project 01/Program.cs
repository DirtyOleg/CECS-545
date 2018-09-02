using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project_01
{
    class Program
    {
        static CityInfo[] cities;
        static CityInfo[] shortestPath;
        static double shortestDistance = double.MaxValue;

        static void Main(string[] args)
        {
            InitializeAllCityInfo("Random12.tsp");

            List<CityInfo> visitedCities, notVisitedCities;
            InitializeSearch(out visitedCities, out notVisitedCities);

            Search(visitedCities, notVisitedCities, 0);

            PrintResult();
            Console.Read();
        }    
        
        static void InitializeAllCityInfo(string fileName)
        {
            //read all lines from the .tsp file, each line will be one element in the following array
            //File.ReadAllLines method will ignore the last empty line
            string[] data = File.ReadAllLines(fileName);

            //since all the files follow the similar format, first 7 lines is description
            //so skip all those useless lines, and only read the useful data
            int numberOfCities = data.Length - 7;
            cities = new CityInfo[numberOfCities];
            shortestPath = new CityInfo[numberOfCities + 1];

            for (int i = 7; i < data.Length; i++)
            {
                //split the line by the space
                string[] temp = data[i].Split(' ');
                cities[i - 7] = new CityInfo() { ID = i - 6, X = Convert.ToDouble(temp[1]), Y = Convert.ToDouble(temp[2]) };
            }
        }

        static void InitializeSearch(out List<CityInfo> visitedCities, out List<CityInfo> notVisitedCities)
        {
            visitedCities = new List<CityInfo>();
            notVisitedCities = new List<CityInfo>();

            //because the goal is to find the Hamiltonian Cycle, choice of starting city is not important, here set the first city as the starting city
            visitedCities.Add(cities[0]);
            for (int i = 1; i < cities.Length; i++)
            {
                notVisitedCities.Add(cities[i]);
            }
        }

        static void Search(List<CityInfo> visitedCities, List<CityInfo> notVisitedCities, double totalDistance)
        {
            if (notVisitedCities.Count == 0)
            {
                //visited all the city
                //calculate the distance between the last city and the starting city
                totalDistance += CalculateDistance(visitedCities.First(), visitedCities.Last());

                if (totalDistance < shortestDistance)
                {
                    shortestDistance = totalDistance;

                    for (int i = 0; i < visitedCities.Count; i++)
                    {
                        shortestPath[i] = visitedCities[i];
                    }
                    shortestPath[visitedCities.Count] = cities[0];
                }

                return;
            }

            //still have some cities which is not visited yet
            foreach (CityInfo nextVisitedCity in notVisitedCities)
            {
                List<CityInfo> newNotVisitedCities = DeepCopy(notVisitedCities);
                List<CityInfo> newVisitedCities = DeepCopy(visitedCities);
                RemoveElement(newNotVisitedCities, nextVisitedCity);
                newVisitedCities.Add(nextVisitedCity);

                totalDistance += CalculateDistance(visitedCities.Last(), nextVisitedCity);

                Search(newVisitedCities, newNotVisitedCities, totalDistance);
                //reset totalDistance, if you do not reset the totalDistance and you have more than one potential next city, the value of totalDistance will mess up
                totalDistance -= CalculateDistance(visitedCities.Last(), nextVisitedCity);
            }
        }

        static double CalculateDistance(CityInfo cityOne, CityInfo cityTwo)
        {
            return Math.Sqrt(Math.Pow(cityOne.X - cityTwo.X, 2) + Math.Pow(cityOne.Y - cityTwo.Y, 2));
        }

        static List<CityInfo> DeepCopy(List<CityInfo> cityList)
        {
            List<CityInfo> newOne = new List<CityInfo>();
            foreach (CityInfo city in cityList)
            {
                newOne.Add(city.Clone() as CityInfo);
            }
            return newOne;
        }

        static void RemoveElement(List<CityInfo> cityList, CityInfo NeedRemovedCity)
        {
            //since I used DeepCopy to create new List, the C# default List<T>.Remove() method will not work, so I have to provide one 
            int index = cityList.FindIndex((city) => city.ID == NeedRemovedCity.ID);
            cityList.RemoveAt(index);
        }

        static void PrintResult()
        {
            Console.Write("the shortest path is ");
            foreach (CityInfo city in shortestPath)
            {
                Console.Write($"{city.ID} ");
            }
            Console.WriteLine("\nthe shortest distance is " + shortestDistance);
        }
    }

    //store each city's ID and coordinate
    class CityInfo : ICloneable
    {
        public int ID { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        //used for deep copy
        public object Clone()
        {
            CityInfo newOne = new CityInfo();
            newOne.ID = this.ID;
            newOne.X = this.X;
            newOne.Y = this.Y;
            return newOne;
        }
    }
}
