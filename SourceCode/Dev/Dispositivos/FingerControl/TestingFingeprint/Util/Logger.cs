using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Finger.Component.Util
{
    public static class Logger
    {
        public static void Write(string message)
        {
            if (!EventLog.SourceExists(ConfigurationManager.AppSettings["NameEventLog"]))
                EventLog.CreateEventSource("Log de Capturas de huella", ConfigurationManager.AppSettings["NameEventLog"]);

            using (EventLog eventLog = new EventLog())
            {
                eventLog.Source = ConfigurationManager.AppSettings["NameEventLog"];
                eventLog.WriteEntry($"Finger Print: {message}", EventLogEntryType.Error, 101, 1);
            }
        }

        public static void Write(string message, EventLogEntryType entry)
        {
            if (!EventLog.SourceExists(ConfigurationManager.AppSettings["NameEventLog"]))
                EventLog.CreateEventSource("Log de Capturas de huella", ConfigurationManager.AppSettings["NameEventLog"]);

            using (EventLog eventLog = new EventLog())
            {
                eventLog.Source = ConfigurationManager.AppSettings["NameEventLog"];
                eventLog.WriteEntry($"Finger Print: {message}", entry, 101, 1);
            }
        }
    }
}
