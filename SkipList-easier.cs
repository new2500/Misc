    #region Skip List with Search, Add
    public class SkipList<TKey, TValue>
    {
        private SkipListNode<TKey, TValue> head;
        private int count;

        public SkipList()  //Constructor
        {
            this.head = new SkipListNode<TKey, TValue>();
            count = 0;
        }

        public void Add(TKey key, TValue value)
        {
            SkipListNode<TKey, TValue> position;
            bool found = Search(key, out position);
            if (found)
                position.KeyValuePair.Value = value;
            else //Not exist, we need to add
            {
                SkipListNode<TKey, TValue> newEntry = new SkipListNode<TKey, TValue>(key, value);
                count++;

                newEntry.back = position;
                if (position.forward != null)
                    newEntry.forward = position.forward;
                position.forward = newEntry;
                Promote(newEntry);
            }
        }

        private bool Search(TKey key, out SkipListNode<TKey, TValue> position)
        {
            if (key == null)
                throw new ArgumentNullException();

            SkipListNode<TKey, TValue> current;
            position = current = head;

            while ((current.isFront || key - current.Key >= 0)       //Is front or Greater then current   && forward is not null OR fown is not null
                   && (current.forward != null || current.down != null))
            {
                position = current;
                if (key - current.Key == 0) //We found it
                    return true;
                if (current.forward == null || key - current.forward.key < 0)  //Forward is null and it's greater then current, wo must go down
                {
                    if (current.down == null)
                        return false;
                    else
                        current = current.down;
                }
                else                          //Rest, go forward
                    current = current.forward;
            }
            position = current;

            //If the matching value is found in the last position of the last row, we could end up here with a match
            if (key - position.key == 0)
                return true;
            else
                return false;
        }


        private void Promote(SkipListNode<TKey, TValue> node)
        {
            //Up: our search for the value just priote to the newly
            //    added value in the next row to which the newly added
            //    value should be promoted.
            //Last: the most recently added node, starting with the newly created node.
            SkipListNode<TKey, TValue> up = node.back;
            SkipListNode<TKey, TValue> last = node;

            for (int levels = this.levels(); levels > 0; levels--)
            {
                //Find the next node back that links to next row up.
                //If we find our way back to the head of row and there
                //is no link up then that means it is time to create a new row.
                while (up.up == null && !up.isFront)
                    up = up.back;
                if (up.isFront && up.up == null)
                {
                    up.up = new SkipListNode<TKey, TValue>();
                    head = up.up;   //Head always point to the highest level
                }

                up = up.up;

                //At this point, up should point the value in the next
                //row immediately prior to where the new node should be promoted. If this node has been promoted to a previously unreached level, then up will be the head of the new row.
                SkipListNode<TKey, TValue> newNode = new SkipListNode<TKey, TValue>(node.KeyValuePair);

                newNode.forward = up.forward;
                up.forward = newNode;
                //Remember last starts as the brand new node but should be updated to always point to the representative node in the previous row.
                newNode.down = last;
                newNode.down.up = newNode;
                last = newNode;
            }
        }

        private int levels()
        {
            Random ran = new Random();
            int levels = 0;
            while (ran.NextDouble() < 0.5)
                levels++;
            return levels;
        }


        public class SkipListNode<TNKey, TNValue>
        {
            public SkipListNode<TNKey, TNValue> forward, back, up, down;
            public SkipKVPair<TNKey, TNValue> KeyValuePair;
            public bool isFront = false;


            public SkipListNode()
            {
                this.KeyValuePair = new SkipKVPair<TNKey, TNValue>(default(TNKey), default(TNValue));
                this.isFront = true;
            }
            public SkipListNode(TNKey key, TNValue val)
            {
                this.KeyValuePair = new SkipKVPair<TNKey, TNValue>(key, val);
                this.isFront = true;
            }

        }
        public struct SkipKVPair<X, Y>
        {
            public X Key;
            public Y Value;

            public SkipKVPair(X key, Y value)
            {
                Key = key;
                Value = value;
            }
        }
    }

    #endregion