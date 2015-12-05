#include <stdio.h>
#include <stdlib.h>
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
    beautyprint(s, mant, ex);

}
int main()
{
    float number1, number2;
    scanf("%f", &number1);
    output(number1);
    scanf("%f", &number2);
    number1 = number1 / number2;
    output(number1);
    return 0;
}

