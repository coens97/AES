using System;
using System.Windows.Forms;

namespace Aes
{
    public partial class FormAes : Form
    {
        public FormAes()
        {
            InitializeComponent();
        }

        private static byte[] ReadHexString(string s)
        {
            var size = s.Length / 2;
            var b = new byte[size];

            if ((size != 16) && (size != 24) && (size != 32))
            {
                throw new Exception();
            }

            for (var i = 0; i < size; i++)
            {
                b[i] = Convert.ToByte(s.Substring(2 * i, 2), 16);
            }
            return (b);
        }

        private static byte[] ReadAsciiString(string s)
        {
            var b = new byte[s.Length];
            b = System.Text.Encoding.ASCII.GetBytes(s);
            return (b);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            var inputKey = ReadHexString(tbKey.Text);
            var key = new Key(inputKey);
            Console.Out.WriteLine("\nkey:\n" + key);

            var inputPlain = ReadAsciiString(tbPlain.Text);
            var inputState = new State(inputPlain);
            Console.Out.WriteLine("state:\n" + inputState.ToMatrixString());
        }
    }
}
