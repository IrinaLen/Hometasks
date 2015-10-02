#include <stdio.h>
int bitAnd (int x, int y)
{

 return ~((~x)|(~y));
}


int bitXor (int x,int y)
{
  int c,d;
  c=x&~y;
  d=~x&y;
  return ~((~c)&(~d));
}
long int thirdBits ()
{
 int N=146;
 long int b,c;
 long int f,m;
 b=N;
 c=N;
 b=b<<8;
 c=c>>1;
 f=b|c;
 m=f>>1;
 f=f<<14;
 return (f|m);
}
int fitBits (int x, int n)
{
  int y=1;
  int z, q;
  y=~y;
  y=y<<(n+((~2)+1));
  z=x&y;
  z=z>>(n+((~1)+1));
  q=~z;
  return (!q|!z);
}

int sign (int x)
{
  int a=1,b,z;
  a=a<<31;
  b=a&x;
  z=!x;
  z=!z;
  b=b>>30;
  z=z|b;
  return z;
}

int getByte (int x, int n)
{
   const int p=255;
   n=n<<3;
   x=x>>n;
   return (x&p);
}

int logicalShift (int x, int n)
{
    int c=1;
    c<<=31;
    x>>=n;
    c>>=(n+((~1)+1));
    return c&x;
}

int addOk (int x, int y)
{
    int a=255,x1,y1, z, z1;
    a=(a<<8)+a;
    x1=a&x;
    y1=a&y;
    z=x1+y1;
    z=z>>16;
    x1=x>>16;
    y1=y>>16;
    x1=x1&a;
    y1=y1&a;
    z1=x1+y1;
    z1=z1+z;
    z1>>=16;
    return (!z1);
}

int Conditional(int x, int y, int z)
{
    int p,q;
    p=!x;
    p<<=31;
    p>>=31;
    q=~p;
    p=p&z;
    q=q|y;
    return q|p;
}
int bang (int x)
{int invx = ~x;                  //if x==0, then -1
 int negx = invx + 1;                //if x==0, then 0
 return ((~negx & invx)>>31) & 1;  //if x was 0, then MSB is 1, so MSB>>31 & 1 = 1

}
int isPower2(int x)
{
    int y,z,c,p;
    p=!x;
    p=!p;
    c=1;
    c=c<<31;
    c=c&x;
    c=!c;
    y=x+((~1)+1);
    z=x&y;
    return(p&(!z)&c);
}
int main()
{
int x,y,n,z;
 scanf("%d %d %d %d",&x,&y,&z,&n);
union check {
 int x;
 char bytes[sizeof(int)];
    };
union check c;
c.x=1;

//printf("%d\n",c.bytes[0]);

printf("%d\n",bitAnd(x,y));
printf("%d\n",bitXor(x,y));
printf("%ld\n",thirdBits());
printf("%d\n",fitBits(x,n));
printf("%d\n",logicalShift(x,n));
printf("%d\n",getByte(x,n));
printf("%d\n",sign(x));
printf("%d\n",addOk(x,y));
printf("%d\n",Conditional(x,y,z));
printf("%d\n",bang(x));
printf("%d\n",isPower2(x));
    return 0;
}
