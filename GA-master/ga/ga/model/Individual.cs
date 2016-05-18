using ga.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ga.model
{
    public class Individual
    {
        public double W { get; set; }
        public double D { get; set; }
        public double Fitness { get; set; }
        public Chromosome Chromosome { get; set; }

        public Individual(List<int> dlGenow,List<int> poczatkiPrzedzialow, Random random)
        {
            Chromosome = new Chromosome(dlGenow,poczatkiPrzedzialow, random);
        }
        public Individual(Individual tmp)
        {
            W = tmp.W;
            D = tmp.D;
            Fitness = tmp.Fitness;
            Chromosome = tmp.Chromosome;
        }
    }
}
