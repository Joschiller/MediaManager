using System;
using System.Threading;

namespace MediaManager.Globals
{
    class SearchRunner<I, O>
    {
        public delegate void SearchResultHandler(O searchResult);
        public event SearchResultHandler SearchFinished;

        private I parameters;
        private Func<I, O> query;
        public SearchRunner(I parameters, Func<I, O> query)
        {
            this.parameters = parameters;
            this.query = query;
        }

        public void run()
        {
            var result = query(parameters);
            SearchFinished?.Invoke(result);
        }

        public static void runSearchOnThread(I parameters, Func<I, O> query, Action<O> onFinished)
        {
            var searchRunner = new SearchRunner<I, O>(parameters, query);
            var th = new Thread(new ThreadStart(searchRunner.run));
            searchRunner.SearchFinished += (result) => onFinished(result);
            th.Start();
        }
    }
}
