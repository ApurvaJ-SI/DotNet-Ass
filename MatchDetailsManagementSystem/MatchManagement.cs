using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MatchDetailsManagementSystem
{
    internal class MatchManagement
    {
        List<MatchDetails> matchDetails;
        public MatchManagement()
        {
            matchDetails = new List<MatchDetails>();
        }


        
        public void AddMatch(MatchDetails matchDetail)
        {
            if (IsValidMatch(matchDetail))
            {
                matchDetails.Add(matchDetail);
            }
            else
            {
                Console.WriteLine("Enter correct value");
            }
        }

        public void DisplayMatch()
        {
            if (matchDetails.Count != null)
            {
                foreach(MatchDetails matchDetail in matchDetails)
                {
                    Console.WriteLine(matchDetail);
                }
            }
            else
            {
                Console.WriteLine("Match details is empty");
            }
        }

        public void DisplayMatchDetails()
        {
            Console.WriteLine("Enter the id of Match : ");
            int Id = int.Parse(Console.ReadLine());
            MatchDetails matchDetail = matchDetails.FirstOrDefault(a => a.MatchId == Id);
            if (matchDetail != null)
            {
                Console.WriteLine(matchDetail);
            }
            else
            {
                Console.WriteLine("Matches no found");
            }
        }

        
        public void UpdateMatchScore()
        {
            Console.WriteLine("Enter Match Id:");
            int updatematchid = int.Parse(Console.ReadLine());

            MatchDetails matchDetail = matchDetails.FirstOrDefault(a => a.MatchId == updatematchid);
            if (matchDetail != null)
            {
                Console.WriteLine("Enter Home Team Score:");
                int updatehomeTeamScore = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Away Team Score:");
                int updateawayTeamScore = int.Parse(Console.ReadLine());

                matchDetail.HomeTeamScore = updatehomeTeamScore;
                matchDetail.AwayTeamScore = updateawayTeamScore;
                Console.WriteLine("Match Detail updated successfully");
            }
            else
            {
                Console.WriteLine("Match Detail not found");
            }
        }

        public void RemoveMatch()
        {
            Console.WriteLine("Enter the MatchId to remove");
            int mid = int.Parse(Console.ReadLine());
            MatchDetails matchDetail = matchDetails.FirstOrDefault(a => a.MatchId == mid);
            if (matchDetail != null)
            {
                matchDetails.Remove(matchDetail);
                Console.WriteLine("Match Detail deleted successfully");
            }
            else
            {
                Console.WriteLine("Match Detail not found");
            }
        }

        public void SortMatches()
        {
            Console.WriteLine("Sort by: (date/sport/location)");
            string criteria = Console.ReadLine();
            Console.WriteLine("Ascending? (true/false)");
            bool ascending = bool.Parse(Console.ReadLine());
            switch (criteria.ToLower())
            {
                case "date":
                    matchDetails = ascending ? matchDetails.OrderBy(m => m.MatchDateTime).ToList() : matchDetails.OrderByDescending(m => m.MatchDateTime).ToList();
                    break;
                case "sport":
                    matchDetails = ascending ? matchDetails.OrderBy(m => m.Sport).ToList() : matchDetails.OrderByDescending(m => m.Sport).ToList();
                    break;
                case "location":
                    matchDetails = ascending ? matchDetails.OrderBy(m => m.Location).ToList() : matchDetails.OrderByDescending(m => m.Location).ToList();
                    break;
                default:
                    Console.WriteLine("Invalid sorting criteria.");
                    break;
            }
        }

        public List<MatchDetails> FilterMatches()
        {
            Console.WriteLine("Filter by: (sport/location/daterange)");
            string criteria = Console.ReadLine();
            Console.WriteLine("Enter value: ");
            string value = Console.ReadLine();
            switch (criteria.ToLower())
            {
                case "sport":
                    return matchDetails.Where(m => m.Sport.Equals(value, StringComparison.OrdinalIgnoreCase)).ToList();
                case "location":
                    return matchDetails.Where(m => m.Location.Equals(value, StringComparison.OrdinalIgnoreCase)).ToList();
                case "daterange":
                    DateTime startDate;
                    DateTime endDate;
                    if (DateTime.TryParse(value.Split('-')[0], out startDate) && DateTime.TryParse(value.Split('-')[1], out endDate))
                    {
                        return matchDetails.Where(m => m.MatchDateTime >= startDate && m.MatchDateTime <= endDate).ToList();
                    }
                    else
                    {
                        Console.WriteLine("Invalid date range format. Use format 'yyyy-mm-dd - yyyy-mm-dd'.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid filtering criteria.");
                    break;
            }

            return new List<MatchDetails>();
        }

        public void CalculateStatistics(string criteria)
        {
            switch (criteria.ToLower())
            {
                case "averagescore":
                    double homeAvg = matchDetails.Average(m => m.HomeTeamScore);
                    double awayAvg = matchDetails.Average(m => m.AwayTeamScore);
                    Console.WriteLine($"Average Score - Home: {homeAvg}, Away: {awayAvg}");
                    break;
                case "highestscore":
                    int highestHomeScore = matchDetails.Max(m => m.HomeTeamScore);
                    int highestAwayScore = matchDetails.Max(m => m.AwayTeamScore);
                    Console.WriteLine($"Highest Score - Home: {highestHomeScore}, Away: {highestAwayScore}");
                    break;
                case "lowestscore":
                    int lowestHomeScore = matchDetails.Min(m => m.HomeTeamScore);
                    int lowestAwayScore = matchDetails.Min(m => m.AwayTeamScore);
                    Console.WriteLine($"Lowest Score - Home: {lowestHomeScore}, Away: {lowestAwayScore}");
                    break;
                default:
                    Console.WriteLine("Invalid statistics criteria.");
                    break;
            }
        }

        public List<MatchDetails> SearchMatches(string keyword)
        {
            return matchDetails.Where(m =>
                m.Sport.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                m.HomeTeam.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                m.AwayTeam.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                m.Location.Contains(keyword, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        private bool IsValidMatch(MatchDetails match)
        {
            if (match.MatchId <= 0 || matchDetails.Any(m => m.MatchId == match.MatchId))
                return false;

            if (string.IsNullOrWhiteSpace(match.Sport))
                return false;

            if (match.MatchDateTime <= DateTime.Now)
                return false;

            if (string.IsNullOrWhiteSpace(match.Location))
                return false;

            if (string.IsNullOrWhiteSpace(match.HomeTeam) || string.IsNullOrWhiteSpace(match.AwayTeam) || match.HomeTeam == match.AwayTeam)
                return false;

            if (match.HomeTeamScore < 0 || match.AwayTeamScore < 0)
                return false;

            return true;
        }

    }
}
