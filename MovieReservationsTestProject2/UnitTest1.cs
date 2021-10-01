using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieReservationsConsole;
using MovieReservationsConsole.Models;
using MovieReservationsConsole.Services;
using System.Collections.Generic;

namespace MovieReservationsTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        MovieSeats ms;
        Root root;

        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void Ensure_NumRows_GreaterThanOrEqualTo1()
        {
            this.ms = new MovieSeats();
            this.root = ms.LoadJson();

            Assert.IsTrue(this.root.Venue.Layout.Rows >= 1);
        } 
        
        [TestMethod]
        public void Ensure_NumRows_LessThan27()
        {
            this.ms = new MovieSeats();
            this.root = ms.LoadJson();

            Assert.IsTrue(this.root.Venue.Layout.Rows < 27);
        }
        
        [TestMethod]
        public void Ensure_GivenJson_SingleSeatExists()
        {
            this.ms = new MovieSeats();
            this.root = ms.LoadJson();

            VenueSeat seat = ms.GetSingleAvailableSeat(root);

            Assert.IsTrue(seat.Column > 0);
        }
        
        [TestMethod]
        public void Ensure_GivenJson_MultipleSeatsCanSelect1()
        {
            this.ms = new MovieSeats();
            this.root = ms.LoadJson();

            List<VenueSeat> seats = ms.GetMultipleAvailableSeats(root, 1);

            Assert.IsTrue(seats.Count == 1);
        }

      
    }
}
