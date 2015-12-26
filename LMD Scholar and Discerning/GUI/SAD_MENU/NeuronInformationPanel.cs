using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMD_Scholar_and_Discerning.GUI.SAD_MENU
{
	public partial class NeuronInformationPanel : UserControl
	{
		public delegate void ADD();
		public delegate void DEL();
		public event ADD Add;
		public event DEL Del;

		public String path;
		public String Text
		{
			get
			{
				return label_text.Text;
			}
			set
			{
				label_text.Text = value;
			}
		}
		public String Text_Info
		{
			get
			{
				return label_textInfo.Text;
			}
			set
			{
				this.label_textInfo.Text = value;
			}
		}
		public float x;

		public NeuronInformationPanel()
		{
			InitializeComponent();
		}

		private void NeuronInformationPanel_Load(object sender, EventArgs ee)
		{
			button_add.Click += (o, e) =>
				{
					if (Add != null)
						Add();
				};
			button_del.Click += (o, e) =>
			{
				if (Del != null)
					Del();
			};

			x = this.Width - 100;

			this.Paint += NeuronInformationPanel_Paint;
		}

		void NeuronInformationPanel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			g.Clear(this.BackColor);
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; 
			g.FillRectangle(new SolidBrush(Color.FromArgb(150, 150, 150)), 0, 22, x, 10);
		}
	}
}
