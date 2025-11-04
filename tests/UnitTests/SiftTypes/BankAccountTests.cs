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
            var bankName = "Test Bank";
            var accountType = "Checking";
            var verifiedStatus = "Verified";
            var time = DateTimeOffset.UtcNow;

            // Act
            var bankAccount = new BankAccount(userId, bankName, accountType, verifiedStatus, time);

            // Assert
            Assert.Equal(userId, bankAccount.UserId);
            Assert.Equal(bankName, bankAccount.BankName);
            Assert.Equal(accountType, bankAccount.AccountType);
            Assert.Equal(verifiedStatus, bankAccount.VerifiedStatus);
            Assert.Equal(time, bankAccount.Time);
        }

        [Fact]
        public void UnixTime_ShouldReturnCorrectValue()
        {
            // Arrange
            var time = DateTimeOffset.FromUnixTimeMilliseconds(1672531200000); // Example Unix time
            var bankAccount = new BankAccount("user123", "Test Bank", "Checking", "Verified", time);

            // Act
            var unixTime = bankAccount.UnixTime;

            // Assert
            Assert.Equal(1672531200000, unixTime);
        }
    }
}