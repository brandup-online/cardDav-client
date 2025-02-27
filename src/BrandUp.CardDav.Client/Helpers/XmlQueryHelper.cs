﻿namespace BrandUp.CardDav.Client.Helpers
{
    public static class XmlQueryHelper
    {
        public static string Propfind(params string[] props)
            => "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n" +
                " <D:propfind xmlns:D=\"DAV:\" xmlns:cs=\"http://calendarserver.org/ns/\">\r\n " +
                   " <D:prop>\r\n  " +
                            Inner(props) +
                   " </D:prop>\r\n" +
                " </D:propfind>\r\n";

        public static string AddressCollection(bool withCTag = false, params string[] vCardParameters)
            => "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n" +
            "<C:addressbook-query xmlns:D=\"DAV:\" xmlns:C=\"urn:ietf:params:xml:ns:carddav\" xmlns:cs=\"http://calendarserver.org/ns/\">\n   " +
            "  <D:prop>\n  " +
            "     <D:getetag/>\n     " +
            (withCTag ? "   <cs:getctag/>\n" : "") +
            "     <C:address-data>\n  " +
                        VCards(vCardParameters) +
            "     </C:address-data>\n " +
            "    </D:prop>\n    " +
            "<C:filter>\r\n    <C:prop-filter name=\"FN\">\r\n    </C:prop-filter>    \r\n</C:filter>" +
            "</C:addressbook-query>";

        public static string SyncCollection(string token = "", string syncLevel = "1")
            => " <?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n " +
                "<D:sync-collection xmlns:D=\"DAV:\" xmlns:cs=\"http://calendarserver.org/ns/\">\r\n   " +
                $"  <D:sync-token>{token}</D:sync-token>\r\n   " +
                    $"<D:sync-level>{syncLevel}</D:sync-level>\r\n" +
                    "  <D:prop>\r\n    " +
                        "<D:getetag/>\r\n" +
                    " </D:prop>\r\n" +
                " </D:sync-collection>\r\n";

        #region Helpers

        private static string Inner(string[] props)
        {
            var inner = string.Empty;

            foreach (var prop in props)
            {
                var prefix = prop == "getctag" ? "cs" : "D";
                inner += $"<{prefix}:" + prop + "/>\r\n";
            }

            return inner;
        }

        private static string VCards(string[] props)
        {
            var inner = string.Empty;

            foreach (var prop in props)
            {
                inner += "<D:prop name=\"" + prop + "\"/>\r\n";
            }

            return inner;
        }

        #endregion
    }
}
