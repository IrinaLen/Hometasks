#include <stdio.h>
#include <stdlib.h>


int mainadress;
int i, stop = 0;

int input ()
{
    int data[5] = {0};

    for (i = 0; i < 20; i++)
    {
        printf ("%3d; %p\n", i, data[i] - mainadress);

    }

    i = -1;
    while (stop != 228)
    {
        i++;

        printf ("%d ", i);

        scanf("%d", &stop);
        data[i] = stop;
    }
}

int other ()
{
    printf ("You're die! It's Ok! =)))\n");
}

int main()
{
    mainadress = &main;
    printf("%d - main\n%d - input\n%d - other\n", &main, &input, &other);

    input();


    return 0;
}

