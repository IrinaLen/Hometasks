#include <stdio.h>


int sign (int x)
{
    int a = 1, b, z;
    a = a<<31;
    b = a&x;
    z = !x;
    z = !z;
    b = b>>30;
    z = z|b;
    return z;
}

int main()
{
    int x;

    scanf("%d", &x);
    printf("%d\n", sign(x));

    return 0;
}
