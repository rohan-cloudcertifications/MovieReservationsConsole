using MovieReservationsConsole.Models;
using MovieReservationsConsole.Services;
using System;
using System.Collections.Generic;

namespace MovieReservationsConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            MovieSeats ms = new MovieSeats();
            Root root = ms.LoadJson();
            Venue testVenue = root.Venue;

            bool test1 = testVenue.Layout.Rows > 1;
            bool test2 = testVenue.Layout.Rows < 27;

            VenueSeat seat = ms.GetSingleAvailableSeat(root);

            

            List<VenueSeat> seats = ms.GetMultipleAvailableSeats(root, 1);


            Console.WriteLine(test1 ? "Passed" : "Failed");
            Console.WriteLine(test2 ? "Passed" : "Failed");
            Console.WriteLine(test1 ? "Passed" : "Failed");
            Console.WriteLine(test1 ? "Passed" : "Failed");
            Console.WriteLine(test1 ? "Passed" : "Failed");
            Console.WriteLine(test1 ? "Passed" : "Failed");
            Console.WriteLine(test1 ? "Passed" : "Failed");


        }
    }
}
