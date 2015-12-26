using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMD_Scholar_and_Discerning.GUI
{
	public class PanelCorrect : Panel
	{
		public delegate void ButtonClick(Boolean flag);

		public event ButtonClick onButtonClick;
		public Button[] but;
		public Label label;
		private Form form;

		public PanelCorrect(Form form)
		{
			this.Visible = false;
			this.form = form;
			this.form.Controls.Add(this);
			this.Size = new Size(form.Width, 20);
			this.BackColor = Color.FromArgb(150, 150, 150);
			this.Location = new Point(1, ((Form1)form).topBoard.Bottom);

			label = new Label();
			label.Text = "Верно ли распознано изображение?";
			label.Width = 250;
			label.Location = new Point(5, 2);
			label.Font = new Font("Arial", 10f);
			label.ForeColor = Color.FromArgb(55, 55, 55);
			this.Controls.Add(label);

			but = new Button[2];
			int indent = form.Width - 50;
			for (int i = 0; i < but.Length; i++)
			{
				but[i] = new Button();
				this.Controls.Add(but[i]);
				but[i].Size = new System.Drawing.Size(50, this.Height);
				but[i].FlatStyle = FlatStyle.Flat;
				but[i].FlatAppearance.BorderSize = 0;
				but[i].Location = new Point(indent, 0);
				but[i].BackColor = Color.FromArgb(140, 140, 140);

				but[i].Click += (o, e) =>
					{
						for (int j = 0; j < but.Length; j++)
							if (but[j].ContainsFocus)
							{
								Boolean flag = false;

								if (j == 0)
									flag = true;
								else if (j == 1)
									flag = false;

								if (onButtonClick != null)
									onButtonClick(flag);
							}
					};

				indent -= but[i].Width;
			}
			but[0].Text = "Да";
			but[1].Text = "Нет";
		}

		public void setVisible(Boolean flag)
		{
			this.Visible = flag;
		}
	}
}
