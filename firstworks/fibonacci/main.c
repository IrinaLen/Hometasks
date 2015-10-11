#include <stdio.h>
#include <stdlib.h>

int main()
{
    int f, f1 = 1, f2 = 1, i, n;

    scanf("%d", &n);

    if (n < 1)
    {
        printf("0");
    }
    if (n == 1 || n == 2)
    {
        printf("1");
    }

    for (i = 3; i <= n; i++)
    {
        f = f1 + f2;
        f1 = f2;
        f2 = f;
    }

    printf("%d", f);

    return 0;
}
