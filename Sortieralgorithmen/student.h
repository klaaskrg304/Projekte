#ifndef STUDENT_H
#define STUDENT_H

#include <iostream>
#include <fstream>
#include <vector>
#include <string>
#include <algorithm>
#include <stdexcept>
#include <ctime>
#include <cstdlib>

using namespace std;

// Klasse Student
class Student {
public:
    string name;
    int seminarGroup;

    Student(const string& n, int sg);

    bool operator<(const Student& other) const;
    bool operator>(const Student& other) const;
};

// Funktion zum Einlesen der Studentendaten aus einer Datei
vector<Student> readStudentsFromFile(const string& filename);

// Templatefunktion für die Ausgabe eines Vektors
template <typename T>
void print_vector(const vector<T>& vec) {
    for (const auto& elem : vec) {
        cout << elem.name << " (" << elem.seminarGroup << ") ";
    }
    cout << endl;
}

// Funktion zum Mischen der Studenten
void shuffle_students(vector<Student>& students);

// Templatefunktion für Selection Sort
template <typename T>
void selection_sort(vector<T>& vec) {
    for (size_t i = 0; i < vec.size() - 1; ++i) {
        size_t min_index = i;
        for (size_t j = i + 1; j < vec.size(); ++j) {
            if (vec[j] < vec[min_index]) {
                min_index = j;
            }
        }
        swap(vec[i], vec[min_index]);
    }
}

// Templatefunktion für Insertion Sort
template <typename T>
void insertion_sort(vector<T>& vec) {
    for (size_t i = 1; i < vec.size(); ++i) {
        T key = vec[i];
        size_t j = i;
        while (j > 0 && vec[j - 1] > key) {
            vec[j] = vec[j - 1];
            --j;
        }
        vec[j] = key;
    }
}

// Templatefunktion für Bubble Sort
template <typename T>
void bubble_sort(vector<T>& vec) {
    bool swapped;
    for (size_t i = 0; i < vec.size() - 1; ++i) {
        swapped = false;
        for (size_t j = 0; j < vec.size() - 1 - i; ++j) {
            if (vec[j] > vec[j + 1]) {
                swap(vec[j], vec[j + 1]);
                swapped = true;
            }
        }
        if (!swapped) break;
    }
}

// Funktion zum Sortieren nach Namen und Seminargruppe
void sort_by_name_and_group(vector<Student>& students, void(*sort_func)(vector<Student>&));

// Funktion zum Sortieren nach Seminargruppe und Namen
void sort_by_group_and_name(vector<Student>& students, void(*sort_func)(vector<Student>&));

#endif // STUDENT_H
