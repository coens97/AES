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
    }
}
