using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Cache
{
    public enum CacheMode
    {
        LOCAL,
        REMOTE,
        SESSION,
        REQUEST
    }

    public enum CacheAction
    {
        FETCH,
        CLEAR,
        UPDATE
    }

    public class CacheContainerKey
    {
        public const string Local_Cache = "Local_Cache";
        public const string Remote_Cache = "Remote_Cache";
        public const string Session_Cache = "Session_Cache";
        public const string Request_Cache = "Request_Cache";
    }
}
