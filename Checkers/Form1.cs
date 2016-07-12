using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    public partial class Form1 : Form
    {
        private Engine e = new Engine();
        public Form1()
        {
            InitializeComponent();
            e.newGame();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            testBoard.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var x = this.e.WhiteMoves();
            
        }
    }
}
