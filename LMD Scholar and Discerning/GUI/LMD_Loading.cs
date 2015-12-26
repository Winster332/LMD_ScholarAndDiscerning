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
	public class LMD_Loading
	{
		#region variables
		public float time_step;
		public string Text;
		public float CountIndex;
		private LMD_Interface myInterface;
		private bool _Enable;
		private StringFormat sFormat;
		public bool Enable
		{
			get
			{
				return this._Enable;
			}
			set
			{
				if (value)
				{
					this.myInterface.UpDate.Start();
				}
				else
				{
					if (!value)
					{
						this.myInterface.UpDate.Stop();
					}
				}
				this._Enable = value;
			}
		}
		#endregion
		#region LMD_Loading
		public LMD_Loading(LMD_Interface myInterface)
		{
			this.myInterface = myInterface;
			this.Enable = false;
			this.sFormat = new StringFormat();
			this.sFormat.Alignment = StringAlignment.Center;
			this.sFormat.LineAlignment = StringAlignment.Center;
			this.Text = "Loading";
		}
		#endregion
		#region Start
		public void Start(float CountIndex)
		{
			this.CountIndex = CountIndex;
		}
		#endregion
		#region Draw
		public void Draw(Graphics g)
		{
			if (this.Enable)
			{
				this.time_step += 0.1f;
				g.DrawLine(new Pen(Color.FromArgb(150, 150, 150), 5f), 0, 180, this.myInterface.Width, 180);
				g.DrawLine(new Pen(Color.FromArgb(150, 150, 150), 5f), 0, 330, this.myInterface.Width, 330);
				g.DrawString(this.Text, new Font("Arial", 40f), new SolidBrush(Color.FromArgb(0, 140, 220)), new PointF((float)(this.myInterface.Width / 2), 150f), this.sFormat);
				for (int i = 0; i < 400; i += 100)
				{
					g.FillEllipse(new SolidBrush(Color.FromArgb(100, 100, 100)), (float)(70 + i), 250f + (float)Math.Sin((double)(this.time_step / 2f)) * 20f, 50f, 50f);
				}
				for (int i = 0; i < 300; i += 100)
				{
					g.FillEllipse(new SolidBrush(Color.FromArgb(100, 100, 100)), (float)(120 + i), 210f + (float)Math.Sin((double)(-(double)this.time_step / 2f)) * 20f, 50f, 50f);
				}
			}
		}
		#endregion
	}
}
