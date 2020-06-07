using System;
using System.IO;
using System.Text.RegularExpressions;


/*Jaimie Clampitt
 *6/6/2020
 */
public class LinkedListTest
{
    Node head;

    //sets up the Node
    public class Node
    {
        public int data;
        public Node next;
        public Node(int d)
        {
            data = d;
            next = null;
        }
    }
    //Append inserts the new node to the end of the linked list
    public void AppendNode(int new_data)
    {
        Node new_node = new Node(new_data);
        //this will make the new node the head if list is empty
        if (head == null)
        {
            head = new Node(new_data);
            return;
        }

        new_node.next = null;

        Node last = head;
        //continuing to the last node
        while (last.next != null)
            last = last.next;

        last.next = new_node;
        return;
        //O(n) since n depends on nodes, and they must be looped through
    }

    //DeleteNode will delete the first occurrence of the matching value
    void DeleteNode(int key)
    {
        Node temp = head, prev = null;
        //checks to see if data matches and it is not null
        if (temp != null && temp.data == key)
        {
            head = temp.next;
            return;
        }

        while (temp != null && temp.data != key)
        {
            prev = temp;
            temp = temp.next;
        }

        if (temp == null) return;

        prev.next = temp.next;
    }
    //prints out the node, and the node location in the list while it is not null
    public void PrintList()
    {
        int count = 0;
        Node n = head;
        while (n != null)
        {
            count++;
            Console.WriteLine("Node" + count + ":" + n.data);
            n = n.next;
        }
    }

    public static void Main(string[] args)
    {
        LinkedListTest llist = new LinkedListTest();


        String line;

        string resultString;
        string fullPath;

        try
        {
            //allows filepath to be entered into command line
            string filePath;
            //it will also work if you just put the 'name'.txt but better safe than sorry with the entire filepath
            Console.WriteLine("Enter Path to desired text file: ");
            filePath = @"" + Console.ReadLine();

            //the below also works if you would rather put the filepath in here and comment out line 96
            //filePath = @"inputNodes.txt";

            //checks filepath, if incomplete, will throw exception
            fullPath = Path.GetFullPath(filePath);
            Console.WriteLine("GetFullPath('{0}') returns '{1}'",
                filePath, fullPath);

            FileInfo file = new FileInfo(filePath);
            //reads file
            StreamReader sr = new StreamReader(filePath);
            line = sr.ReadLine();
            
            //will read file until it reaches the end
            while (line != null)
            {

                if (line.StartsWith("i"))
                {   //performing append on the list if it starts with the i command and taking the number out by regular expression
                    resultString = Regex.Match(line, @"\d+").Value;
                    //putting the number into the linked list
                    llist.AppendNode(Int32.Parse(resultString));
                    
                    line = sr.ReadLine();
                }
                else if (line.StartsWith("d"))
                {   //performing delete on the list if it starts with the d command and removing num by regex
                    resultString = Regex.Match(line, @"\d+").Value;
                    llist.DeleteNode(Int32.Parse(resultString));

                    line = sr.ReadLine();

                }
            }

            //printing the linkedlist
            llist.PrintList();

            //closed the file
            sr.Close();


        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }


    }
}
