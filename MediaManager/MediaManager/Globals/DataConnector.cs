using MediaManager.GUI.Controls.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaManager.Globals
{
    public static class DataConnector
    {
        private static MediaDBEntities DBCONNECTION = new MediaDBEntities();

        public static class Reader
        {
            public static Part GetPart(int id) => DBCONNECTION.Parts.Find(id);

            public static List<SearchResultItem> SearchUsingParameters(SearchParameters parameters)
            {
                var result = new List<SearchResultItem>();

                // TODO filter

                return result;
            }
        }
    }
}
