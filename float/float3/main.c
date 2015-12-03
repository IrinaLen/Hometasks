#include <stdio.h>
#include <stdlib.h>

void output (float number)
{
    int s, ex = 0, mant = 0, *first;

    first = (int *) &number;
    s = *first;
    s = (s >> 31) & 1;

    ex = *first;
    ex = ex & ~(1 << 31);
    ex >>= 23;

    mant = *first;
    mant = mant & ~(511 << 31);

    if (ex == 255 && mant > 0)
    {
        printf("NaN");
        return;
    }

    if (ex == 255 && mant == 0)
    {
        if (s == 1)
        {
            printf("-inf");
        }
        else
        {
            printf("+inf");
        }
        return;
    }

    printf("(-1)^%d * 1.%d * 2^%d", s, mant, (ex - 127));

}
int main()
{
    float number;

    scanf("%f", &number);

    output(number);
    return 0;
}

