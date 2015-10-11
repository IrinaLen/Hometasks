#include <stdio.h>

int main()
{
    int N;
    FILE *fo;
    fo = fopen("massiv.txt","r");
    fscanf(fo, "%d", &N);

    int massiv[N], i, quantity = 0;

    for (i = 0; i < N; i++)
    {
        fscanf(fo, "%d", &massiv[i]);
    }

    for (i = 0; i < N/2; i++)
    {
        if (massiv[i] == massiv[N-i-1])
        {
            quantity++;
        }
    }

    if (quantity==N/2)
    {
        printf("symmetrical");
    }
    else
    {
        printf("asymmetrical");
    }

    return 0;
}
