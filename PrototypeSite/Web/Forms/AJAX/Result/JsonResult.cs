using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Forms.AJAX.Result
{
    public class JsonResult : IResult
    {
        private object result;

        public object Result
        {
            get { return result; }
        }

        public JsonResult(object result)
        {
            this.result = result;
        }
    }
}
