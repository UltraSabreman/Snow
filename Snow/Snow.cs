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
    /// <summary>
    /// We pretend each snowflake is a sphere.
    /// </summary>
	class SnowFlake {
        public bool removeMe { get; set; }
        
        Vec2 Pos;
        Vec2 Vel;

        //In Kilograms
        double Mass;
        //In Centimeters
        double Radius;

        public SnowFlake(Vec2 startPos, double startMass, double startRadius) {
            Mass = startMass;
            Radius = startRadius;
		
			removeMe = false;

            Pos = startPos;
		}

        public void DoTick(Wind theWind) {
            //1) Get gravity force
            //2) Get Air-Risistance force
            //3) Get wind Force
            //4) Add all together and get vel
            //5) Add vell (do check?)

            Vec2 gravForce = Mass * new Vec2(0, -9.8);

            //Drag calc from (http://www.grc.nasa.gov/WWW/k-12/airplane/dragsphere.html)
            // Drag = Cd * .5 * rho * V^2 * A
            // Cd: drag coeficent (0.47 for sphere)
            // V: velocity of sphere
            // A: reference area
            // rho: air density
            // Avarage air density: 1.2041 kg/m^3

            double AirDensity = 1.2041;
            double DragCoeffcient = 0.47;
            Vec2 AirDrag = DragCoeffcient * 0.5 * AirDensity * (Vel * Vel) * (Math.PI * (Radius * Radius));
            /*if (posY >= Console.WindowHeight) { removeMe = true; return; }



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
			}*/
        }

        public void DoDraw() {
			//Console.CursorLeft = posX;
			//Console.CursorTop = posY;
			//Console.Write(models[size]);
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
