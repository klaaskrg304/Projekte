// Heap 
//Klaas Krüger

#include <iostream>
#include <memory>

using namespace std;

template <typename T>
class Knoten {
public:
    Knoten(T val) : value(val), leftChild(nullptr), rightChild(nullptr) {}

    shared_ptr<Knoten<T>> getLeftChild() const {
        return leftChild;
    }

    void setLeftChild(shared_ptr<Knoten<T>> left) {
        leftChild = left;
    }

    shared_ptr<Knoten<T>> getRightChild() const {
        return rightChild;
    }

    void setRightChild(shared_ptr<Knoten<T>> right) {
        rightChild = right;
    }

    T getValue() const {
        return value;
    }

    void setValue(T val) {
        value = val;
    }

private:
    T value;
    shared_ptr<Knoten<T>> leftChild;
    shared_ptr<Knoten<T>> rightChild;
};

template <typename T>
class Baum {
public:
    Baum() : wurzel(nullptr) {}

    void einfuegen(T val) {
        wurzel = einfuegenRekursiv(wurzel, val);
    }

    void InOrder() const {
        inOrderRekursiv(wurzel);
        cout << endl;
    }

    void PreOrder() const {
        preOrderRekursiv(wurzel);
        cout << endl;
    }

    void PostOrder() const {
        postOrderRekursiv(wurzel);
        cout << endl;
    }

private:
    shared_ptr<Knoten<T>> wurzel;

    shared_ptr<Knoten<T>> einfuegenRekursiv(shared_ptr<Knoten<T>> knoten, T val) {
        if (!knoten) {
            return make_shared<Knoten<T>>(val);
        }
        if (val < knoten->getValue()) {
            knoten->setLeftChild(einfuegenRekursiv(knoten->getLeftChild(), val));
        }
        else {
            knoten->setRightChild(einfuegenRekursiv(knoten->getRightChild(), val));
        }
        return knoten;
    }

    void inOrderRekursiv(shared_ptr<Knoten<T>> knoten) const {
        if (knoten) {
            inOrderRekursiv(knoten->getLeftChild());
            cout << knoten->getValue() << " ";
            inOrderRekursiv(knoten->getRightChild());
        }
    }

    void preOrderRekursiv(shared_ptr<Knoten<T>> knoten) const {
        if (knoten) {
            cout << knoten->getValue() << " ";
            preOrderRekursiv(knoten->getLeftChild());
            preOrderRekursiv(knoten->getRightChild());
        }
    }

    void postOrderRekursiv(shared_ptr<Knoten<T>> knoten) const {
        if (knoten) {
            postOrderRekursiv(knoten->getLeftChild());
            postOrderRekursiv(knoten->getRightChild());
            cout << knoten->getValue() << " ";
        }
    }
};

int main() {
    Baum<int> baum;
    baum.einfuegen(8);
    baum.einfuegen(3);
    baum.einfuegen(10);
    baum.einfuegen(1);
    baum.einfuegen(6);
    baum.einfuegen(14);
    baum.einfuegen(4);
    baum.einfuegen(7);
    baum.einfuegen(13);

    cout << "InOrder Traversierung: ";
    baum.InOrder();

    cout << "PreOrder Traversierung: ";
    baum.PreOrder();

    cout << "PostOrder Traversierung: ";
    baum.PostOrder();

    return 0;
}
