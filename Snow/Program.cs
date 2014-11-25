using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow {
	class Program {
		static SnowController snow = new SnowController();

		static void Main(string[] args) {
			snow.Init();
			snow.Run();
		}
	}
}
