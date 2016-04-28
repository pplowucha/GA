using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ga.model
{
    public interface ICrossoverOperator
    {
        void Crossover(Chromosome parent1, Chromosome parent2);
    }

    public interface IMutatationOperator
    {
        void Mutate(Chromosome chromosome);
    }

    public class UniformMutation : IMutatationOperator
    {
        private readonly Random _random;
        private readonly double _mutationProbability;

        public UniformMutation(Random random, double mutationProbability)
        {
            _random = random;
            _mutationProbability = mutationProbability;
        }

        public void Mutate(Chromosome chromosome)
        {
            for (int i = 0; i < chromosome.Genes.Count; i++)
            {
                if (_random.NextDouble() <= _mutationProbability)
                {
                    chromosome.Genes[i] = !chromosome.Genes[i];
                }
            }
        }
    }

    public class OnePointCrossover : ICrossoverOperator
    {
        private readonly Random _random;

        public OnePointCrossover(Random random)
        {
            _random = random;
        }

        public void Crossover(Chromosome parent1, Chromosome parent2)
        {
            int tmp = _random.Next(1, parent2.Genes.Count-1);
            Console.WriteLine($"Random = {tmp}");
            for (int i = tmp; i < parent2.Genes.Count; i++)
            {
                Console.WriteLine($"{parent1.Genes[i]} na {parent2.Genes[i]}");
                parent1.Genes[i] = parent2.Genes[i];
            }
        }
    }

    public class Chromosome
    {
        public List<bool> Genes { get; set; }

        public double Fenotype
        {
            get
            {
                return CalculateFenotype();
            }
        }

        public Chromosome(int chromosomeLength, Random random)
        {
            Genes = Enumerable.Range(0, chromosomeLength)
                .Select(x => random.NextDouble() <= 0.5)
                .ToList();
        }

        private double CalculateFenotype()
        {
            double wynik = 0;
            for (int i = Genes.Count-1, j = 0; i >= 0; i--, j++)
            {
                wynik += Convert.ToDouble(Genes[i]) * Math.Pow(2, j);
            }
            Console.WriteLine($"x: {wynik}");

            return wynik;
        }
    }
}
