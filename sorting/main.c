#include <stdio.h>
#include <time.h>
#include <stdlib.h>

void sort1 (int *data, size_t count) // n^2
{
    int tmp, i, j;
    clock_t start = clock();
    for(i = 0; i < count - 1; ++i) // i - номер прохода
    {
        for(j = 0; j < count - 1; ++j) // внутренний цикл прохода
        {
            if (data[j + 1] < data[j])
            {
                tmp = data[j + 1];
                data[j + 1] = data[j];
                data[j] = tmp;
            }
        }
    }
    clock_t finish = clock();

    printf("%lf   \n",(double) (finish - start) / CLOCKS_PER_SEC);
}

void sort2 (int *data, long int st, long int fin) //n log n
{
    int tmp;

    if (fin - st < 2)
    {
       return;
    }

    if (fin - st == 2)
    {
        if (data[st] > data[st + 1])
        {
            tmp = data[st];
            data[st] = data[st + 1];
            data[st + 1] = tmp;
        }
        return;
    }

    sort2 (data, st, st + (fin - st) / 2);
    sort2 (data, st + (fin - st) / 2, fin);

    int *help;
    help = malloc(fin * sizeof(int));
    long int s1 = st, f1 = st + (fin - st) / 2;
    long int s2 = f1, i;

    for (i = st; i < fin; i++)
    {
        if (s1 >= f1 || (s2 < fin && data[s2] <= data[s1]))
        {
            help[i] = data [s2];
            s2++;
        }
        else
        {
            help[i] = data [s1];
            s1++;
        }

    }

    for (i = st; i < fin; i++)
    {
        data[i] = help[i];
    }

}
void sort3(int *myarray, size_t length)
{
    int i, j, n;
    int *counting;
    counting = malloc(n * sizeof(int));//?
    clock_t start = clock();

    for(i = 0; i < length; i++)
    {
        counting[myarray[i]]++;
    }
    n = 0;
    for(i = 0; i < length; i++)
    {
        for(j = 0; j < counting[i]; j++)
        {
            myarray[n] = i;
            n++;
        }
    }

    clock_t finish = clock();
    printf("%lf   ",(double) (finish - start) / CLOCKS_PER_SEC);

    free(myarray);

}


int compare(const void *i, const void *j)
{
    return *(int *)i - *(int *)j;
}

int main()
{
    long int i, n = 5;
    int massivb[n], massivm[n], massivs[n];

    for (i = 0; i < n; i++)
    {
        massivs[i] = massivm[i] = massivb[i] = rand() % 100;
        printf("%d ", massivb[i]);

    }

/*  printf("            n ^ 2 \t n(log n)\t qsort\n");
    printf("%10ld  ", n);

    sort1 (massivb, n);


    clock_t start = clock();
    sort2 (massivm, 0, n);
    clock_t finish = clock();
    printf("%lf   ",(double) (finish - start) / CLOCKS_PER_SEC);

    printf("   ");

    start = clock();
    qsort(massivs, n, sizeof(int), compare);
    finish = clock();

    printf("%lf\n",(double) (finish - start) / CLOCKS_PER_SEC);
*/

    printf("\n");
    sort3(massivb, n);
   /*
    for (n = 10; n <= 100000000; n *= 10)
    {
        printf("%10ld  ", n);
        int *massivb, *massivm, *massivs;

        massivb = malloc(n * sizeof(int));
        massivm = malloc(n * sizeof(int));
        massivs = malloc(n * sizeof(int));

        for (i = 0; i < n; i++)
        {
            massivs[i] = massivm[i] = massivb[i] = rand() % 100;
        }


      //  sort1 (massivb, n);

   /*     start = clock();
        sort2 (massivm, 0, n);
        finish = clock();
        printf("%lf   \n",(double) (finish - start) / CLOCKS_PER_SEC);
/*
        start = clock();
        qsort(massivs, n, sizeof(int), compare);
        finish = clock();
        printf("%lf\n",(double) (finish - start) / CLOCKS_PER_SEC);

        free (massivb);
        free (massivm);
        free (massivs);

    }
*/
    return 0;
}
