using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_05
{
    public static class InitailizationHelper
    {
        public static void CreateInitialGeneration(List<CityInfo> initialCityListOne, List<CityInfo> initialCityListTwo, List<CityInfo> initialCityListThree, List<CityInfo> initialCityListFour)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Random100.tsp");

            //read all lines from the .tsp file, each line will be one element in the following array
            //File.ReadAllLines method will ignore the last empty line
            string[] data = File.ReadAllLines(path);

            //since all the files follow the similar format, first 7 lines is description
            //so skip all those useless lines, and only read the useful data
            for (int i = 7; i < data.Length; i++)
            {
                //split the line by the space
                string[] row = data[i].Split(' ');
                initialCityListOne.Add(new CityInfo()
                {
                    ID = i - 6,
                    X = Convert.ToSingle(row[1]),
                    Y = Convert.ToSingle(row[2])
                });
            }

            CreateCityListOperationOne(initialCityListTwo, initialCityListOne);
            CreateCityListOperationTwo(initialCityListThree, initialCityListOne);
            CreateCityListOperationThree(initialCityListFour, initialCityListOne);

            //Random r = new Random();
            //CreateCityListBasedOnNearestFirst(initialCityListFour, r.Next(1,initialCityListOne.Count + 1), initialCityListOne);
        }

        private static void CreateCityListBasedOnNearestFirst(List<CityInfo> cityList, int startCityID, List<CityInfo> sample)
        {
            cityList.Add(sample.Find(city => city.ID == startCityID));

            while (cityList.Count < sample.Count)
            {
                float distance = float.MaxValue;
                CityInfo nextCity = cityList.First();

                foreach (CityInfo city in sample)
                {
                    if (cityList.Find(c => c.ID == city.ID) != null)
                    {
                        continue;
                    }

                    float temp = DistanceHelper.DistanceBetweenTwoPoints(city, cityList.Last());
                    if (temp < distance)
                    {
                        distance = temp;
                        nextCity = city;
                    }
                }

                cityList.Add(nextCity);
            }
        }

        private static void CreateCityListBasedOnNearestInsertionHeuristic(List<CityInfo> cityList, int startCityID, List<CityInfo> sample)
        {
            //firstly, find the first two cities taht salesman will visit
            //the fisrt city is based on the user choice, the second city is the city which is most closest to first city 
            CityInfo startCity = sample.Find(city => city.ID == startCityID);
            cityList.Add(startCity);
            CityInfo secondCity = sample[0];

            float distance = float.MaxValue;

            foreach (CityInfo city in sample)
            {
                if (city.ID == startCity.ID)
                {
                    continue;
                }

                float temp = DistanceHelper.DistanceBetweenTwoPoints(startCity, city);
                if (distance > temp)
                {
                    secondCity = city;
                    distance = temp;
                }
            }

            cityList.Add(secondCity);

            //Closest Edge Insertion Heuristic
            while (cityList.Count != sample.Count)
            {
                //Determine whether adding this city to the partial tour based on Closest Edge Insertion Heuristic
                distance = float.MaxValue;
                CityInfo nextCity = null;
                int insertIndex = 0;

                foreach (CityInfo potentialNextCity in sample)
                {
                    if (cityList.Exists(c => c.ID == potentialNextCity.ID))
                    {
                        //if the city is already visited, skip it
                        continue;
                    }

                    //compute the distance between potentialNextCity with all edge in the partial tour
                    for (int i = 0; i < cityList.Count - 1; i++)
                    {
                        float temp = DistanceHelper.DistanceFromPointToLine(cityList[i], cityList[i + 1], potentialNextCity);
                        if (temp < distance)
                        {
                            distance = temp;
                            nextCity = potentialNextCity;
                            insertIndex = i + 1;
                        }
                    }
                }
                cityList.Insert(insertIndex, nextCity);
            }
        }

        private static void CreateCityListOperationOne(List<CityInfo> cityList, List<CityInfo> sample)
        {
            for (int i = 0; i < sample.Count; i += 2)
            {
                cityList.Add(sample[i]);
            }

            for (int i = 1; i < sample.Count; i += 2)
            {
                cityList.Add(sample[i]);
            }
        }

        private static void CreateCityListOperationTwo(List<CityInfo> cityList, List<CityInfo> sample)
        {
            for (int i = 0; i < sample.Count / 2; i++)
            {
                cityList.Add(sample[i]);
                cityList.Add(sample[sample.Count - i - 1]);
            }
        }

        private static void CreateCityListOperationThree(List<CityInfo> cityList, List<CityInfo> sample)
        {
            for (int i = 0; i < sample.Count / 2; i++)
            {
                cityList.Add(sample[i]);
                cityList.Add(sample[i + sample.Count / 2]);
            }
        }
    }
}
