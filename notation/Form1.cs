using System;
using System.Windows.Forms;

namespace notation
{
    public partial class Form1 : Form
    {
        public event EventHandler KeyEventHandler;
        public Form1()
        {
            InitializeComponent();
            //Application.OpenForms[this.Name].Focus();
            this.ActiveControl = richTextBox1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.save();
        }

        public void save()
        {
            var sfd = new System.Windows.Forms.SaveFileDialog();
            
            sfd.ShowDialog();
            if(!string.IsNullOrWhiteSpace(sfd.FileName))
            { System.IO.File.WriteAllText(sfd.FileName, encrypt(richTextBox1.Text)); }
        }

        public void open()
        {
            var ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.ShowDialog();
            if(!string.IsNullOrWhiteSpace(ofd.FileName))
            {
                richTextBox1.Text = decrypt(System.IO.File.ReadAllText(ofd.FileName));
            }
        }

        public string encrypt(string s)
        {
            Form2 pwResult = new Form2();
            pwResult.ShowDialog(this);
            if (pwResult.DialogResult == DialogResult.OK)
            {
                var encryptedText = StringCipher.Encrypt(s, pwResult.pw);
                s = encryptedText;
            }
            else
            {
                s = "";
            }
            return s;
        }

        public string decrypt(string s)
        {
            Form2 pwResult = new Form2();
            pwResult.ShowDialog(this);
            if (pwResult.DialogResult == DialogResult.OK)
            {
                var encryptedText = StringCipher.Decrypt(s, pwResult.pw);
                s = encryptedText;
            }
            else
            {
                s = "";
            }
            return s;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.open();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //this.save();
            System.Windows.Forms.Application.Exit();
            
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            Form1_KeyDown(this, e);
        }

        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S)
            {
                this.save();
            }

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.O)
            {
                this.open();
            }
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.X)
            {
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}

