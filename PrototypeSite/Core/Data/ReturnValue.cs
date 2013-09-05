using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Data
{
    public class InsertReturnValue : ReturnValue
    {
        private int newId;

        public int NewId
        {
            get { return newId; }
            set { newId = value; }
        }
    }

    public class ReturnValue
    {
        private string message;
        private Status status;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public Status Status
        {
            get { return status; }
            set { status = value; }
        }
    }

    public enum Status
    {
        /// <summary>
        /// success
        /// </summary>
        Success,
        /// <summary>
        /// failure
        /// </summary>
        Failure
    }
}
