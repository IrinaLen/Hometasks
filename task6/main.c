#include <stdio.h>
#include <stdlib.h>
#include <string.h>

void uniquestring (void)
{
    char s1[255];
    int s2[255] = {0};
    int i = 0;

    gets(s1);

    while (strcmp(s1,"000") != 0)
    {
        for (i = 0; i <= strlen(s1); i++)
        {
            s2[i] = ((int)s1[i]) ^ s2[i];
        }
        gets(s1);
    }


    for (i = 0; i < 255; i++)
    {
        s1[i] = (char) s2[i];
    }

    puts(s1);
}

int main()
{
    printf("enter 000 for the end\n");
    uniquestring();
    return 0;
}
