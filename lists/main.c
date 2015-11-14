#include <stdio.h>
#include <stdlib.h>
int c = 0;
int i = 1; // введена для головы

typedef struct node
{
    int data;
    struct node *next;
} node1;

void add(int numb, node1 **begin, node1 **nodeend)
{
    if (i == 1)
    {
        (*begin) = (node1*) malloc (sizeof(node1));
        (*begin) -> data = numb;
        (*begin) -> next = NULL;
        (*nodeend) = (*begin);
        i++;
    }
    else
    {
        (*nodeend) -> next = (node1*) malloc (sizeof(node1));
        (*nodeend) = (*nodeend) -> next;
        (*nodeend) -> data = numb;
        (*nodeend) -> next = NULL;
    }
}

void firstdelete(node1 **begin,int numb)
{
    node1 *b, *n;
    n = b = *begin;

    if (b == NULL)
    {
        printf("Error\n");
        return;
    }
    if (numb == b -> data)
    {
        (*begin) = n -> next;
        free (b);
        return;
    }

    n = b -> next;

    while (n != NULL)
    {
        if (n -> data == numb)
        {
            b -> next = n -> next;
            free (n);
            return;
        }
        b = n;
        n = n -> next;
    }
}

void output (node1 *head)
{
    node1 *n = head;

    while (n != NULL)
    {
        printf("%d  ", n -> data);
        n = n -> next;
    }

    printf("\n");
}

void quit (node1 *head)
{
    node1 *n = head;
    node1 *tmp;

    while (n != NULL)
    {
        tmp = n;
        n = n -> next;
        free (tmp);
    }
}

void circle (node1 *start, node1 **nodeend, int el)
{
    node1 *n;
    n = start;

    if (start == NULL)
    {
        printf ("Error\n");
        return;
    }

    while (n -> data != el)
    {
        n = n -> next;
    }




    (*nodeend) -> next = n;
    c = 1;

}

void testnode (node1 *start)
{
    node1 *t1, *t2, *t;
    t1 = t2 = start;

    do
    {
        t1 = t1 -> next;
        t2 = t2 -> next;
        t2 = t2 -> next;
        t = t2 ->next;
        if (t -> next == NULL)
        {
            break;
        }

    }while (t1 != t2 && t2 -> next != NULL);


    if (t1 == t2)
    {
        printf ("Have got circle\n");
        c = 1;

    }
    else
    {
       printf ("Haven't got circle\n");
    }
}

/*
a <число> - добавляет число в список,
r <число> - удаляет первое число, равное введенному, из списка,
p - выводит содержимое списка на экран,
q - выход из программы с очисткой памяти.
c <число> - создать список с циклом с указанием на первый элемент равный данному
t - проверка на наличие цикла
*/


int main()
{
    char comand;
    node1 *begin = NULL;
    node1 *nodeend;
    int numb;

    do
    {
        scanf("%c", &comand);

        if (comand == 'a')
        {
            scanf("%d", &numb);
            add(numb,&begin, &nodeend);
            continue;
        }

        if (comand == 'r')
        {
            scanf("%d", &numb);
            firstdelete(&begin, numb);
            continue;
        }

        if (comand == 'p')
        {

            if (c == 1)
            {
                nodeend -> next = NULL;
            }
            output(begin);
            continue;
        }

        if (comand == 'c')
        {
            scanf ("%d", &numb);
            circle(begin, &nodeend, numb);
            continue;
        }

        if (comand == 't')
        {
            testnode(begin);
            continue;
        }
    }while (comand != 'q');


    if (c == 1)
    {
        nodeend -> next = NULL;
    }
    quit(begin);
    return 0;
}
