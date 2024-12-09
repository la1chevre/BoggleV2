﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BoggleV2
{
    public class Dictionnaire
    {
        public string[] dictionnaire;
        string langue;


        
        public Dictionnaire(string langue)
        {
            if (langue == "fr")
            {
                string Chemin = "MotsPossiblesFR.txt";
                try
                {
                    this.dictionnaire = File.ReadAllText(Chemin).Split(' ');
                    this.langue = langue;
                }
                catch (FileNotFoundException e) { Console.WriteLine("echec lors de la lecture du fichier" + e.Message); }
                
                
            }
            else if (langue == "en")
            {
                string Chemin = "MotsPossiblesEN.txt";
                try
                {
                    this.dictionnaire = File.ReadAllText(Chemin).Split(' ');
                    this.langue = langue;
                }
                catch (FileNotFoundException e) { Console.WriteLine("echec lors de la lecture du fichier" + e.Message); }
            }
            else
            {
                Console.WriteLine("Langue inconnue");
                this.dictionnaire = new string[0];


            }


        }



        

        /// <summary>
        /// Renvoie les spécificitées du tableau de mots : le nombre de mots par longeur, le nombre de mot commencant par chaque lettre et la langue
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            string res = "Langue : " + this.langue + "\nle nombre de mot avec des longueurs similaires : \n";

            /// le nombre de mot avec des longueurs similaires
            Dictionary<int, int> NbMotLongeur = new Dictionary<int, int>();
            for (int i = 0; i < dictionnaire.Length; i++)
            {
                if (NbMotLongeur.ContainsKey(dictionnaire[i].Length))
                {
                    NbMotLongeur[dictionnaire[i].Length]++;
                }
                else
                {
                    NbMotLongeur.Add(dictionnaire[i].Length, 1);
                }
            }

            foreach (int key in NbMotLongeur.Keys)
            {
                res += "il y a " + NbMotLongeur[key] + " mot(s) de longueur " + key + "\n";
            }

            /// le nombre de mot commencant par chaque lettre

            Dictionary<char, int> NbMotLettre = new Dictionary<char, int>();
            for (int i = 0; i < dictionnaire.Length; i++)
            {
                if (NbMotLettre.ContainsKey(dictionnaire[i][0]))
                {
                    NbMotLettre[dictionnaire[i][0]]++;
                }
                else
                {
                    NbMotLettre.Add(dictionnaire[i][0], 1);
                }
            }
            res += "le nombre de mot commencant par chaque lettre : \n";
            foreach (char key in NbMotLettre.Keys)
            {
                res += "il y a " + NbMotLettre[key] + "commencant par la lettre " + key;
            }

            return res;
        }



        #region tri

        // TRI FUSION

        /// <summary>
        /// Tri fusion d'un tableau de string
        /// </summary>
        /// <param name="dictionnaire"></param>
        /// <param name="debut"></param>
        /// <param name="fin"></param>
        public static string[] TriFusion(string[] dictionnaire,int debut,int fin)
        {
            int milieu = (debut + fin) / 2;
            if (debut < fin)
            {
                TriFusion(dictionnaire,debut, milieu);
                TriFusion(dictionnaire,milieu + 1, fin);
                string[] gauche = CopieTableau(dictionnaire, debut, milieu);
                string[] droit = CopieTableau(dictionnaire, milieu+1, fin);
                string[] fusionne = Fusion(gauche, droit);
                for (int i = 0; i < fusionne.Length; i++)
                {
                    dictionnaire[debut + i] = fusionne[i];
                }

            }
            return dictionnaire;

        }

        static string[] Fusion(string[] gauche, string[] droit)
        {
            string[] t = new string[gauche.Length + droit.Length];

            int indexG = 0, indexD = 0, indexT = 0;


            while (indexG < gauche.Length && indexD < droit.Length)
            {
                if (String.Compare(gauche[indexG],droit[indexD])<=0)
                {
                    t[indexT] = gauche[indexG];
                    indexG++;
                }
                else
                {
                    t[indexT] = droit[indexD];
                    indexD++;
                }
                indexT++;
            }

            while (indexG < gauche.Length)
            {
                t[indexT] = gauche[indexG];
                indexG++;
                indexT++;
            }


            while (indexD < droit.Length)
            {
                t[indexT] = droit[indexD];
                indexD++;
                indexT++;
            }


            return t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Acopier"> tableau a copier</param>
        /// <param name="debut">premier index a partir duquel il faut copié  </param>
        /// <param name="fin">dernier index a copier dans le tableau a colle</param>
        /// <returns>le tableau collé </returns>
        static string[] CopieTableau(string[] Acopier, int debut, int fin)
        {
            string[] Acolle = new string[fin - debut+1];
            for (int i = 0; i <= fin - debut; i++)
            {
                Acolle[i] = Acopier[i + debut];
            }
            return Acolle;
        }


        // TRI QUICKSORT


        // TRI INSERTION

        #endregion
    }
}

