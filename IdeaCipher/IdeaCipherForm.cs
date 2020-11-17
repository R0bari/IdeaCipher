using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdeaCipher
{
    public partial class cipherForm : Form
    {
        private String _inputFilename;
        private readonly String tempInputFilename = "tempPlainText.txt";
        private readonly String tempOutputFilename = "tempEncryptedData.dat";
        public cipherForm()
        {
            InitializeComponent();
        }

        private void HandleEncryptClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(inputPlainText.Text))
            {
                MessageBox.Show("Input text should not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(inputKey.Text)) {
                MessageBox.Show("Key word should be specified!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else {
                File.WriteAllText(tempInputFilename, inputPlainText.Text);
                IdeaCrypt.СryptFile(tempInputFilename, tempOutputFilename, inputKey.Text, true);
                inputEncryptedText.Text = String.Join(" ", File.ReadAllBytes(tempOutputFilename));
            }
            
        }

        private void HandleDecryptClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(inputEncryptedText.Text))
            {
                MessageBox.Show("Input text should not be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(inputKey.Text))
            {
                MessageBox.Show("Key word should be specified!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                String[] values = inputEncryptedText.Text.Split(' ');
                byte[] bytes = new byte[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    bytes[i] = Byte.Parse(values[i]);
                }
                File.WriteAllBytes(tempOutputFilename, bytes);
                try
                {
                    IdeaCrypt.СryptFile(tempOutputFilename, tempInputFilename, inputKey.Text, false);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                inputPlainText.Text = File.ReadAllText(tempInputFilename);
            }
        }

        private void HandlePlainLoadClick(object sender, EventArgs e)
        {
            DialogResult result = dlgPlainFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                _inputFilename = dlgPlainFile.FileName;
                inputPlainText.Text = File.ReadAllText(_inputFilename);
            }
        }

        private void HandleEncryptedLoadClick(object sender, EventArgs e)
        {
            DialogResult result = dlgEncryptedFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                _inputFilename = dlgEncryptedFile.FileName;
                inputEncryptedText.Text = String.Join(" ", File.ReadAllBytes(_inputFilename));
            }
        }

        private void HandleSavePlainClick(object sender, EventArgs e)
        {
            if (dlgPlainFileSave.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(dlgPlainFileSave.FileName, inputPlainText.Text);
            }
        }

        private void HandleSaveEncryptedClick(object sender, EventArgs e)
        {
            if (dlgEncryptedFileSave.ShowDialog() == DialogResult.OK)
            {
                String[] values = inputEncryptedText.Text.Split(' ');
                byte[] bytes = new byte[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    bytes[i] = Byte.Parse(values[i]);
                }
                File.WriteAllBytes(dlgEncryptedFileSave.FileName, bytes);
            }
        }
    }
}
