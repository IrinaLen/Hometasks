#include <stdio.h>
#include <string.h>

int main()
{
   char s[100], s1[100];
   int i, j, l, l1, k = 0, k1 = 0;

   gets(s);
   gets(s1);

   l = strlen(s);
   l1 = strlen(s1);

   for (i = 0; i < l-l1+1; i++)
   {
        for (j = 0; j < l1-1; j++)
        {
            if (s[j+i] == s1[j])
            {
                k1++;
            }
            else
            {
                break;
            }
        }

        if (k1 == l1-1)
        {
            k++;
        }
        k1 = 0;
   }

   printf("%d", k);

    return 0;
}
