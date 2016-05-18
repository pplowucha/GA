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
        public double dellta(double t, double y)
        {
            int maxLiczbaGeneracji=0;
                int wspC = 5;
            return y * (1 - Math.Pow(_random.Next(0, 1), (1 - t / maxLiczbaGeneracji) * wspC));
        }
        public void Mutate(Chromosome chromosome)
        {
            for (int i = 0; i < chromosome.Genes.Count; i++)
            {
                
                 // nr pokolenia


                int rand = _random.Next(0, 1);
                if (rand == 0)
                {
                    chromosome.Genes[i] = chromosome.Genes[i] + dellta(Program.t, (chromosome.DlGenow[i]+chromosome.PoczatkiPrzedzialowGenow[i]) - chromosome.Genes[i] );
                    // t++ ? po skonczeniu populacji trzeba zwiekszyc t , niewiem gdzie :(
                }
                else
                {
                    chromosome.Genes[i] = chromosome.Genes[i] + dellta(Program.t, chromosome.Genes[i] - chromosome.PoczatkiPrzedzialowGenow[i]);
                }
            }
        }
    }

    public class MultiPoinCrossOver : ICrossoverOperator
    {
        private readonly Random _random;

        public MultiPoinCrossOver(Random random)
        {
            _random = random;
        }

        public void Crossover(Chromosome parent1, Chromosome parent2)
        {
            int tmp = _random.Next(0, parent2.Genes.Count);
            for (int i = tmp; i < parent2.Genes.Count; i++) 
            {
                double a = _random.NextDouble();
                var p1 = parent1.Genes[i];
                var p2 = parent2.Genes[i];

                parent1.Genes[i] = a + ((a * p2) + ((1 - a) * p1 )) / 2; 
            }
        }
    }

    public class Chromosome
    {
        public List<double> Genes { get; set; }
        public List<int> DlGenow { get; set; }
        public List<int> PoczatkiPrzedzialowGenow { get; set; }

        public double Fenotype
        {
            get
            {
                return CalculateFenotype();
            }
        }

        public Chromosome(List<int> dlGenow,List<int> poczatkiPrzedzialowGenow, Random random)
        {
            Genes = new List<double>();
            for (int i = 0; i < dlGenow.Count; i++)
            {
                double tmp = poczatkiPrzedzialowGenow[i] + (random.NextDouble() + (random.Next(dlGenow[i]) - 1));
                Genes.Add(tmp);
            }
        }

        private double CalculateFenotype()
        {
            double wynik = 0;
            for (int i = Genes.Count-1, j = 0; i >= 0; i--, j++)
            {
                wynik += Convert.ToDouble(Genes[i]);
                //Console.Write(Convert.ToDouble(Genes[i])+",");
            }
            //Console.WriteLine($"    x: {wynik}");
            
            return wynik;
        }
    }
}
