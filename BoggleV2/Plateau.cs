using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoggleV2
{
    public class Plateau
    {
        De[,] gameBoard;

        public De[,] GameBoard
        {
            get { return gameBoard; }
        }


        public Plateau(int taille)
        {
            bool VerifPlateau = true;
            this.gameBoard = new De[taille, taille];
            Dictionary<char, int> ConditionLettre = this.DicoConditionLettre(taille);
            
                
            for (int i = 0; i < taille; i++)
            {
                for (int j = 0; j < taille; j++)
                {
                    gameBoard[i, j] = new De();
                    gameBoard[i, j].lance();
                    ConditionLettre[gameBoard[i, j].face]--;
                    while (ConditionLettre[gameBoard[i, j].face] < 0)
                    {
                        gameBoard[i, j].lance();
                    }
                }
            }

            
        }

        public string toString()
        {
            string txt = "\t";
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(0); j++)
                {
                    txt += gameBoard[i, j].face + " ";
                }
                txt += "\n\t";
            }
            return txt;
        }

        public bool Test_Plateau(string mot, Dictionnaire Dico)
        {
            mot = mot.ToUpper();
            bool TestBon = false;
            // pour chaque fois que la case = première lettre on fait le test d'adjacence de toute les lettres
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(0); j++)
                {
                    if (Test_AdjacenceRec(mot, i, j, new int[0][])) TestBon = true;
                }
            }
            return TestBon &&  Dictionnaire.RechercheDichoRecursif(Dico.dictionnaire,mot,0,Dico.dictionnaire.Length-1);

        }

        /// <summary>
        /// Test si le mot existe sur le plateau avec (x,y) comme coordonnées de la première lettre du mot
        /// Fonctionne de manière récursive
        /// </summary>
        /// <param name="mot">Le mot à tester</param>
        /// <param name="x">la coordonée de la première lettre du mot selon x</param>
        /// <param name="y">la coordonée de la première lettre du mot selon y</param>
        /// <param name="PositionsParcourues">les positions des lettres utilisées du plateau, doit être de taille 0 lors du premier appel</param>
        /// <returns></returns>
        public bool Test_AdjacenceRec(string mot, int x, int y, int[][] PositionsParcourues)
        {
            // Test si les coordonnées sont dans le plateau
            if (x < 0 || x >= gameBoard.GetLength(0) || y < 0 || y >= gameBoard.GetLength(0)) return false;

            // Crans d'arrêts
            if (mot.Length == 0) return true;
            if (GameBoard[x, y].face != mot[0]) return false;

            // Test si la lettre en (x,y) à deja été utilisé dans le mot
            for (int i = 0; i < PositionsParcourues.Length; i++)
            {
                if (PositionsParcourues[i][0] == x && PositionsParcourues[i][1] == y) return false;
            }

            // ajoute la position actuelle à la liste des positions utilisée
            PositionsParcourues = Append(PositionsParcourues, new int[] { x, y });

            // if (mot.Length == 1 && GameBoard[x, y].face == mot[0]) return true;

            // Regarde si la suite du mot est possible dans les cases avoisinante
            return(Test_AdjacenceRec(mot.Substring(1, mot.Length - 1), x + 1, y    , PositionsParcourues)
                || Test_AdjacenceRec(mot.Substring(1, mot.Length - 1), x - 1, y    , PositionsParcourues)
                || Test_AdjacenceRec(mot.Substring(1, mot.Length - 1), x    , y + 1, PositionsParcourues)
                || Test_AdjacenceRec(mot.Substring(1, mot.Length - 1), x    , y - 1, PositionsParcourues)
                || Test_AdjacenceRec(mot.Substring(1, mot.Length - 1), x + 1, y + 1, PositionsParcourues)
                || Test_AdjacenceRec(mot.Substring(1, mot.Length - 1), x - 1, y - 1, PositionsParcourues)
                || Test_AdjacenceRec(mot.Substring(1, mot.Length - 1), x + 1, y - 1, PositionsParcourues)
                || Test_AdjacenceRec(mot.Substring(1, mot.Length - 1), x - 1, y + 1, PositionsParcourues)
                );

        }
        /// <summary>
        /// Ajoute une valeur à la fin d'un tableau
        /// </summary>
        /// <param name="tab">le tableau à laquelle on ajoute la valeur</param>
        /// <param name="val">la valeur à ajouter</param>
        /// <returns></returns>
        static int[][] Append(int[][] tab, int[] val)
        {
            int[][] result = new int[tab.Length + 1][];
            for (int i = 0; i < tab.Length; i++)
            {
                result[i] = tab[i];
            }
            result[tab.Length] = val;
            return result;
        }

        /// <summary>
        /// Rempli un dictionnaire avec comme clé une lettre et comme valeur son nombre d'occurences max dans le plateau
        /// </summary>
        /// <returns></returns>
        public Dictionary<char, int> DicoConditionLettre(int taille)
        {
            Dictionary<char, int> ConditionLettre = new Dictionary<char, int>();
            string Chemin = "ConditionLettres.txt";
            try
            {
                string[] lignes = File.ReadAllLines(Chemin);
                foreach (string ligne in lignes)
                {
                    
                    string[] partie = ligne.Split(';');
                    char key = partie[0][0];// la premiere partie est la lettre
                    ConditionLettre[key] = int.Parse(partie[1]);// ajoute la valeur corrspondant au nombre max d'occurence de la lettre dans le plateau

                }
            }
            catch(FileNotFoundException e) { Console.WriteLine("fichier introuvable" + e.Message); }
            
            return ConditionLettre;



        }

        /// <summary>
        /// Melange le plateau pour en créer un nouveau à partir des dés déjà existants.
        /// </summary>
        public void Melange()
        {
            List<De> Des = new List<De>();
            for (int i = 0; i < this.gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < this.gameBoard.GetLength(1); j++)
                {
                    Des.Add(this.gameBoard[i, j]);
                }
            }
            Random random = new Random();
            for (int i = 0; i < this.gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < this.gameBoard.GetLength(1); j++)
                {
                    int n = random.Next(0, 25 - i);
                    this.gameBoard[i, j] = Des[n];
                    Des.RemoveAt(n);
                    this.gameBoard[i, j].lance();
                }
            }
        }
    }
}
