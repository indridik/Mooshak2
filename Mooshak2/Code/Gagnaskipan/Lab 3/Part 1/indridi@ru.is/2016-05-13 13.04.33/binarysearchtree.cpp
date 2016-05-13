#include "binarysearchtree.h"
#include "binarytree.cpp"


template <class T>
BinarySearchTree<T>::BinarySearchTree() : BinaryTree<T>()
{
}

template <class T>
BinarySearchTree<T>::BinarySearchTree(const T& rootItem) : BinaryTree<T>(rootItem)
{
}

template <class T>
BinarySearchTree<T>::~BinarySearchTree()
{
    
}

template<class T>
BinaryNode<T>* BinarySearchTree<T>::maxNode() const {
    return maxNode(BinaryTree<T>::root);
}

template<class T>
BinaryNode<T>* BinarySearchTree<T>::maxNode(BinaryNode<T>* node) const{
    if(node == NULL) {
        return NULL;
    }
    if(node->rightChild == NULL ) {
        return node;
    }
    else {
        return(maxNode(node->rightChild));
    }
}

template<class T>
BinaryNode<T>* BinarySearchTree<T>::minNode() const {
    return minNode(BinaryTree<T>::root);
}

template<class T>
BinaryNode<T>* BinarySearchTree<T>::minNode(BinaryNode<T>* node) const{
    if(node == NULL) {
        return NULL;
    }
    if(node->leftChild == NULL) {
        return node;
    }
    else {
        return minNode(node->leftChild);
    }
}


template<class T>
BinaryNode<T>* BinarySearchTree<T>::find(const T& anItem) const {
    return findAt(BinaryTree<T>::root, anItem);
}

template<class T>
BinaryNode<T>* BinarySearchTree<T>::findAt(BinaryNode<T>* node, const T& anItem) const {
    if(node == NULL) {
        return NULL;
    }
    else if(anItem > node->item) {
        return findAt(node->rightChild, anItem);
    }
    else if(anItem < node->item) {
        return findAt(node->leftChild, anItem);
    }
    else {
        return node;
    }
}

template<class T>
void BinarySearchTree<T>::insert(const T& anItem) {
    if(BinaryTree<T>::root == NULL) {
        BinaryNode<T>* node = new BinaryNode<T>(anItem);
        BinaryTree<T>::root = node;
    }
    else {
        insertAt(BinaryTree<T>::root, anItem);
    }
}


template<class T>
void BinarySearchTree<T>::insertAt(BinaryNode<T>* &node, const T& anItem) {
    if(node == NULL) {
        node = new BinaryNode<T>(anItem);
    }
    if(anItem < node->item) {
        insertAt(node->leftChild, anItem);
    }
    else if(anItem> node->item) {
        insertAt(node->rightChild, anItem);
    }
    else {
        return;
    }
}


template<class T>
void BinarySearchTree<T>::remove(const T& anItem) {
    removeAt(BinaryTree<T>::root, anItem);
}


template<class T>
void BinarySearchTree<T>::removeAt(BinaryNode<T>* &node, const T& anItem) {
    if(node == NULL) {
        return;
    }
    else if(anItem > node->item) {
        removeAt(node->rightChild, anItem);
    }
    else if(anItem < node->item) {
        removeAt(node->leftChild, anItem);
    }
    else {
        removeNode(node);
    }
    
}


template<class T>
void BinarySearchTree<T>::removeNode(BinaryNode<T>* &node) {
    if(node->isLeaf()) {
        delete node;
        node = NULL;
    }
    else if(node->rightChild == NULL) {
        BinaryNode<T>* temp =node;
        node = node->leftChild;
        delete temp;
    }
    else if(node->leftChild == NULL) {
        BinaryNode<T>* temp = node;
        node = node->rightChild;
        delete temp;
    }
    else {
        node->item = processLeftmost(node->rightChild);
        
    }
}


template<class T>
T BinarySearchTree<T>::processLeftmost(BinaryNode<T>* &node) {
    if(node->leftChild == NULL) {
        T elem = node->item;
        removeNode(node);
        return elem;
    }
    else {
        return processLeftmost(node->leftChild);
    }
}

template<class T>
T BinarySearchTree<T>::sum() {
    return sum(BinaryTree<T>::root);
}

template<class T>
T BinarySearchTree<T>::sum(BinaryNode<T>* node) {
    if(node == NULL) {
        return 0;
    }
    return node->item + sum(node->leftChild) + sum(node->rightChild);
}


template<class T>
BinaryNode<T>* BinarySearchTree<T>::find(T value) {
    return find(BinaryTree<T>::root, value);
}


template<class T>
BinaryNode<T>* BinarySearchTree<T>::find(BinaryNode<T>* node, T value) {
    if(node == NULL) {
        return NULL;
    }
    if(node->item > value) {
        return find(node->leftChild, value);
    }
    else if(node->item < value) {
        return find(node->rightChild, value);
    }
    else {
        return node;
    }
}


template<class T>
BinaryNode<T>* BinarySearchTree<T>::max(BinaryNode<T>* node) {
    if(node == NULL) {
        return NULL;
    }
    if(node->rightChild == NULL) {
        cout << "the maxMin is: " << node->item << endl;
        return node;
    }
    return max(node->rightChild);
}
template<class T>
BinaryNode<T>* BinarySearchTree<T>::maxMin() {
    return maxMin(BinaryTree<T>::root);
}

template<class T>
BinaryNode<T>* BinarySearchTree<T>::maxMin(BinaryNode<T>* node) {
    if(node == NULL) {
        return NULL;
    }
    if(node->leftChild == NULL) {
        return NULL;
    }
    BinaryNode<T>* temp= node->leftChild;
    while(temp->rightChild != NULL) {
        temp = temp->rightChild;
    }
    
    return temp;
    //return max(node->leftChild);
}

