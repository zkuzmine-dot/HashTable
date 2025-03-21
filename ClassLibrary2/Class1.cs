using System;

public class AVLTree<TKey, TValue> where TKey : IComparable<TKey>
{
    private class Node
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public int Height { get; set; }

        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Height = 1;
        }
    }

    private Node _root;

    // Вставка элемента
    public void Insert(TKey key, TValue value)
    {
        _root = Insert(_root, key, value);
    }

    private Node Insert(Node node, TKey key, TValue value)
    {
        if (node == null)
            return new Node(key, value);

        int compare = key.CompareTo(node.Key);
        if (compare < 0)
            node.Left = Insert(node.Left, key, value);
        else if (compare > 0)
            node.Right = Insert(node.Right, key, value);
        else
            node.Value = value; // Если ключ уже существует, обновляем значение

        return Balance(node);
    }

    // Удаление элемента
    public void Remove(TKey key)
    {
        _root = Remove(_root, key);
    }

    private Node Remove(Node node, TKey key)
    {
        if (node == null)
            return null;

        int compare = key.CompareTo(node.Key);
        if (compare < 0)
            node.Left = Remove(node.Left, key);
        else if (compare > 0)
            node.Right = Remove(node.Right, key);
        else
        {
            // Узел найден
            if (node.Left == null)
                return node.Right;
            if (node.Right == null)
                return node.Left;

            // Узел с двумя детьми: заменяем на минимальный элемент из правого поддерева
            Node min = FindMin(node.Right);
            node.Key = min.Key;
            node.Value = min.Value;
            node.Right = Remove(node.Right, min.Key);
        }

        return Balance(node);
    }

    private Node FindMin(Node node)
    {
        while (node.Left != null)
            node = node.Left;
        return node;
    }

    // Поиск элемента
    public bool TryGetValue(TKey key, out TValue value)
    {
        Node node = Find(_root, key);
        if (node != null)
        {
            value = node.Value;
            return true;
        }
        value = default(TValue);
        return false;
    }

    private Node Find(Node node, TKey key)
    {
        if (node == null)
            return null;

        int compare = key.CompareTo(node.Key);
        if (compare < 0)
            return Find(node.Left, key);
        else if (compare > 0)
            return Find(node.Right, key);
        else
            return node;
    }

    // Балансировка
    private Node Balance(Node node)
    {
        if (node == null)
            return null;

        UpdateHeight(node);
        int balanceFactor = GetBalanceFactor(node);

        // Левый поддерево выше
        if (balanceFactor > 1)
        {
            if (GetBalanceFactor(node.Left) < 0)
                node.Left = RotateLeft(node.Left); // Левый-правый случай
            return RotateRight(node);
        }
        // Правый поддерево выше
        if (balanceFactor < -1)
        {
            if (GetBalanceFactor(node.Right) > 0)
                node.Right = RotateRight(node.Right); // Правый-левый случай
            return RotateLeft(node);
        }

        return node;
    }

    private int GetHeight(Node node) => node?.Height ?? 0;

    private void UpdateHeight(Node node)
    {
        node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
    }

    private int GetBalanceFactor(Node node)
    {
        return GetHeight(node.Left) - GetHeight(node.Right);
    }

    private Node RotateRight(Node y)
    {
        Node x = y.Left;
        Node T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        UpdateHeight(y);
        UpdateHeight(x);

        return x;
    }

    private Node RotateLeft(Node x)
    {
        Node y = x.Right;
        Node T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        UpdateHeight(x);
        UpdateHeight(y);

        return y;
    }
}