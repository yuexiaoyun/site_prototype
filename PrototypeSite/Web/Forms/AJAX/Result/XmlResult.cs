using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Forms.AJAX.Result
{
    public class XmlResult : IResult
    {
        private object result;

        public object Result
        {
            get { return result; }
        }

        public XmlResult(object result)
        {
            this.result = result;
        }
    }
}
