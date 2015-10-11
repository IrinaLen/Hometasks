#include <stdio.h>
#include <stdlib.h>

int main()
{
    union check
    {
        int x;
        char bytes[sizeof(int)];
    }

    union check c;
    c.x=1;

    if (c.bytes[0] == 1)
    {
        printf("little-endian");
    }
    else
    {
        printf("big-endian");
    }

    return 0;
}
