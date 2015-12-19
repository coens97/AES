using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aes
{
    public partial class FormAes : Form
    {
        private Bitmap _bitmap;
        private Stopwatch _sw = new Stopwatch();

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
            var aes = new AesCipher(inputPlain);
            Console.Out.WriteLine("state:\n" + aes.ToMatrixString());

            if (rbEcb.Checked)
                aes.CipherEcbStates(key);
            else
                aes.CipherCbcStates(key);

            Console.Out.WriteLine("result:\n" + aes.ToMatrixString());

            tbResult.Text = aes.ToString();
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            var key = ReadKey();

            var decodeCipher = ReadHexString(tbResult.Text);
            var aes = new AesCipher(decodeCipher);
            Console.Out.WriteLine("state:\n" + aes.ToMatrixString());

            if (rbEcb.Checked)
                aes.CipherEcbInvStates(key);
            else
                aes.CipherCbcInvStates(key);

            Console.Out.WriteLine("result:\n" + aes.ToMatrixString());

            tbPlain.Text = WriteAsciiString(aes.GetBytes());
        }

        private void btnClearPlain_Click(object sender, EventArgs e)
        {
            tbPlain.Text = string.Empty;
        }

        private void btnClearCipher_Click(object sender, EventArgs e)
        {
            tbResult.Text = string.Empty;
        }

        private void btnNewKey_Click(object sender, EventArgs e)
        {
            var random = new Random();
            tbKey.Text =
                $"{random.Next(0x1000000):X6}" +
                $"{random.Next(0x1000000):X6}" +
                $"{random.Next(0x1000000):X6}" +
                $"{random.Next(0x1000000):X6}" +
                $"{random.Next(0x1000000):X6}" +
                $"{random.Next(0x100):X2}";
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _bitmap = new Bitmap(dlg.FileName);
                    pictureBox.Image = _bitmap;
                }
            }
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Title = "Save Image";
                dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _bitmap.Save(dlg.FileName);
                }
            }
        }

        private async void btnEncrypt_Click(object sender, EventArgs e)
        {
            // await cause it takes soooo long and it keeps the window respondible
            StartProcess();
            await Task.Run(() =>
            {
                var key = ReadKey();

                pictureBox.Image = null;

                var aes = new AesCipher(BitmapToBytes());

                if (rbEcb.Checked)
                    aes.CipherEcbStates(key);
                else
                    aes.CipherCbcStates(key);

                SetBitmapFromBytes(aes.GetBytes());
            });
            EndProcess();
        }

        private async void btnDecrypt_Click(object sender, EventArgs e)
        {
            // await cause it takes soooo long and it keeps the window respondible
            StartProcess();
            await Task.Run(() =>
            {
                var key = ReadKey();

                pictureBox.Image = null;

                var aes = new AesCipher(BitmapToBytes());

                if (rbEcb.Checked)
                    aes.CipherEcbInvStates(key);
                else
                    aes.CipherCbcInvStates(key);

                SetBitmapFromBytes(aes.GetBytes());
            });
            EndProcess();
        }

        private void StartProcess()
        {
            _sw.Reset();
            _sw.Start();
            progressBar.Style = ProgressBarStyle.Marquee;
        }

        private void EndProcess()
        {
            _sw.Stop();
            lbTime.Text = $"Elapsed:\n{_sw.Elapsed}";
            
            progressBar.Style = ProgressBarStyle.Blocks;
        }

        public byte[] BitmapToBytes()
        {
            Rectangle rect = new Rectangle(0, 0, _bitmap.Width, _bitmap.Height);
            System.Drawing.Imaging.BitmapData bmpData = _bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, _bitmap.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap. 
            int bytes = Math.Abs(bmpData.Stride) * _bitmap.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            return rgbValues;
        }

        public void SetBitmapFromBytes(byte[] b)
        {
            // this method replaces the old bitmap, since they have the same properties copy from the old bitmap
            var bitmap = new Bitmap(_bitmap.Width, _bitmap.Height, _bitmap.PixelFormat);
            var bmData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            var pNative = bmData.Scan0;
            Marshal.Copy(b, 0, pNative, _bitmap.Width * _bitmap.Height * (Image.GetPixelFormatSize(_bitmap.PixelFormat) / 8));
            bitmap.UnlockBits(bmData);

            _bitmap = bitmap;
            pictureBox.Image = _bitmap;
        }
    }
}
