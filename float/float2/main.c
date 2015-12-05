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

void output (union Def defenition)
{
    int e, m, s;
    m = defenition.numb.m;
    e = defenition.numb.e;
    s = defenition.numb.s;
    beautyprint(s, m, e);
}


int main()
{
    union Def defenition1, defenition2;

    scanf("%f", &defenition1.basicnumb);
    output(defenition1);

    scanf("%f", &defenition2.basicnumb);
    defenition2.basicnumb = defenition1.basicnumb / defenition2.basicnumb;
    output(defenition2);
    return 0;
}
