#include <stdio.h>

long int binpow (int a, int n)
{
    int b;
    if (n == 0)
        {
            return 1;
        }
    if (n % 2 == 1)
        {
            return binpow (a, n-1) * a;
        }

	else
        {
            b = binpow (a, n/2);
            return b * b;
        }
}

int main()
{
    int a, n;
    double total;

    scanf("%d %d", &a, &n);

    if (n < 0)
        {
            n = n * (-1);
            total = 1.0 / (binpow(a, n));
        }
    else
        {
            total = binpow(a, n);
        }

    printf("%6.10lf", total);

    return 0;
}
