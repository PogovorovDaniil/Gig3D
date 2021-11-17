
namespace Gig3D
{
    public class DeltaTimer
    {
        private double lasttime;
        public DeltaTimer()
        {
            lasttime = DllMethod.TimeNow();
        }
        public void SetLastTime()
        {
            lasttime = DllMethod.TimeNow();
        }
        public double deltaTime
        {
            get
            {
                double nowtime = DllMethod.TimeNow();
                if (lasttime == 0)
                {
                    return 0;
                }
                else
                {
                    return nowtime - lasttime;
                }
            }
        }
    }
}
