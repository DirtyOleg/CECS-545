using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_04
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
            childOne = new List<CityInfo>();
            childTwo = new List<CityInfo>();

            int cutPointOne, cutPointTwo;
            cutPointOne = (int)parentOne.Count / 3;
            cutPointTwo = (int)2 * parentOne.Count / 3;

            List<CityInfo> centerOne = parentOne.GetRange(cutPointOne, cutPointTwo - cutPointOne);
            List<CityInfo> centerTwo = parentTwo.GetRange(cutPointOne, cutPointTwo - cutPointOne);


        }

        static void Mutation()
        { }

        public static void Mate(ref List<CityInfo>[] population, ref float[] distances, int crossoverSelection, float mutationPossibility)
        {
            float[] fitnesses = Fitness(distances);
            List<CityInfo>[] selected = Selection(population, fitnesses);
            List<CityInfo>[] childGeneration = Crossover(selected, crossoverSelection);
        }
    }
}
