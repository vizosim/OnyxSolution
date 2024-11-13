﻿using Moq;
using Onyx.Application.Models;

namespace Onyx.Tests
{
    public class LoggerWritterTests
    {
        private readonly Mock<IWriteProvider> _writeProviderMock = new();
        private readonly Mock<IDateTimeProvider> _datTimeProviderMock = new();
        private readonly LoggerWritter _loggerWritter;

        public LoggerWritterTests()
        {
            _loggerWritter = new LoggerWritter(_writeProviderMock.Object, _datTimeProviderMock.Object);
        }

        [Fact]
        public void Initialize_ShouldWriteMessage()
        {
            // Arrange
            string expectedLogMessage = string.Format(LogsMesaagesPatterns.DefaultMessage, _datTimeProviderMock.Object.GetTime(), "Logger initialized");

            // Assert
            _writeProviderMock.Verify(w => w.Write(expectedLogMessage), Times.Once);
        }

        [Fact]
        public void Log_ShouldWriteMessage()
        {
            // Arrange
            _datTimeProviderMock
                .Setup(d => d.GetTime())
                .Returns(new DateTime(2024, 10, 10, 13, 25, 35));

            string expectedLogMessage = string.Format(LogsMesaagesPatterns.DefaultMessage, _datTimeProviderMock.Object.GetTime(), "Test Log messasge");

            // Act
            _loggerWritter.Log("Test Log messasge");

            // Assert
            _writeProviderMock.Verify(w => w.Write(expectedLogMessage), Times.Once);
        }
    }
}