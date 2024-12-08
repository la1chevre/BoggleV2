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
    }
}