//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Bb.Sdk.Exceptions
//{

//    /// <summary>
//    /// MyFrame
//    /// </summary>
//    [Serializable]
//    public class MyFrame
//    {
        
//        /// <summary>
//        /// Initializes a new instance of the <see cref="MyFrame"/> class.
//        /// </summary>
//        public MyFrame()
//        {

//        }

//        /// <summary>
//        /// Gets or sets the frame item.
//        /// </summary>
//        /// <value>
//        /// The frame item.
//        /// </value>
//        public System.Diagnostics.StackFrame FrameItem { get; set; }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="MyFrame"/> class.
//        /// </summary>
//        /// <param name="item">The item.</param>
//        public MyFrame(System.Diagnostics.StackFrame item)
//        {

//            // TODO: Complete member initialization

//            this.Offset = item.GetILOffset();
//            var m1 = item.GetMethod();

//            // Emit
//            MethodBodyInfo graph = MethodBodyInfo.Create(m1, this.Offset);
//            StringBuilder st = new StringBuilder(graph.MethodToString);
//            st.AppendLine("<BR />");
//            st.Append("{");
//            st.AppendLine("<BR />");
//            foreach (var il in graph.Instructions)
//            {
//                st.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
//                st.Append(il.ToString());
//                st.AppendLine("<BR />");
//            }
//            st.AppendLine("}");
//            this.ReflectionMethod = new MyMethod() { IL = st.ToString() };
//            this.ReflectionMethodName = graph.MethodToString;

//            if (m1.DeclaringType != null)
//            {
//                this.ReflectionMethod.Method = m1;
//                this.FrameItem = item;
//                this.ReflectionMethodName = m1.ToString();
//            }
//            else
//            {

//            }

//        }

//        /// <summary>
//        /// Gets or sets the reflection method.
//        /// </summary>
//        /// <value>
//        /// The reflection method.
//        /// </value>
//        public MyMethod ReflectionMethod { get; set; }

//        /// <summary>
//        /// Gets or sets the offset.
//        /// </summary>
//        /// <value>
//        /// The offset.
//        /// </value>
//        public int Offset { get; set; }



//        /// <summary>
//        /// Gets or sets the name of the reflection method.
//        /// </summary>
//        /// <value>
//        /// The name of the reflection method.
//        /// </value>
//        public string ReflectionMethodName { get; set; }

//        /// <summary>
//        /// Returns a <see cref="System.String" /> that represents this instance.
//        /// </summary>
//        /// <returns>
//        /// A <see cref="System.String" /> that represents this instance.
//        /// </returns>
//        public override string ToString()
//        {
//            return this.ReflectionMethod.IL;
//        }
    
//    }

//}
