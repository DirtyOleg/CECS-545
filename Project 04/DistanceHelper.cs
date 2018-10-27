using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_05
{
    public static class DistanceHelper
    {
        public static float DistanceBetweenTwoPoints(CityInfo cityOne, CityInfo cityTwo)
        {
            return Convert.ToSingle(Math.Sqrt(Math.Pow(cityOne.X - cityTwo.X, 2) + Math.Pow(cityOne.Y - cityTwo.Y, 2)));
        }

        public static float DistanceFromPointToLine(CityInfo partialTourNodeOne, CityInfo partialTourNodeTwo, CityInfo city)
        {
            //the distance function copy from Wikipedia: https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line
            double numberator = Math.Abs((partialTourNodeTwo.Y - partialTourNodeOne.Y) * city.X - (partialTourNodeTwo.X - partialTourNodeOne.X) * city.Y + partialTourNodeTwo.X * partialTourNodeOne.Y - partialTourNodeTwo.Y * partialTourNodeOne.X);
            double denominator = Math.Sqrt(Math.Pow((partialTourNodeTwo.Y - partialTourNodeOne.Y), 2) + Math.Pow((partialTourNodeTwo.X - partialTourNodeOne.X), 2));

            return Convert.ToSingle(numberator / denominator);
        }

        public static float TotalDistance(List<CityInfo> cityList)
        {
            float distance = 0f;

            for (int i = 0; i < cityList.Count - 1; i++)
            {
                distance += DistanceBetweenTwoPoints(cityList[i], cityList[i + 1]);
            }
            distance += DistanceBetweenTwoPoints(cityList.First(), cityList.Last());

            return distance;
        }
    }
}
