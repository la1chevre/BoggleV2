using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoggleV2
{
    internal class Dictionnaire
    {
        public string[] dictionnaire;
        string langue;

        public Dictionnaire(string langue)
        {
            if (langue == "fr")
            {
                string Chemin = "MotsPossiblesFR.txt";
                this.dictionnaire = File.ReadAllText(Chemin).Split(' ');
                this.langue = langue;
            }
            else if (langue == "en")
            {
                string Chemin = "MotsPossiblesEN.txt";
                this.dictionnaire = File.ReadAllText(Chemin).Split(' ');
                this.langue = langue;
            }
            else
            {
                Console.WriteLine("Langue inconnue");
                this.dictionnaire = new string[0];


            }


        }

        public bool RechDichoRecursif(string mot)
        {
            for(int i = 0; i < dictionnaire.Length; i++)
            {
                if (dictionnaire[i].ToUpper() == mot.ToUpper()) return true;
            }
            return false;
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
    }
}
