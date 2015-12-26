using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMD_Scholar_and_Discerning.GUI.SAD_MENU
{
	public partial class Form_save : Form
	{
		#region variables
		public LMD_TopBoard topBoard;
		#endregion
		#region Form_Save
		public Form_save()
		{
			InitializeComponent();
		}
		#endregion
		#region Form_Save_Load
		private void Form_save_Load(object sender, EventArgs e)
		{
			topBoard = new LMD_TopBoard(this, true);
			topBoard.but_account.Enabled = false;
			topBoard.but_account.Visible = false;
			topBoard.pos_title_no_free = new Point(5, 5);
			this.BackColor = Color.FromArgb(50, 50, 50);
			this.Paint += (o, ee) =>
				{
					Graphics g = ee.Graphics;
					g.DrawRectangle(new Pen(Color.FromArgb(0, 140, 220), 2), 0, 0, this.Width, this.Height);
				};
		}
		#endregion
		#region button click exit
		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		#endregion
	}
}
