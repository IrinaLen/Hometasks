#include <stdio.h>
#include <math.h>

union number
{
    int intnumb;
    float floatnumb;
};

int main()
{
    union number scan;
    const int N = 32, mask = ~ (1 << 31);
    int i, mynumb[N], part, ex = 0;

    scanf("%f", &scan.floatnumb);

    if (scan.floatnumb < 0)
    {
        mynumb[0] = 1;
    }
    else
    {
        mynumb[0] = 0;
    }

    part = scan.intnumb & mask;

    for (i = 0; i < N - 1; i++)
    {
        mynumb[N - i - 1] = part % 2;
        part = part / 2;
    }

    printf("(-1)^%d * 1.", mynumb[0]);
    for (i = 9; i < N; i++)
    {
        printf("%d", mynumb[i]);
    }
    for (i = 1; i < 9; i++)
    {
        ex <<= 1;
        ex = ex + mynumb[i];
    }
    printf(" * 2 ^ %d", ex - 127);

    return 0;
}
