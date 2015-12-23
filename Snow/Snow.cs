using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Snow {
	class SnowFlake {
        static Random randGen = new Random();
        static char[] models = new char[] { '.', ',', '*', '+', 'o', '¤', '#', '%'};
        static double flakeDragCoef = 100;
        static double maxVelX = 1;
		
		public double posX, posY;
        double velX, velY;
		int size = -1;
        int density = -1;
        int level = 0;

		public bool removeMe { get; set; }

		public SnowFlake() {
            int maxSize = models.Count();

            size = randGen.Next(maxSize);
            density = maxSize - size;
            level = randGen.Next(-1, 1);
			removeMe = false;

			posX = randGen.Next(Console.WindowWidth);
			posY = -5;
            velX = 0;// = SnowController.rand.Next(-1, 1);
            velY = density * 0.01;
		}

		public void DoTick(double WindX) {
			if (posY >= Console.WindowHeight) { removeMe = true; return; }

            if (WindX == 0) {
                velX -= (velX / flakeDragCoef);
            } else {
                if (velX > -(maxVelX / size) && velX < (maxVelX / size))
                    velX += (WindX / (density / 2)) / 300;
            }

			int rem = Console.WindowHeight - (int)posY;
			posY += (velY > rem ? rem : velY);

			posX += velX;
			if (posX < 0)
				posX = Console.WindowWidth + (int)posX;
			else if (posX >= Console.WindowWidth)
				posX = (int)posX % Console.WindowWidth;
		}

		public void DoDraw() {
            if (posY < 0) return;
            try {
                switch (level) {
                    case -1:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        break;
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                }

                //Console.CursorLeft = (int)posX;
                //Console.CursorTop = (int)posY;
                //Console.Write(models[size]);
                Printer.PrintCharAtX(models[size], (int)posX, (int)posY);
            } catch (Exception) { }
		}

	}

}
