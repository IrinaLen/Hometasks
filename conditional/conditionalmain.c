#include <stdio.h>
#include <stdlib.h>

int Conditional(int x, int y, int z)
{
    int p, q;
    p = !x;
    p <<= 31;
    p >>= 31;
    q = ~p;
    p = p&z;
    q = q&y;
    return q|p;
}

int main()
{
    int x, y, z;

    scanf("%d %d %d",&x, &y, &z);

    printf("%d\n",Conditional(x, y, z));

    return 0;
}
