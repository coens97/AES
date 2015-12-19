﻿using System;
using System.Management.Instrumentation;
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

        private readonly byte[] _expectedResultEcb =
        {
            0x3a, 0xd7, 0x7b, 0xb4, 0x0d, 0x7a, 0x36, 0x60, 0xa8, 0x9e, 0xca, 0xf3, 0x24, 0x66, 0xef, 0x97
        };

        private readonly byte[] _expectedResultCbc =
        {
            0x76, 0x49, 0xab, 0xac, 0x81, 0x19, 0xb2, 0x46, 0xce, 0xe9, 0x8e, 0x9b, 0x12, 0xe9, 0x19, 0x7d
        };

        [TestMethod]
        public void TestAesCipher()
        {
            var key = new Key(_bkey);

            var inputState = new State(_input);
            var expectedState = new State(_expectedResultEcb);

            var cipher = AesCipher.Cipher(key, inputState);

            Assert.AreEqual(cipher.ToString(), expectedState.ToString());
        }

        [TestMethod]
        public void TestAesCipherInv()
        {
            var key = new Key(_bkey);

            var expectedNormal = new State(_input);
            var expectedState = new State(_expectedResultEcb);

            var normal = AesCipher.CipherInv(key, expectedState);

            Assert.AreEqual(normal.ToString(), expectedNormal.ToString());
        }

        [TestMethod]
        public void TestAesCipherCbc()
        {
            var key = new Key(_bkey);

            var aes = new AesCipher(_input);
            var expectedState = new State(_expectedResultCbc);

            aes.CipherCbcStates(key);

            Assert.AreEqual(aes.States[0].ToString(), expectedState.ToString());
        }

        [TestMethod]
        public void TestAesCipherMultipleCbc()
        {
            // Values from http://www.inconteam.com/software-development/41-encryption/55-aes-test-vectors#aes-cbc-128
            // Initialization vector is static, not best implementation since it should be dynamic
            var key = new Key(_bkey);

            var aes = new AesCipher(new byte[]
            {
                0x6b, 0xc1, 0xbe, 0xe2, 0x2e, 0x40, 0x9f, 0x96, 0xe9, 0x3d, 0x7e, 0x11, 0x73, 0x93, 0x17, 0x2a,
                0xae, 0x2d, 0x8a, 0x57, 0x1e, 0x03, 0xac, 0x9c, 0x9e, 0xb7, 0x6f, 0xac, 0x45, 0xaf, 0x8e, 0x51,
                0x30, 0xc8, 0x1c, 0x46, 0xa3, 0x5c, 0xe4, 0x11, 0xe5, 0xfb, 0xc1, 0x19, 0x1a, 0x0a, 0x52, 0xef,
                0xf6, 0x9f, 0x24, 0x45, 0xdf, 0x4f, 0x9b, 0x17, 0xad, 0x2b, 0x41, 0x7b, 0xe6, 0x6c, 0x37, 0x10
            });

            var expected = new AesCipher(new byte[]
            {
                0x76, 0x49, 0xab, 0xac, 0x81, 0x19, 0xb2, 0x46, 0xce, 0xe9, 0x8e, 0x9b, 0x12, 0xe9, 0x19, 0x7d,
                0x50, 0x86, 0xcb, 0x9b, 0x50, 0x72, 0x19, 0xee, 0x95, 0xdb, 0x11, 0x3a, 0x91, 0x76, 0x78, 0xb2,
                0x73, 0xbe, 0xd6, 0xb8, 0xe3, 0xc1, 0x74, 0x3b, 0x71, 0x16, 0xe6, 0x9e, 0x22, 0x22, 0x95, 0x16,
                0x3f, 0xf1, 0xca, 0xa1, 0x68, 0x1f, 0xac, 0x09, 0x12, 0x0e, 0xca, 0x30, 0x75, 0x86, 0xe1, 0xa7
            });

            aes.CipherCbcStates(key);

            Assert.AreEqual(aes.ToString(), expected.ToString());
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
