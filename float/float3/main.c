#include <stdio.h>
#include <stdlib.h>

int main()
{
    float number;
    int s, ex = 0, mant = 0, *first;

    scanf("%f", &number);

    first = (int *) &number;
    s = *first;
    s = (s >> 31) & 1;

    ex = *first;
    ex = ex & ~(1 << 31);
    ex >>= 23;

    mant = *first;
    mant = mant & ~(511 << 31);

    printf("(-1)^%d * 1.%d * 2^%d", s, mant, (ex - 127));

    return 0;
}

