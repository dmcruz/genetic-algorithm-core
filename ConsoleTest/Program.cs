using System;
using GeneticAlgorithm;

namespace ConsoleTest
{
    class Program
    {

        static string targetString = "Fortytwo";
        static string validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ,.|!#$%&/()=?0123456789 ";
        static Random random;
        static GeneticAlgorithm<char> geneticAlgorithm;

        static void Main(string[] args)
        {
            Console.Write("Enter target string: ");
            targetString = Console.ReadLine();

            int populationSize = 200;
            int dnaSize = targetString.Length;
            random = new Random();
            float mutationRate = 0.05f;
            geneticAlgorithm = new GeneticAlgorithm<char>(populationSize, dnaSize, random, getRandomChar, fitnessFunction, mutationRate);


            while (geneticAlgorithm.BestFitness != 1)
            {
                Console.WriteLine("Best Genes {0}, mutation rate {1}, generation: {2}", new string(geneticAlgorithm.BestGenes),
                    geneticAlgorithm.MutationRate,
                    geneticAlgorithm.Generation);
                geneticAlgorithm.NewGeneration();
            }

            Console.WriteLine("Best Genes {0}, mutation rate {1}, generation: {2}", new string(geneticAlgorithm.BestGenes),
                geneticAlgorithm.MutationRate,
                geneticAlgorithm.Generation);

            Console.ReadLine();
        }

        static char getRandomChar()
        {
            int i = random.Next(validCharacters.Length);
            return validCharacters[i];
        }

        static float fitnessFunction(int index)
        {
            float score = 0;
            DNA<char> dna = geneticAlgorithm.Population[index];

            for (int i = 0; i < dna.Genes.Length; i++)
            {
                if (dna.Genes[i] == targetString[i])
                {
                    score += 1;
                }
            }

            score /= targetString.Length;
            score = (MathF.Pow(2, score) - 1) / (2 - 1);
            return score;
        }
    }
}
