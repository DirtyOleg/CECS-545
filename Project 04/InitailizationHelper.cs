using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_04
{
    public static class InitailizationHelper
    {
        public static void CreateInitialGeneration(List<CityInfo> initialCityListOne, List<CityInfo> initialCityListTwo, List<CityInfo> initialCityListThree, List<CityInfo> initialCityListFour, float[] distances)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Random10.tsp");

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
            
            CreateCityBasedOnNearestInsertionHeuristic(initialCityListTwo, 3, initialCityListOne);
            CreateCityBasedOnNearestInsertionHeuristic(initialCityListThree, 5, initialCityListOne);
            CreateCityListBasedOnNearestFirst(initialCityListFour, 7, initialCityListOne);

            distances[0] = DistanceHelper.TotalDistance(initialCityListOne);
            distances[1] = DistanceHelper.TotalDistance(initialCityListTwo);
            distances[2] = DistanceHelper.TotalDistance(initialCityListThree);
            distances[3] = DistanceHelper.TotalDistance(initialCityListFour);
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

                    float temp = DistanceHelper.DistanceBetweenTwoCities(city, cityList.Last());
                    if (temp < distance)
                    {
                        distance = temp;
                        nextCity = city;
                    }
                }

                cityList.Add(nextCity);
            }
        }

        private static void CreateCityBasedOnNearestInsertionHeuristic(List<CityInfo> cityList, int startCityID, List<CityInfo> sample)
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

                float temp = DistanceHelper.DistanceBetweenTwoCities(startCity, city);
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
    }
}
