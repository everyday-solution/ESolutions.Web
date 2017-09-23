using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Web
{
	public class Randomizer : IRandomizer
	{
		//Fields
		#region randomizer
		private Random randomizer = new Random();
		#endregion

		//Methods
		#region Next
		public Int32 Next(Int32 minimum, Int32 maximum)
		{
			return this.randomizer.Next(minimum, maximum);
		}
		#endregion
	}
}
