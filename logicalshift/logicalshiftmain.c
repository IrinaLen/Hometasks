#include <stdio.h>


int logicalShift (int x, int n)
{
    int c = 1;

    c <<= 31;
    c = ~c;
    x >>= n;
    c >>= (n+((~1)+1));

    return c&x;
}

int main()
{
    int x,n;

    scanf("%d %d", &x, &n);
    printf("%d\n", logicalShift(x, n));

    return 0;
}
