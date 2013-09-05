using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Forms.AJAX.Result
{
    public class StringResult : IResult
    {
        private string result;

        public string Result
        {
            get { return result; }
        }

        public StringResult(string result)
        {
            this.result = result;
        }
    }
}
