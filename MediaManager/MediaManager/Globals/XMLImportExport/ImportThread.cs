using System;
using System.Collections.Generic;

namespace MediaManager.Globals.XMLImportExport
{
    /// <summary>
    /// A default <see cref="ThreadStepper"/> for an import of data supporting some utility methods.
    /// </summary>
    public abstract class ImportThread : ThreadStepper
    {
        protected string fileSource;
        protected string importFailedStep;
        protected string importFailedMessage;
        protected string formatExceptionHeader;
        protected string dbConstraintExceptionHeader;
        protected Dictionary<string, string> dbConstraintMessages;
        private ImportThread() { }
        /// <summary>
        /// Create a new <see cref="ImportThread"/>.
        /// </summary>
        /// <param name="fileSource">File to read from</param>
        /// <param name="importFailedStep">Caption to show in a <see cref="ThreadProcessViewer"/> if the import fails</param>
        /// <param name="importFailedMessage">Message of the exception to throw if the import fails</param>
        /// <param name="formatExceptionHeader">Default header of a <see cref="FormatException"/></param>
        /// <param name="dbConstraintExceptionHeader">Default header of a <see cref="FormatException"/> that is caused by a constraint violation in the database</param>
        /// <param name="dbConstraintMessages">
        /// Mapping for each possible constraint exception from the database to a readable message.
        /// <br/>
        /// <br/>
        /// <b>Important:</b>
        /// <br/>
        /// Violations of indexes are usually returned with a single quote (e.g. 'UQ_Account_Id') whilst all other violations use double quotes (e.g. "Account_Name_Not_Empty").
        /// To differentiate between all constraints clearly, the quotes should be included in the dictionary key.
        /// </param>
        protected ImportThread(string fileSource, string importFailedStep, string importFailedMessage, string formatExceptionHeader, string dbConstraintExceptionHeader, Dictionary<string, string> dbConstraintMessages)
        {
            this.fileSource = fileSource;
            this.importFailedStep = importFailedStep;
            this.importFailedMessage = importFailedMessage;
            this.formatExceptionHeader = formatExceptionHeader;
            this.dbConstraintExceptionHeader = dbConstraintExceptionHeader;
            this.dbConstraintMessages = dbConstraintMessages;
        }
        public override void run()
        {
            try
            {
                runImport();
            }
            catch (Exception e)
            {
                CallStep(1 / 1, importFailedStep);
                CallFinished(AssembleFormatException(importFailedMessage, e));
            }
        }
        protected abstract void runImport();

        #region Internal Import Methods
        /// <summary>
        /// Parses a time string of the following format to a timespan:
        /// PT00H00M00.000S
        /// </summary>
        /// <param name="timestring">Time string</param>
        /// <returns>TimeSpan</returns>
        protected TimeSpan ParseManualTime(string timestring)
        {
            timestring = timestring.Substring(2);
            var hours = timestring.IndexOf('H') >= 0 ? timestring.Split('H')[0] : "0";
            timestring = timestring.IndexOf('H') >= 0 ? timestring.Substring(timestring.IndexOf('H') + 1) : timestring;
            var mins = timestring.IndexOf('M') >= 0 ? timestring.Split('M')[0] : "0";
            timestring = timestring.IndexOf('M') >= 0 ? timestring.Substring(timestring.IndexOf('M') + 1) : timestring;
            var seconds = timestring.IndexOf('.') >= 0 ? timestring.Split('.')[0] : "0";
            timestring = timestring.IndexOf('.') >= 0 ? timestring.Substring(timestring.IndexOf('.') + 1) : timestring;
            var milliseconds = timestring.IndexOf('S') >= 0 ? (timestring.Split('S')[0] + "000").Substring(0, 3) : "0";
            return new TimeSpan(
                0,
                int.Parse(hours),
                int.Parse(mins),
                int.Parse(seconds),
                int.Parse(milliseconds)
                );
        }
        /// <summary>
        /// Checks if a found import version can be imported by the program by comparing it to a maximum importable version.
        /// <br/>
        /// It is recommended to keep an export version and a reference to the minimum version within an export file (downwards compatibility) so that export files may be importable by older versions.
        /// </summary>
        /// <param name="maximumVersion">Version to compare to</param>
        /// <param name="actualVersion">Found import version</param>
        /// <returns></returns>
        protected bool CheckVersionImportable(string maximumVersion, string actualVersion)
        {
            var currentVersion = maximumVersion.Split('.');
            var foundVersion = actualVersion.Split('.');
            for (int i = 0; i < 3; i++)
            {
                if (int.Parse(foundVersion[i]) > int.Parse(currentVersion[i])) return false;
            }
            return true;
        }
        /// <summary>
        /// Assembles a <see cref="FormatException"/> with a default header.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        /// <returns></returns>
        protected FormatException AssembleFormatException(string message, Exception innerException) => new FormatException(formatExceptionHeader + ": " + message + ((innerException != null && innerException is FormatException) ? "\n" + innerException.Message : ""), innerException);
        /// <summary>
        /// Parses a constraint exception based on the configured constraints and translates it into a <see cref="FormatException"/> with a readable message.
        /// </summary>
        /// <param name="message">Original exception message</param>
        /// <param name="e">Inner exception</param>
        /// <returns>Assembled <see cref="FormatException"/></returns>
        protected FormatException ParseDBConstraintException(string message, Exception e)
        {
            // 2nd inner exception contains constraint details => there i can filter for the constraint
            var innerExc = e?.InnerException?.InnerException;
            var messageHeader = dbConstraintExceptionHeader;
            if (innerExc == null) return new FormatException(messageHeader + ": " + message, e);

            // iterate through constraints => return more details
            foreach (var constraint in dbConstraintMessages.Keys)
            {
                if (innerExc.Message.Contains(constraint))
                {
                    var msg = messageHeader + " " + dbConstraintMessages[constraint] + ": " + message;
                    return constraint.Contains("'UQ_") ? new DuplicateNameException(msg, e) : new FormatException(msg, e);
                }
            }
            return new FormatException(messageHeader + ": " + message, e);
        }
        #endregion
    }
}
