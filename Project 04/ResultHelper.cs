using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_04
{
    public struct ResultStruct
    {
        public float min;
        public float max;
        public float avg;
        public float std;
    }

    public static class ResultHelper
    {
        public static ResultStruct GenerateResult(float[] distances)
        {
            ResultStruct result = new ResultStruct();
            result.min = distances.Min();
            result.max = distances.Max();
            result.avg = distances.Average();

            float sumOfSquaresOfDifferences = distances.Select(val => (val - result.avg) * (val - result.avg)).Sum();
            result.std = Convert.ToSingle(Math.Sqrt(sumOfSquaresOfDifferences / distances.Length));

            return result;
        }

        public static void printResult(ResultStruct[] results)
        {
            foreach (ResultStruct result in results)
            {
                Console.WriteLine("min: " + result.min + "\tmax: " + result.max + "\tavg: " + result.avg + "\tstd: " + result.std);
            }
        }
    }
}
