#include <stdio.h>
#include <stdlib.h>

int getByte (int x, int n)
{
   const int p=255;
   n=n<<3;
   x=x>>n;
   return (x&p);
}

int main()
{
   int x,n;
 scanf("%d %d",&x,&n);
 printf("%d\n",getByte(x,n));
    return 0;
}
