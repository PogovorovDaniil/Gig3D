using System;
using System.Drawing;

namespace Gig3D
{
    public class Sprite
    {
        internal unsafe uint* texture;
        internal IntPtr array_view;
        public float x;
        public float y;
        public float angle;
        public int width;
        public int height;
        public int offsetx;
        public int offsety;
        public Sprite(float x, float y, float angle, int width, int height, Color color)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;
            this.width = width;
            this.height = height;
            offsetx = width / 2;
            offsety = height / 2;

            unsafe
            {
                texture = DllMethod.GetUintMem(width * height);
                array_view = DllMethod.GetArrayView(texture, width, height);
                DllMethod.Clear(array_view, color.ARGB);
            }
        }
        public Sprite(float x, float y, float angle, int width, int height, string texturePath)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;
            this.width = width;
            this.height = height;
            offsetx = width / 2;
            offsety = height / 2;

            Bitmap bmpPre = new Bitmap(texturePath);
            Bitmap bmp = new Bitmap(bmpPre, width, height);
            IntPtr hbmp = bmp.GetHbitmap();
            unsafe
            {
                texture = DllMethod.GetBmpMem(hbmp, width, height);
                array_view = DllMethod.GetArrayView(texture, width, height);
            }
        }
    }
}
