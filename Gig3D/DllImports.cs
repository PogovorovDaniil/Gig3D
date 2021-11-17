using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Gig3D
{
    internal class DllMethod
    {
        //WinApi
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern unsafe bool CheckMessage(IntPtr hwnd, int* message);
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern unsafe bool FinishCheckMessage(IntPtr* hwnd, int hwndcount, int* message);
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern unsafe IntPtr SetWindowC(StringBuilder title, StringBuilder className, int width, int height);
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr GetPaintStructure(IntPtr hwnd);
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern void Paint(IntPtr hwnd, IntPtr hdc);
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern void SetPos(IntPtr hwnd, int X, int Y);
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern void SetSize(IntPtr hwnd, int Width, int Height);
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern unsafe void GetRect(IntPtr hwnd, int* lrect);
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr GetMSG();
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern unsafe void ShowC(uint* bmp, int width, int height, IntPtr hdc);
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint GetWindowState(IntPtr hwnd);


        //amp
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern void RayCast(IntPtr abmp, IntPtr polygons, IntPtr lenghts, float x, float y, float z, float angleX, float angleY, float angleZ, float cx, float cy, float cz, float cangleX, float cangleY, float cangleZ);
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern void OverDraw(IntPtr abmp1, IntPtr abmp2, int width2, int height2, float x, float y, float angle, float offsetx, float offsety);
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern void Clear(IntPtr bmp, uint color);
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern unsafe IntPtr GetArrayView(uint* bmp, int width, int height);
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern unsafe IntPtr GetFloatArrayView(float* bmp, int width, int height);
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern unsafe IntPtr GetArrayViewPolygons(float* polygons, int count);
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern void DeleteArrayView(IntPtr a);


        //applied
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern double TimeNow();
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern unsafe uint* GetUintMem(int mem);
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern unsafe uint* GetBmpMem(IntPtr hbitmap, int width, int height);

        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern unsafe float* GetFloatMem(int mem);
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern unsafe void DelMem(uint* arr);
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int GetKeyIsDown(int key);
        [DllImport(@"Gig3Dbase.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern unsafe int GetMousePosC(int* pos);
    }
}