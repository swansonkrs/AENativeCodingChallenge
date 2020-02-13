using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AEConsoleApp
{
    public class Challenge
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments found.");
                Console.WriteLine("Use help for a list of commands.");
                return;
            }
            switch (args[0])
            {
                // Print available commands
                case "help":
                    Console.WriteLine("To sum the evens from a list of numbers use: SumEvens 0,1,2...");
                    Console.WriteLine("To make a GET request use: GET [url]");
                    Console.WriteLine("To print a list of numbers with 500ms delay on one thread and 1000ms delay in reverse on another, use: DelayedPrint 0,1,2...");
                    Console.WriteLine("To get the current time from the local debug web server use: GetTime");
                    Console.WriteLine("To simulate errors use: GetTime 500 or GetTime timeout");
                    break;
                // Sum the even integers given a comma delimited list of numbers
                case "SumEvens":
                    if (args.Length == 1)
                        Console.WriteLine("Please provide a comma delimited list of numbers: SumEvens 0,1,2...");
                    else
                    {
                        var numbers = CreateListFromDelimitedString(args[1]);
                        var sum = SumEvens(numbers);
                        Console.WriteLine("The sum of even numbers in the input list is " + sum.ToString());
                    }
                    break;
                // Make an http GET request given a valid url
                case "GET":
                    if (args.Length > 1)
                    {
                        var urlArg = args[1];
                        if (Uri.TryCreate(urlArg, UriKind.Absolute, out Uri result) && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
                        {
                            var response = GETRequest(result);
                            Console.WriteLine(response);
                            break;
                        }
                    }
                    Console.WriteLine("Please provide a valid URL.");
                    break;
                // Print a list of numbers using 2 threads with different delays, one in reverse, and print the first to report each number
                case "DelayedPrint":
                    var invalidArgString = "Please provide a comma delimited list of numbers: DelayedPrint 0,1,2...";
                    if (args.Length == 1)
                        Console.WriteLine(invalidArgString);
                    else
                    {
                        var numbers = CreateListFromDelimitedString(args[1]);
                        if (numbers.Count == 0)
                            Console.WriteLine(invalidArgString);

                        await DelayedPrint(numbers);
                    }
                    break;
                // Make GET request to local debug web server
                case "GetTime":
                    var queryString = "";
                    if (args.Length > 1)
                        queryString = args[1];

                    GetTime(queryString);
                    break;
                // Make GET request with error parameter to simulate 500 error code
                case "SimulateError":
                    GetTime("error");
                    break;
                default:
                    Console.WriteLine("Invalid command. Use help for a list of valid commands.");
                    break;
            }
        }

        public static int SumEvens(List<int> numbers)
        {
            var sum = 0;
            foreach (var number in numbers)
            {
                if (number % 2 == 0)
                    sum += number;
            }

            return sum;
        }

        public static string GETRequest(Uri url, bool logResponse = false, string serverName = "localhost")
        {
            var startTime = DateTime.Now.ToString();
            var endTime = "";

            var request = WebRequest.Create(url);
            request.Timeout = 3000;
            try
            {
                var response = request.GetResponse();
                endTime = DateTime.Now.ToString();
                var sReader = new StreamReader(response.GetResponseStream());
                var responseText = sReader.ReadToEnd().Trim();

                if (logResponse)
                {
                    var httpCode = 0;
                    if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                        httpCode = 200;

                    LogResponse(startTime, endTime, httpCode, response, responseText);
                }

                return responseText;
            }
            catch (WebException ex)
            {
                if (logResponse)
                {
                    endTime = DateTime.Now.ToString();
                    var httpCode = 0;
                    if (ex.Status == WebExceptionStatus.Timeout)
                        httpCode = 408;
                    else if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.InternalServerError)
                        httpCode = 500;

                    LogResponse(startTime, endTime, httpCode, ex.Response, ex.Message);
                }
                return ex.Message;
            }
        }

        public static void GetTime(string queryString = "")
        {
            var response = GETRequest(new Uri("https://localhost:44342/Time?" + queryString), true);
            Console.WriteLine(response);
        }

        public static void LogResponse(string startTime, string endTime, int httpCode, WebResponse response, string responseText, string serverName = "localhost")
        {
            var errorCode = 0;

            switch (httpCode)
            {
                case 200:
                    errorCode = 1;
                    break;
                case 500:
                    errorCode = 2;
                    break;
                case 408:
                    errorCode = -999;
                    break;
                default:
                    break;
            }

            var connBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = serverName,
                InitialCatalog = "ae_code_challenge",
                IntegratedSecurity = true
            };

            var conn = new SqlConnection(connBuilder.ConnectionString);
            conn.Open();
            string cmdString = String.Format("INSERT into server_response_Log (ResponseID,StartTime,EndTime,StatusCode,ResponseText,ErrorCode) values('{0}','{1}','{2}',{3},'{4}',{5})", Guid.NewGuid(), startTime, endTime, httpCode, responseText, errorCode);
            var command = new SqlCommand(cmdString, conn);
            command.ExecuteNonQuery();
            command.Dispose();
            conn.Close();
        }

        public static async Task DelayedPrint(List<int> numbers)
        {
            var reported = new Dictionary<int, string>();
            // Create a copy of numbers in reverse order
            var numbersReverse = new List<int>(numbers);
            numbersReverse.Reverse();

            var forwardTask = PrintNumbersAsync(numbers, 500, "t1", reported);
            var reverseTask = PrintNumbersAsync(numbersReverse, 1000, "t2", reported);

            // Wait for both threads to report all numbers
            await Task.WhenAll(new[] { forwardTask, reverseTask });

            var reportResults = string.Empty;
            foreach (var num in numbers)
            {
                if (reported.ContainsKey(num))
                    reportResults += String.Format("{0}: {1}", num, reported[num]) + " ";

                // In case of duplicate numbers, remove key to prevent redundant results
                reported.Remove(num);
            }

            Console.WriteLine(reportResults.Trim());
        }

        public static async Task PrintNumbersAsync(List<int> numbers, int delay, string threadName, Dictionary<int, string> firstReported)
        {
            foreach (var num in numbers)
            {
                await Task.Delay(delay);
                Console.WriteLine(String.Format("{0}: {1}", threadName, num));

                // The first time a number is reported, track the thread which reported it
                if (!firstReported.ContainsKey(num))
                    firstReported.Add(num, threadName);
            }
        }

        public static List<int> CreateListFromDelimitedString(string delimitedString)
        {
            var numbers = new List<int>();
            var input = delimitedString.Split(',');
            for (int i = 0; i < input.Length; i++)
            {
                if (int.TryParse(input[i], out int num))
                    numbers.Add(num);
            }

            return numbers;
        }
    }
}
