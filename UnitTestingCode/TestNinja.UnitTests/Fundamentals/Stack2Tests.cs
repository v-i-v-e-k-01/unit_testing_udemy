using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    internal class Stack2Tests
    {
        [Test]
        public void Count_EmptyStack_ReturnZero()
        {
            var stack = new Stack2<string>();
            Assert.That(stack.Count, Is.EqualTo(0));
        }
        


        [Test]
        public void Push_ObjIsNull_ThrowsArgumentNullException()
        {
            var stack = new Stack2<string>();

            Assert.That(() => stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_ValidObj_AddsObjToStack()
        {
            var stack = new Stack2<string>();
            stack.Push("strHere");
            Assert.That(stack.Count, Is.EqualTo(1));
        }



        [Test]
        public void Peek_StackIsEmpty_ThrowsInvalidOperationException()
        {
            var stack = new Stack2<string>();

            Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackHasElements_ReturnsTopElement()
        {
            var stack = new Stack2<string>();
            stack.Push("abc");
            stack.Push("def");

            var result = stack.Peek();
            Assert.That(result, Is.EqualTo("def"));
            Assert.That(stack.Count, Is.EqualTo(2));
        }



        [Test]
        public void Pop_StackIsEmpty_ThrowsInvalidOperationException()
        {
            var stack = new Stack2<string>();

            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackHasElements_ReturnsRemovedElement()
        {
            var stack = new Stack2<string>();
            stack.Push("abc");
            stack.Push("def");
            var result = stack.Pop();

            Assert.That(result, Is.EqualTo("def"));
            Assert.That(stack.Peek(), Is.EqualTo("abc"));
        }
    }
}
