//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Bb.Sdk.Exceptions
//{

//    /// <summary>
//    /// MyException
//    /// </summary>
//    [Serializable]
//    public class MyException
//    {

//        /// <summary>
//        /// Gets or sets the stack trace.
//        /// </summary>
//        /// <value>
//        /// The stack trace.
//        /// </value>
//        public List<MyFrame> stackTrace { get; set; }

//        /// <summary>
//        /// Gets or sets the message.
//        /// </summary>
//        /// <value>
//        /// The message.
//        /// </value>
//        public string Message { get; set; }

//        /// <summary>
//        /// Gets or sets the exception.
//        /// </summary>
//        /// <value>
//        /// The exception.
//        /// </value>
//        [System.ComponentModel.Browsable(false)]
//        public Exception Exception { get; set; }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="MyException"/> class.
//        /// </summary>
//        public MyException()
//        {

//        }


//        /// <summary>
//        /// Initializes a new instance of the <see cref="MyException"/> class.
//        /// </summary>
//        /// <param name="stackTrace">The stack trace.</param>
//        /// <param name="message">The message.</param>
//        public MyException(List<MyFrame> stackTrace, string message)
//        {
//            // TODO: Complete member initialization
//            this.stackTrace = stackTrace;
//            this.Message = message;
//        }


//        /// <summary>
//        /// Initializes a new instance of the <see cref="MyException"/> class.
//        /// </summary>
//        /// <param name="stackTrace">The stack trace.</param>
//        /// <param name="e">The e.</param>
//        public MyException(List<MyFrame> stackTrace, Exception e)
//        {
//            // TODO: Complete member initialization
//            this.stackTrace = stackTrace;
//            this.Message = e.Message;
//            this.Exception = e;
//        }

//    }

//}
