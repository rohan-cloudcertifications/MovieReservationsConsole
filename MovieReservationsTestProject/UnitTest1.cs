using NUnit.Framework;
using MovieReservationsConsole.Services;
using MovieReservationsConsole;

namespace MovieReservationsTestProject
{
    public class Tests
    {
        MovieSeats ms;
        Venue testVenue;

        [SetUp]
        public void Setup()
        {
            

        }

        [Test]
        public void Ensure_NumRows_GreaterThanOrEqualTo1()
        {
            this.ms = new MovieSeats();
            this.testVenue = ms.LoadJson();

            Assert.IsTrue(this.testVenue.VenueLayout.Rows >= 1);
        }

        [Test]
        public void Ensure_NumRows_Lessthan27()
        {
            this.ms = new MovieSeats();
            this.testVenue = ms.LoadJson();

            Assert.IsTrue(this.testVenue.VenueLayout.Rows < 27);
        }

        //[Test]
        //public void Test1()
        //{
        //    VenueSeat availableSeat = GetSingleAvailableSeat(this.testVenue);

        //    availableSeat.Column = 0;
        //}
    }
}