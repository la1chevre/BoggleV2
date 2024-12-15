using System;
using System.Diagnostics;

namespace BoggleV2
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Jeu partie = new Jeu();
            //Temps_ExecutionTri();
            //Temps_ExecutionRecherche();
            //Dictionnaire d = new Dictionnaire("fr");
            //string a = d.toString();
            //Console.WriteLine(a);

        }

        static void Temps_ExecutionTri()
        {
            Dictionnaire d = new Dictionnaire("fr");
            Stopwatch chrono = new Stopwatch();
            chrono.Start();
            Array.Sort(d.dictionnaire);
            chrono.Stop();
            Console.WriteLine("l'algo de tri de c#, a pris "+chrono.ElapsedMilliseconds+"ms pour s'effectuer");
            chrono.Restart();
            string[] testTri = Dictionnaire.TriFusion(d.dictionnaire,0,d.dictionnaire.Length-1);
            chrono.Stop();
            Console.WriteLine("l'algo de tri fusion, a pris " + chrono.ElapsedMilliseconds + "ms pour s'effectuer");
            chrono.Restart();
            testTri = Dictionnaire.TriInsertion(d.dictionnaire);
            chrono.Stop();
            Console.WriteLine("l'algo de tri par insertion, a pris " + chrono.ElapsedMilliseconds + "ms pour s'effectuer");

        }

        static void Temps_ExecutionRecherche()
        {
            Console.WriteLine("mot a rechercher : ");
            string mot = Console.ReadLine();
            Dictionnaire d = new Dictionnaire("fr");
            Stopwatch chrono = new Stopwatch();
            chrono.Start();
            bool res = Dictionnaire.RechercheDichoRecursif(d.dictionnaire, mot, d.dictionnaire.Length - 1, 0);
            chrono.Stop();
            Console.WriteLine("la recherche dichotomique a pris "+chrono.ElapsedMilliseconds+"ms pour s'effectuer");
            chrono.Restart();
            res = Dictionnaire.RechercheClassique(d.dictionnaire, mot);
            chrono.Stop();
            Console.WriteLine("la recherche classique a pris " + chrono.ElapsedMilliseconds + "ms pour s'effectuer");
        }
    }
}
