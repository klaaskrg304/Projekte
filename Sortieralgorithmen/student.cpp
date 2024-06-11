#include "student.h"

// Definition der Klasse Student
Student::Student(const string& n, int sg) : name(n), seminarGroup(sg) {}

bool Student::operator<(const Student& other) const {
    return name < other.name;
}

bool Student::operator>(const Student& other) const {
    return name > other.name;
}

// Funktion zum Einlesen der Studentendaten aus einer Datei
vector<Student> readStudentsFromFile(const string& filename) {
    vector<Student> students;
    ifstream file(filename);

    if (!file) {
        cerr << "Fehler: Datei " << filename << " nicht gefunden." << endl;
        throw runtime_error("Datei nicht gefunden");
    }

    string name;
    int seminarGroup;
    while (file >> name >> seminarGroup) {
        students.emplace_back(name, seminarGroup);
    }

    if (students.empty()) {
        cerr << "Warnung: Datei " << filename << " ist leer oder hat ein falsches Format." << endl;
    }
    else {
        cout << "Erfolgreich " << students.size() << " Studenten eingelesen." << endl;
    }

    return students;
}

// Funktion zum Mischen der Studenten
void shuffle_students(vector<Student>& students) {
    srand(static_cast<unsigned>(time(0)));
    random_shuffle(students.begin(), students.end());
}

// Funktion zum Sortieren nach Namen und Seminargruppe
void sort_by_name_and_group(vector<Student>& students, void(*sort_func)(vector<Student>&)) {
    sort_func(students);
    auto name_cmp = [](const Student& a, const Student& b) { return a.name < b.name; };
    stable_sort(students.begin(), students.end(), name_cmp);

    auto group_cmp = [](const Student& a, const Student& b) { return a.seminarGroup < b.seminarGroup; };
    stable_sort(students.begin(), students.end(), group_cmp);
}

// Funktion zum Sortieren nach Seminargruppe und Namen
void sort_by_group_and_name(vector<Student>& students, void(*sort_func)(vector<Student>&)) {
    sort_func(students);
    auto group_cmp = [](const Student& a, const Student& b) { return a.seminarGroup < b.seminarGroup; };
    stable_sort(students.begin(), students.end(), group_cmp);

    auto name_cmp = [](const Student& a, const Student& b) { return a.name < b.name; };
    stable_sort(students.begin(), students.end(), name_cmp);
}
