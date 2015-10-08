#include <stdio.h>

int bitXor (int x,int y)
{
  int c,d;
  c=x&~y;
  d=~x&y;
  return ~((~c)&(~d));
}

int main()
{
   int x,y;
 scanf("%d %d",&x,&y);
 printf("%d\n",bitXor(x,y));
    return 0;
}
