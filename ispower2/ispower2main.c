#include <stdio.h>

int isPower2(int x)
{
    int y, z, c, p;
    p = !x;
    p = !p;
    c = 1;
    c = c<<31;
    c = c&x;
    c = !c;
    y = x+((~1)+1);
    z = x&y;
    return(p&(!z)&c);
}

int main()
{
    int x;

    scanf("%d", &x);
    printf("%d\n", isPower2(x));

    return 0;
}
