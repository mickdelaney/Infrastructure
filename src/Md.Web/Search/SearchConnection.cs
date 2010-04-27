using System.Collections.Generic;
using System.Linq;
using log4net;
using SolrNet;

namespace Md.Web.Search
{
    public class SearchConnection : ISolrConnection
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SearchConnection));

        private readonly ISolrConnection connection;

        public SearchConnection(ISolrConnection connection)
        {
            this.connection = connection;
        }

        public string ServerURL
        {
            get { return connection.ServerURL; }
            set { connection.ServerURL = value; }
        }

        public string Version
        {
            get { return connection.Version; }
            set { connection.Version = value; }
        }

        public string Post(string relativeUrl, string s)
        {
            Logger.DebugFormat("POSTing '{0}' to '{1}'", s, relativeUrl);
            return connection.Post(relativeUrl, s);
        }

        public string Get(string relativeUrl, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var stringParams = string.Join(", ", parameters.Select(p => string.Format("{0}={1}", p.Key, p.Value)).ToArray());
            Logger.DebugFormat("GETting '{0}' from '{1}'", stringParams, relativeUrl);
            return connection.Get(relativeUrl, parameters);
        }
    }
}
