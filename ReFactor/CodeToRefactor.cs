///// Refactoring
///// Performer: Andres Breton
///// Start Date: 05/05/2023 - 9:00 a.m.
///// End Date 05/05/2023 - 10:47 a.m.

using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingAssessment.Refactor
{
    public class People
    {
        ///// Removed unused code. - Andres
        public string Name { get; private set; }
        public DateTimeOffset DOB { get; private set; }

        public People(string name, DateTime dob)
        {
            Name = name;
            DOB = dob;
        }
    }

    public class BirthingUnit
    {
        private List<People> _people;

        public BirthingUnit()
        {
            _people = new List<People>();
        }

        ///// Improved the commentaries for the method making them more specific. Changed the variable name for a more informative one.
        /// <summary>
        /// Creates a list of people with random name (Bob or Betty) and a random DOB between 18 to 85 ages.
        /// </summary>
        /// <param name="numberOfPeople">Number of people that will be generated.</param>
        /// <returns>A list with the people created containing name and DOB for each.</returns>
        public List<People> GetPeople(int numberOfPeople)
        {
            ///// Changed the for to a while to improve performance on higher loads.
            var i = 0;
            while(i < numberOfPeople)
            {
                try
                {
                    // Creates a random Name
                    string name = string.Empty;
                    var random = new Random();

                    ///// Changed to a simplified convention since it is a short if and can help readability.
                    name = (random.Next(0, 1) == 0) ? "Bob" : "Betty";

                    // Adds new people to the list
                    ///// Use a new utility method to generate the DOB.
                    var dob = GetDOB(random.Next(18, 85));
                    _people.Add(new People(name, dob));
                }
                catch (Exception e)
                {
                    ///// Displays the error message in the command line for logging purposes, usually should be using a logging tool.
                    Console.WriteLine("An exception ({0}) occurred.",
                           e.GetType().Name);
                    Console.WriteLine("Message:\n   {0}\n", e.Message);
                    Console.WriteLine("Stack Trace:\n   {0}\n", e.StackTrace);

                    ///// Personalized the exception that will be thrown since it may stop the method to alert the user.
                    throw new Exception("An error ocurred while creating the people, please try again and if the error persist contact support.");
                }
                i++;
            }
            return _people;
        }

        ///// Added a detailed summary to the method to have a better understanding when using it.
        /// <summary>
        /// Searches the list of people created to find and return all the people named Bob. 
        /// It can return either any ages or only older than 30.
        /// </summary>
        /// <param name="olderThan30">It defines if the method should only return people older than 30.</param>
        /// <returns>A list with the people found with the name Bob and the age condition when applied.</returns>
        public IEnumerable<People> GetBobs(bool olderThan30)
        {
            ///// Saved the DOB target for 30 in a variable to improve readability.
            var dobFor30 = GetDOB(30);
            ///// Adjusted the condition for the age since it was looking for younger than 30.
            return olderThan30 ? _people.Where(x => x.Name == "Bob" && x.DOB < dobFor30) : _people.Where(x => x.Name == "Bob");
        }

        ///// Added a detailed summary to the method to have a better understanding when using it.
        /// <summary>
        /// Concatenates the name of the given person with the given last name and returns that full name. If the combination exceeds 
        /// 255 characters it gets truncated.
        /// </summary>
        /// <param name="person">The person that will have tha last name added.</param>
        /// <param name="lastName">Last name to add to the person.</param>
        /// <returns>The full combination of name and last name, truncated if it exceeded 255 characters.</returns>
        public string GetMarried(People p, string lastName)
        {
            if (lastName.Contains("test"))
                return p.Name;
            ///// Adjusted the condition since it wasn't correct and added the return to ensure this method is returning the truncated version.
            if ((p.Name.Length + lastName.Length) > 254)
                return (p.Name + " " + lastName).Substring(0, 255);

            return p.Name + " " + lastName;
        }
        
        ///// Added this utility to reuse instead of using the creation in each line which improves the cleanliness of the code.
        /// <summary>
        /// Utility method to calculate the DOB from any desired Age.
        /// </summary>
        /// <param name="targetAge">Desired age to get the DOB.</param>
        /// <returns>A DateTime value for the DOB requested.</returns>
        private DateTime GetDOB(int targetAge)
        {
            return DateTime.Now.Subtract(new TimeSpan(targetAge * 365, 0, 0, 0));
        }
    }
}