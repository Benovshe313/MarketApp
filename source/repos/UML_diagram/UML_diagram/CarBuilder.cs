

namespace UML_diagram
{
    public class CarBuilder:IBuilder
    {
        private Car car;
        public CarBuilder()
        {
            this.Reset();
        }
        public void Reset()
        {
            this.car = new Car();
        }
        public void SetSeats(int number)
        {
            this.car.Seats = number;
        }
        public void SetEngine(string engine)
        {
            this.car.Engine = engine;
        }
        public void SetTripComputer()
        {
            this.car.Computer = new Computer();
        }
        public void SetGPS()
        {
            this.car.GPS = new GPS();

        }
        public Car GetResult()
        {
            return this.car;
        }
    }
}
