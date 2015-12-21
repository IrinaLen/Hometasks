#include <stdio.h>
#include <stdlib.h>
#include <string.h>
const int maxl = 255;

void findslash (char *s)
{
    char s2[3] = "  ";
    int i = 0, l;
    l = strlen(s);
    for (i = 0; i < l; i++)
    {
        if (s2[0] == '/' && s2[1] == '/')
        {
                printf("%c", s[i]);
                continue;
        }
        if (s[i] == '/')
        {
            if (s2[0] == '/' && s[i-1] == '/')
            {
                s2[1] = '/';
            }
            else
            {
                s2[0] = '/';
            }
        }
    }

}

int main()
{
    char s[maxl];
    FILE *fo;
    fo = fopen("input.txt", "r");

    if (fo == NULL)
    {
        printf("error.");
        return 0;
    }
    while (fo)
    {
        fscanf(fo, "%s", s);
        findslash(s);
    }

    fclose(fo);
    return 0;
}
