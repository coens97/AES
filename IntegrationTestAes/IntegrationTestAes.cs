using System;
using Aes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTestAes
{
    [TestClass]
    public class IntegrationTestAes
    {
        // values from http://www.inconteam.com/software-development/41-encryption/55-aes-test-vectors#aes-ecb-128
        private readonly byte[] _bkey =
        {
            0x2b, 0x7e, 0x15, 0x16, 0x28, 0xae, 0xd2, 0xa6, 0xab, 0xf7, 0x15, 0x88, 0x09, 0xcf, 0x4f, 0x3c
        };

        private readonly byte[] _input =
        {
           0x6b, 0xc1, 0xbe, 0xe2, 0x2e, 0x40, 0x9f, 0x96, 0xe9, 0x3d, 0x7e, 0x11, 0x73, 0x93, 0x17, 0x2a
        };

        private readonly byte[] _expectedResult =
        {
            0x3a, 0xd7, 0x7b, 0xb4, 0x0d, 0x7a, 0x36, 0x60, 0xa8, 0x9e, 0xca, 0xf3, 0x24, 0x66, 0xef, 0x97
        };

        [TestMethod]
        public void TestAesCipher()
        {
            var key = new Key(_bkey);

            var inputState = new State(_input);
            var expectedState = new State(_expectedResult);

            var cipher = AesCipher.Cipher(key, inputState);

            Assert.AreEqual(cipher.ToString(), expectedState.ToString());
        }

        [TestMethod]
        public void TestAesCipherInv()
        {
            var key = new Key(_bkey);

            var expectedNormal = new State(_input);
            var expectedState = new State(_expectedResult);

            var normal = AesCipher.CipherInv(key, expectedState);

            Assert.AreEqual(normal.ToString(), expectedNormal.ToString());
        }

        [TestMethod]
        public void TestAesCipherAndBack()
        {
            var key = new Key(_bkey);

            var inputState = new State(_input);

            var cipher = AesCipher.Cipher(key, inputState);

            var normal = AesCipher.CipherInv(key, cipher);

            Assert.AreEqual(inputState.ToString(), normal.ToString());
        }
    }
}
