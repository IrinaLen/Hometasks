#include <stdio.h>


int main()
{
    int i,a=0,c, N;
    FILE *fo;
    fo=fopen("massiv.txt","r");
    fscanf(fo, "%d", &N);

    for (i = 0; i < N; i++)
    {
        fscanf(fo, "%d", &c);
        if (c == 0) a++;
    }
    printf("%d", a);
    return 0;
}
