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
            int numberOfPeople = random.Next(0, 5);
            
            // Act
            var persons = _birthingUnit.GetPeople(numberOfPeople);

            // Assert
            // The list returned should contain exactly the number of people set at the arrange.
            Assert.True(persons.Count == numberOfPeople);
        }
    }
}
