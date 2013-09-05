using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class RequestContext
    {
        private static RequestContext _context = new RequestContext();

        private Dictionary<MemberKey, object> members = new Dictionary<MemberKey, object>();

        private bool isDebug;
        private bool isSecurity;

        public bool IsDebug
        {
            get { return isDebug; }
            set { isDebug = value; }
        }

        public bool IsSecurity
        {
            get { return isSecurity; }
            set { isSecurity = value; }
        }

        private RequestContext()
        {
        }

        public static RequestContext GetContext()
        {
            if (_context == null) _context = new RequestContext();
            return _context;
        }

        public bool Contains<TValue>(string name)
        {
            MemberKey key = new MemberKey(name, typeof (TValue));
            return members.ContainsKey(key);
        }

        public bool Contains<TValue>()
        {
            return Contains<TValue>(string.Empty);
        }

        public void SetMember<TValue>(TValue value, string name)
        {
            MemberKey key = new MemberKey(name, typeof(TValue));
            members[key] = value;
        }

        public void SetMember<TValue>(TValue value)
        {
            SetMember(value, string.Empty);
        }

        public TValue GetMember<TValue>()
        {
            return GetMember<TValue>(string.Empty);
        }

        public TValue GetMember<TValue>(string name)
        {
            MemberKey key = new MemberKey(name, typeof(TValue));
            
            object result;
            if(members.TryGetValue(key, out result))
            {
                return (TValue) result;;
            }

            return default(TValue);
        }

        public void Clear()
        {
            members.Clear();
            IsDebug = false;
            isSecurity = false;
        }

        private class MemberKey
        {
            private string name;
            private Type type;

            public string Name
            {
                get { return name; }
            }

            public Type Type
            {
                get { return type; }
            }

            public MemberKey(string name, Type type)
            {
                this.name = name;
                this.type = type;
            }

            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;

                MemberKey key = obj as MemberKey;
                if (key == null)
                    return false;

                if (type != key.Type)
                    return false;

                if (name != key.Name)
                    return false;

                return true;
            }

            public override int GetHashCode()
            {
                return name.GetHashCode() ^ type.GetHashCode();
            }
        }
    }
}
