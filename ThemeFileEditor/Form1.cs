using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThemeFileEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            PopulateSystemColors(tableLayoutPanel1);
        }


        private void ClearTable(TableLayoutPanel t)
        {
            t.Controls.Clear();
            t.ColumnStyles.Clear();
            t.RowStyles.Clear();

            t.ColumnCount = 0;
            t.RowCount = 0;

            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            t.ColumnCount = 4;
        }


        private void PopulateSystemColors(TableLayoutPanel t)
        {
            ClearTable(t);
            string name = "Background";
            Color c = Color.FromArgb(255, 255, 255);
            Control con = null;

            foreach ()
            AddRow(t, name, c, con);


            //pad the rest with autosize row
            t.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            t.RowCount = t.RowCount + 1;
        }

        private void AddRow(TableLayoutPanel t, string name, Color c, Control con)
        {
            //add row
            t.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            t.RowCount = t.RowCount + 1;

            //row content
            Label lbl = new Label();
            lbl.Text = name;
            t.Controls.Add(lbl, 0, 0);

            PictureBox p = new PictureBox();
            p.BackColor = c;
            p.Height = 40;
            p.Width = 80;
            p.Click += P_Click;
            t.Controls.Add(p, 1, 0);

            if (con == null)
            {
                con = new TextBox
                {
                    Text = "The quick brown fox jumped over the lazy dog.",
                    Multiline = true,
                    Dock = DockStyle.Fill,
                    BackColor = c
                };
            }
            t.Controls.Add(con, 2, 0);
        }

        private void P_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;

            ColorDialog colorDialog1 = new ColorDialog();

            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                p.BackColor = colorDialog1.Color;
            }
            
        }
    }
}
