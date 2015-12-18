using System;
using System.Collections.Generic;
using System.Linq;
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
            return Enumerable.Range(0, s.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(s.Substring(x, 2), 16))
                     .ToArray();
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

        private Key ReadKey()
        {
            var size = tbKey.Text.Length/2;
            if ((size != 16) && (size != 24) && (size != 32))
            {
                throw new ArgumentException("String size has to be 16, 24 or 32");
            }
            var inputKey = ReadHexString(tbKey.Text);
            var key = new Key(inputKey);
            Console.Out.WriteLine("\nkey:\n" + key);
            return key;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            var key = ReadKey();

            var inputPlain = ReadAsciiString(tbPlain.Text);
            var inputState = new State(inputPlain);
            Console.Out.WriteLine("state:\n" + inputState.ToMatrixString());

            var cipherState = AesCipher.Cipher(key, inputState);

            Console.Out.WriteLine("result:\n" + cipherState.ToMatrixString());

            //tbResult.Text = System.Convert.ToBase64String(cipherState.GetBytes());
            tbResult.Text = cipherState.ToString();
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            var key = ReadKey();

            //var decodeCipher = System.Convert.FromBase64String(tbResult.Text);
            var decodeCipher = ReadHexString(tbResult.Text);
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
