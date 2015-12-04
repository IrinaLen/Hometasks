#include <stdio.h>
#include <stdlib.h>


size_t strlen (char *src) // длина строки +
{
    int i = 0;
    while (src[i] != '\0')
    {
        i++;
    }
    return i;
}

size_t strcmp (char *s1, char *s2) //сравнение лексикографическое +
{
    int i = 0;
    while (s1[i] == s2[i] && (s1[i] != '\0' || s1[i] != '\0'))
    {
        i++;
    }
    if (s1[i] == s2[i])
    {
        return 0;
    }

    if (s1[i] > s2[i])
    {
        return 10;
    }

    if (s1[i] < s2[i])
    {
        return -10;
    }
}

void strcpy (char *dst, char *src)//копировать из 2 в 1 +
{
    int i = 0;

    while (src[i] != '\0')
    {
        dst[i] = src[i];
        i++;
    }
    dst[i] = src[i];
}

void strcat (char *dst, char *src)//записать в конец, склеивание строк +
{
    int i = 0, j = 0;

    while (dst[i] != '\0')
    {
        i++;
    }
    while (src[j] != '\0')
    {
        dst[i] = src[j];
        j++;
        i++;
    }
    dst[i] = src[j];
}


int main()
{
    char s1[20], s2[20];
    gets (s1);
    gets (s2);
    printf ("s1 %d s2 %d\n", strlen(s1), strlen(s2));
    printf ("%d\n", strcmp(s1, s2));
    strcpy (s1, s2);
    puts(s1);
    strcat (s1, s2);
    puts (s1);

    return 0;
}
