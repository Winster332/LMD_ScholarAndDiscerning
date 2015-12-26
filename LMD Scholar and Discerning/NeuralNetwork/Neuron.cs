using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMD_Scholar_and_Discerning.NeuralNetwork
{
	public class Neuron
	{
		public List<float> w;
		public float power;
		public float prev_power;
		public String userDate;

		public Neuron()
		{
			w = new List<float>();
		}
	}
}
