#include <stdio.h>
#include <string.h>
#include <ctype.h>

int main()
{
    int N = 100;
    FILE *fo;
    fo = fopen("sentence.txt", "r");

    int i, quantity = 0;
    char massiv[N];


    for (i = 0; i < N; i++)
    {
        fscanf(fo, "%c", &massiv[i]);
        if (massiv[i] == ' ')
        {
            i--;
            continue;
        }
        if (massiv[i] == '.')
        {
            break;
        }
        if ((massiv[i] >= 'A' && massiv[i] <= 'Z') || (massiv[i] >= 'À' && massiv[i] <= 'ß'))
        {
            massiv[i] = tolower(massiv[i]);
        }
    }
    massiv[i] = '\0';

    N = strlen(massiv);

    for (i = 0; i < N/2; i++)
    {
        if (massiv[i] == massiv[N-i-1])
        {
            quantity++;
        }
    }

    if (quantity == N/2)
    {
        printf("It's palindrome");
    }
    else
    {
        printf("It's not palindrome");
    }

    return 0;
}
