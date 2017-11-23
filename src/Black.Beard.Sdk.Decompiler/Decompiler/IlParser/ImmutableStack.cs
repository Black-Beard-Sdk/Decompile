using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Bb.Sdk.Decompiler.IlParser
{

    
    public static class ImmutableStack
    {
        // Methods
        public static ImmutableStack<T> Create<T>()
        {
            return ImmutableStack<T>.Empty;
        }

        public static ImmutableStack<T> Create<T>(this T item)
        {
            return ImmutableStack<T>.Empty.Push(item);
        }

        public static ImmutableStack<T> Create<T>(params T[] items)
        {
            Debug.Assert(items != null, "items");
            ImmutableStack<T> empty = ImmutableStack<T>.Empty;
            foreach (T local in items)
            {
                empty = empty.Push(local);
            }
            return empty;
        }

        public static ImmutableStack<T> CreateRange<T>(this IEnumerable<T> items)
        {
            Debug.Assert(items != null, "items");
            ImmutableStack<T> empty = ImmutableStack<T>.Empty;
            foreach (T local in items)
            {
                empty = empty.Push(local);
            }
            return empty;
        }

        public static ImmutableStack<T> Pop<T>(this ImmutableStack<T> stack, out T value)
        {
            Debug.Assert(stack != null, "stack");
            value = stack.Peek();
            return stack.Pop();
        }
    }



    //[DebuggerDisplay("IsEmpty = {IsEmpty}; Top = {_head}"), DebuggerTypeProxy((Type)typeof(ImmutableStackDebuggerProxy<>))]
    public sealed class ImmutableStack<T> : /*IImmutableStack<T>,*/ IEnumerable<T>, IEnumerable
    {

        // Fields
        private readonly T _head;
        private readonly ImmutableStack<T> _tail;
        private static readonly ImmutableStack<T> s_EmptyField;

        // Methods
        static ImmutableStack()
        {
            ImmutableStack<T>.s_EmptyField = new ImmutableStack<T>();
        }

        private ImmutableStack()
        {
        }

        private ImmutableStack(T head, ImmutableStack<T> tail)
        {
            Debug.Assert(tail != null, "tail");
            this._head = head;
            this._tail = tail;
        }

        public ImmutableStack<T> Clear()
        {
            return ImmutableStack<T>.Empty;
        }

        public Enumerator<T> GetEnumerator()
        {
            return new Enumerator<T>((ImmutableStack<T>)this);
        }

        public T Peek()
        {
            if (this.IsEmpty)
            {
                throw new InvalidOperationException("InvalidEmptyOperation=This operation does not apply to an empty instance.");
            }
            return this._head;
        }

        public ImmutableStack<T> Pop()
        {
            if (this.IsEmpty)
            {
                throw new InvalidOperationException("InvalidEmptyOperation=This operation does not apply to an empty instance.");
            }
            return this._tail;
        }

        public ImmutableStack<T> Pop(out T value)
        {
            value = this.Peek();
            return this.Pop();
        }

        public ImmutableStack<T> Push(T value)
        {
            return new ImmutableStack<T>(value, (ImmutableStack<T>)this);
        }

        internal ImmutableStack<T> Reverse()
        {
            ImmutableStack<T> stack = this.Clear();
            for (ImmutableStack<T> stack2 = (ImmutableStack<T>)this; !stack2.IsEmpty; stack2 = stack2.Pop())
            {
                stack = stack.Push(stack2.Peek());
            }
            return stack;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new EnumeratorObject<T>((ImmutableStack<T>)this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new EnumeratorObject<T>((ImmutableStack<T>)this);
        }        

        // Properties
        public static ImmutableStack<T> Empty
        {
            get
            {
                return ImmutableStack<T>.s_EmptyField;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return (this._tail == null);
            }
        }

        // Nested Types
        //[StructLayout(LayoutKind.Sequential), EditorBrowsable((EditorBrowsableState)EditorBrowsableState.Advanced)]
        public struct Enumerator<T>
        {
            private readonly ImmutableStack<T> _originalStack;
            private ImmutableStack<T> _remainingStack;
            internal Enumerator(ImmutableStack<T> stack)
            {
                Debug.Assert(stack != null, "stack");
                this._originalStack = stack;
                this._remainingStack = null;
            }

            public T Current
            {
                get
                {
                    if ((this._remainingStack == null) || this._remainingStack.IsEmpty)
                        throw new InvalidOperationException();
                    return this._remainingStack.Peek();
                }
            }
            public bool MoveNext()
            {
                if (this._remainingStack == null)
                    this._remainingStack = this._originalStack;
                else if (!this._remainingStack.IsEmpty)
                    this._remainingStack = this._remainingStack.Pop();
                return !this._remainingStack.IsEmpty;
            }
        }

        private class EnumeratorObject<T> : IEnumerator<T>, IEnumerator, IDisposable
        {
            // Fields
            private bool _disposed;
            private readonly ImmutableStack<T> _originalStack;
            private ImmutableStack<T> _remainingStack;

            // Methods
            internal EnumeratorObject(ImmutableStack<T> stack)
            {
                Debug.Assert(stack != null, "stack");
                this._originalStack = stack;
            }

            public void Dispose()
            {
                this._disposed = true;
            }

            public bool MoveNext()
            {
                this.ThrowIfDisposed();
                if (this._remainingStack == null)
                    this._remainingStack = this._originalStack;
                else if (!this._remainingStack.IsEmpty)
                    this._remainingStack = this._remainingStack.Pop();
                return !this._remainingStack.IsEmpty;
            }

            public void Reset()
            {
                this.ThrowIfDisposed();
                this._remainingStack = null;
            }

            private void ThrowIfDisposed()
            {
                if (this._disposed)
                    Debug.Fail("allready disposed");
            }

            // Properties
            public T Current
            {
                get
                {
                    this.ThrowIfDisposed();
                    if ((this._remainingStack == null) || this._remainingStack.IsEmpty)
                    {
                        throw new InvalidOperationException();
                    }
                    return this._remainingStack.Peek();
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }
        }
    }





}
