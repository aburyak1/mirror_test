using iikoTransport.Service;

namespace iikoTransport.SbpService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApiHost.Start<Startup>(args);
        }
    }
}