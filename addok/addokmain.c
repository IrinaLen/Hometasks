#include <stdio.h>
#include <stdlib.h>

int addOk (int x, int y)
{
    int a = 255, x1, y1, z, z1, b;
    b = (a << 7) | a;
    a = (a << 8) | a;
    x1 = a & x;
    y1 = a & y;
    z = x1 + y1;
    z = z >> 16;
    x1 = x >> 16;
    y1 = y >> 16;
    x1 = x1 & b;
    y1 = y1 & b;
    z1 = x1 + y1;
    z1 = z1 + z;
    z1 >>= 15;
    return (!z1);
}

int main()
{
    int x, y;
    scanf("%d %d", &x, &y);
    printf("%d\n", addOk(x, y));
    return 0;
}
