#include <stdio.h>
#include <stdlib.h>
#define maxsize 255
typedef struct myht
{
    char str[maxsize];
    int numb;
    struct myht *next;
}ht;

int main()
{
    int n, i, h;
    ht *hast;
    char s[maxsize];
    printf("N = ");
    scanf("%d", &n);
    hast = create(n);

    for (i = 0; i < n; i++)
    {
        gets(s);
        h = findh(s);
        addtoht(&hast[h],s);
    }

    outputht();
    return 0;
}
