using System.Collections.Generic;
using System.Linq;

namespace Aes
{
    public class AesCipher
    {
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
        public AesCipher(byte[] b)
        {
            var length = (b.Length - 1) / 16 + 1;
            States = new State[length];

            for (var i = 0; i < length; i++)
            {
                var bytes = b.Skip(i * 16).Take(16).ToArray();
                States[i] = new State(bytes);
            }
        }

        public void CipherStates(Key key)
        {
            for (var i = 0; i < States.Length; i++)
            {
                States[i] = Cipher(key, States[i]);
            }
        }

        public void CipherInvStates(Key key)
        {
            for (var i = 0; i < States.Length; i++)
            {
                States[i] = CipherInv(key, States[i]);
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
