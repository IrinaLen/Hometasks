#include <stdio.h>

struct Number
{
    unsigned m: 23;
    unsigned e: 8;
    unsigned s: 1;
};

union Def
{
    float basicnumb;
    struct Number numb;
};

void output (union Def defenition)
{

    if (defenition.numb.e == 255 && defenition.numb.m > 0)
    {
        printf("NaN");
        return;
    }

    if (defenition.numb.e == 255 && defenition.numb.m == 0)
    {
        if (defenition.numb.s == 1)
        {
            printf("-inf");
        }
        else
        {
            printf("+inf");
        }
        return;
    }


     printf("(-1)^%d * 1.%d * 2 ^ %d ", defenition.numb.s, defenition.numb.m, (defenition.numb.e - 127));
}

int main()
{
    union Def defenition;

    scanf("%f", &defenition.basicnumb);

    output(defenition);
    return 0;
}
