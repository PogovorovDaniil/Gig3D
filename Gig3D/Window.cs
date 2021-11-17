using System;
using System.Text;

namespace Gig3D
{
    public class Window
    {
        public delegate void KeyEventHandler(object sender, KeyEvents e);
        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;

        public delegate void MouseEventHandler(object sender, MouseKeyEvents e);
        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseUp;
        public event MouseEventHandler WheelRoll;

        public delegate void CharEventHandler(object sender, char e);
        public event CharEventHandler CharGet;

        private unsafe uint* bg;
        private IntPtr av_bg;

        private unsafe float* lenghts;
        private IntPtr av_lenghts;
        public int width { private set; get; }
        public int height { private set; get; }

        public float cx;
        public float cy;
        public float cz;
        public float cangleX;
        public float cangleY;
        public float cangleZ;

        private static IntPtr[] hwnds = new IntPtr[0];

        private IntPtr hwnd;
        private IntPtr hdc;
        private static IntPtr msg = DllMethod.GetMSG();
        private static int id = 0;
        public Window(string title, int width = 640, int height = 480)
        {
            this.width = width;
            this.height = height;

            cx = 0;
            cy = 0;
            cz = 0;
            cangleX = 0;
            cangleY = 0;
            cangleZ = 0;

            id++;
            hwnd = DllMethod.SetWindowC(new StringBuilder(title), new StringBuilder("GWindow" + id.ToString()), width, height);
            hdc = DllMethod.GetPaintStructure(hwnd);

            IntPtr[] newhwnds = new IntPtr[hwnds.Length + 1];
            for (int i = 0; i < hwnds.Length; i++)
            {
                newhwnds[i] = hwnds[i];
            }
            newhwnds[hwnds.Length] = hwnd;
            hwnds = newhwnds;

            unsafe
            {
                bg = DllMethod.GetUintMem(width * height);
                av_bg = DllMethod.GetArrayView(bg, width, height);
                DllMethod.Clear(av_bg, new Color(230, 200, 200).ARGB);

                lenghts = DllMethod.GetFloatMem(width * height);
                av_lenghts = DllMethod.GetFloatArrayView(lenghts, width, height);
            }

            hdc = DllMethod.GetPaintStructure(hwnd);
        }
        public void Clear()
        {
            Clear(new Color(255, 255, 255));
        }
        public void Clear(Color color)
        {
            DllMethod.Clear(av_lenghts, 0);
            DllMethod.Clear(av_bg, color.ARGB);
        }
        public void Show()
        {
            unsafe
            {
                DllMethod.ShowC(bg, width, height, hdc);
                DllMethod.Paint(hwnd, hdc);
                hdc = DllMethod.GetPaintStructure(hwnd);
            }
        }
        public void CheckMessage()
        {
            EventMsg eventMsg;
            unsafe
            {
                int[] message = new int[3];
                fixed (int* mesg = &message[0])
                {
                    while (DllMethod.CheckMessage(hwnd, mesg))
                    {
                        //Console.WriteLine(hwnd + " " + mesg[0] + " <<");
                        eventMsg = new EventMsg(mesg[0], mesg[1], mesg[2]);
                        //Console.WriteLine(eventMsg.message);
                        switch (eventMsg.message)
                        {
                            case 0x0100:
                                if ((eventMsg.lParam >> 30) == 0) KeyDown?.Invoke(this, new KeyEvents((KeyEvents.KeyCode)eventMsg.wParam));
                                break;
                            case 0x0101:
                                KeyUp?.Invoke(this, new KeyEvents((KeyEvents.KeyCode)eventMsg.wParam));
                                break;
                            case 0x0102:
                                CharGet?.Invoke(this, (char)eventMsg.wParam);
                                break;
                            case 0x0201:
                                MouseDown?.Invoke(this, new MouseKeyEvents(MouseKeyEvents.MouseButton.Lbutton, (eventMsg.wParam & 0x0004) > 0, (eventMsg.wParam & 0x0008) > 0, eventMsg.lParam & 0xffff, eventMsg.lParam / 0x10000));
                                break;
                            case 0x0204:
                                MouseDown?.Invoke(this, new MouseKeyEvents(MouseKeyEvents.MouseButton.Rbutton, (eventMsg.wParam & 0x0004) > 0, (eventMsg.wParam & 0x0008) > 0, eventMsg.lParam & 0xffff, eventMsg.lParam / 0x10000));
                                break;
                            case 0x0207:
                                MouseDown?.Invoke(this, new MouseKeyEvents(MouseKeyEvents.MouseButton.Mbutton, (eventMsg.wParam & 0x0004) > 0, (eventMsg.wParam & 0x0008) > 0, eventMsg.lParam & 0xffff, eventMsg.lParam / 0x10000));
                                break;
                            case 0x0202:
                                MouseUp?.Invoke(this, new MouseKeyEvents(MouseKeyEvents.MouseButton.Lbutton, (eventMsg.wParam & 0x0004) > 0, (eventMsg.wParam & 0x0008) > 0, eventMsg.lParam & 0xffff, eventMsg.lParam / 0x10000));
                                break;
                            case 0x0205:
                                MouseUp?.Invoke(this, new MouseKeyEvents(MouseKeyEvents.MouseButton.Rbutton, (eventMsg.wParam & 0x0004) > 0, (eventMsg.wParam & 0x0008) > 0, eventMsg.lParam & 0xffff, eventMsg.lParam / 0x10000));
                                break;
                            case 0x0208:
                                MouseUp?.Invoke(this, new MouseKeyEvents(MouseKeyEvents.MouseButton.Mbutton, (eventMsg.wParam & 0x0004) > 0, (eventMsg.wParam & 0x0008) > 0, eventMsg.lParam & 0xffff, eventMsg.lParam / 0x10000));
                                break;
                            case 0x020A:
                                WheelRoll?.Invoke(this, new MouseKeyEvents(eventMsg.wParam / 0x10000, (eventMsg.wParam & 0x0004) > 0, (eventMsg.wParam & 0x0008) > 0, eventMsg.lParam & 0xffff, eventMsg.lParam / 0x10000));
                                break;
                            default:
                                break;
                        }
                    }
                }
                message = new int[4];
                fixed (IntPtr* ptrhwnds = &hwnds[0]) fixed (int* mesg = &message[0])
                {
                    while (DllMethod.FinishCheckMessage(ptrhwnds, hwnds.Length, mesg))
                    {
                        //Console.WriteLine(mesg[3] + " " + mesg[0]);
                    }
                }
            }
        }
        public Vector<int> GetSize()
        {
            int[] rect = new int[4];
            unsafe
            {
                fixed (int* lrect = &rect[0])
                    DllMethod.GetRect(hwnd, lrect);
            }
            return new Vector<int>(rect[2] - rect[0], rect[3] - rect[1]);
        }
        public Vector<int> GetPosition()
        {
            int[] rect = new int[4];
            unsafe
            {
                fixed (int* lrect = &rect[0])
                    DllMethod.GetRect(hwnd, lrect);
            }
            return new Vector<int>(rect[0], rect[1]);
        }
        public void SetPosition(int X, int Y)
        {
            DllMethod.SetPos(hwnd, X, Y);
        }
        public void SetSize(int Width, int Height)
        {
            width = Width;
            height = Height;
            DllMethod.SetSize(hwnd, Width, Height);

            unsafe
            {
                DllMethod.DelMem(bg);
                bg = DllMethod.GetUintMem(width * height);
                DllMethod.DeleteArrayView(av_bg);
                av_bg = DllMethod.GetArrayView(bg, width, height);
            }
        }
        public void Draw(Object3D gObject)
        {
            DllMethod.RayCast(av_bg, gObject.array_view, av_lenghts, gObject.x, gObject.y, gObject.z, gObject.angleX, gObject.angleY, gObject.angleZ, cx, cy, cz, cangleX, cangleY, cangleZ);
        }
        public void Draw(Sprite gSprite)
        {
            DllMethod.OverDraw(av_bg, gSprite.array_view, gSprite.width, gSprite.height, gSprite.x, gSprite.y, gSprite.angle, gSprite.offsetx, gSprite.offsety);
        }
        public bool isOpen()
        {
            //Console.WriteLine(DllMethod.GetWindowState(hwnd));
            return DllMethod.GetWindowState(hwnd) != 0;
        }
        public static bool GetKeyState(KeyEvents.KeyCode key)
        {
            return DllMethod.GetKeyIsDown((int)key) < 0;
        }
        public static Vector<int> GetMousePos()
        {
            int[] posxy = new int[2];
            unsafe
            {
                fixed (int* pos = &posxy[0])
                    DllMethod.GetMousePosC(pos);
            }
            return new Vector<int>(posxy[0], posxy[1]);
        }
        public Vector<int> GetWinMousePos()
        {
            return GetMousePos() - GetPosition();
        }
    }
}
