using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MediaManager.Globals.XMLImportExport
{
    /// <summary>
    /// Provides helper methods for the XML export.
    /// </summary>
    public static class XMLExport
    {
        /// <summary>
        /// Translates all entries of a table into an xml element.
        /// <para/>
        /// The following options can be added to the export:
        /// <br/>
        /// - <b>selection</b>: The mandatory list of column names that should be exported. This also includes the additional properties names to provide the order they should be exported in.
        /// <br/>
        /// - <b>filter</b>: An optional predicate that checks each table entry. If the expression is evaluated to true, the entry will be exported.
        /// <br/>
        /// - <b>additional properties</b>: An optional map of additional properties that will be computed based on a provided function that takes the table entry as an input.
        /// <br/>
        /// - <b>mappings</b>: An optional dictionary of column names to a mapping function that takes the columns value as input and returns an alternative value if needed. The mappings are applied to the selected columns and the additional properties!
        /// <br/>
        /// - <b>additional children</b>: An optional function that takes the table entry as an input and computs additional children.
        /// </summary>
        /// <typeparam name="DB">Type of the database connection</typeparam>
        /// <typeparam name="T">Type of the table entries; <b>the actual table must have the same name as the entry type but with an s at the end</b></typeparam>
        /// <param name="dbConnection">Database connection to use</param>
        /// <param name="columns">List of columns that should be exported</param>
        /// <param name="filter">Filter to apply on the entries</param>
        /// <param name="additionalComputedProperties">Additional columns to compute for each entry</param>
        /// <param name="mappings">Mappings to apply for certain columns</param>
        /// <param name="computeChildren">Function to use for computing the children</param>
        /// <returns>XML-Representation of the whole table</returns>
        public static XElement ExportDataFromTable<DB, T>(DB dbConnection, List<string> columns, Dictionary<string, Func<T, object>> additionalComputedProperties = null, Func<T, bool> filter = null, Dictionary<string, Func<object, object>> mappings = null, Func<T, List<XElement>> computeChildren = null) where T : class where DB : System.Data.Entity.DbContext
        {
            // prepare parameters
            filter = filter ?? ((t) => true);
            additionalComputedProperties = additionalComputedProperties ?? new Dictionary<string, Func<T, object>>();
            mappings = mappings ?? new Dictionary<string, Func<object, object>>();

            // retrieve variables
            var tType = typeof(T);
            var table = dbConnection.GetType().GetProperty(tType.Name + "s").GetValue(dbConnection) as System.Data.Entity.DbSet<T>;
            var xmlList = new XElement(tType.Name + "s");

            // translate data
            foreach (var elem in table)
            {
                if (!filter(elem)) continue; // fun filter afterwards because it may be an method invocation

                var xmlElem = new XElement(tType.Name);
                // properties
                foreach (var prop in columns)
                {
                    var property = tType.GetProperty(prop);
                    // if the property is not found on the object, an additional property is needed
                    var value = property != null ? property.GetValue(elem) : additionalComputedProperties[prop](elem);
                    if (mappings.ContainsKey(prop)) xmlElem.Add(new XAttribute(prop, mappings[prop](value)));
                    else xmlElem.Add(new XAttribute(prop, value));
                }
                // additional children
                if (computeChildren != null) computeChildren(elem).ForEach(xmlElem.Add);
                xmlList.Add(xmlElem);
            }

            return xmlList;
        }
    }
}
