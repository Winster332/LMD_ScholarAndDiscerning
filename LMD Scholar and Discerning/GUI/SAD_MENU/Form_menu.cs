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
	public partial class Form_menu : Form
	{
		#region variables

		public LMD_TopBoard topBoard;
		public PanelWorkspace workspace;
		#endregion
		#region Form_menu
		public Form_menu()
		{
			InitializeComponent();

			this.BackColor = Color.FromArgb(0, 140, 220);
		}
		#endregion
		#region Form_menu_Load
		private void Form_menu_Load(object sender, EventArgs e)
		{
			topBoard = new LMD_TopBoard(this, true);
			workspace = new PanelWorkspace(this);
		}
		#endregion
	}
}
