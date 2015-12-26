using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMD_Scholar_and_Discerning.NeuralNetwork
{
	public class Net
	{
		#region variables
		public int neurons_count
		{
			get
			{
				return neurons.Count;
			}
		}
		public int last_index_neuron;
		private List<Neuron> neurons;
		private int scr_size;
		#endregion
		#region Net
		public Net(int scr_size)
		{
			this.scr_size = scr_size;
			this.neurons = new List<Neuron>();
			neurons.Add(new Neuron());
		}
		#endregion
		#region Recognize
		public String Recognize(List<float> x)
		{
			try
			{
				int index = 0;

				for (int i = 0; i < neurons.Count; i++)
				{
					for (int j = 0; j < neurons[i].w.Count; j++)
					{
						neurons[i].power += neurons[i].w[j] * x[j];
						neurons[i].prev_power = neurons[i].power;
					}

					if (neurons[i].power > neurons[index].power)
						index = i;
				}
				for (int i = 0; i < neurons.Count; i++)
					neurons[i].power = 0;

				last_index_neuron = index;
				return neurons[index].userDate;
			}
			catch (Exception ex)
			{
				GUI.LogManager.Write(ex);
				System.Windows.Forms.MessageBox.Show("Не предвиденная ошибка.\nДанные ошибки записаны в лог.", "Ошибка!");

				return "Ошибка. Перезапустите программу";
			}
		}
		#endregion
		#region Correct
		public void Correct(int index, List<float> x, float speed, int delta)
		{
			try
			{
				for (int i = 0; i < neurons[index].w.Count; i++)
					neurons[index].w[i] = neurons[index].w[i] + speed * delta * x[i];
			}
			catch (Exception ex)
			{
				GUI.LogManager.Write(ex);
				System.Windows.Forms.MessageBox.Show("Не предвиденная ошибка.\nДанные ошибки записаны в лог.", "Ошибка!");
			}
		}
		#endregion
		#region RemoveNeuron
		public void RemoveNeuron(int index)
		{
			neurons.RemoveAt(index);
		}
		#endregion
		#region AddNeuron
		public Neuron AddNeuron(List<float> w, String date)
		{
			try
			{
				Neuron n = new Neuron();

				for (int i = 0; i < scr_size; i++)
					n.w.Add((float)new Random().NextDouble());

				for (int i = 0; i < scr_size; i++)
					n.w[i] = n.w[i] * w[i] * 0.5f;

				//	n.w = w;
				n.userDate = date;
				neurons.Add(n);

				return n;
			}
			catch (Exception ex)
			{
				GUI.LogManager.Write(ex);
				System.Windows.Forms.MessageBox.Show("Не предвиденная ошибка.\nДанные ошибки записаны в лог.", "Ошибка!");

				return new Neuron();
			}
		}
		#endregion
		#region setNeurons
		public void setNeurons(List<Neuron> neurons)
		{
			this.neurons = neurons;
		}
		#endregion
		#region getNeurons
		public List<Neuron> getNeurons()
		{
			return neurons;
		}
		#endregion
	}
}
