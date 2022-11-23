using System;

namespace MediaManager.Globals.XMLImportExport
{
    /// <summary>
    /// Indicates, that a name of an entry is duplicated.
    /// </summary>
    public class DuplicateNameException : FormatException
    {
        public DuplicateNameException(string message, Exception innerException) : base(message, innerException) { }
    }
}
