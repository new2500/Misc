#region O(1) map

public class ListNode
{
    public int val;  //Sotre the hashing value
    public ListNode prev;
    public ListNode next;

    public ListNode(int val)
    {
        this.val = val;
        prev = null;
        next = null;
    }
}

public class HashEntry
{
    private int key;
    private int value;
    public ListNode listNode;
    public HashEntry(int k, int v, ListNode n)
    {
        key = k;
        value = v;
        listNode = n;
    }
    public int GetKey()
    {
        return key;
    }
    public int GetValue()
    {
        return value;
    }
}

public class myDS
{
    //public LinkedList<int> idxArr;
    private ListNode head, tail;
    public HashEntry[] eleArr;
    private int capacity = 251; //Some random prime number
    //Constructor
    public myDS()
    {
        //eleArr = new HashEntry[capacity];   //Default value is null
        //idxArr = new LinkedList<int>();
        head = new ListNode(0);
        tail = new ListNode(0);
        Clear();
    }
    /* Helper - Add hash value into list's tail*/

    private ListNode addToTail(int hash)
    {
        ListNode newNode = new ListNode(hash);
        //Insert it to the end of the list
        newNode.prev = tail.prev;
        newNode.next = tail;
        tail.prev.next = newNode;
        tail.prev = newNode;

        return newNode;
    }

    //Add
    public void Add(int key, int value)
    {
        int hash = key % capacity;
        while (eleArr[hash] != null && eleArr[hash].GetKey() != key)
        {
            hash = (hash + 1) % capacity;
        }
        eleArr[hash] = new HashEntry(key, value, addToTail(hash));
        //idxArr.AddFirst(hash);
    }

    //Search
    public int Get(int key)
    {
        int hash = key % capacity;
        while (eleArr[hash] != null && eleArr[hash].GetKey() != key)
        {
            hash = (hash + 1) % capacity;
        }
        //Not exist
        if (eleArr[hash] == null)
            return -1;
        else
            return eleArr[hash].GetValue();
    }
    //Clear, O(1)
    public void Clear()
    {
        var newEleArr = new HashEntry[capacity];
        //var newIdxArr = new LinkedList<int>();
        eleArr = newEleArr;
        //idxArr = newIdxArr;
        head.next = tail;
        tail.prev = head;
    }
    //Delete
    public void Delete(int key)
    {
        int hash = key % capacity;
        //Remove the list node in O(1) time
        ListNode DeletedNode = eleArr[hash].listNode;
        DeletedNode.prev.next = DeletedNode.next;
        DeletedNode.next.prev = DeletedNode.prev;


        eleArr[hash] = null;
        /*
         * Delete the index in linked-list in O(1) time...impossible
         */
    }
    //Iterator
    public void Iterate()
    {
        //var idxEnumerator = idxArr.GetEnumerator();
        //while (idxEnumerator.MoveNext() && idxEnumerator.Current != null)
        //{
        //    int idx = idxEnumerator.Current;
        //    if (eleArr[idx] != null)
        //    {
        //        Console.WriteLine("Key: " + eleArr[idx].GetKey() + "Value: " + eleArr[idx].GetValue()); //Print or do something
        //    }
        //}
        /*Now just iterate the Double-LinkedList, and retrieve the KeyValuePair associated with the ListNode*/

        ListNode start = head.next;
        while (start != tail)
        {
            if (eleArr[start.val] != null)
            {
                //Do something, We print the value here
                Console.WriteLine("Key: " + eleArr[start.val].GetKey() +
                                  "Value: " + eleArr[start.val].GetValue() );
            }
        }
    }
}

#endregion