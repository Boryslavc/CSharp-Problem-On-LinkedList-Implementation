using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;


namespace Clones
{
    [TestFixture]
    internal class LinkedStack_should
    {
        private LinkedStack<int> CreateAndPush(params int[] valuesToAdd)
        {
            var res = new LinkedStack<int>();

            foreach (var value in valuesToAdd) 
                res.Push(value);

            return res;
        }


        [Test]
        public void ReturnValuesInRightSequence()
        {
            var linkedStack = CreateAndPush(1,2,3);

            ClassicAssert.AreEqual(linkedStack.Pop(), 3);
        }

        [Test]
        public void UpdateCount()
        {
            var linkedStack = CreateAndPush(1, 2, 3, 4, 5);

            ClassicAssert.AreEqual(5, linkedStack.Count);
        }

        [Test]
        public void EnumerateSelfWithNoException()
        {
            var linkedStack = CreateAndPush(1,2,3,4,5);

            
            foreach (var value in linkedStack)
                value.ToString();
        }


        [Test]
        public void ReturnsAllValuesOnEnumeration()
        {
            var linkesStack = CreateAndPush(1,2,3,5);

            var list = new List<int>();

            foreach(var i in linkesStack)
                list.Add(i);

            ClassicAssert.AreEqual(list.Count, linkesStack.Count);
        }

        [Test]
        public void EnumeratesFromHead()
        {
            var linkedStack = CreateAndPush(2,3,4,5,6);

            var list = new List<int>();

            foreach (var i in linkedStack)
                list.Add(i);

            int counter = 0;
            foreach(var i in linkedStack)
                ClassicAssert.AreEqual(list[counter++], i);
        }

        [Test]
        public void CloneOtherLinkedStack()
        {
            var linkedStack = CreateAndPush(1, 2, 3);

            var newStack = new LinkedStack<int>(linkedStack);

            ClassicAssert.AreEqual(newStack.Pop(), 3);
            ClassicAssert.AreEqual(newStack.Pop(), 2);
            ClassicAssert.AreEqual(newStack.Pop(), 1);
        }
    }
}
