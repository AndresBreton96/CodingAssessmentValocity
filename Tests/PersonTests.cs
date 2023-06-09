using System;
using FluentAssertions;
using CodingAssessment.Refactor;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    public class PersonTests
    {
        // Global Arrangements
        BirthingUnit _birthingUnit = new BirthingUnit();
        
        [Fact]
        public void GetPeople_ShouldCreatePeople()
        {
            // Arrange
            // Set the number of people to create.
            var random = new Random();
            int numberOfPeople = random.Next(1, 5);
            
            // Act
            var people = _birthingUnit.GetPeople(numberOfPeople);

            // Assert
            // The list returned should contain exactly the number of people set at the arrange.
            Assert.True(people.Count == numberOfPeople);
        }

        [Fact]
        public void GetPeople_ShouldContainOnlyBobOrBetty()
        {
            // Arrange
            // Set the number of people to create.
            var random = new Random();
            int numberOfPeople = random.Next(1, 5);
            var differentNameExists = false;
            
            // Act
            var people = _birthingUnit.GetPeople(numberOfPeople);

            // Assert
            // The list returned should contain only Bob and Betty names.
            foreach (var person in people)
            {
                differentNameExists = (person.Name != "Bob" && person.Name != "Betty");
            }

            Assert.True(!differentNameExists);
        }

        [Fact]
        public void GetPeople_ShouldContainOnlyAgesBetween18To85()
        {
            // Arrange
            // Set the number of people to create and set variables.
            var random = new Random();
            int numberOfPeople = random.Next(1, 5);
            var agesOutOfRange = false;
            var dobFor18 = DateTime.UtcNow.Subtract(new TimeSpan(18 * 365, 0, 0, 0));
            var dobFor85 = DateTime.UtcNow.Subtract(new TimeSpan(85 * 365, 0, 0, 0));
            
            // Act
            var people = _birthingUnit.GetPeople(numberOfPeople);

            // Assert
            // The list returned should contain only people with ages within 18 to 85.
            foreach (var person in people)
            {
                agesOutOfRange = (person.DOB >= dobFor18 || person.DOB <= dobFor85);
            }

            Assert.True(!agesOutOfRange);
        }

        [Fact]
        public void GetBobs_ShouldReturnOnlyBobsOlderThan30()
        {
            // Arrange
            // Create and set variables.
            var olderThan30 = true;
            var differentNameOrAgeExists = false;
            var dobFor30 = DateTime.UtcNow.Subtract(new TimeSpan(30 * 365, 0, 0, 0));
            var dobFor50 = DateTime.UtcNow.Subtract(new TimeSpan(50 * 365, 0, 0, 0));
            var dobFor20 = DateTime.UtcNow.Subtract(new TimeSpan(20 * 365, 0, 0, 0));
            var people = new List<People>()
            {
                new People("Bob", dobFor20),
                new People("Betty", dobFor30),
                new People("Bob", dobFor30),
                new People("Bob", dobFor50),
                new People("Betty", dobFor30)
            };
            
            
            // Act
            var bobs = _birthingUnit.GetBobs(olderThan30);

            // Assert
            // The list returned should contain only Bob and none DOB higher than the DOB for 30 years.
            foreach (var person in bobs)
            {
                Console.WriteLine(person.Name);
                Console.WriteLine(person.DOB);
                differentNameOrAgeExists = (person.Name != "Bob" || person.DOB > dobFor30);
            }

            Assert.True(!differentNameOrAgeExists);
        }

        [Fact]
        public void GetBobs_ShouldReturnOnlyBobsAnyAge()
        {
            // Arrange
            // Create and set variables.
            var olderThan30 = false;
            var differentNameExists = false;
            var dobFor30 = DateTime.UtcNow.Subtract(new TimeSpan(30 * 365, 0, 0, 0));
            var dobFor50 = DateTime.UtcNow.Subtract(new TimeSpan(50 * 365, 0, 0, 0));
            var dobFor20 = DateTime.UtcNow.Subtract(new TimeSpan(20 * 365, 0, 0, 0));
            var people = new List<People>()
            {
                new People("Bob", dobFor20),
                new People("Betty", dobFor30),
                new People("Bob", dobFor30),
                new People("Bob", dobFor50),
                new People("Betty", dobFor30)
            };
            
            // Act
            var bobs = _birthingUnit.GetBobs(olderThan30);

            // Assert
            // The list returned should contain only Bob.
            foreach (var person in bobs)
            {
                differentNameExists = person.Name != "Bob";
            }

            Assert.True(!differentNameExists);
        }

        [Fact]
        public void GetMarried_ShouldReturnSameNameWhenTest()
        {
            // Arrange
            // Create a people object to use and set variables.
            var name = "Andres";
            var dob = DateTime.UtcNow.Subtract(new TimeSpan(30 * 365, 0, 0, 0));
            var person = new People(name, dob);
            var lastName = "test";
            
            // Act
            var marriedName = _birthingUnit.GetMarried(person, lastName);

            // Assert
            Assert.True(marriedName == person.Name);

        }
        
        [Fact]
        public void GetMarried_ShouldReturnPersonNamePlusGivenLastName()
        {
            // Arrange
            // Create a people object to use and set variables.
            var name = "Andres";
            var dob = DateTime.UtcNow.Subtract(new TimeSpan(30 * 365, 0, 0, 0));
            var person = new People(name, dob);
            var lastName = "Breton";
            
            // Act
            var marriedName = _birthingUnit.GetMarried(person, lastName);

            // Assert
            Assert.True(marriedName == (person.Name + " " + lastName));

        }
        
        [Fact]
        public void GetMarried_ShouldReturnPersonNamePlusGivenLastNameShorterThan255Characters()
        {
            // Arrange
            // Create a people object to use and set variables.
            var name = "Andresqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnm";
            var dob = DateTime.UtcNow.Subtract(new TimeSpan(30 * 365, 0, 0, 0));
            var person = new People(name, dob);
            var lastName = "Bretonqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnmqwertyuiopasdfhjklzxcvbnm";
            
            // Act
            var marriedName = _birthingUnit.GetMarried(person, lastName);

            // Assert
            Assert.True(marriedName.Length <= 255);

        }
    }
}
