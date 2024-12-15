using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace BoggleV2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_Add_Mot()
        {
            
            Joueur j1 = new Joueur("j1");
            j1.Add_Mot("tu");
            
            Assert.AreEqual(j1.Contain("tu"), true);
        }

        [TestMethod]
        public void Test_ConditionDE()
        {
            De d1 = new De();
            bool Unique = true;
            for(int i = 0;i<d1.faces.Length;i++) 
            {
                for(int j = i+1; j < d1.faces.Length; j++)
                {
                    if (d1.faces[i] == d1.faces[j])
                    {
                        Unique = false;
                    }
                }
            }
            Assert.IsTrue(Unique);
        }

        [TestMethod]
        public void Test_TriFusion()
        {
            
            string[] Atrie = { "gare", "arbre","serre","trouve","attendre" };
            string[] trie = { "arbre","attendre", "gare","serre","trouve"};
            bool estTrie = true;
            string[] t  = Dictionnaire.TriFusion(Atrie, 0, Atrie.Length - 1);
            for(int i = 0; i < t.Length; i++)
            {
                if (trie[i] != t[i])
                {
                     estTrie = false;
                }
            }
            Assert.IsTrue(estTrie);
            

        }

        [TestMethod]

        public void Test_RechercheDicho()
        {
            string[] mots = { "ARBRE",  "ATTENDRE", "GARE", "SERRE" ,"ZEBRE"};
            Assert.IsTrue(Dictionnaire.RechercheDichoRecursif(mots,"gare",mots.Length-1,0));
            Assert.IsFalse(Dictionnaire.RechercheDichoRecursif(mots, "vin", mots.Length - 1, 0));

        }

       


    }
}