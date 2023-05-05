///// Code Review
///// Performer: Andres Breton
///// Start Date: 04/05/2023 - 2:10 p.m.
///// End Date: 04/05/2023 - 2:43 p.m.

using System;
///// Be careful when adding references, this could break the project. 
///// Should Be: using System.Collections.Generic; - Andres
using System.Collegctions.Generic;
using System.Linq;

namespace Utility.Valocity.ProfileHelper
{
    public class People
    {
     private static readonly DateTimeOffset Under16 = DateTimeOffset.UtcNow.AddYears(-15);
     public string Name { get; private set; }
     public DateTimeOffset DOB { get; private set; }
     ///// This constructor method is unused, try not to save unused lines of coding. Same for line 15. - Andres
     public People(string name) : this(name, Under16.Date) { }
     public People(string name, DateTime dob) {
         Name = name;
         DOB = dob;
     }}

    public class BirthingUnit
    {
        /// <summary>
        /// MaxItemsToRetrieve
        /// </summary>
        private List<People> _people;

        public BirthingUnit()
        {
            _people = new List<People>();
        }

        ///// It is good to use a summary. Try to explain in a better way the functionality.
        ///// In this case, the param name you used is i, instead you used j in line 43, it could be confusing in the future. - Andres

        /// <summary>
        /// GetPeoples
        /// </summary>
        /// <param name="j"></param>
        /// <returns>List<object></returns>
        ///// Try to use a better variable name, for instance it could be numberOfPeople instead of i, is more informative. - Andres
        public List<People> GetPeople(int i)
        {
            ///// Try to avoid using for, specially for larger iterations as it would impact execution times. - Andres
            for (int j = 0; j < i; j++)
            {
                try
                {
                    ///// It is nice that you add explanation to the code, it is helpful.
                    ///// Try to write correctly when commenting, in larger or more complex code blocks it can be an issue.
                    ///// Should be: Random Name. - Andres
                    // Creates a dandon Name
                    string name = string.Empty;
                    var random = new Random();
                    if (random.Next(0, 1) == 0) {
                        name = "Bob";
                    }
                    else {
                        name = "Betty";
                    }
                    // Adds new people to the list

                    ///// You could split the DateTime value creation to have a more readable code, although is not necessary. - Andres
                    _people.Add(new People(name, DateTime.UtcNow.Subtract(new TimeSpan(random.Next(18, 85) * 356, 0, 0, 0))));
                }
                catch (Exception e)
                {
                    // Dont think this should ever happen

                    ///// It could so it is good to have added a catch in there, you can handle it externally if necessary. - Andres
                    throw new Exception("Something failed in user creation");
                }
            }
            return _people;
        }

        ///// You missed the summary in the remaining methods. It is good to have it, don't miss it! 
        ///// This method is set as private, it should be public since it seems meant to be used from the outside of the class.
        ///// This method does have a good name for the entry parameter, well done. - Andres
        private IEnumerable<People> GetBobs(bool olderThan30)
        {
            ///// Nice usage of the ternary conditional operator. 
            ///// The DOB condition is not correct, it should be minor than instead of higher than.
            ///// You could set apart the DateTime value creation for better readability. - Andres
            return olderThan30 ? _people.Where(x => x.Name == "Bob" && x.DOB >= DateTime.Now.Subtract(new TimeSpan(30 * 356, 0, 0, 0))) : _people.Where(x => x.Name == "Bob");
        }


        ///// Again, you should use better naming for entry parameters, p is not a good option. - Andres
        public string GetMarried(People p, string lastName)
        {
            if (lastName.Contains("test"))
                return p.Name;
            ///// This if statement is not working, be careful with where you put the brackets.
            if ((p.Name.Length + lastName).Length > 255)
            {
                ///// This should also return, otherwise you are not saving the text anywhere. - Andres
                (p.Name + " " + lastName).Substring(0, 255);
            }

            return p.Name + " " + lastName;
        }
    }
}