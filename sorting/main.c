#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#define N 32767

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

    printf("%lf   ",(double) (finish - start) / CLOCKS_PER_SEC);
}

void sort2 (int *data, long int st, long int fin, int *help) //n log n
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

    sort2 (data, st, st + (fin - st) / 2, help);
    sort2 (data, st + (fin - st) / 2, fin, help);


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

void sort3(int *myarray, size_t length) // n
{

    int i, j, n = 0;
    int counting[N];
    for (i = 0; i < N; i++)
    {
        counting[i] = 0;
    }

    clock_t start = clock();

    for(i = 0; i < length; i++)
    {
        counting[myarray[i]]++;
    }

   for(i = 0; i < N; i++)
    {
        for(j = 0; j < counting[i]; j++)
        {
            myarray[n] = i;
            n++;
        }
    }

    clock_t finish = clock();
    printf("%lf   \n",(double) (finish - start) / CLOCKS_PER_SEC);

}


int compare(const void *i, const void *j)
{
    return *(int *)i - *(int *)j;
}

int main()
{
    long int i, n = 5;
    int *massivb, *massivm, *massivs, *massivq;
    int *help;
    massivb = malloc(n * sizeof(int));
    massivm = malloc(n * sizeof(int));
    massivs = malloc(n * sizeof(int));
    massivq = malloc(n * sizeof(int));
    help = malloc(n * sizeof(int));

    if (massivb == NULL || massivm == NULL || massivs == NULL || massivq == NULL || help == NULL)
    {
        printf("non memory");
        return 0;
    }

    for (i = 0; i < n; i++)
    {
        massivq[i] = massivs[i] = massivm[i] = massivb[i] = rand() % N;
    }

    printf("            n ^ 2 \t n(log n)\t qsort\t n\n");
    printf("%10ld  ", n);

    sort1 (massivb, n);

    clock_t start = clock();
    sort2 (massivm, 0, n, help);
    clock_t finish = clock();
    printf("%lf   ",(double) (finish - start) / CLOCKS_PER_SEC);


    start = clock();
    qsort(massivq, n, sizeof(int), compare);
    finish = clock();

    printf("%lf  ",(double) (finish - start) / CLOCKS_PER_SEC);

    sort3(massivs, n);

    free(help);
    free(massivb);
    free(massivm);
    free(massivq);
    free(massivs);

    for (n = 10; n <= 100000000; n *= 10)
    {
        printf("%10ld  ", n);


        if (n < 1000 * 1000)
        {
            massivb = malloc(n * sizeof(int));
            massivm = malloc(n * sizeof(int));
            massivs = malloc(n * sizeof(int));
            massivq = malloc(n * sizeof(int));
            help = malloc(n * sizeof(int));

            if (massivb == NULL || massivm == NULL || massivs == NULL || massivq == NULL || help == NULL)
            {
                printf("non memory");
                return 0;
            }

        }
        else
        {
            massivm = malloc(n * sizeof(int));
            massivs = malloc(n * sizeof(int));
            massivq = malloc(n * sizeof(int));
            help = malloc(n * sizeof(int));

            if (massivm == NULL || massivs == NULL || massivq == NULL || help == NULL)
            {
                printf("non memory");
                return 0;
            }
        }



        for (i = 0; i < n; i++)
        {
            massivq[i] = massivs[i] = massivm[i] = massivb[i] = rand() % N;
        }

        if (n < 1000000)
        {
            sort1 (massivb, n);
        }
        else
        {
            printf("n/a          "); // из предварительных подсчетов
        }

        start = clock();
        sort2 (massivm, 0, n, help);
        finish = clock();
        printf("%lf   ",(double) (finish - start) / CLOCKS_PER_SEC);

        start = clock();
        qsort(massivq, n, sizeof(int), compare);
        finish = clock();
        printf("%lf   ",(double) (finish - start) / CLOCKS_PER_SEC);

        sort3(massivs, n);

        free(help);
        free(massivm);
        free(massivq);
        free(massivs);
        if (n < 1000 * 1000)
        {
            free(massivb);
        }

    }

    return 0;
}
