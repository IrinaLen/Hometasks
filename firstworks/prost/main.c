#include <stdio.h>
#include <stdlib.h>

int dividers(int a)
{
    int b = 0,i;

    for (i = 2; i < a; i++)
    {
       if (a%i == 0) b++;
    }
 return (b);
}


int main()
{
   int a, i;

   scanf("%d", &a);

   for (i = 2; i <= a; i++)
   {
       if (dividers(i) == 0) printf("%d ", i);
   }

    return 0;
}
