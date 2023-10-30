using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Prodem.Fingerprint.FingerPrintControl.Util
{
    public static class Logger
    {
        public static void Write(string message)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry($"Finger Print: {message}", EventLogEntryType.Error, 101, 1);
            }
        }

        public static void Write(string message, EventLogEntryType entry)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry($"Finger Print: {message}", entry, 101, 1);
            }
        }
    }
}
