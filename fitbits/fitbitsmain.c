#include <stdio.h>
#include <stdlib.h>

int fitBits (int x, int n)
{
    int y = 0;
    int z, q;
    y = ~y;
    y = y<<(n+((~1)+1));
    z = x&y;
    z = z>>(n+((~1)+1));
    q = ~z;
    return (!q|!z);
}

int main()
{
    int x, n;
    scanf("%d %d", &x, &n);

    printf("%d\n", fitBits(x,n));

    return 0;
}
