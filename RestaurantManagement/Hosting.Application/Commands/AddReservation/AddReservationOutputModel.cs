namespace RestaurantManagement.Hosting.Application.Commands.AddReservation
{
    public class AddReservationOutputModel
    {
        public AddReservationOutputModel(int resId) 
        {
            this.ReservationId = resId;
        }

        public int ReservationId { get; }
    }
}
