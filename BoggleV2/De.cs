using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoggleV2
{
    public class De
    {
        public char face;
        public char[] faces;

        public De()
        {
            List<char> lettres = new List<char>() {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            this.faces = new char[6];
            Random random = new Random();

            
            for (int i = 0; i < 6; i++)
            {
                int n = random.Next(0, 25 - i);
                this.faces[i] = lettres[n];
                lettres.RemoveAt(n); // pour ne pas avoir 2 fois la même lettre sur 1 dé

            }
                
        }

        /// <summary>
        /// Définis une face visible aléatoirement parmis les 6 faces du dé
        /// </summary>
        /// <returns></returns>
        public void lance()
        {
            Random random = new Random();
            this.face = this.faces[random.Next(0, 6)];
        }

        /// <summary>
        ///  retourne une chaîne de caractères qui décrit un dé 
        /// </summary>

        /// <returns>"FaceA FaceB FaceC FaceD FaceE FaceF"</returns>

        public string toString()
        {
            string res = "";
            for (int i = 0; i < faces.Length; i++)
            {
                res += " " + faces[i];
            }
            return res;
        }

        public char Valeur()
        {
            Random random = new Random();
            return this.faces[random.Next(0, 6)];
        }
    }
}
