

namespace UML_diagram
{
    public class Director
    {
        public void MakeSUV(IBuilder builder)
        {
            builder.Reset();
            builder.SetSeats(6);
            builder.SetEngine("Any engine");
            builder.SetTripComputer();
            builder.SetGPS();
        }
        public void MakeSportsCar(IBuilder builder)
        {
            builder.Reset();
            builder.SetSeats(4);
            builder.SetEngine("Any engine");
            builder.SetTripComputer();
            builder.SetGPS();
        }
    }
}
