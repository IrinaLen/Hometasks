#include <stdio.h>
#include <stdlib.h>

int addOk (int x, int y)
{
    int s, x1, y1, itog;
    s = x + y;
    x1 = x >> 31;
    y1 = y >> 31;
    itog = s >> 31;
    return !((x1 ^ itog) & (y1 ^ itog));
}

int main()
{
    int x, y;
    scanf("%d %d", &x, &y);
    printf("%d\n", addOk(x, y));
    return 0;
}
