using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace BoggleV2
{
    internal class Jeu
    {
        private Joueur joueur1;
        private Joueur joueur2;
        private Plateau plateau;
        private Dictionnaire dico;
        private int dureeRound;
        private int taille;

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
                    joueur2.Add_Mot(dico.dictionnaire[i]);
                    joueur2.Add_Score(dico.dictionnaire[i]);
                }
            }

            Console.WriteLine("Fin. \n Son score est désormais de " + joueur2.Score + ".");



            // Traitement fin de partie
            if (joueur1.Score > joueur2.Score)
            {
                Console.WriteLine("\n\n\tVous avez gagné !!!\n\tVotre score est de " + joueur1.Score + " et l'IA de " + joueur2.Score + ".");
            }
            else if (joueur2.Score > joueur1.Score)
            {

                Console.WriteLine("\n\n\tVous avez perdu !!!\n\tVotre score est de " + joueur1.Score + " et l'IA de " + joueur2.Score + ".\n\tNe vous en faites pas l'IA est imbatable.");
            }
            else
            {
                Console.WriteLine("\n\n\tIl y a égalité !!! Vous et l'IA avez un score de " + joueur1.Score + ". Vous pouvez vous arrêter là l'IA est imbattable.");
            }

            nuagedeMots();

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
                if (i != 0) { plateau.Melange(this.taille); }


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

            nuagedeMots();
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
            string rep = "";
            int n = 0;
            bool ok = true;
            while (n < 3 || n > 10)
            {
                ok = true;
                rep = Console.ReadLine();
                try
                {
                    n = Convert.ToInt32(rep);
                }
                catch
                {
                    Console.WriteLine("\tVeuillez entrer un nombre.");
                    ok = false;
                }

                if (n < 3 && ok) Console.WriteLine("\tLa taille est trop petite (3 ou plus), veuillez donner une taille valide.");

                if (n > 10 && ok) Console.WriteLine("\tLa taille est trop grande (10 ou moins), veuillez donner une taille valide.");
            }
            Console.WriteLine("\tLa taille définie est " + n + ".");

            plateau = new Plateau(n);
            this.taille = n;

        }

        public void DemandeTemps()
        {
            Console.WriteLine("\nCombien de temps doit durer un tour ? \n\tLe temps est en secondes.");
            string rep = "";
            int n = 0;
            bool ok = true;
            while (n < 5)
            {
                ok = true;
                rep = Console.ReadLine();
                try
                {
                    n = Convert.ToInt32(rep);
                }
                catch
                {
                    Console.WriteLine("\tVeuillez entrer un nombre.");
                    ok = false;
                }
                if (n < 5 && ok) Console.WriteLine("\tIl n'y a pas assez de temps (5s ou plus), veuillez donner un temps valide.");
            }
            Console.WriteLine("\tLe temps défini est " + n + "s.");
            dureeRound = n;
        }

        public int DemandeRound()
        {
            Console.WriteLine("\nCombien de tours doit durer la partie ? \n\tUn tour est joué par chacun des joueurs.");
            string rep = "";
            int n = 0;
            bool ok = true;
            while (n < 1)
            {
                ok = true;
                rep = Console.ReadLine();
                try
                {
                    n = Convert.ToInt32(rep);
                }
                catch
                {
                    Console.WriteLine("\tVeuillez entrer un nombre.");
                    ok = false;
                }
                if (n < 1 && ok) Console.WriteLine("\tIl n'y a pas assez de tours (1 ou plus), veuillez donner un nombre valide.");
            }
            Console.WriteLine("\tLe temps défini est " + n + ".");
            return n;
        }

        public void nuagedeMots()
        {
            List<string> mots = new List<string>();
            for (int i = 0; i < joueur1.Mots.Length; i++)
            {
                if (joueur1.Mots[i] != null) mots.Add(joueur1.Mots[i].ToUpper());
            }
            for (int i = 0; i < joueur2.Mots.Length; i++)
            {
                if (joueur2.Mots[i] != null && !mots.Contains(joueur2.Mots[i].ToUpper())) mots.Add(joueur2.Mots[i].ToUpper()); Console.WriteLine(joueur2.Mots[i]);
            }


            Dictionary<string, int> MotsScores = new Dictionary<string, int>();
            foreach (string mot in mots)
            {
                MotsScores[mot] = joueur1.CalculScore(mot);
            }

            int width = 800;
            int height = 600;


            using (SKBitmap bitmap = new SKBitmap(width, height))
            using (SKCanvas canvas = new SKCanvas(bitmap))
            {
                canvas.Clear(SKColors.White);


                Random random = new Random();

                int x = 300, y = 200;

                foreach (var paire in MotsScores)
                {
                    string mot = paire.Key;
                    int score = paire.Value;

                    int fontSize = Math.Max(10, score * 5);
                    using (SKPaint paint = new SKPaint
                    {
                        Typeface = SKTypeface.Default,
                        TextSize = fontSize,
                        IsAntialias = true,
                        Color = new SKColor(
                            (byte)random.Next(50, 256),
                            (byte)random.Next(50, 256),
                            (byte)random.Next(50, 256)
                            )
                    
                    })
                    {
                        SKRect bounds = new SKRect();
                        paint.MeasureText(mot, ref bounds);

                        if (x + bounds.Width > width)
                        {
                            x = 10;
                            y += (int)bounds.Height + 10;
                        }

                        canvas.DrawText(mot, x, y, paint);

                        x += (int)bounds.Width + 10;
                    }
                }
                using (SKImage image = SKImage.FromBitmap(bitmap))
                using (SKData data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (System.IO.Stream stream = System.IO.File.OpenWrite("nuage_de_mots.png"))
                {
                    data.SaveTo(stream);
                }

            }

            Process.Start(new ProcessStartInfo
            {
                FileName = "nuage_de_mots.png",
                UseShellExecute = true
            });

        }
    }
}
