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
    public partial class ThemeForm : Form
    {
        private ThemeFile ActiveFile;

        public ThemeForm()
        {
            InitializeComponent();
            ActiveFile = new ThemeFile();
        }

        public ThemeForm(string themeFile)
        {
            InitializeComponent();
            ActiveFile = new ThemeFile(themeFile);
        }

        #region Display
        public void RefreshThemeDisplay()
        {
            PopulateSystemColors(tableLayoutPanel1);
        }



        private void ClearTable(TableLayoutPanel t)
        {
            t.SuspendLayout();
            t.Controls.Clear();
            t.ColumnStyles.Clear();
            t.RowStyles.Clear();

            t.ColumnCount = 0;
            t.RowCount = 0;

            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250));
            t.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            t.ColumnCount = 5;
            t.ResumeLayout();
        }


        private void PopulateSystemColors(TableLayoutPanel t)
        {
            t.SuspendLayout();
            ClearTable(t);
            //string name = "Background";
            //Color c = Color.FromArgb(255, 255, 255);
            //Control con = null;

            foreach (string name in ThemeHelper.SystemColorPropertyNames)
            {
                AddRow(t, name, ThemeHelper.SystemColorFromSystemName(name), null, null);
            }



            //pad the rest with autosize row
            t.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            t.RowCount = t.RowCount + 1;
            t.ResumeLayout(true);
        }

        private void AddRow(TableLayoutPanel t, string name, Color c, Control con, Control con2)
        {
            //add row
            t.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));

            //row content
            Label lbl = new Label();
            lbl.Text = name;
            t.Controls.Add(lbl, 0, t.RowCount);

            PictureBox p = new PictureBox();
            p.BackColor = c;
            p.Height = 20;
            p.Width = 80;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Tag = name;
            p.Click += ChangeColor;
            t.Controls.Add(p, 1, t.RowCount);

            if (con == null)
            {
                con = new TextBox
                {
                    Text = "The quick brown fox jumped over the lazy dog.",
                    //Multiline = true,
                    //ScrollBars=ScrollBars.Vertical,
                    Height = 20,
                    Dock = DockStyle.Fill,
                    BackColor = c
                };
            }
            t.Controls.Add(con, 2, t.RowCount);

            if (con2 == null)
            {
                con2 = new TextBox
                {
                    Text = "The quick brown fox jumped over the lazy dog.",
                    //Multiline = true,
                    //ScrollBars=ScrollBars.Vertical,
                    Height = 20,
                    Dock = DockStyle.Fill,
                    BackColor = c,
                    ForeColor = Color.White
                };
            }
            t.Controls.Add(con2, 3, t.RowCount);

            t.RowCount = t.RowCount + 1;
        }
        #endregion




        private void ChangeColor(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;

            ColorDialog colorDialog1 = new ColorDialog();

            DialogResult result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                p.BackColor = colorDialog1.Color;

                ActiveFile.SetColor(p.Tag.ToString(), p.BackColor);
            }
            
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            ThemeFile t = new ThemeFile();
            t.SaveAs(@"C:\temp\systemthemetest.theme");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshThemeDisplay();
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            ActiveFile.Apply();
        }

        #region FileActions

        public void Save()
        {
            ActiveFile.Save();
        }

        public void SaveAs(string fileName)
        {
            ActiveFile.SaveAs(fileName);
        }

        public void Apply()
        {
            ActiveFile.Apply();
        }

        #endregion
    }
}
