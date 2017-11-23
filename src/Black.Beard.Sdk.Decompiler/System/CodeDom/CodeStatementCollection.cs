// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Collections.Generic;

namespace System.CodeDom
{
    public class CodeStatementCollection : CollectionBase
    {

        public CodeStatementCollection() { }

        public CodeStatement this[int index]
        {
            get { return (CodeStatement)List[index]; }
            set { List[index] = value; }
        }

        public int Add(CodeStatement value) => List.Add(value);

        public int Add(CodeExpression value) => Add(new CodeExpressionStatement(value.ILInstruction, value));

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public int Add(CodeObject value)
        {

            if (value is CodeExpression c)
                return Add(c);

            else if (value is CodeStatement s)
                return Add(s);

            return 0;

        }

        public int Add(IEnumerable<CodeObject> values)
        {

            int result = 0;

            foreach (var value in values)
            {

                if (value is CodeExpression c)
                    result += Add(c);

                else if (value is CodeStatement s)
                    result += Add(s);

            }

            return result;

        }

        public void AddRange(CodeStatement[] value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            for (int i = 0; i < value.Length; i++)
                Add(value[i]);
        }

        public void AddRange(CodeStatementCollection value)
        {

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            int currentCount = value.Count;
            for (int i = 0; i < currentCount; i++)
                Add(value[i]);

        }

        public bool Contains(CodeStatement value) => List.Contains(value);

        public void CopyTo(CodeStatement[] array, int index) => List.CopyTo(array, index);

        public int IndexOf(CodeStatement value) => List.IndexOf(value);

        public void Insert(int index, CodeStatement value) => List.Insert(index, value);

        public void Remove(CodeStatement value) => List.Remove(value);

    }
}
