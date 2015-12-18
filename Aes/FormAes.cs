using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Aes
{
    public partial class FormAes : Form
    {
        public FormAes()
        {
            InitializeComponent();
        }

#region stringmanipulation
        private static byte[] ReadHexString(string s)
        {
            var size = s.Length / 2;
            var b = new byte[size];

            if ((size != 16) && (size != 24) && (size != 32))
            {
                throw new ArgumentException("String size has to be 16, 24 or 32");
            }

            for (var i = 0; i < size; i++)
            {
                b[i] = Convert.ToByte(s.Substring(2 * i, 2), 16);
            }
            return (b);
        }

        private static byte[] ReadAsciiString(string s)
        {
            return System.Text.Encoding.ASCII.GetBytes(s);
        }

        private static string WriteAsciiString(byte[] b)
        {
            return System.Text.Encoding.ASCII.GetString(b);
        }
#endregion

        private void btnTest_Click(object sender, EventArgs e)
        {
            var inputKey = ReadHexString(tbKey.Text);
            var key = new Key(inputKey);
            Console.Out.WriteLine("\nkey:\n" + key);

            var inputPlain = ReadAsciiString(tbPlain.Text);
            var inputState = new State(inputPlain);
            Console.Out.WriteLine("state:\n" + inputState.ToMatrixString());

            var cipherState = AesCipher.Cipher(key, inputState);

            Console.Out.WriteLine("result:\n" + cipherState.ToMatrixString());

            tbResult.Text = System.Convert.ToBase64String(cipherState.GetBytes());
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            var inputKey = ReadHexString(tbKey.Text);
            var key = new Key(inputKey);
            Console.Out.WriteLine("\nkey:\n" + key);

            var decodeCipher = System.Convert.FromBase64String(tbResult.Text);
            var decodeState = new State(decodeCipher);
            Console.Out.WriteLine("state:\n" + decodeState.ToMatrixString());

            var normalState = AesCipher.CipherInv(key, decodeState);

            Console.Out.WriteLine("result:\n" + normalState.ToMatrixString());

            tbPlain.Text = WriteAsciiString(normalState.GetBytes());
        }

        private void btnClearPlain_Click(object sender, EventArgs e)
        {
            tbPlain.Text = string.Empty;
        }

        private void btnClearCipher_Click(object sender, EventArgs e)
        {
            tbResult.Text = string.Empty;
        }
    }
}
