using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Snow {
    class Printer {
        private static object theLock = new object();
        private static object drawLock = new object();

        private static StreamWriter stdout = new StreamWriter(Console.OpenStandardOutput());
        private static Timer RefreshRate = new Timer();

        private static List<StringBuilder> buffer = new List<StringBuilder>();
        private static List<StringBuilder> backBuffer = new List<StringBuilder>();

        static Printer() {
            stdout.AutoFlush = false;
            Console.SetOut(stdout);

            for (int i = 0; i < Console.WindowHeight; i++)
                buffer.Add(new StringBuilder(new String(' ', Console.WindowWidth)));

            for (int i = 0; i < Console.WindowHeight; i++)
                backBuffer.Add(new StringBuilder(new String(' ', Console.WindowWidth)));

            RefreshRate.Interval = 100;
            RefreshRate.Elapsed += DrawScreen;
            RefreshRate.AutoReset = true;
            RefreshRate.Enabled = true;
            RefreshRate.Start();
        }

        private static void ClearBuffer() {
            for (int i = 0; i < buffer.Count; i++) {
                buffer[i].Clear();
                buffer[i].Append(new String(' ', Console.WindowWidth));
            }
        }

        private static void SwapBuffers() {
            lock(theLock) {
                var temp = buffer;
                buffer = backBuffer;
                backBuffer = temp;
            }
        }

        private static void DrawScreen(Object o, EventArgs e) {
            lock (drawLock) {
                for (int i = 0; i < buffer.Count; i++) {
                    stdout.Write(buffer[i].ToString());
                    stdout.Write('\n');
                }

                stdout.Flush();
                Console.SetCursorPosition(0, 0);

                ClearBuffer();
                SwapBuffers();
            }
        }

        public static void PrintCharAtX(Char s, int x, int y) {
            //TODO: error checking
            lock (theLock) {
                backBuffer[y][x] = s;
            }
        }

    }
}
