namespace BoggleV2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("+--------------------------------------------------------+");
            Console.WriteLine("|                         BOGGLE                         |");
            Console.WriteLine("+--------------------------------------------------------+");
            Console.WriteLine("\npar Grégoire LEGROS et Maximilien BERNARD");

            Console.WriteLine("Dans quelle langue voulez vous jouer ?");
            Dictionnaire dico = CreationMots();

            Plateau plato = new Plateau(4);
            Console.WriteLine(plato.toString());
            string mot = Console.ReadLine();
            Console.WriteLine(mot);
            Console.WriteLine(plato.Test_Plateau(mot, dico));
            Console.WriteLine("azr");
        }

        static Dictionnaire CreationMots()
        {
            Console.WriteLine("\n\tTaper \"fr\" pour jouer en français et \"en\" pour jouer en anglais.");

            string langue = Console.ReadLine();

            switch (langue)
            {
                case "fr":
                    return new Dictionnaire("fr");
                    break;
                case "en";
                    return new Dictionnaire("en");
                    break;
                default:
                    Console.WriteLine("Cette langue n'est pas reconnue.");
                    return CreationMots();
                    break;
            }



        }
    }
}
