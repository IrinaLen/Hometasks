#include <stdio.h>
#include <math.h>

union number
{
    int intnumb;
    float floatnumb;
};

void outputs (union number scan)
{
    const int N = 32, mask = ~ (1 << 31);
    int i, mynumb[N], part, ex = 0, mant = 0;

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
        mynumb[N - i - 1] = (part >> i) & 1;
    }

    for (i = 9; i < N; i++)
    {
        mant = (mant << 1) + mynumb[i];
    }

    for (i = 1; i < 9; i++)
    {
        ex <<= 1;
        ex = ex + mynumb[i];
    }

    if (ex == 255 && mant > 0)
    {
        printf("NaN");
        return;
    }

    if (ex == 255 && mant == 0)
    {
        if (mynumb[0] == 1)
        {
            printf("-inf");
        }
        else
        {
            printf("+inf");
        }
        return;
    }


    printf("(-1)^%d * 1.", mynumb[0]);
    for (i = 9; i < N; i++)
    {
        printf("%d", mynumb[i]);
    }
    printf(" * 2 ^ %d", ex - 127);

}

int main()
{
    union number scan;

    scanf("%f", &scan.floatnumb);

    outputs(scan);

    return 0;
}
