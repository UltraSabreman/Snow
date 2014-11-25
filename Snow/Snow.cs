using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Snow {
	class Wind {
		public int VelX { get; set; }
		public int VelY { get; set; }

		public void Randomize() { }
	}
	class SnowFlake {
		static char[] models = new char[] { '.', ',', '*', '+', 'o', '¤', '#', '%'};
		
		int posX, posY;
		int velX, velY;
		int size = -1;
		int stepAccum = 0;

		public bool removeMe { get; set; }

		public SnowFlake() {
			size = SnowController.rand.Next(models.Count());
			removeMe = false;

			posX = SnowController.rand.Next(Console.WindowWidth);
			posY = 0;
			velX = 0;// SnowController.rand.Next(-2, 2);
			velY = SnowController.rand.Next(1, 2);
		}

		public void DoTick(Wind theWind) {
			if (posY >= Console.WindowHeight) { removeMe = true; return; }



			int maxSize = models.Count();
			stepAccum += (maxSize - size);
			if (stepAccum >= 80) {
				int rem = Console.WindowHeight - posY;
				posY += (velY > rem ? rem : velY);
				stepAccum = 0;

				posX += velX;
				if (posX < 0)
					posX = Console.WindowWidth + posX;
				else if (posX >= Console.WindowWidth)
					posX = posX % Console.WindowWidth;
			}
		}

		public void DoDraw() {
			Console.CursorLeft = posX;
			Console.CursorTop = posY;
			Console.Write(models[size]);
		}

	}

	class SnowController {
		static public Random rand = new Random();
		static public object locker = new object();

		List<int> SnowLayer = new List<int>();
		List<SnowFlake> SnowFlakes = new List<SnowFlake>();
		Timer snowTick = new Timer();
		Timer snowDraw = new Timer();

		Wind theWind = new Wind();

		int maxFlakes = 10;

		public void Init() {
			SnowLayer.Capacity = Console.WindowWidth;
			snowTick.Interval = 10;
			snowTick.Elapsed += SnowTicker;

			snowDraw.Interval = 100;
			snowDraw.Elapsed += SnowDrawer;

			theWind.VelX = 2;

			Console.CursorVisible = false;
		}

		public void Run() {
			snowTick.Start();
			snowDraw.Start();
			//Console.WriteLine(". , * + o ¤ # %");
			while (Console.ReadKey().Key != ConsoleKey.Q) { }
		}


		private void SnowDrawer(object o, EventArgs e) {
			Console.Clear();
			lock (locker) {
				SnowFlakes.ForEach(X => X.DoDraw());
			}

		}
		private void SnowTicker(object o, EventArgs e) {
			lock (locker) {
				SnowFlakes.RemoveAll(X => X.removeMe);
				
				if (SnowFlakes.Count != maxFlakes)
					SnowFlakes.Add(SpawnFlake());

				SnowFlakes.ForEach(X => X.DoTick(theWind));
			}
		}

		private SnowFlake SpawnFlake() {
			SnowFlake temp = new SnowFlake();


			return temp;
		}
	}
}
