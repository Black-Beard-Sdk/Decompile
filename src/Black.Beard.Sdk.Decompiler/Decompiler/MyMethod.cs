using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bb.Sdk.Exceptions
{

    /// <summary>
    /// MyMethod
    /// </summary>
    [Serializable]
    public class MyMethod
    {

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>
        /// The method.
        /// </value>
        public System.Reflection.MethodBase Method { get; set; }

        /// <summary>
        /// Gets or sets the il.
        /// </summary>
        /// <value>
        /// The il.
        /// </value>
        public string IL { get; set; }

    }

}
