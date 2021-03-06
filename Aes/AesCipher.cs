﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Aes
{
    public class AesCipher
    {
        // usualy initialization vector should be randimized (from a hashing method), for the 
        // purpose to keep it simple chose to use this one to make it easy to test and prove.
        private readonly byte[] _iv =
        {
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F
        };
        public static State Cipher(Key key, State state)
        {
            state = state
                .AddRoundKey(key, 0);

            for (var i = 1; i < 10; i++)
            {
                state = state
                    .SubBytes()
                    .ShiftRows()
                    .MixColumns()
                    .AddRoundKey(key, i);
            }
            state = state
                .SubBytes()
                .ShiftRows()
                .AddRoundKey(key, 10);
            return state;
        }

        public static State CipherInv(Key key, State state)
        {
            state = state
                .AddRoundKey(key, 10);

            for (var i = 9; i  > 0; i--)
            {
                state = state
                    .ShiftRowsInv()
                    .SubBytesInv()
                    .AddRoundKey(key, i)
                    .MixColumnsInv();
            }
            state = state
                .ShiftRowsInv()
                .SubBytesInv()
                .AddRoundKey(key, 0);
            return state;
        }

        public State[] States { get; private set; }
        public AesCipher(IReadOnlyList<byte> b)
        {
            var length = (b.Count - 1) / 16 + 1;
            States = new State[length];

            for (var i = 0; i < length; i++)
            {
                States[i] = new State(b, i);
            }
        }

        public void CipherEcbStates(Key key)
        {
            for (var i = 0; i < States.Length; i++)
            {
                States[i] = Cipher(key, States[i]);
            }
        }

        public void CipherEcbInvStates(Key key)
        {
            for (var i = 0; i < States.Length; i++)
            {
                States[i] = CipherInv(key, States[i]);
            }
        }

        public void CipherCbcStates(Key key)
        {
            var xorVal = _iv;
            for (var i = 0; i < States.Length; i++)
            {
                States[i] = States[i].Xor(xorVal);
                States[i] = Cipher(key, States[i]);
                xorVal = States[i].GetBytes();
            }
        }

        public void CipherCbcInvStates(Key key)
        {
            var xorVal = States[0].GetBytes();
            States[0] = CipherInv(key, States[0]);
            States[0] = States[0].Xor(_iv);

            for (var i = 1; i < States.Length; i++)
            {
                var nextXorVal = States[i].GetBytes();
                States[i] = CipherInv(key, States[i]);
                States[i] = States[i].Xor(xorVal);
                xorVal = nextXorVal;
            }
        }

        public override string ToString()
        {
            return States.Aggregate(string.Empty, (current, state) => current + state.ToString());
        }

        public byte[] GetBytes()
        {
            return States.SelectMany(x => x.GetBytes()).ToArray();
        }

        public string ToMatrixString()
        {
            return States.Aggregate(string.Empty, (current, state) => current + state.ToMatrixString());
        }
    }
}
