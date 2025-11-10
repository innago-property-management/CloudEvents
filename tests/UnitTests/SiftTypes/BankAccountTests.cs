using System;
using Innago.SiftTypes;
using Xunit;

namespace UnitTests.SiftTypes
{
    public class BankAccountTests
    {
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var userId = "user123";
            var userEmail = "user@yopmail.com";
            var userFullName = "User Test";
            var bankName = "Test Bank";
            var accountType = "Checking";
            var sessionId = "sessionId";
            var time = DateTimeOffset.UtcNow;

            // Act
            var bankAccount = new BankAccount(userId, userEmail, userFullName, sessionId, bankName, accountType, time);

            // Assert
            Assert.Equal(userId, bankAccount.UserId);
            Assert.Equal(userEmail, bankAccount.UserEmail);
            Assert.Equal(userFullName, bankAccount.UserFullName);
            Assert.Equal(sessionId, bankAccount.SessionId);
            Assert.Equal(bankName, bankAccount.BankName);
            Assert.Equal(accountType, bankAccount.AccountType);
            Assert.Equal(time, bankAccount.Time);
        }
        [Fact]
        public void BankAccount_NullOrEmptyUserEmail_UserFullName_SessionId_ShouldHandleProperly()
        {
            var userId = "user123";
            var bankName = "Test Bank";
            var accountType = "Checking";
            var time = DateTimeOffset.UtcNow;

            // Null values
            var bankAccountNulls = new BankAccount(userId, null, null, null, bankName, accountType, time);
            Assert.Null(bankAccountNulls.UserEmail);
            Assert.Null(bankAccountNulls.UserFullName);
            Assert.Null(bankAccountNulls.SessionId);

            // Empty strings
            var bankAccountEmpties = new BankAccount(userId, "", "", "", bankName, accountType, time);
            Assert.Equal("", bankAccountEmpties.UserEmail);
            Assert.Equal("", bankAccountEmpties.UserFullName);
            Assert.Equal("", bankAccountEmpties.SessionId);
        }
        [Fact]
        public void UnixTime_ShouldReturnCorrectValue()
        {
            // Arrange
            var time = DateTimeOffset.FromUnixTimeMilliseconds(1672531200000); // Example Unix time
            var bankAccount = new BankAccount("user123","user@yopmail.com", "User Test", "Test Bank", "Checking", "Verified", time);

            // Act
            var unixTime = bankAccount.UnixTime;

            // Assert
            Assert.Equal(1672531200000, unixTime);
        }
    }
}