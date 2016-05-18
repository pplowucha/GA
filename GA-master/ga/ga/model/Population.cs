using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ga.model
{
    class Population
    {
        public List<Individual> Individuals { get; set; }
        Random R = new Random();
        private Func<double, double> _fitnessFunction;
        private Func<Population, bool> _funkcjaCelu;

        public double s { get; set; }

        public Population(int numberOfIndividuals, List<int> dlGenow,List<int> poczatkiPrzedzialow, Func<double, double> fitnessFunction, 
            Random random)
        {
            Individuals = Enumerable.Range(0, numberOfIndividuals)
                .Select(x => new Individual(dlGenow,poczatkiPrzedzialow, random))
                .ToList();
            _fitnessFunction = fitnessFunction;

            UpdateFitness();
        }


        public void Evolution(Func<Population,bool> funkcjaCelu)
        {
            MultiPoinCrossOver krzyzowanie = new MultiPoinCrossOver(R);
            UniformMutation mutacje = new UniformMutation(R, 0.2);

            //while(!funkcjaCelu(this))
            //{

                for (int j = 1; j < Individuals.Count; j++)
                {
                    krzyzowanie.Crossover(Individuals[j - 1].Chromosome, Individuals[j].Chromosome);
                }
            foreach (var item in Individuals)
            {
                mutacje.Mutate(item.Chromosome);
            }
            Program.t++;
            UpdateFitness();
            //}            
        }
        public void Show()
        {
            foreach (var item in Individuals)
            {
                Console.WriteLine($"X:{item.Chromosome.Fenotype} D:{Math.Round(item.D,5)} W:{Math.Round(item.W, 5)}");
            }
        }
        public void ShowChromosomes()
        {
            foreach (var item in Individuals)
            {
                foreach (var oneByte in item.Chromosome.Genes)
                {
                    Console.Write($"[{Math.Round(oneByte,3)}]");
                }
                Console.WriteLine();
            }
        }
        public Population()
        {
            Individuals = new List<Individual>();
        }
        public Population Ruletka()
        {
            UpdateSum();
            UpdateW();
            UpdateD();
            
            Population nowa = new Population();
            Individual tmp;
            double los;

            foreach (var item in Individuals)
            {
                los = R.NextDouble();
                tmp = znajdzOsobe(los);
                //Console.WriteLine($"LOS: {los} a wylosowany koles to {tmp.D}");
                nowa.Individuals.Add(tmp);
            }

            return nowa;
            
        }
        Individual znajdzOsobe(double los)
        {
            for (int i = 0; i < Individuals.Count; i++)
            {
                if (Individuals[i].D > los) return Individuals[i];
            }
            Console.WriteLine("KURDE PRZYPAU");
            return null;
        }
        public void UpdateFitness()
        {
            foreach (var individual in Individuals)
            {
                individual.Fitness = _fitnessFunction(individual.Chromosome.Fenotype);
            }
        }
        public void UpdateSum()
        {
            s = 0;
            foreach (var item in Individuals)
            {
                s += item.Fitness;
            }
        }
        public void UpdateW()
        {
            foreach (var item in Individuals)
            {
                item.W = item.Fitness / s;
                //Console.WriteLine(item.W);
            }
        }
        public void UpdateD()
        {
            //Console.WriteLine("TUTA");
            Individuals[0].D = Individuals[0].W;
            for (int i = 1; i < Individuals.Count; i++)
            {
                Individuals[i].D = Individuals[i - 1].D + Individuals[i].W;
                //Console.WriteLine(Individuals[i].D);
            }
        }
    }
}
