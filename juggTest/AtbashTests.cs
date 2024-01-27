using NUnit.Framework;
using jugg;

namespace AtbashTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AtbashTest_1()
        {
           
            string source = "привет";
            string expected = "поцэъм";

            
            string actual = new Cipher(source, true).Atbash();

            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AtbashTest_2()
        {
            
            string source = "как дела";
            string expected = "фяф ыъуя";

            
            string actual = new Cipher(source, true).Atbash();

            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AtbashTest_3()
        {
            
            string source = "Hello World!";
            string expected = "Svool Dliow!";

            
            string actual = new Cipher(source, false).Atbash();

            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AtbashTest_4()
        {
            
            string source = "How are you?";
            string expected = "Sld ziv blf?";

            
            string actual = new Cipher(source, false).Atbash();

            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AtbashTest_5()
        {
            
            string source = "поцэъм";
            string expected = "привет";

            
            string actual = new Cipher(source, true).Atbash();

            
            Assert.AreEqual(expected, actual);
        }
    }
}