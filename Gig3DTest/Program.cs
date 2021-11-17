using System;
using Gig3D;

namespace Gig3DTest
{
    class Program
    {
        static Window gWindow;

        //static Sprite gSprite1;
        //static Sprite gSprite2;
        //static Sprite gSprite3;

        static Object3D gObject1;
        static Object3D gObject2;
        static Object3D gObject3;
        static Object3D gObject4;

        static void Main(string[] args)
        {
            gWindow = new Window("Squares", 640, 480);
            gWindow.KeyDown += GWindow_KeyDown;
            gWindow.KeyUp += GWindow_KeyUp;
            gWindow.CharGet += GWindow_CharGet;
            gWindow.MouseDown += GWindow_MouseDown;
            gWindow.MouseUp += GWindow_MouseUp;
            gWindow.WheelRoll += GWindow_WheelRoll;

            //gSprite1 = new Sprite(0, 0, 0, 100, 100, new Color(255, 0, 0));
            //gSprite2 = new Sprite(0, 0, 0, 100, 100, new Color(0, 255, 0, 128));
            //gSprite3 = new Sprite(0, 0, 0, 100, 100, new Color(0, 0, 255));

            DeltaTimer timer = new DeltaTimer();
            double srtime = 230;
            double dt = 0;

            gObject1 = new Object3D(0, 0, -6, 0, 0, 0);
            {
                gObject1.StartDraw();
                gObject1.AddPoly(-1, -1, -1, 1, -1, -1, 1, 1, -1);
                gObject1.AddPoly(-1, -1, -1, -1, 1, -1, 1, 1, -1);

                gObject1.AddPoly(-1, -1, 1, 1, -1, 1, 1, 1, 1);
                gObject1.AddPoly(-1, -1, 1, -1, 1, 1, 1, 1, 1);

                gObject1.AddPoly(-1, -1, -1, 1, -1, -1, 1, -1, 1);
                gObject1.AddPoly(-1, -1, -1, -1, -1, 1, 1, -1, 1);

                gObject1.AddPoly(-1, 1, -1, 1, 1, -1, 1, 1, 1);
                gObject1.AddPoly(-1, 1, -1, -1, 1, 1, 1, 1, 1);

                gObject1.AddPoly(-1, -1, -1, -1, 1, -1, -1, 1, 1);
                gObject1.AddPoly(-1, -1, -1, -1, -1, 1, -1, 1, 1);

                gObject1.AddPoly(1, -1, -1, 1, 1, -1, 1, 1, 1);
                gObject1.AddPoly(1, -1, -1, 1, -1, 1, 1, 1, 1);
                gObject1.StopDraw();
            }

            gObject2 = new Object3D(0, 0, 6, 0, 0, 0);
            {
                gObject2.StartDraw();
                gObject2.AddPoly(-1, -1, 0, 1, -1, 0, 0, 0, 1);
                gObject2.AddPoly(-1, -1, 0, -1, 1, 0, 0, 0, 1);
                gObject2.AddPoly(1, 1, 0, 1, -1, 0, 0, 0, 1);
                gObject2.AddPoly(1, 1, 0, -1, 1, 0, 0, 0, 1);

                gObject2.AddPoly(-1, -1, 0, 1, -1, 0, 0, 0, -1);
                gObject2.AddPoly(-1, -1, 0, -1, 1, 0, 0, 0, -1);
                gObject2.AddPoly(1, 1, 0, 1, -1, 0, 0, 0, -1);
                gObject2.AddPoly(1, 1, 0, -1, 1, 0, 0, 0, -1);
                gObject2.StopDraw();
            }

            gObject3 = new Object3D(6, 0, 0, 0, 0, 0);
            {
                gObject3.StartDraw();
                gObject3.AddPoly(-1, -1, 0, 1, -1, 0, 1, 0, 1);
                gObject3.AddPoly(-1, -1, 0, 1, -1, 0, 1, 0, -1);

                gObject3.AddPoly(1, 0, -1, 1, 0, 1, -1, -1, 0);
                gObject3.AddPoly(1, 0, -1, 1, 0, 1, 1, -1, 0);
                gObject3.StopDraw();

            }

            gObject4 = new Object3D(-6, 0, 0, 0, 0, 0);
            {
                gObject4.StartDraw();
                gObject4.AddPoly(-1, -1, 0, 1, -1, 0, 0, 1, 0);
                gObject4.StopDraw();
            }

            while (gWindow.isOpen())
            {
                gWindow.CheckMessage();

                dt = timer.deltaTime;
                //if (dt > 1.0 / 60)
                {
                    timer.SetLastTime();
                    srtime = (0.9 * srtime + 0.1 * dt);
                    Console.WriteLine(1 / srtime);
                    gWindow.Clear(new Color(0, 0, 0));

                    //gSprite1.angle += 0.02f;
                    //gSprite1.x = gWindow.width / 2 - 70;
                    //gSprite1.y = gWindow.height / 2;

                    //gSprite2.angle -= 0.02f;
                    //gSprite2.x = gWindow.width / 2;
                    //gSprite2.y = gWindow.height / 2;

                    //gSprite3.angle += 0.02f;
                    //gSprite3.x = gWindow.width / 2 + 70;
                    //gSprite3.y = gWindow.height / 2;

                    //gWindow.Draw(gSprite1);
                    //gWindow.Draw(gSprite3);
                    //gWindow.Draw(gSprite2);
                    gWindow.cangleY += 0.01f;

                    gObject1.angleX += 0.01f;
                    gObject1.angleY += 0.01f;
                    gWindow.Draw(gObject1);

                    gObject2.angleX -= 0.01f;
                    gObject2.angleY -= 0.01f;
                    gWindow.Draw(gObject2);

                    gObject3.angleX -= 0.01f;
                    gObject3.angleY += 0.01f;
                    gWindow.Draw(gObject3);

                    gObject4.angleX += 0.01f;
                    gObject4.angleY -= 0.01f;
                    gWindow.Draw(gObject4);

                    gWindow.Show();
                }
            }
        }

        private static void GWindow_WheelRoll(object sender, MouseKeyEvents e)
        {
            Console.WriteLine("Wheel - " + e.whell_delta + " " + e.x + " " + e.y + " " + e.ctrl + " " + e.shift);
        }
        private static void GWindow_MouseUp(object sender, MouseKeyEvents e)
        {
            Console.WriteLine("Up - " + e.button.ToString() + " " + e.x + " " + e.y + " " + e.ctrl + " " + e.shift);
        }
        private static void GWindow_MouseDown(object sender, MouseKeyEvents e)
        {
            Console.WriteLine("Down - " + e.button.ToString() + " " + e.x + " " + e.y + " " + e.ctrl + " " + e.shift);
            //Console.WriteLine(gWindow.GetWinMousePos());
        }
        private static void GWindow_CharGet(object sender, char e)
        {
            //Console.WriteLine(e);
        }
        private static void GWindow_KeyDown(object sender, KeyEvents e)
        {
            //if (e.Key == KeyEvents.KeyCode.Q) gWindow.SetSize(480, 640);
        }
        private static void GWindow_KeyUp(object sender, KeyEvents e)
        {
            //if (e.Key == KeyEvents.KeyCode.Q) gWindow.SetSize(640, 480);
        }
    }
}
