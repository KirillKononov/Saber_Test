using System;
using System.IO;

namespace Serializer
{
    class Program
    {
        private static readonly Random Rand = new Random();

        private static ListNode AddNode(ListNode previous)
        {
            var node = new ListNode
            {
                Previous = previous,
                Next = null,
                Data = Rand.Next(0, 100).ToString()
            };
            previous.Next = node;
            return node;
        }

        private static ListNode SetRandomNode(ListNode head, int length)
        {
            var randomIndex = Rand.Next(0, length);
            var counter = 0;
            var randomNode = head;

            while (counter < randomIndex)
            {
                randomNode = randomNode.Next;
                counter++;
            }

            return randomNode;
        }

        private static void CheckResult(ListRandom listToSerialize, ListRandom listToDeserialize)
        {
            if (listToSerialize.Count != listToDeserialize.Count)
            {
                Console.WriteLine("Incorrect length of lists!");
                return;
            }

            var serializedNode = listToSerialize.Head;
            var deserializedNode = listToDeserialize.Head;

            while (serializedNode != null)
            {
                if (serializedNode.Data == deserializedNode.Data
                    && serializedNode.Random.Data == deserializedNode.Random.Data)
                    Console.WriteLine("Ok!");
                else
                    Console.WriteLine("Incorrect data!");

                serializedNode = serializedNode.Next;
                deserializedNode = deserializedNode.Next;
            }
        }

        static void Main(string[] args)
        {
            const int length = 4;
            var head = new ListNode {Data = Rand.Next(0, 100).ToString()};
            var tail = head;

            for (var i = 1; i < length; i++)
                tail = AddNode(tail);

            var temp = head;

            for (var i = 0; i < length; i++)
            {
                temp.Random = SetRandomNode(head, length);
                temp = temp.Next;
            }

            var listToSerialize = new ListRandom
            {
                Head = head,
                Tail = tail, 
                Count = length
            };
            var fileStreamToSerialize = new FileStream("serializedData.txt", FileMode.OpenOrCreate);
            listToSerialize.Serialize(fileStreamToSerialize);

            var listToDeserialize = new ListRandom();
            var fileStreamToDeserialize = new FileStream("serializedData.txt", FileMode.Open);
            listToDeserialize.Deserialize(fileStreamToDeserialize);

            CheckResult(listToSerialize, listToDeserialize);
        }
    }
}
