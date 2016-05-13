#ifndef BINARYTREE_CPP
#define BINARYTREE_CPP

#include <iostream>
#include <algorithm>    // For the max function
#include "binarytree.h"
using namespace std;

template <class T>
int BinaryTree<T>::height() const {    // Returns the height of the tree
    return height(root);
}

template <class T>
int BinaryTree<T>::height(BinaryNode<T>* node) const  { // Returns the height of the tree starting at node
    if (node == NULL) {
        return -1;
    }
    else if (node->isLeaf()) {
        return 0;
    }
    else {
        return (1 + max<int>(height(node->leftChild), height(node->rightChild)));
    }
}

template <class T>
int BinaryTree<T>::size() const { // Returns the number of nodes in the tree
    return size(root);
}

template <class T>
int BinaryTree<T>::size(BinaryNode<T>* node) const {
    // Returns the number of nodes in the tree starting at the given node
    if (node == NULL) {       // Base case
        return 0;
    }
    else {
        return 1 + size(node->leftChild) + size(node->rightChild);
    }
}


template <class T>
void BinaryTree<T>::deleteAll(BinaryNode<T>*& node) {
    // Deletes all nodes in the tree starting at node
    if (node == NULL)
        ;               // Don't do anything
    else {
        deleteAll(node->leftChild);
        deleteAll(node->rightChild);
        
        if (node->isLeaf()) {
            //cout << "Deleting " << node->item << endl;
            delete node;
            node = NULL;    // Set it to NULL because the node that this pointer pointed to has been removed
        }
    }
}


template <class T>
void BinaryTree<T>::inorder() const { // Traverses the tree in inorder
    inorder(root);
}

template <class T>
void BinaryTree<T>::preorder() const { // Traverses the tree in preorder
    preorder(root);
}

template <class T>
void BinaryTree<T>::postorder() const { // Traverses the tree in postorder
    postorder(root);
}

template <class T>
void BinaryTree<T>::inorder(BinaryNode<T>* node) const { // Traverses the nodes in inorder
    if (node != NULL) {
        inorder(node->leftChild);
        cout << node->item << " ";
        inorder(node->rightChild);
    }
}

template <class T>
void BinaryTree<T>::preorder(BinaryNode<T>* node) const { // Traverses the nodes in preorder
    if (node != NULL) {
        cout << node->item << " ";
        preorder(node->leftChild);
        preorder(node->rightChild);
    }
}

template <class T>
void BinaryTree<T>::postorder(BinaryNode<T>* node) const { // Traverses the nodes in postorder
    if (node != NULL) {
        postorder(node->leftChild);
        postorder(node->rightChild);
        cout << node->item << " ";
    }
}
#endif // BINARYTREE_CPP
