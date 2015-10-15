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

int main()
{
    union Def defenition;

    scanf("%f", &defenition.basicnumb);

    printf("(-1)^%d * 1.%d * 2 ^ %d ", defenition.numb.s, defenition.numb.m, (defenition.numb.e - 127));

    return 0;
}
