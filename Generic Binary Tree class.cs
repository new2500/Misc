#region Generic Binary Tree

    public class Node<T>
    {
        //Private attributes
        private T _node = default(T);
        private Node<T> _rightChild = default(Node<T>);
        private Node<T> _leftChild = default(Node<T>);

        //Constructor
        public Node()
        {
            _node = default(T);
            LeftChild = null;
            _rightChild = null;
        }
        public Node(T val)
        {
            _node = val;
            LeftChild = null;
            _rightChild = null;
        }

        #region Accessors
		//Here we don't use the Auto-Properties for clearer logic.
		//Could consider put the initializer for attributes here, then we need to remove attributes.
        public T Value 
		{ 
			get{  return _node; } 
			set{  _node = value; } 
		}
        public Node<T> LeftChild        
        {
            get { return _leftChild; }
            set { _leftChild = value; }
        }
        public Node<T> RightChild
        {
            get { return _rightChild; }
            set { _rightChild = value; }
        }
        #endregion
		
        #region Methods
        //Return total element of trees
        public int Count()     //Should separate the logic and public method, but here we implement the whole logic for simplicity.
        {
            return 1 +
                (_leftChild != null ? _leftChild.Count() : 0) +
                (_rightChild != null ? _rightChild.Count() : 0)
            ;

            //Or just 1 line...
            // return 1 + (_leftChild?.Count() ?? 0) + (_rightChild?.Count() ?? 0);
        }
        //Enumerate the Nodes
        public IEnumerable<T> EnumerateNodes()
        {
            yield return _node;
            if (_leftChild != null)
                foreach (T child in _leftChild.EnumerateNodes())
                {
                    yield return child;
                }
            if (_rightChild != null)
                foreach (T child in _rightChild.EnumerateNodes())
                {
                    yield return child;
                }
        }
        #endregion
    }

    #endregion