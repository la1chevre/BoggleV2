namespace BoggleV2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Jeu partie = new Jeu();
            Dictionnaire d = new Dictionnaire("fr");
            for(int i = 0; i < d.dictionnaire.Length; i++)
            {
                Console.Write(d.dictionnaire[i]+" ");
            }
            //string a = d.toString();
            //Console.WriteLine(a);
        }
    }
}
