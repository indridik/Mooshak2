#include <iostream>

using namespace std;

void getInput(int a[]);
int getSearchValue();
int searchForValue(int a[], int value);
void removeValueAtIndex(int a[], int index);
void displayResult(const int a[]);

const int MAX = 5;
const int NOT_FOUND = -1;

int main()
{
    int arr[MAX];

    getInput(arr);
    int value = getSearchValue();
    int index = searchForValue(arr, value);
    removeValueAtIndex(arr, index);
    displayResult(arr);

    return 0;
}

void getInput(int a[]) {
    cout << "Enter " << MAX << " numbers: ";
    for (int i = 0; i < MAX; i++)
        cin >> a[i];
}

int getSearchValue() {
    int value;
    cout << "Input a search value: ";
    cin >> value;

    return value;
}

int searchForValue(int a[], int value) {
    for (int i = 0; i < MAX; i++) {
        if (a[i] == value)
            return i;
    }
    return NOT_FOUND;
}

void removeValueAtIndex(int a[], int index) {
    for (int i = index; i < MAX-1; i++)
        a[i] = a[i+1];
    a[MAX-1] = 0;
}

void displayResult(const int a[]) {
    for (int i = 0; i < MAX; i++)
        cout << a[i] << " ";
}


