// This program counts the occurrences of each English letter in an input string
#include <iostream>
#include <string>
#include <cctype>

using namespace std;

string getString();
string makeLower(const string& s);
void countChars(const string& s, int arr[]);
void displayResult(int arr[]);

const int LETTERS = 26;

int main( )
{
    int arr[LETTERS] = {0};

    string str = getString();
    string lowerStr = makeLower(str);
    countChars(lowerStr, arr);
    displayResult(arr);

    return 0;
}

string getString()
{
    string str;
    cout << "Enter a string: ";
    getline(cin, str);

    return str;
}

string makeLower(const string& s)
{
    string temp(s);
    for (unsigned int i = 0; i < s.length( ); i++)
        temp[i] = tolower(s[i]);

    return temp;
}

void countChars(const string& s, int arr[]) {

    for (unsigned int i = 0; i < s.length(); i++) {
        char ch = s[i];
        if (isalpha(ch)) {
            int idx = ch - 'a';
            arr[idx]++;
        }
    }
}

void displayResult(int arr[]) {

    for (int i = 0; i < LETTERS; i++) {
        if (arr[i] > 0) {
            char letter = 'a' + i;
            cout << letter << ": " << arr[i] << endl;
        }
    }
}






