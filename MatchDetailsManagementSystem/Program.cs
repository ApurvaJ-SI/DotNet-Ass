using MatchDetailsManagementSystem;

namespace MatchDetailsManagementSys
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MatchManagement matchManager = new MatchManagement();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Add a Match");
                Console.WriteLine("2. Display Matches");
                Console.WriteLine("3. Display Match Details");
                Console.WriteLine("4. Update Match Score");
                Console.WriteLine("5. Remove Match");
                Console.WriteLine("6. Sort Matches");
                Console.WriteLine("7. Filter Matches");
                Console.WriteLine("8. View Statistics");
                Console.WriteLine("9. Search Matches");
                Console.WriteLine("10. Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter Match Details:");
                        Console.WriteLine("Enter Match Id:");
                        int matchid = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Sport:");
                        string sport = Console.ReadLine();
                        Console.WriteLine("Enter MatchDateTime:");
                        DateTime matchdatetime = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Match Location:");
                        string location = Console.ReadLine();
                        Console.WriteLine("Enter Home Teams:");
                        string hometeam = Console.ReadLine();
                        Console.WriteLine("Enter Away Team:");
                        string awayteam = Console.ReadLine();
                        Console.WriteLine("Enter Home Team Score:");
                        int hometeamscore = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Away Team Score:");
                        int awayteamscore = int.Parse(Console.ReadLine());
                        MatchDetails newMatch = new MatchDetails(matchid, sport, matchdatetime, location, hometeam, awayteam, hometeamscore, awayteamscore);
                        matchManager.AddMatch(newMatch);
                        break;
                    case 2:
                        Console.WriteLine("Match Details : ");
                        matchManager.DisplayMatch();
                        break;
                    case 3:
                        //Console.WriteLine("Enter the id of Match : ");
                        //int Id = int.Parse(Console.ReadLine());
                        matchManager.DisplayMatchDetails();
                        break;
                    case 4:
                        matchManager.UpdateMatchScore();
                        break;
                    case 5:
                        matchManager.RemoveMatch();
                        break;
                    case 6:
                        matchManager.SortMatches();
                        matchManager.DisplayMatch();
                        break;
                    case 7:
                        List<MatchDetails> filteredMatches = matchManager.FilterMatches();
                        Console.WriteLine("\nFiltered Matches:");
                        foreach (var match in filteredMatches)
                        {
                            Console.WriteLine($"Match ID: {match.MatchId}, Sport: {match.Sport}, Date: {match.MatchDateTime}, Location: {match.Location}, Teams: {match.HomeTeam} vs {match.AwayTeam}, Scores: {match.HomeTeamScore} - {match.AwayTeamScore}");
                        }
                        break;
                    case 8:
                        Console.WriteLine("Calculate statistics for: (averagescore/highestscore/lowestscore)");
                        string statisticsCriteria = Console.ReadLine();
                        matchManager.CalculateStatistics(statisticsCriteria);
                        break;
                    case 9:
                        Console.WriteLine("Search for: ");
                        string keyword = Console.ReadLine();
                        List<MatchDetails> searchedMatches = matchManager.SearchMatches(keyword);
                        Console.WriteLine("\nSearched Matches:");
                        foreach (var match in searchedMatches)
                        {
                            Console.WriteLine($"Match ID: {match.MatchId}, Sport: {match.Sport}, Date: {match.MatchDateTime}, Location: {match.Location}, Teams: {match.HomeTeam} vs {match.AwayTeam}, Scores: {match.HomeTeamScore} - {match.AwayTeamScore}");
                        }
                        break;
                    case 10:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

    }
}