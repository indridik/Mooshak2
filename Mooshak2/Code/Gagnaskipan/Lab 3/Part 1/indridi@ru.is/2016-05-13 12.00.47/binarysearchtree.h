#ifndef BINARYSEARCHTREE_H
#define BINARYSEARCHTREE_H

#include "binarytree.h"

template <class T>
class BinarySearchTree : public BinaryTree<T>
{
    public:
        BinarySearchTree();
        BinarySearchTree(const T& rootItem);
        ~BinarySearchTree();
        BinaryNode<T>* maxNode() const; // Returns the node containing the largest item
        BinaryNode<T>* minNode() const; // Returns the node containing the smallest item
        BinaryNode<T>* find(const T& anItem) const; // Retrieves the node corresponding to the item.
                                                    // If not found, returns null
        void insert(const T& anItem); // Inserts the item into the tree
        void remove(const T& anItem); // Removes the item from the tree
    
        T sum();
        T sum(BinaryNode<T>* node);     //returns the sum of all the nodes in the tree

    
        BinaryNode<T>* find(T value);
        //returns a pointer to the node in the tree node that has the value value. If no such
        //node exists, the function returns NULL. Note that node is a binary search tree
        BinaryNode<T>* find(BinaryNode<T>* node, T value);
        BinaryNode<T>* max(BinaryNode<T>* node);
        BinaryNode<T>* maxMin();
        BinaryNode<T>* maxMin(BinaryNode<T>* node);
    
    private:
        BinaryNode<T>* maxNode(BinaryNode<T> *node) const;  // Returns the node containing the largest item
                                                            // in the subtree whose root is node
        BinaryNode<T>* minNode(BinaryNode<T> *node) const;  // Returns the node containing the smallest item
                                                            // in the subtree whose root is node
        BinaryNode<T>* findAt(BinaryNode<T> *node, const T& anItem) const; // Retrieves an item, searching from node
        void insertAt(BinaryNode<T> *& node, const T& anItem);  // Inserts the item into the subtree whose root is node
        void removeAt(BinaryNode<T> *& node, const T& anItem);  // Removes the item from the subtree whose root is node
        void removeNode(BinaryNode<T> *& node);                 // Remove the node from the tree
        T processLeftmost(BinaryNode<T> *& node); // Retrieves and removes the leftmost descendant of the given node

};

#endif // BINARYSEARCHTREE_H
