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
	public class LMD_Store : Panel
	{
		#region variable
		private VScrollBar scrBar;
		private Control parent;
		Label lab_time = new Label();
		#endregion
		#region ProcessPanel
		public LMD_Store(Control parent)
		{
			this.parent = parent;
			parent.Controls.Add(this);
			this.Location = new Point(0, 30);
			this.Size = new Size(parent.Width, parent.Height - 36);

			scrBar = new VScrollBar();
			this.Controls.Add(scrBar);
			scrBar.Location = new Point(this.Width - scrBar.Width, 0);
			scrBar.Size = new Size(20, this.Height);

			lab_time.Text = "Раздел в разработке";
			lab_time.Font = new Font("Arial", 30);
			lab_time.Size = new Size(300, this.Width);
			lab_time.Location = new Point(this.Width / 2 - 100, 150);
			this.Controls.Add(lab_time);
		}
		#endregion
	}
}
