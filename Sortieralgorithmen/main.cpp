// Pro2_UEB05_KlaasKrüger
//
#include "student.h"

int main() {
    try {
        string filename = "C:\\Users\\Klaas\\Desktop\\Studenten.txt"; // Absoluter Pfad zur Datei
        vector<Student> students = readStudentsFromFile(filename);

        cout << "Ungeordnete Liste von Studenten: ";
        print_vector(students);

        // Shuffle Studenten
        shuffle_students(students);
        cout << "Gemischte Liste von Studenten: ";
        print_vector(students);

        // Sortieren mit Selection Sort
        vector<Student> selectionSorted = students;
        sort_by_name_and_group(selectionSorted, selection_sort);
        cout << "Selection Sort nach Name und Seminargruppe: ";
        print_vector(selectionSorted);

        sort_by_group_and_name(selectionSorted, selection_sort);
        cout << "Selection Sort nach Seminargruppe und Name: ";
        print_vector(selectionSorted);

        // Sortieren mit Insertion Sort
        vector<Student> insertionSorted = students;
        sort_by_name_and_group(insertionSorted, insertion_sort);
        cout << "Insertion Sort nach Name und Seminargruppe: ";
        print_vector(insertionSorted);

        sort_by_group_and_name(insertionSorted, insertion_sort);
        cout << "Insertion Sort nach Seminargruppe und Name: ";
        print_vector(insertionSorted);

        // Sortieren mit Bubble Sort
        vector<Student> bubbleSorted = students;
        sort_by_name_and_group(bubbleSorted, bubble_sort);
        cout << "Bubble Sort nach Name und Seminargruppe: ";
        print_vector(bubbleSorted);

        sort_by_group_and_name(bubbleSorted, bubble_sort);
        cout << "Bubble Sort nach Seminargruppe und Name: ";
        print_vector(bubbleSorted);
    }
    catch (const exception& e) {
        cerr << "Fehler: " << e.what() << endl;
    }

    return 0;
}
