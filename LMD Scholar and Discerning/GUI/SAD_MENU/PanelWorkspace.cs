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
	public class PanelWorkspace : Panel
	{
		#region variable
		public delegate void ClickButton(LOCATION_STATE location);
		public event ClickButton onClickButton;
		public enum LOCATION_STATE { processes = 0, local_date = 1, net_date = 2 }
		public LOCATION_STATE location_state
		{
			get
			{
				return _location_state;
			}
			set
			{
				if (value == LOCATION_STATE.local_date)
				{
					but[0].BackColor = but_color;
					but[1].BackColor = but_color_active;
					but[2].BackColor = but_color;

					p_brains.Visible = true;
					p_process.Visible = false;
					p_store.Visible = false;
				}
				else if (value == LOCATION_STATE.net_date)
				{
					but[0].BackColor = but_color;
					but[1].BackColor = but_color;
					but[2].BackColor = but_color_active;

					p_brains.Visible = false;
					p_process.Visible = false;
					p_store.Visible = true;
				}
				else if (value == LOCATION_STATE.processes)
				{
					but[0].BackColor = but_color_active;
					but[1].BackColor = but_color;
					but[2].BackColor = but_color;

					p_brains.Visible = false;
					p_process.Visible = true;
					p_store.Visible = false;

					if (onClickButton != null)
						onClickButton(LOCATION_STATE.processes);
				}

				_location_state = value;

				
			}
		}
		public ProcessPanel p_process;
		public LMD_Store p_store;
		public BrainsPanel p_brains;
		private Color but_color;
		private Color but_color_active;
		private LOCATION_STATE _location_state;
		private Form form;
		private Button[] but;
		#endregion
		#region PanelWorkspace
		public PanelWorkspace(Form form)
		{
			#region this
			this.form = form;
			form.Controls.Add(this);
			this.BackColor = Color.FromArgb(180, 180, 180);
			this.Location = new Point(1, ((Form_menu)form).topBoard.Bottom);
			this.Size = new Size(form.Width - 2, form.Height - this.Top - 1);
			this.but_color = Color.FromArgb(130, 130, 130);
			this.but_color_active = Color.FromArgb(80, 80, 80);
			#endregion
			#region create buttons
			but = new Button[3];
			int indent = 0;
			for (int i = 0; i < but.Length; i++)
			{
				but[i] = new Button();
				this.Controls.Add(but[i]);
				but[i].Size = new Size(form.Width / 3, 30);
				but[i].FlatAppearance.BorderSize = 0;
				but[i].FlatStyle = FlatStyle.Flat;
				but[i].BackColor = but_color;
				but[i].Location = new Point(indent, 0);
				but[i].Font = new System.Drawing.Font("Arial", 12f);
				but[i].ForeColor = Color.FromArgb(200, 200, 200);
				but[i].Click += (o, e) =>
					{
						for (int j = 0; j < but.Length; j++)
							if (but[j].ContainsFocus)
								switch (j)
								{
									case 0:
										location_state = LOCATION_STATE.processes;
										break;
									case 1:
										location_state = LOCATION_STATE.local_date;
										break;
									case 2:
										location_state = LOCATION_STATE.net_date;
										break;
								}
					};

				indent += but[i].Width + 2;
			}
			but[0].Text = "Последний анализ";
			but[1].Text = "Локальные ресурсы";
			but[2].Text = "Интернет ресурсы";
			#endregion

			p_process = new ProcessPanel(this);
			p_brains = new BrainsPanel(this);
			p_store = new LMD_Store(this);

			this.Paint += (o, e) =>
			{
				e.Graphics.DrawLine(new Pen(Color.FromArgb(0, 140, 220), 2), 0, this.Height - 5, this.Width, this.Height - 5);
			};

			location_state = LOCATION_STATE.processes;
		}
		#endregion
		#region OpenLocalDate
		public void OpenLocalDate()
		{
			but[1].PerformClick();
			location_state = LOCATION_STATE.local_date;
		}
		#endregion
	}
}
