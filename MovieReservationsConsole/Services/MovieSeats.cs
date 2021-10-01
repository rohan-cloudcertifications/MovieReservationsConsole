using MovieReservationsConsole.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieReservationsConsole.Services
{
    public class MovieSeats
    {
        public MovieSeats()
        {

        }

        public Root LoadJson()
        {
            using StreamReader r = new StreamReader("Data\\input.json");
            string json = r.ReadToEnd();

            return JsonConvert.DeserializeObject<Root>(json);
        }

        public VenueSeat GetSingleAvailableSeat(Root root)
        {
            Layout layout = root.Venue.Layout;
            List<VenueSeat> venueSeats = root.Seats.Values.ToList();

            int middleCol = (int)Math.Floor(layout.Columns / 2.0);

            VenueSeat foundSeat = new VenueSeat
            {
                Row = "-1",
                Column = -1
            };

            // Prioritizing rows, as audience would want to be closer,
            // even if they may have to sit at the corners of a given row.

            for (int r = 1; r < layout.Rows; r++)
            {
                for (int dist = 0; dist < middleCol; dist++)
                {
                    // check avilable seats by expanding from middle                  

                    if (venueSeats.Any(seat => seat.Row == IToA(r) && (seat.Column == middleCol + dist || seat.Column == middleCol - dist)))
                    {
                        foundSeat = venueSeats.FirstOrDefault(seat => seat.Row == IToA(r) && (seat.Column == middleCol + dist || seat.Column == middleCol - dist));
                        return foundSeat;
                    }
                }
            }

            return foundSeat;

        }

        public List<VenueSeat> GetMultipleAvailableSeats(Root root, int numRequestedSeats)
        {
            if (numRequestedSeats < 1) return new List<VenueSeat>();
            //if (numRequestedSeats == 1) return new List<VenueSeat>() { GetSingleAvailableSeat(root) };

            Layout layout = root.Venue.Layout;
            List<VenueSeat> venueSeats = root.Seats.Values.ToList();

            int middleCol = (int)Math.Floor(layout.Columns / 2.0);


            List<VenueSeat> foundSeats = new List<VenueSeat>();
            List<VenueSeat> possibleSeats = new List<VenueSeat>();

            // Prioritizing rows, as audience would want to be closer,
            // even if they may have to sit at the corners of a given row.

            for (int r = 1; r < layout.Rows; r++)
            {
                if (venueSeats.Count(seat => seat.Row == IToA(r)) >= numRequestedSeats) // if we can find enough available seats in current row ...
                {
                    int processedSeats = 0;
                    List<int> goodCols = venueSeats.Where(seat => seat.Row == IToA(r)).Select(seat => seat.Column).ToList();

                    if (goodCols.Contains(middleCol))
                    {
                        possibleSeats.Add(new VenueSeat { Row = IToA(r), Column = middleCol });
                        processedSeats++;

                        // check avilable seats by expanding from middle
                        for (int dist = 1; dist < middleCol; dist++)
                        {
                            if (goodCols.Contains(middleCol + dist) && goodCols.Contains(middleCol - dist) && processedSeats < numRequestedSeats)
                            {
                                possibleSeats.Add(new VenueSeat { Row = IToA(r), Column = middleCol + dist });
                                possibleSeats.Add(new VenueSeat { Row = IToA(r), Column = middleCol - dist });
                                processedSeats += 2;
                            }

                            if (processedSeats >= numRequestedSeats)
                            {
                                foundSeats = possibleSeats;
                                return foundSeats;
                            }
                        }
                    }

                    // check right
                    possibleSeats = CheckRightConsecutiveSeats(numRequestedSeats, layout, middleCol, r, goodCols);
                    if (possibleSeats.Count >= numRequestedSeats) return possibleSeats;

                    // check left
                    possibleSeats = CheckLeftConsecutiveSeats(numRequestedSeats, middleCol, r, goodCols);
                    if (possibleSeats.Count >= numRequestedSeats) return possibleSeats;
                }
            }

            return foundSeats;
        }

        private List<VenueSeat> CheckRightConsecutiveSeats(int numRequestedSeats, Layout layout, int middleCol, int row, List<int> goodCols)
        {
            int processedSeats = 0;

            List<VenueSeat> possibleSeats = new List<VenueSeat>();

            if (goodCols.Contains(middleCol))
            {
                possibleSeats.Add(new VenueSeat { Row = IToA(row), Column = middleCol });
                processedSeats++;
            }

            for (int c = middleCol + 1; c < layout.Columns; c++)
            {
                if (goodCols.Contains(c))
                {
                    if (goodCols.Contains(c - 1))
                    {
                        processedSeats++;
                        possibleSeats.Add(new VenueSeat { Row = IToA(row), Column = c });

                        if (processedSeats >= numRequestedSeats)
                        {
                            return possibleSeats;
                        }
                    }
                    else
                    {
                        // start again
                        processedSeats = 1;
                        possibleSeats = new List<VenueSeat>() { new VenueSeat { Row = IToA(row), Column = c } };
                    }
                }
                else
                {
                    processedSeats = 0;
                }
            }

            return possibleSeats;
        }

        private List<VenueSeat> CheckLeftConsecutiveSeats(int numRequestedSeats, int middleCol, int row, List<int> goodCols)
        {
            int processedSeats = 0;
            List<VenueSeat> possibleSeats = new List<VenueSeat>();

            if (goodCols.Contains(middleCol))
            {
                possibleSeats.Add(new VenueSeat { Row = IToA(row), Column = middleCol });
                processedSeats++;
            }

            for (int c = middleCol - 1; c >= 0; c--)
            {
                if (goodCols.Contains(c))
                {
                    if (goodCols.Contains(c + 1))
                    {
                        processedSeats++;
                        possibleSeats.Add(new VenueSeat { Row = IToA(row), Column = c });

                        if (processedSeats >= numRequestedSeats)
                        {
                            return possibleSeats;
                        }
                    }
                    else
                    {
                        // start again
                        processedSeats = 1;
                        possibleSeats = new List<VenueSeat>() { new VenueSeat { Row = IToA(row), Column = c } };
                    }
                }
                else
                {
                    processedSeats = 0;
                }
            }

            return possibleSeats;
        }

        private int AToI(string str)
        {
            char c = str[0];
            return (c < 97 ? c - 64 : c - 96);
        }
        private string IToA(int num)
        {
            string c = (char)(num + 96) + "";
            return c;
        }
    }
}
