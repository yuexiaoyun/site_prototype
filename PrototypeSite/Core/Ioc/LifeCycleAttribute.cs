using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Ioc
{
    /// <summary>
    /// Class applied this attribute indicates that this class in a Bussiness layer class 
    /// and would be registerd as singlton
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ServiceAttribute : Attribute
    {

    }

    /// <summary>
    /// Class applied this attribute indicates that this class in a DataAccess layer class 
    /// and would be registerd as singlton
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DataAccessAttribute : Attribute
    {

    }
}
