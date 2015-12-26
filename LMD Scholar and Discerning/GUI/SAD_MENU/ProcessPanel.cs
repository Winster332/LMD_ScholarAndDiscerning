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
	public class ProcessPanel : Panel
	{
		#region variable
		public NeuronInformationPanel[] nip;
		private List<NeuralNetwork.Neuron> neurons;
		private VScrollBar scrBar;
		private Control parent;
		#endregion
		#region ProcessPanel
		public ProcessPanel(Control parent)
		{
			this.neurons = new List<NeuralNetwork.Neuron>();
			this.parent = parent;
			parent.Controls.Add(this);
			this.Location = new Point(0, 30);
			this.Size = new Size(parent.Width, parent.Height - 36);

			scrBar = new VScrollBar();
			this.Controls.Add(scrBar);
			scrBar.Location = new Point(this.Width - scrBar.Width, 0);
			scrBar.Size = new Size(20, this.Height);
		}
		#endregion
		#region Calculate
		public void Calculate(List<NeuralNetwork.Neuron> neurons)
		{
			this.neurons = neurons;

			try
			{
				nip = new NeuronInformationPanel[neurons.Count];
				int indent = 2;
				for (int i = 1; i < neurons.Count; i++)
				{
			//		float x = (float)(neurons[i].prev_power % 100);
					
					nip[i] = new NeuronInformationPanel();
					this.Controls.Add(nip[i]);
					nip[i].Location = new Point(0, indent);
					nip[i].Text = i + ". Данные: " + neurons[i].userDate;
					nip[i].Text_Info = " Мощность: " + neurons[i].prev_power;
					nip[i].button_add.Visible = false;
					nip[i].button_del.Visible = false;

					nip[i].x = nip[i].Width;
					indent += nip[i].Height + 1;
				}

				scrBar.Maximum = neurons.Count;
				int prev_value = 0;
				scrBar.ValueChanged += (o, e) =>
				{
					for (int i = 1; i < nip.Length; i++)
					{
						if (scrBar.Value > prev_value)
							nip[i].Top -= nip[i].Height + 1;
						else nip[i].Top += nip[i].Height + 1;
					}

					prev_value = scrBar.Value;
				};
			}
			catch (Exception ex)
			{
				LogManager.Write(ex);
				MessageBox.Show("Не предвиденная ошибка.\nДанные ошибки записаны в лог.", "Ошибка!");
			}
		}
		#endregion
	}
}
