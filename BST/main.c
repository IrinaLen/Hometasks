#include <stdio.h>
#include <stdlib.h>

typedef struct tree
{
    int list;
    struct tree *left;
    struct tree *right;

} mytree;

mytree* createlist (int el)//ô-èÿ ñîçäàíèå ëèñòà
{
    mytree *n;
    n = (mytree*) malloc(sizeof(mytree));
    if (n)
    {
        n -> list = el;
        n -> left = NULL;
        n -> right = NULL;
        return n;
    }
    else
    {
        printf("NULL! Error. Please, finish work!");
    }
}

mytree* add (mytree *begin, int newel) //a äîáàâëÿòü çíà÷åíèÿ
{
    if (begin == NULL)
    {
        begin = createlist(newel);
    }

    if (newel < begin -> list)
    {
        if (begin -> left == NULL)
        {
            begin -> left = createlist(newel);
        }
        else
        {
            add (begin -> left, newel);
        }
    }

    if (newel > begin -> list)
    {
        if (begin -> right == NULL)
        {
            begin -> right = createlist(newel);
        }
        else
        {
            add(begin -> right, newel);
        }
    }

    return begin;
}


int mostright (mytree *root)
{
    while (root -> right != NULL)
    {
        root = root -> right;
    }
    return root -> list;
}

mytree* del (mytree *begin, int el)//x óäàëÿòü çíà÷åíèÿ
{
    if (begin == NULL)
    {
        return NULL;
    }

    if (begin -> list == el)
    {
        if (begin -> left == NULL && begin -> right == NULL)
        {
            free(begin);
            return NULL;
        }

        if (begin -> left != NULL && begin -> right == NULL)
        {
            mytree *n;
            n = begin -> left;
            free(begin);
            return n;
        }

        if (begin -> left == NULL && begin -> right != NULL)
        {
            mytree *n;
            n = begin -> right;
            free(begin);
            return n;
        }

        begin -> list = mostright(begin -> left);
        begin -> left = del(begin -> left, begin -> list);
        return begin;
    }

    if (el < begin -> list)
    {
        begin -> left = del(begin -> left, el);
        return begin;
    }

    if (el > begin -> list)
    {
        begin -> right = del(begin -> right, el);
        return begin;
    }

    return begin;
}

mytree* check (mytree *begin, int el)//c ïðîâåðÿòü, ïðèíàäëåæèò ëè çíà÷åíèå ìíîæåñòâó
{
    if (begin == NULL)
    {
        return NULL;
    }

    if (el == begin -> list)
    {
        return begin;
    }

    if (el < begin -> list)
    {
        return check(begin -> left, el);
    }

    if (el > begin -> list)
    {
        return check(begin -> right, el);
    }
}

void outup (mytree *root)//u ïå÷àòàòü òåêóùèå ýëåìåíòû ìíîæåñòâà â âîçðàñòàþùåì ïîðÿäêå
{
    if (root != NULL)
    {
        outup(root->left);
        printf("%d ", root->list);
        outup(root->right);
    }
}

void outdown(mytree *root)//d ïå÷àòàòü òåêóùèå ýëåìåíòû ìíîæåñòâà â óáûâàþùåì ïîðÿäêe,
{
    if (root != NULL)
    {
        outdown(root->right);
        printf("%d ", root->list);
        outdown(root->left);

    }
}



void pl (mytree *n)// íå áóäåò ðàáîòàòü
{
    printf("(%d ", n -> list);

    if (n -> right == NULL && n -> left == NULL)
    {
        printf("null null) ");
        return;
    }

    if (n -> left == NULL)
    {
        printf("null ");
    }
    else
    {
        pl(n -> left);
    }

    if (n -> right == NULL)
    {
        printf("null) ");
        return;
    }

    pl(n -> right);
    printf(") ");

}


void treeout(mytree *begin)//s ïå÷àòàòü òåêóùèå ýëåìåíòû ìíîæåñòâà â ôîðìàòå (a b c), ãäå a — çíà÷åíèå â óçëå, à b è c — àíàëîãè÷íûå ïðåäñòàâëåíèÿ ïîääåðåâüåâ ïðàâîãî è ëåâîãî ïîòîìêà. Ïðèìåð: "(5 (2 null null) (10 null (12 null null)))"
{
    if (begin == NULL)
    {
        printf("(null)\n");
    }
    else
    {
        pl(begin);
        printf("\n");
    }

}

void cleaning (mytree *root)
{
    if (root == NULL)
    {
        return;
    }
    if (root -> left == NULL && root -> right == NULL)
    {
        free(root);
        return;
    }
    if (root -> left != NULL)
    {
        cleaning(root -> left);
    }

    if (root -> right != NULL)
    {
        cleaning(root -> right);
    }

}

int main()
{
    mytree *root, *n;
    root = NULL;
    char point;
    int newel;

    printf(" a - add element\n x - delete element\n c - check element\n u - output in ascending order (a1 < a2)\n d - output in descending order(a1 > a2)\n s - output in special order\n f - finish work\n");

    do
    {
        scanf("%c", &point);

        if (point == 'a')//+
        {
            scanf("%d", &newel);
            root = add(root, newel);
            continue;
        }

        if (point == 'x')//+
        {
            scanf("%d", &newel);
            root = del(root, newel);
            continue;
        }

        if (point == 's')//-
        {
            treeout(root);
            continue;
        }

        if (point == 'u')//+
        {
            outup(root);
            printf("\n");
            continue;
        }

        if (point == 'd')//+
        {
            outdown(root);
            printf("\n");
            continue;
        }

        if (point == 'c')//+-
        {
            scanf("%d", &newel);
            n = check(root, newel);
            if (n == NULL)
            {
                printf("no element\n");
            }
            else
            {
                printf("element\n");
            }
            continue;
        }

    } while (point != 'f');
    cleaning(root);
    return 0;
}
