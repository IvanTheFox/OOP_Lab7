using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Lab7_Rework
{
    public partial class StartupForm : Form
    {
        private bool _isConsoleMode = true;

        public StartupForm()
        {
            InitializeComponent();
        }

        public bool IsConsoleMode() => _isConsoleMode;

        private void ConsoleButton_Click(object sender, EventArgs e)
        {
            _isConsoleMode = true;
            Close();
        }
        private void GuiButton_Click(Object sender, EventArgs e)
        {
            _isConsoleMode = false;
            Close();
        }
    }
}
