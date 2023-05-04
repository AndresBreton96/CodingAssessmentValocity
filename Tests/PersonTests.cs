using System;
using FluentAssertions;
using CodingAssessment.Refactor;
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
            // Set the number of people to create and set variables.
            var random = new Random();
            int numberOfPeople = random.Next(1, 5);
            var olderThan30 = true;
            var differentNameOrAgeExists = false;
            var dobFor30 = DateTime.UtcNow.Subtract(new TimeSpan(30 * 365, 0, 0, 0));
            
            // Act
            var people = _birthingUnit.GetPeople(numberOfPeople);
            var bobs = _birthingUnit.GetBobs(olderThan30);

            // Assert
            // The list returned should contain only Bob and none DOB higher than the DOB for 30 years.
            foreach (var person in people)
            {
                differentNameOrAgeExists = (person.Name != "Bob" || person.DOB > dobFor30);
            }

            Assert.True(!differentNameOrAgeExists);
        }

        [Fact]
        public void GetBobs_ShouldReturnOnlyBobsAnyAge()
        {
            // Arrange
            // Set the number of people to create and set variables.
            var random = new Random();
            int numberOfPeople = random.Next(1, 5);
            var olderThan30 = false;
            var differentNameExists = false;
            
            // Act
            var people = _birthingUnit.GetPeople(numberOfPeople);
            var bobs = _birthingUnit.GetBobs(olderThan30);

            // Assert
            // The list returned should contain only Bob.
            foreach (var person in people)
            {
                differentNameExists = person.Name != "Bob";
            }

            Assert.True(!differentNameExists);
        }
    }
}
