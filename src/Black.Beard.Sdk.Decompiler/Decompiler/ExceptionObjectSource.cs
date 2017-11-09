using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bb.Sdk.Exceptions
{


    /// <summary>
    /// ExceptionObjectSource
    /// </summary>
    public class ExceptionObjectSource
    {

        /// <summary>
        /// Extracts the specified e.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static Queue<MyException> Extract(Exception e)
        {
            StackTrace s;
            MyException st;
            Queue<MyException> st1 = new Queue<MyException>();

            // serialize
            while (e != null)
            {
                s = new StackTrace(e);
                st = Serialize(e, s);
                st1.Enqueue(st);
                e = e.InnerException;
            }

            return st1;

        }


        /// <summary>
        /// Extracts the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static Queue<MyException> Extract(StackTrace s)
        {

            MyException st;
            Queue<MyException> st1 = new Queue<MyException>();

            st = Serialize(null, s);
            st1.Enqueue(st);

            return st1;

        }


        private static MyException Serialize(Exception e, StackTrace s)
        {
            MyException st;
            var lst1 = new List<MyFrame>();

            foreach (var item in s.GetFrames())
                lst1.Add(new MyFrame(item));

            if (e != null)
            {
                if (IsSerializable(e))
                    st = new MyException(lst1, e);
                else
                    st = new MyException(lst1, e.Message);
            }
            else
                st = new MyException(lst1, string.Empty);

            return st;
        }

        private static bool IsSerializable(Exception e)
        {
            if (e != null)
            {
                bool result = e.GetType().IsSerializable;

                if (e.InnerException != null)
                    result = result & IsSerializable(e.InnerException);

                return result;
            }

            return false;
        }


    }


}
