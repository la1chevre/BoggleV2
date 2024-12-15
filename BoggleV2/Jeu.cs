using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoggleV2
{
    internal class Jeu
    {
        private Joueur joueur1;
        private Joueur joueur2;
        private Plateau plateau;
        private Dictionnaire dico;
        private int dureeRound;

        public Jeu()
        {
            Console.WriteLine("+--------------------------------------------------------+");
            Console.WriteLine("|                         BOGGLE                         |");
            Console.WriteLine("+--------------------------------------------------------+");
            Console.WriteLine("\npar Grégoire LEGROS et Maximilien BERNARD\n\n");

            Console.WriteLine("Voulez vous jouer seul ou à deux ?");

            Console.WriteLine("\t tapez \"Solo\" pour jouer seul et \"Duo\" pour jouer à deux.");
            string Mode = Console.ReadLine();
            Mode = Mode.ToUpper();
            while (Mode != "SOLO" && Mode != "DUO")
            {
                Console.WriteLine("Ce mode n'est pas compris.");
                Mode = Console.ReadLine();
                Mode = Mode.ToUpper();
            }

            if (Mode == "SOLO") Solo();
            else Duo();


        }
        public void Solo()
        {
            joueur1 = new Joueur("Joueur");
            joueur2 = new Joueur("IA");
            CreationDico();
            CreationPlateau();
            DemandeTemps();

            Console.WriteLine("Vous pouvez commencer.");

            joueur1 = Round(joueur1);

            Console.WriteLine("Fin. \n Votre score est désormais de " + joueur1.Score + ".");

            Console.WriteLine("\nL'IA troue les mots :");
            for (int i = 0; i < dico.dictionnaire.Length; i++)
            {
                if (plateau.Test_Plateau(dico.dictionnaire[i], dico))
                {
                    Console.WriteLine(dico.dictionnaire[i]);
                    joueur2.Add_Score(dico.dictionnaire[i]);
                }
            }

            Console.WriteLine("Fin. \n Son score est désormais de " + joueur2.Score + ".");



        }

        public void Duo()
        {
            Console.WriteLine("Quel est le nom du premier joueur ?");
            string nom = Console.ReadLine();
            joueur1 = new Joueur(nom);

            Console.WriteLine("Quel est le nom du second joueur ?");
            nom = Console.ReadLine();
            joueur2 = new Joueur(nom);

            Console.WriteLine("\tLe premier joueur est " + joueur1.Nom + " et le second joueur est " + joueur2.Nom + ".");

            CreationDico();
            CreationPlateau();
            DemandeTemps();

            int nombreDeRounds = DemandeRound();

            for (int i = 0; i < nombreDeRounds; i++)
            {
                if (i != 0) { plateau.Melange(); }
                

                Console.WriteLine("\n\tLe tours " + (i + 1) + " va commencer.");
                Console.WriteLine(joueur1.Nom + " peut commencer.");

                joueur1 = Round(joueur1);

                Console.WriteLine("Fin. \n Votre score est désormais de " + joueur1.Score + ".");



                Console.WriteLine("\n" + joueur2.Nom + " peut commencer.");

                joueur2 = Round(joueur2);

                Console.WriteLine("Fin. \n Votre score est désormais de " + joueur2.Score + ".");


            }



            // Traitement fin de partie
            if (joueur1.Score > joueur2.Score)
            {
                Console.WriteLine("\n\n\t" + joueur1.Nom + " à gagner !!!\n\tLe score est de " + joueur1.Score + " à " + joueur2.Score + ".");
            }
            else if (joueur2.Score > joueur1.Score)
            {

                Console.WriteLine("\n\n\t" + joueur2.Nom + " à gagner !!!\n\tLe score est de " + joueur2.Score + " à " + joueur1.Score + ".");
            }
            else
            {
                Console.WriteLine("\n\n\tIl y a égalité !!! Les deux joueurs ont un score de " + joueur1.Score + ".");
            }
        }


        public Joueur Round(Joueur joueur)
        {

            Console.WriteLine("\n" + plateau.toString() + "\n");
            Console.WriteLine("Veuillez entrer vos mots.");

            List<string> mots = new List<string>();

            TimeSpan duree = TimeSpan.FromSeconds(this.dureeRound);
            DateTime debut = DateTime.Now;

            while (DateTime.Now - debut < duree)
            {
                bool ok = false;
                string mot = Console.ReadLine();


                for (int i = 0; i < mots.Count; i++)
                {
                    if (mots[i] == mot)
                    {
                        Console.WriteLine("Ce mot à déja été trouvé !");
                        ok = true;
                    }
                }


                if (plateau.Test_Plateau(mot, dico) && !ok)
                {
                    mots.Add(mot);
                    joueur.Add_Mot(mot);

                    joueur.Add_Score(mot);

                    ok = true;
                }


                if (!ok)
                {
                    Console.WriteLine("Ce mot ne semble pas exister ou n'est pas présent sur la grille.");
                }
            }



            return joueur;
        }

        public void CreationDico(bool phrase = true)
        {
            if (phrase)
            {
                Console.WriteLine("\nDans quelle langue voulez vous jouer ?");
                phrase = false;
                Console.WriteLine("\tTaper \"fr\" pour jouer en français et \"en\" pour jouer en anglais.");
            }

            string langue = Console.ReadLine();

            switch (langue)
            {
                case "fr":
                    dico = new Dictionnaire("fr");
                    Console.WriteLine("\tLe français a été définis comme langue.");
                    break;
                case "en":
                    dico = new Dictionnaire("en");
                    Console.WriteLine("\tL'anglais a été définis comme langue.");
                    break;
                default:
                    Console.WriteLine("\tCette langue n'est pas reconnue.");
                    CreationDico(phrase);
                    break;
            }
        }

        public void CreationPlateau()
        {
            Console.WriteLine("\nQuelle taille de plateau voulez-vous ?");
            int n = 0;
            while (n < 3 || n > 10)
            {
                n = Convert.ToInt32(Console.ReadLine());
                if (n < 3) Console.WriteLine("\tLa taille est trop petite (3 ou plus), veuillez donner une taille valide.");

                if (n > 10) Console.WriteLine("\tLa taille est trop grande (10 ou moins), veuillez donner une taille valide.");
            }
            Console.WriteLine("\tLa taille définie est " + n + ".");

            plateau = new Plateau(n);

        }

        public void DemandeTemps()
        {
            Console.WriteLine("\nCombien de temps doit durer un tour ? \n\tLe temps est en secondes.");
            int n = 0;
            while (n < 5)
            {
                n = Convert.ToInt32(Console.ReadLine());
                if (n < 5) Console.WriteLine("\tIl n'y a pas assez de temps (5s ou plus), veuillez donner un temps valide.");
            }
            Console.WriteLine("\tLe temps défini est " + n + "s.");
            dureeRound = n;
        }

        public int DemandeRound()
        {
            Console.WriteLine("\nCombien de tours doit durer la partie ? \n\tUn tour est joué par chacun des joueurs.");
            int n = 0;
            while (n < 1)
            {
                n = Convert.ToInt32(Console.ReadLine());
                if (n < 1) Console.WriteLine("\tIl n'y a pas assez de tours (1 ou plus), veuillez donner un nombre valide.");
            }
            Console.WriteLine("\tLe temps défini est " + n + ".");
            return n;
        }



    }
}
