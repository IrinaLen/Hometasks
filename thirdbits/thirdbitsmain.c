#include <stdio.h>

long int thirdBits ()
{
    int N = 146;
    long int b, c;
    long int f, m;

    c = b = N;
    b = b<<8;
    c = c>>1;
    f = b|c;
    m = f>>1;
    f = f<<14;

    return (f|m);
}

int main()
{
    printf("%ld\n", thirdBits());

    return 0;
}
