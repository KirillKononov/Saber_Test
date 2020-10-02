using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Serializer
{
    class ListRandom
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        private readonly Encoding _enc8 = Encoding.UTF8;

        public void Serialize(Stream s)
        {
            var listNodes = new List<ListNode> {Head};
            var nextNode = Head.Next;

            while (nextNode != null)
            {
                listNodes.Add(nextNode);
                nextNode = nextNode.Next;
            }

            foreach (var listNode in listNodes)
            {
                var dataToSerialize = $"{listNode.Data}:{listNodes.IndexOf(listNode.Random)}\n";
                s.Write(_enc8.GetBytes(dataToSerialize));
            }

            s.Close();
        }

        public void Deserialize(Stream s)
        {
            var bytes = new byte[s.Length];
            s.Read(bytes);
            var deserializedData = _enc8.GetString(bytes).Split("\n");
            var listNodes = new List<ListNode>();
            var temp = new ListNode();
            Count = 0;
            Head = temp;

            foreach (var tempData in deserializedData)
            {
                if (tempData == "") continue;

                Count++;
                temp.Data = tempData;
                var next = new ListNode();
                temp.Next = next;
                listNodes.Add(temp);
                next.Previous = temp;
                temp = next;
            }

            Tail = temp.Previous;
            Tail.Next = null;

            foreach (var listNode in listNodes)
            {
                var dataAndRandomIndex = listNode.Data.Split(":");
                listNode.Random = listNodes[Convert.ToInt32(dataAndRandomIndex[1])];
                listNode.Data = dataAndRandomIndex[0];
            }

            s.Close();
        }
    }
}
