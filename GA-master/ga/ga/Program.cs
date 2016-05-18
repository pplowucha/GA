using ga.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ga
{
    // kodowanie rzeczywisto liczbowe
    // wyznaczac dziedzine danego genu
    // chromosom ma tyle genow ile zmiennych decyzyjnych
    // zmienic krzyzowanie 
    // zmienic mutacje

    class Program
    {
        static public int t = 0;
        static void Main(string[] args)
        {
            
            List<int> dlGenow = new List<int> {2,2,2,2 };
            List<int> poczatkiPrzedzialowGenow = new List<int> { 1, 1, 1, 1 };
            Population populacja = new Population(5, dlGenow,poczatkiPrzedzialowGenow, n => n*n, new Random());

            populacja.ShowChromosomes();
            //populacja.Show();
            
            Func<Population, bool> lambda = n => n.Individuals.Select(x => x.Fitness).Sum() > (n.Individuals.Count() * Math.Pow(Math.Pow(2, dlGenow.Count) - 1, 2)/2) ? true : false;
            
            // n => n.Individuals.Select(x => x.Fitness).Sum()  suma fitnesu
            // n=>n.Individuals.Count() * Math.Pow(Math.Pow(2, iloscGenow) - 1,2)  maxymalny fitnes
            
            //Console.WriteLine("Populacja 1:");
            //populacja.ShowChromosomes();

            Console.WriteLine("Populacja 1 po ewolucji:");
            populacja.Evolution(lambda);
            populacja.ShowChromosomes();

            //Console.WriteLine("Populacja 2:");
            //populacja = populacja.Ruletka();
            //populacja.ShowChromosomes();

            
        }
    }
}
