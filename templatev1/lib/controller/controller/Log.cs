﻿using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace controller
{
    public class Log
    {
        private static ILogger<Log> _logger;
        private static readonly string CurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");

        public enum LogLevel
        {
            Error,
            Warning,
            Information,
            Debug
        }

        public Log(ILogger<Log> logger = null)
        {
            _logger = logger ?? new LoggerFactory().CreateLogger<Log>();
            CreateLogFile();
            CreateConsoleLogger();
        }

        public static void LogMessage(LogLevel logLevel, string programName, string message)
        {
            var logMessage = $"[{CurrentTime}] - [{logLevel}] - [{programName}] - {message}";

            Microsoft.Extensions.Logging.LogLevel LogLv = (Microsoft.Extensions.Logging.LogLevel)logLevel;

            _logger.Log(LogLv, logMessage);
            WriteToLogFile(logMessage);
        }

        public static void LogException(Exception ex, string programName)
        {
            var logMessage = $"[{CurrentTime}] - [{ex.GetType().Name}] - [{programName}] - {ex.Message}";
            _logger.LogError(logMessage);
        }

        private void CreateLogFile()
        {
            var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            Directory.CreateDirectory(logDirectory);
            var logFileName = Path.Combine(logDirectory, $"Log-{CurrentTime}.txt");
            File.WriteAllText(logFileName, "");
        }

        private void CreateConsoleLogger()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", Microsoft.Extensions.Logging.LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", Microsoft.Extensions.Logging.LogLevel.Debug)
                    .AddConsole();
            });
            _logger = loggerFactory.CreateLogger<Log>();
        }

        private static void WriteToLogFile(string message)
        {
            File.AppendAllText($"Logs/Log-{CurrentTime}.txt", $"{message}\n");
        }
    }
}