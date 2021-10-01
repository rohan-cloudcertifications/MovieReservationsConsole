namespace MovieReservationsConsole
{
    public class VenueSeat
    {
        public string Id { get; set; }

        public string Row { get; set; }

        public int Column { get; set; }

        public string Status { get; set; } // could be enum too ...

    }
}
