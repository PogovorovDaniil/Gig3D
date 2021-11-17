using System;

namespace Gig3D
{
    public class Color
    {
        public uint ARGB;
        public byte A
        {
            get
            {
                return (byte)((ARGB / 256 * 256 * 256) % 256);
            }
        }
        public byte R
        {
            get
            {
                return (byte)((ARGB / 256 * 256) % 256);
            }
        }
        public byte G
        {
            get
            {
                return (byte)((ARGB / 256) % 256);
            }
        }
        public byte B
        {
            get
            {
                return (byte)(ARGB % 256);
            }
        }
        public Color(byte R, byte G, byte B, byte A = 255)
        {
            ARGB = B + (uint)G * 256 + (uint)R * 256 * 256 + (uint)A * 256 * 256 * 256;
        }
    }
}
