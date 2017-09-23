using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Web
{
	public interface IRandomizer
	{
		Int32 Next(Int32 minimum, Int32 maximum);
	}
}
