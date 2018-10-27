using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_05
{
    public static class GeneticAlgorithm
    {
        static float[] Fitness(float[] distances)
        {
            float[] fitnesses = new float[4];

            float total = distances.Sum();

            for (int i = 0; i < 4; i++)
            {
                fitnesses[i] = total / distances[i];
            }

            total = fitnesses.Sum();

            for (int i = 0; i < 4; i++)
            {
                fitnesses[i] = fitnesses[i] / total * 100;

                if (i == 0)
                {
                    continue;
                }
                else
                {
                    fitnesses[i] += fitnesses[i - 1];
                }
            }

            return fitnesses;
        }

        static List<CityInfo>[] Selection(List<CityInfo>[] currentPopulation, float[] fitnesses)
        {
            List<CityInfo>[] selected = new List<CityInfo>[4];
            Random r = new Random();

            for (int i = 0; i < 4; i++)
            {
                float ran = Convert.ToSingle(r.NextDouble() * 100);

                if (ran <= fitnesses[0])
                {
                    selected[i] = currentPopulation[0];
                }
                else if (ran <= fitnesses[1])
                {
                    selected[i] = currentPopulation[1];
                }
                else if (ran <= fitnesses[2])
                {
                    selected[i] = currentPopulation[2];
                }
                else
                {
                    selected[i] = currentPopulation[3];
                }
            }

            return selected;
        }

        static List<CityInfo>[] Crossover(List<CityInfo>[] selected, int crossoverSelection)
        {
            List<CityInfo>[] childGeneration = new List<CityInfo>[4];

            switch (crossoverSelection)
            {
                case 1:
                    CrossoverOperation1(selected[0], selected[1], out childGeneration[0], out childGeneration[1]);
                    CrossoverOperation1(selected[2], selected[3], out childGeneration[2], out childGeneration[3]);
                    break;
                case 2:
                    CrossoverOperation2(selected[0], selected[1], out childGeneration[0], out childGeneration[1]);
                    CrossoverOperation2(selected[2], selected[3], out childGeneration[2], out childGeneration[3]);
                    break;
            }

            return childGeneration;
        }

        //The Original Idea of both two of the Crossover Operations are coming from https://www.hindawi.com/journals/cin/2017/7430125/
        //the idea is divided each of the parent list into three part, child keep the center part same as the parent, but the first part and the three part will be different based on which crossover operation will be used.

        //the first crossover operation is partially mapped crossover operator
        static void CrossoverOperation1(List<CityInfo> parentOne, List<CityInfo> parentTwo, out List<CityInfo> childOne, out List<CityInfo> childTwo)
        {
            childOne = new List<CityInfo>();
            childTwo = new List<CityInfo>();

            int cutPointOne, cutPointTwo;
            cutPointOne = (int)parentOne.Count / 3;
            cutPointTwo = (int)2 * parentOne.Count / 3;

            List<CityInfo> centerOne = parentOne.GetRange(cutPointOne, cutPointTwo - cutPointOne);
            List<CityInfo> centerTwo = parentTwo.GetRange(cutPointOne, cutPointTwo - cutPointOne);

            #region child one            
            for (int i = 0; i < cutPointOne; i++)
            {
                if (centerTwo.Contains(parentOne[i]))
                {
                    childOne.Add(null);
                }
                else
                {
                    childOne.Add(parentOne[i]);
                }
            }

            childOne.AddRange(centerTwo);

            for (int i = cutPointTwo; i < parentOne.Count; i++)
            {
                if (centerTwo.Contains(parentOne[i]))
                {
                    childOne.Add(null);
                }
                else
                {
                    childOne.Add(parentOne[i]);
                }
            }

            for (int i = 0; i < childOne.Count; i++)
            {
                if (childOne[i] == null)
                {
                    foreach (CityInfo item in parentOne)
                    {
                        if (!childOne.Contains(item))
                        {
                            childOne[i] = item;
                        }
                    }
                }
            }
            #endregion

            #region child two
            for (int i = 0; i < cutPointOne; i++)
            {
                if (centerOne.Contains(parentTwo[i]))
                {
                    childTwo.Add(null);
                }
                else
                {
                    childTwo.Add(parentTwo[i]);
                }
            }

            childTwo.AddRange(centerOne);

            for (int i = cutPointTwo; i < parentOne.Count; i++)
            {
                if (centerOne.Contains(parentTwo[i]))
                {
                    childTwo.Add(null);
                }
                else
                {
                    childTwo.Add(parentTwo[i]);
                }
            }

            for (int i = 0; i < childOne.Count; i++)
            {
                if (childTwo[i] == null)
                {
                    foreach (CityInfo item in parentTwo)
                    {
                        if (!childTwo.Contains(item))
                        {
                            childTwo[i] = item;
                        }
                    }
                }
            }
            #endregion
        }

        //the second one is order crossover operator
        static void CrossoverOperation2(List<CityInfo> parentOne, List<CityInfo> parentTwo, out List<CityInfo> childOne, out List<CityInfo> childTwo)
        {
            CityInfo[] tempOne = new CityInfo[parentOne.Count];
            CityInfo[] tempTwo = new CityInfo[parentTwo.Count];

            int cutPointOne, cutPointTwo;
            cutPointOne = (int)parentOne.Count / 3;
            cutPointTwo = (int)2 * parentOne.Count / 3;

            List<CityInfo> centerOne = parentOne.GetRange(cutPointOne, cutPointTwo - cutPointOne);
            List<CityInfo> centerTwo = parentTwo.GetRange(cutPointOne, cutPointTwo - cutPointOne);

            for (int i = cutPointOne; i < cutPointTwo; i++)
            {
                tempOne[i] = parentOne[i];
                tempTwo[i] = parentTwo[i];
            }

            #region Child One            
            int counter = 0;
            int index = cutPointTwo;
            for (int i = index; counter < parentTwo.Count - (cutPointTwo - cutPointOne); i++)
            {
                if (i == parentOne.Count)
                {
                    i = 0;
                }

                if (tempOne.Contains(parentTwo[i]))
                {
                    continue;
                }
                else
                {
                    tempOne[index] = parentTwo[i];
                    index++;
                    counter++;
                }

                if (index == parentTwo.Count)
                {
                    index = 0;
                }
            }
            #endregion

            #region Child Two            
            counter = 0;
            index = cutPointTwo;
            for (int i = index; counter < parentTwo.Count - (cutPointTwo - cutPointOne); i++)
            {
                if (i == parentTwo.Count)
                {
                    i = 0;
                }

                if (tempTwo.Contains(parentOne[i]))
                {
                    continue;
                }
                else
                {
                    tempTwo[index] = parentOne[i];
                    index++;
                    counter++;
                }

                if (index == parentOne.Count)
                {
                    index = 0;
                }
            }
            #endregion

            childOne = new List<CityInfo>(tempOne);
            childTwo = new List<CityInfo>(tempTwo);
        }

        //new mutation method, instead of random switch two city, this new method iterate the new cith path, resolve intersection with mutation possibility
        static void Mutation(ref List<CityInfo>[] crossed, float mutationPossibility)
        {
            foreach (List<CityInfo> cityList in crossed)
            {
                MutationOperation(cityList, mutationPossibility);
            }
        }

        static void MutationOperation(List<CityInfo> cityList, float mutationPossibility)
        {
            Random r = new Random();

            for (int i = 0; i < cityList.Count - 1; i++)
            {
                for (int j = i + 2; j < cityList.Count - 1; j++)
                {
                    CityInfo cityOne = cityList[i];
                    CityInfo cityTwo = cityList[j];
                    CityInfo cityThree = cityList[i + 1];
                    CityInfo cityFour = cityList[j + 1];

                    //check if two lines are intersected, method is compare area
                    //if two lines are intersected, the sum of the area of the two triangle remain same, while if the two lines are not intersected, the areas are not the same

                    if (AreaCheck(cityOne, cityTwo, cityThree, cityFour))
                    {
                        if (r.NextDouble() > mutationPossibility)
                        {
                            continue;
                        }

                        CityInfo temp = cityList[i];
                        cityList[i] = cityList[j];
                        cityList[j] = temp;

                        temp = cityList[i + 1];
                        cityList[i + 1] = cityList[j + 1];
                        cityList[j + 1] = temp;
                    }
                }
            }
        }

        static bool AreaCheck(CityInfo cityOne, CityInfo cityTwo, CityInfo cityThree, CityInfo cityFour)
        {
            float areaOne, areaTwo;

            areaOne = 0.5f * DistanceHelper.DistanceBetweenTwoPoints(cityOne, cityThree) * (DistanceHelper.DistanceFromPointToLine(cityOne, cityThree, cityTwo) + DistanceHelper.DistanceFromPointToLine(cityOne, cityThree, cityFour));

            areaTwo = 0.5f * DistanceHelper.DistanceBetweenTwoPoints(cityTwo, cityFour) * (DistanceHelper.DistanceFromPointToLine(cityTwo, cityFour, cityOne) + DistanceHelper.DistanceFromPointToLine(cityTwo, cityFour, cityThree));

            return areaOne == areaTwo ? true : false;
        }

        #region Old Mutation Method

        //static void Mutation(List<CityInfo>[] crossed, float mutationPossibility)
        //{
        //    Random r = new Random();

        //    for (int i = 0; i < crossed.Count(); i++)
        //    {
        //        for (int j = 0; j < crossed[i].Count(); j++)
        //        {
        //            if (r.NextDouble() > mutationPossibility)
        //            {
        //                continue;
        //            }

        //            int swapGene = r.Next(crossed[i].Count());

        //            CityInfo temp = crossed[i][swapGene];
        //            crossed[i][swapGene] = crossed[i][j];
        //            crossed[i][j] = temp;
        //        }
        //    }
        //}
        #endregion

        static void Elimination(ref float[] previousDistances, ref float[] nextDistances, ref List<CityInfo>[] previousPopulation, ref List<CityInfo>[] nextPopulation)
        {
            if (nextDistances.Max() > previousDistances.Min())
            {
                int indexOne = Array.IndexOf(nextDistances, nextDistances.Max());
                int indexTwo = Array.IndexOf(previousDistances, previousDistances.Min());
                nextDistances[indexOne] = previousDistances[indexTwo];
                nextPopulation[indexOne] = previousPopulation[indexTwo];
            }
        }

        public static void Evolution(ref List<CityInfo>[] previousPopulation, ref float[] previousDistances, int crossoverSelection, float mutationPossibility)
        {
            float[] fitnesses = Fitness(previousDistances);
            List<CityInfo>[] selected = Selection(previousPopulation, fitnesses);
            List<CityInfo>[] nextPopulation = Crossover(selected, crossoverSelection);
            Mutation(ref nextPopulation, mutationPossibility);

            float[] nextDistances = new float[4];
            for (int i = 0; i < nextPopulation.Count(); i++)
            {
                nextDistances[i] = DistanceHelper.TotalDistance(nextPopulation[i]);
            }

            Elimination(ref previousDistances, ref nextDistances, ref previousPopulation, ref nextPopulation);
            previousPopulation = nextPopulation;
            previousDistances = nextDistances;
        }
    }
}
