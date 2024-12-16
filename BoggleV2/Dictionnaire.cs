using System;
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
        public string langue;


        
        public Dictionnaire(string langue)
        {
            if (langue == "fr")
            {
                string Chemin = "MotsPossiblesFR.txt";
                try
                {
                    this.dictionnaire = File.ReadAllText(Chemin).Split(' ');
                    dictionnaire = TriFusion(dictionnaire, 0, dictionnaire.Length - 1);
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
                    dictionnaire = TriFusion(dictionnaire, 0, dictionnaire.Length - 1);
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
            string res = "Langue : " + this.langue + "\n\nle nombre de mot avec des longueurs similaires : \n";

            // le nombre de mot avec des longueurs similaires
            
            Dictionary<int, int> nbMotLongeur = new Dictionary<int, int>();
            for (int i = 1; i < dictionnaire.Length; i++)
            {
                if (nbMotLongeur.ContainsKey(dictionnaire[i].Length))
                {
                    nbMotLongeur[dictionnaire[i].Length]++;
                }
                else
                {
                    nbMotLongeur.Add(dictionnaire[i].Length, 1);
                }
            }

            foreach (int key in nbMotLongeur.Keys)
            {
                res += "il y a " + nbMotLongeur[key] + " mot(s) de longueur " + key + "\n";
            }

            // le nombre de mot commencant par chaque lettre

            Dictionary<char, int> nbMotLettre = new Dictionary<char, int>();
            for (int i = 1; i < dictionnaire.Length; i++)
            {
                if (dictionnaire[i] != null)
                {
                    if (nbMotLettre.ContainsKey(dictionnaire[i][0]))
                    {
                        nbMotLettre[dictionnaire[i][0]]++;
                    }
                    else
                    {
                        nbMotLettre.Add(dictionnaire[i][0], 1);
                    }
                }
                
            }
            res += "\nle nombre de mot commencant par chaque lettre : \n";
            foreach (char key in nbMotLettre.Keys)
            {
                res += "il y a " + nbMotLettre[key] + " commencant par la lettre " + key+"\n";
            }

            return res;
        }


        /// <summary>
        /// Recherche d'une valeur dans un tableau trié
        /// </summary>
        /// <param name="t"> le tableau doit être trié</param>
        /// <param name="val">valeur a trouvé</param>
        /// <param name="fin">indice fin de recherche</param>
        /// <param name="debut">indice debut de recherche</param>
        /// <returns>true si le tableau contient la valeur</returns>
        public static bool RechercheDichoRecursif(string[] t, string val, int debut, int fin)
        {
            val = val.ToUpper();
            int milieu = (debut + fin) / 2;
            if (debut > fin || milieu < 0 || milieu >= t.Length)
            {
                return false;
            }
            if (String.Compare(t[milieu], val) > 0)
            {
                return RechercheDichoRecursif(t, val, debut, milieu-1);
            }

            else if (String.Compare(t[milieu], val) < 0)
            {
                return RechercheDichoRecursif(t, val, milieu+1, fin);
            }
            else
            {
                return true;
            }
        }

        public static bool RechercheClassique(string[] t , string val)
        {
            bool res = false;
            val = val.ToUpper();
            for(int i = 0; i < t.Length; i++) 
            {
                if (t[i] == val)
                {
                    res = true;
                }
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



        // TRI INSERTION


        public static string[] TriInsertion(string[] dictionnaire)
        {
            for (int i = 1; i < dictionnaire.Length; i++)
            {
                string mot = dictionnaire[i];
                int j = i - 1;
                while (j >= 0 && String.CompareOrdinal(dictionnaire[j], mot) > 0) 
                    {
                    dictionnaire[j + 1] = dictionnaire[j];
                    j--;
                }

                dictionnaire[j + 1] = mot;
            }
            return dictionnaire;
        }

        #endregion
    }
}
