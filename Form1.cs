namespace NotePad
{
    public partial class Form1 : Form
    {
        string fn;
        Boolean saveFlag;
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void setbk(object sender, EventArgs e)
        {
            txtnotepad.BackColor = Color.FromName(((ToolStripMenuItem)sender).Text);
            foreach (ToolStripMenuItem x in backColorToolStripMenuItem.DropDownItems)
            {
                if (x.Equals(sender))
                    x.Checked = true;
                else
                    x.Checked = false;
            }
        }

        private void setfk(object sender, EventArgs e)
        {
            txtnotepad.ForeColor = Color.FromName(((ToolStripMenuItem)sender).Text);
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusBar1.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void forColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.FullOpen = true;
            colorDialog1.ShowDialog();
            txtnotepad.ForeColor = colorDialog1.Color;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = txtnotepad.Font;
            fontDialog1.ShowDialog();
            txtnotepad.Font = fontDialog1.Font;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.IO.File.WriteAllText("E:\\backcolor.txt", txtnotepad.BackColor.Name);
            System.IO.File.WriteAllText("E:\\forcolor.txt", txtnotepad.ForeColor.Name);
            newToolStripMenuItem_Click(null, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            saveFlag = true;
            if (System.IO.File.Exists("E:\\backcolor.txt"))
            {
                string b;
                b = System.IO.File.ReadAllText("E:\\backcolor.txt");
                //txtnotepad.BackColor = Color.FromName(System.IO.File.ReadAllText("E:\\layout.txt"));
                //ToolStripMenuItem temp = new ToolStripMenuItem();
                //temp.Text = s;
                foreach (ToolStripMenuItem x in backColorToolStripMenuItem.DropDownItems)
                {
                    if (x.Text == b)
                        setbk(x, null);
                }
                //setbk(temp, null);
            }
            if (System.IO.File.Exists("E:\\forcolor.txt"))
            {
                string f;
                f = System.IO.File.ReadAllText("E:\\forcolor.txt");
                ToolStripMenuItem x = new ToolStripMenuItem();
                x.Text = f;
                setfk(x, null);
                //txtnotepad.ForeColor = Color.FromName(f);
            }
        }

        private void wordRapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtnotepad.WordWrap = wordRapToolStripMenuItem.Checked;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fn == null)
            {

                saveFileDialog1.DefaultExt = "txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                fn = saveFileDialog1.FileName;
            }
            System.IO.File.WriteAllText(fn, txtnotepad.Text);
            saveFlag = true;
            this.Text = fn;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFlag == false)
            {
                DialogResult r;
                r = MessageBox.Show("Do you want to save your file ?", "Waiting...", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(null, null);
                }
            }
            txtnotepad.Text = "";
            this.Text = "My Notepad";
            saveFlag = true;
            fn = null;
        }

        private void txtnotepad_TextChanged(object sender, EventArgs e)
        {
            saveFlag = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem_Click(null, null);
            openFileDialog1.Filter = "Text File |*.txt|Document File |*.doc|All Files |*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            fn = openFileDialog1.FileName;
            txtnotepad.Text = System.IO.File.ReadAllText(fn);
            saveFlag = true;
            this.Text = fn;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fn = null;
            saveToolStripMenuItem_Click(null, null);
        }
    }
}