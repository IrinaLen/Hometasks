#include <stdio.h>
#include <math.h>

union number
{
    int intnumb;
    float floatnumb;
};


void beautyprint (int s, int m, int e)
{
    int i;
    if (e == 255 && m != 0)
    {
        printf("NaN\n");
        return;
    }

    if (e == 255 && m == 0)
    {
        if (s == 1)
        {
            printf("-inf\n");
        }
        else
        {
            printf("+inf\n");
        }
        return;
    }


    printf("(-1)^%d * 1.", s);
    for (i = 32 - 9 - 1; i > 1 ; i--)
    {
        printf("%d", (m >> i) & 1);
    }
    printf(" * 2 ^ %d\n", e - 127);
}

void outputs (union number scan)
{
    const int mask = ~ (1 << 31);
    int i, s, part, ex = 0, mant = 0;

    if (scan.floatnumb < 0)
    {
        s = 1;
    }
    else
    {
        s = 0;
    }

    part = scan.intnumb & mask;
    ex = part >> 23;
    mant = part & (mask >> 8);

    beautyprint(s, mant, ex);

}

int main()
{
    union number scan1;
    union number scan2;
    scanf("%f", &scan1.floatnumb);
    outputs(scan1);
    scanf("%f", &scan2.floatnumb);
    scan2.floatnumb = scan1.floatnumb / scan2.floatnumb;
    outputs(scan2);

    return 0;
}
