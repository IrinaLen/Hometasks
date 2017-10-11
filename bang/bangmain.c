#include <stdio.h>
#include <stdlib.h>

int bang (int x)
{
    int invx = ~x;
    int negx = invx + 1;

    return ((~negx & invx)>>31) & 1;

}

int main()
{
    int x,y;

    scanf("%d", &x);

    printf("%d\n", bang(x));

    return 0;
}
