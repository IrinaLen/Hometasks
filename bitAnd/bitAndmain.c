#include <stdio.h>

int bitAnd (int x, int y)
{
    return ~((~x)|(~y));
}

int main()
{

    int x,y;

    scanf("%d %d", &x, &y);

    printf("%d", bitAnd(x,y));

    return 0;

}
