using System;
using System.Windows;
using VaraniumSharp.Initiator.Monitoring;

namespace AppInsightClientUsageExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <inheritdoc />
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            const string instrumentationKey = "[Redacted]";
            //DO NOT DO THIS!! This value should be unique to the application user and be reused across application sessions
            var userKey = Guid.NewGuid().ToString();

            await AppInsightClient.InitializeAsync(instrumentationKey, userKey);

            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            currentDomain.ProcessExit += CurrentDomainOnProcessExit;
        }

        /// <summary>
        /// Invoked when the CurrentDomain process exits
        /// </summary>
        /// <param name="sender">Sender of the callback</param>
        /// <param name="eventArgs">Callback event arguments</param>
        private static void CurrentDomainOnProcessExit(object sender, EventArgs eventArgs)
        {
            AppInsightClient.Flush();
        }

        /// <summary>
        /// Invoked when the CurrentDomain exits with an unhandled exception
        /// </summary>
        /// <param name="sender">Sender of the callback</param>
        /// <param name="unhandledExceptionEventArgs">Arguments about the exception that has occured</param>
        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            AppInsightClient.TrackException((Exception)unhandledExceptionEventArgs.ExceptionObject);
            AppInsightClient.Flush();
        }
    }
}