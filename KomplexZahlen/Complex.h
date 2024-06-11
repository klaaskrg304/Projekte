#ifndef COMPLEX_H
#define COMPLEX_H

#include <iostream>
#include <cmath>
#include <exception>

using namespace std;

// Klasse zur Darstellung von komplexen Zahlen
class Complex {
    double re; // Realteil
    double im; // Imagin�rteil
public:
    Complex(double real_part, double imag_part);

    // �berladung des Ausgabeoperators
    friend ostream& operator<<(ostream& os, const Complex& c);

    // �berladung des Additionsoperators
    friend Complex operator+(const Complex& c1, const Complex& c2);

    // �berladung des Multiplikationsoperators (Complex * Complex)
    friend Complex operator*(const Complex& c1, const Complex& c2);

    // �berladung des Multiplikationsoperators (double * Complex)
    friend Complex operator*(double d, const Complex& c);

    // �berladung des Multiplikationsoperators (Complex * double)
    friend Complex operator*(const Complex& c, double d);

    // �berladung des Zuweisungsoperators (+=)
    Complex& operator+=(const Complex& other);

    // �berladung des Divisionsoperators
    friend Complex operator/(const Complex& c1, const Complex& c2);

    // Berechnung des Betrags der komplexen Zahl
    double magnitude() const;
};

// Basisklasse f�r Divisions-Ausnahmen
class Complex_ExcDiv : public exception {
protected:
    Complex dividend; // Dividend der fehlerhaften Division
    Complex divisor;  // Divisor der fehlerhaften Division
public:
    Complex_ExcDiv(const Complex& c1, const Complex& c2);
    virtual void print() const = 0; // Virtuelle Ausgabefunktion

    friend ostream& operator<<(ostream& os, const Complex_ExcDiv& e);
};

// Ausnahmeklasse f�r Division durch Null
class Complex_ExcDivByZero : public Complex_ExcDiv {
public:
    Complex_ExcDivByZero(const Complex& c1, const Complex& c2);
    void print() const override; // �berschriebene Ausgabefunktion
};

// Ausnahmeklasse f�r numerische Fehler bei der Division
class Complex_ExcDivNumeric : public Complex_ExcDiv {
public:
    Complex_ExcDivNumeric(const Complex& c1, const Complex& c2);
    void print() const override; // �berschriebene Ausgabefunktion
};

// Funktion zur Durchf�hrung von Testdivisionen
void testDivision();

#endif // COMPLEX_H
