using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoggleV2
{
    public class Joueur
    {
        string nom;
        int score;
        string[] mots;

        public string Nom { get { return this.nom; } }
        public int Score { get { return this.score; } }
        public string[] Mots { get { return this.mots; } }

        public Joueur(string nom)
        {
            if (nom == null)
            {
                Console.WriteLine("Le nom du joueur ne peut pas être null");
            }
            else
            {
                this.nom = nom;
                this.score = 0;
                this.mots = new string[0];
            }
        }


        /// <summary>
        /// Verifie que le mot passé en paramètre est dans la liste des mots du joueur
        /// </summary>
        /// <param name="mot">Le mot à vérifier</param>
        /// <returns>bool>true si le mot a déja été trouvé et false sinon</returns>
        public bool Contain(string mot)
        {
            bool res = false;
            for (int i = 0; i < mots.Length; i++)
            {
                if (mots[i] == mot)
                {
                    res = true;
                }
               
            }
            return res;
            
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


        /// <summary>
        /// Ajoute au joueur le score lié au mot donné en paramètres
        /// </summary>
        /// <param name="mot"> Le mot à partir duquel le score est calculé</param>
        public void Add_Score(string mot)
        {
            mot = mot.ToUpper();

            Dictionary<char, int> PoidLettre = new Dictionary<char, int>();
            string Chemin = "PoidsLettres.txt";
            string[] lignes = File.ReadAllLines(Chemin);

            foreach (string ligne in lignes)
            {
                string[] partie = ligne.Split(';');
                char key = partie[0][0];// la premiere partie est la lettre
                PoidLettre[key] = int.Parse(partie[1]);// ajoute le score de la lettre
            }


            for (int i = 0; i < this.mots.Length; i++)
            {
                this.score += PoidLettre[mot[i]];
            }
        }
    }
}
