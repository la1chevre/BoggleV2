using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoggleV2
{
    internal class Joueur
    {
        string nom;
        int score;
        string[] mots;

        public string Nom { get { return this.nom; } }
        public int Score { get { return this.score; } }
        public string[] Mots { get { return this.mots; } }

        public Joueur(string nom, int score, string[] mots)
        {
            if (nom == null)
            {
                Console.WriteLine("Le nom du joueur ne peut pas être null");
            }
            else
            {
                this.nom = nom;
                this.score = score;
                this.mots = mots;
            }
        }


        /// <summary>
        /// Verifie que le mot passé en paramètre est dans la liste des mots du joueur
        /// </summary>
        /// <param name="mot">Le mot à vérifier</param>
        /// <returns>bool>true si le mot a déja été trouvé et false sinon</returns>
        public bool Contain(string mot)
        {
            for (int i = 0; i < mots.Length; i++)
            {
                if (mots[i] == mot)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        ///  ajoute le mot dans la liste des mots déjà trouvés par le joueur au cours de la partie en modifiant le nombre d’occurrences si nécessaire
        /// </summary>
        /// <param name="mot">Le mot à ajouter</param>
        /// <returns></returns>

        public void Add_Mot(string mot)
        {
            string[] new_mots = new string[mots.Length + 1];
            new_mots[mots.Length] = mot;
            this.mots = new_mots;

        }

        public void Add_Score(int score)
        {
            this.score += score;
        }

        /// <summary>
        /// retourne une chaîne de caractères qui décrit unjoueur. 
        /// </summary>
        /// <returns>name="res">chaine composé Nom score et mots trouvés</returns>

        public string toString()
        {
            string res = "Nom : " + this.nom + "\nScore : " + this.score + "\nMots trouvés : ";
            for (int i = 0; i < mots.Length; i++)
            {
                res += mots[i] + "\n";
            }
            return res;
        }
    }
}
