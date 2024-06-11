#include "Complex.h"

// Implementierung der Klasse Complex
Complex::Complex(double real_part, double imag_part) : re(real_part), im(imag_part) {}

// Überladung des Ausgabeoperators
ostream& operator<<(ostream& os, const Complex& c) {
    os << c.re << " + " << c.im << "i";
    return os;
}

// Überladung des Additionsoperators
Complex operator+(const Complex& c1, const Complex& c2) {
    return Complex(c1.re + c2.re, c1.im + c2.im);
}

// Überladung des Multiplikationsoperators (Complex * Complex)
Complex operator*(const Complex& c1, const Complex& c2) {
    return Complex(c1.re * c2.re - c1.im * c2.im, c1.re * c2.im + c1.im * c2.re);
}

// Überladung des Multiplikationsoperators (double * Complex)
Complex operator*(double d, const Complex& c) {
    return Complex(d * c.re, d * c.im);
}

// Überladung des Multiplikationsoperators (Complex * double)
Complex operator*(const Complex& c, double d) {
    return Complex(d * c.re, d * c.im);
}

// Überladung des Zuweisungsoperators (+=)
Complex& Complex::operator+=(const Complex& other) {
    re += other.re;
    im += other.im;
    return *this;
}

// Überladung des Divisionsoperators
Complex operator/(const Complex& c1, const Complex& c2) {
    double denom = c2.re * c2.re + c2.im * c2.im;
    if (denom == 0) throw Complex_ExcDivByZero(c1, c2); // Division durch Null überprüfen
    if (c1.magnitude() / sqrt(denom) > 1e14) throw Complex_ExcDivNumeric(c1, c2); // Numerischen Fehler überprüfen
    return Complex((c1.re * c2.re + c1.im * c2.im) / denom, (c1.im * c2.re - c1.re * c2.im) / denom);
}

// Berechnung des Betrags der komplexen Zahl
double Complex::magnitude() const {
    return sqrt(re * re + im * im);
}

// Implementierung der Klasse Complex_ExcDiv
Complex_ExcDiv::Complex_ExcDiv(const Complex& c1, const Complex& c2) : dividend(c1), divisor(c2) {}

ostream& operator<<(ostream& os, const Complex_ExcDiv& e) {
    e.print();
    return os;
}

// Implementierung der Klasse Complex_ExcDivByZero
Complex_ExcDivByZero::Complex_ExcDivByZero(const Complex& c1, const Complex& c2) : Complex_ExcDiv(c1, c2) {}

void Complex_ExcDivByZero::print() const {
    cout << "Division durch Null Fehler: " << dividend << " / " << divisor << endl;
}

// Implementierung der Klasse Complex_ExcDivNumeric
Complex_ExcDivNumeric::Complex_ExcDivNumeric(const Complex& c1, const Complex& c2) : Complex_ExcDiv(c1, c2) {}

void Complex_ExcDivNumeric::print() const {
    cout << "Numerischer Fehler: " << dividend << " / " << divisor << endl;
}

// Funktion zur Durchführung von Testdivisionen
void testDivision() {
    Complex c1(4.0, 2.0);
    Complex c2(2.0, 1.0);
    Complex c3(0.0, 0.0);
    Complex c4(1e15, 0.0);

    try {
        cout << "c1 / c2 = " << c1 / c2 << endl;
    }
    catch (const Complex_ExcDivByZero& e) {
        cout << e << endl;
    }
    catch (const Complex_ExcDivNumeric& e) {
        cout << e << endl;
    }
    catch (const Complex_ExcDiv& e) {
        cout << e << endl;
    }

    try {
        cout << "c1 / c3 = " << c1 / c3 << endl; // Division durch Null
    }
    catch (const Complex_ExcDivByZero& e) {
        cout << e << endl;
    }
    catch (const Complex_ExcDivNumeric& e) {
        cout << e << endl;
    }
    catch (const Complex_ExcDiv& e) {
        cout << e << endl;
    }

    try {
        cout << "c1 / c4 = " << c1 / c4 << endl; // Numerischer Fehler
    }
    catch (const Complex_ExcDivByZero& e) {
        cout << e << endl;
    }
    catch (const Complex_ExcDivNumeric& e) {
        cout << e << endl;
    }
    catch (const Complex_ExcDiv& e) {
        cout << e << endl;
    }
}
