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


        /// <summary>
        /// Il faut retirer le tri du constructeur Dictionnaire pour tester
        /// </summary>
        static void Temps_ExecutionTri()
        {

            Stopwatch chrono = new Stopwatch();


            Dictionnaire d1 = new Dictionnaire("fr");
            chrono.Start();
            Array.Sort(d1.dictionnaire);
            chrono.Stop();
            Console.WriteLine("l'algo de tri de c#, a pris "+chrono.ElapsedMilliseconds+"ms pour s'effectuer");

            
            Dictionnaire d2 = new Dictionnaire("fr");
            chrono.Restart();
            string[] testTri2 = Dictionnaire.TriFusion(d2.dictionnaire,0,d2.dictionnaire.Length-1);
            chrono.Stop();
            
            Console.WriteLine("l'algo de tri fusion, a pris " + chrono.ElapsedMilliseconds + "ms pour s'effectuer");

            
            Dictionnaire d3 = new Dictionnaire("fr");
            chrono.Restart();
            string[] testTri3 = Dictionnaire.TriInsertion(d3.dictionnaire);
            chrono.Stop();
            //Console.WriteLine(String.Join(';',testTri3));
            Console.WriteLine("l'algo de tri par insertion, a pris " + chrono.ElapsedMilliseconds + "ms pour s'effectuer");

        }

        static void Temps_ExecutionRecherche()
        {
            Console.WriteLine("mot a rechercher : ");
            string mot = Console.ReadLine();
            Stopwatch chrono = new Stopwatch();

            Dictionnaire d1 = new Dictionnaire("fr");
            chrono.Start();
            bool res = Dictionnaire.RechercheDichoRecursif(d1.dictionnaire, mot, 0, d1.dictionnaire.Length - 1);
            Console.WriteLine(res);
            chrono.Stop();
            Console.WriteLine("la recherche dichotomique a pris "+chrono.ElapsedMilliseconds+"ms pour s'effectuer");


            Dictionnaire d2 = new Dictionnaire("fr");
            chrono.Restart();
            bool res2 = Dictionnaire.RechercheClassique(d2.dictionnaire, mot);
            Console.WriteLine(res2);
            chrono.Stop();
            Console.WriteLine("la recherche classique a pris " + chrono.ElapsedMilliseconds + "ms pour s'effectuer");
        }
    }
}
