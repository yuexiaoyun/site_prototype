using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Util;

namespace Web.Session
{
    public class CookieManager
    {
        private static HttpCookieCollection ResponseCookies
        {
            get { return HttpContext.Current.Response.Cookies; }
        }

        private static HttpCookieCollection RequestCookies
        {
            get { return HttpContext.Current.Request.Cookies; }
        }

        public HttpCookie GetCookie(string name)
        {
            return RequestCookies[name];
        }

        public string GetStringCookie(string name, string defaultValue)
        {
            if(RequestCookies[name] != null)
            {
                return RequestCookies[name].Value;
            }
            return defaultValue;
        }

        public string GetEncryptedStringCookie(string name, string defaultValue)
        {
            if(RequestCookies[name] != null)
            {
                return CryptographyUtility.DecryptString(RequestCookies[name].Value);
            }
            return defaultValue;
        }

        public int GetIntCookie(string name)
        {
            if(RequestCookies[name] != null)
            {
                return int.Parse(RequestCookies[name].Value);
            }
            return -1;
        }

        public int GetEncryptedIntCookie(string name)
        {
            if (RequestCookies[name] != null)
            {
                return int.Parse(CryptographyUtility.DecryptString(RequestCookies[name].Value));
            }
            return -1;
        }

        public void SetCookie(string name, object value)
        {
            if (value != null)
            {
                HttpCookie cookie = new HttpCookie(name, value.ToString());
                cookie.Path = "/";
                ResponseCookies.Add(cookie);
            }
        }

        public void SetCookie(string name, object value, DateTime expiresTime)
        {
            if (value != null)
            {
                HttpCookie cookie = new HttpCookie(name, value.ToString());
                cookie.Path = "/";
                cookie.Expires = expiresTime;
                ResponseCookies.Add(cookie);
            }
        }

        public void SetEncryptedCookie(string name, object value)
        {
            if (value != null)
            {
                HttpCookie cookie = new HttpCookie(name, CryptographyUtility.EncryptString(value));
                cookie.Path = "/";
                ResponseCookies.Add(cookie);
            }
        }

        public void ClearCookie(string name)
        {
            SetCookie(name, string.Empty, DateTime.Now.AddYears(-1));
        }
    }
}
