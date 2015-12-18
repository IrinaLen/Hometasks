#include <stdio.h>
#include <stdlib.h>

#define HashTableSize 10000
#define maxlength 20
typedef struct node
{
	int numb;
	char word[maxlength];
	struct node *next;

} node1;

typedef struct HashTable_
{
	node1 *hashtab[HashTableSize];

} HashTable;

size_t(*h)(char *);

/////////////////////ht functions

void fillht(HashTable **htab)
{
	int i;
	for (i = 0; i < HashTableSize; i++)
	{
		(*htab)->hashtab[i] = NULL;
	}
}

HashTable* create(void)
{
	HashTable *f;
	f = (HashTable*)malloc(sizeof(HashTable));
	if (f == NULL)
	{
		printf("Memory error\n");
		return NULL;
	}
	fillht(&f);

	return f;
}

void cleaning(node1 **head)
{

	node1 *tmp;

	while ((*head) != NULL)
	{
		tmp = (*head);
		(*head) = (*head)->next;
		free(tmp);
	}
}

void delht(HashTable **ht)
{
	int i;
	node1 *p;
	for (i = 0; i < HashTableSize; i++)
	{
		if ((*ht) == NULL)
		{
			return;
		}
		p = (*ht)->hashtab[i];
		if (p == NULL)
		{
			continue;
		}
		cleaning(&p);
	}
	free(*ht);
}
//////////////////////////
size_t hashf(char *s)
{
	size_t i = 0, h = 0;
	const int magicnumb = 487;
	while (s[i] != '\0')
	{
		h = (h * magicnumb) % HashTableSize + (int)s[i];
		i++;
	}
	return (h % HashTableSize);
}

///////////////////////////another
void newel(node1 **p, char *s)
{
	int i = 0;

	(*p) = (node1*)malloc(sizeof(node1));

	if ((*p) == NULL)
	{
		printf("Memory error!!\n");
		return;
	}

	while (s[i] != '\0')
	{
		(*p)->word[i] = s[i];
		i++;
	}

	(*p)->word[i] = s[i];
	(*p)->numb = 1;
	(*p)->next = NULL;
}

void add(HashTable **ht, char *s)
{
	size_t hash1;
	int i = 0;
	h = hashf;
	hash1 = h(s);
	node1 *p, *p1;
	p1 = p = (*ht)->hashtab[hash1];

	if (p == NULL)
	{
		newel(&p, s);
		(*ht)->hashtab[hash1] = p;
		return;
	}

	while (p != NULL)
	{
		i = 0;
		while (p->word[i] == s[i] && p->word[i] != '\0' && s[i] != '\0')
		{
			i++;
		}

		if (p->word[i] - s[i] == 0)
		{
			p->numb++;
			return;
		}
		p1 = p;
		p = p->next;
	}

	newel(&p, s);
	p1 -> next = p;
}

void del(HashTable **ht, char *s)
{
	int p;
	h = hashf;
	p = h(s);
	if ((*ht)->hashtab[p] == NULL)
	{
		printf("Error. No elements.\n");
		return;
	}
	cleaning(&(*ht)->hashtab[p]);
	(*ht)->hashtab[h(s)] = NULL;

}

void output(node1 *head)
{
	node1 *n = head;

	while (n != NULL)
	{
		printf("%s %d   ", n->word, n->numb);
		n = n->next;
	}

	printf("\n");
}

void findel(HashTable *ht, int key)
{
	if (ht->hashtab[key] == NULL)
	{
		printf("NULL\n");
	}
	else
	{
		output(ht->hashtab[key]);
	}

}
void statistic(HashTable *ht)
{
	int i, nozero = 0, maxl = 0, minl = 350, l = 0, interval;
	node1 *p;

	if (ht == NULL)
    {
        printf("error. haven't got a table\n");
    }

	for (i = 0; i < HashTableSize; i++)
	{
		if (ht->hashtab[i] == NULL)
		{
			continue;
		}

		nozero++;
		p = ht->hashtab[i];
		interval = 0;
		while (p != NULL)
		{
			l++;
			interval++;
			p = p->next;
		}

		if (interval > maxl)
		{
			maxl = interval;
		}

		if (interval < minl)
		{
			minl = interval;
		}
	}

	printf("Not NULL: %d\n", nozero);

	if (nozero == 0)
	{
		printf("Error.\n");
	}
	else
	{
	    printf("Max length: %d\n", maxl);
        printf("Min length: %d\n", minl);
		printf("Medium length: %d\n", l / HashTableSize);
        printf("Total length: %d\n", l);
	}

}
//////////////////////////
/*
Реализовать набор функций для работы с hash table:
-создать (хеш-функция передается как параметр)
-удалить таблицу
-установить, удалить и извлечь элемент
-собрать статистику
*количество непустых ячеек
*количество элементов
*средняя длина цепочки
*минимальная ненулевая длина цепочки
*максимальная длина цепочки
*/
int main()
{
	HashTable *ht;
	ht = NULL;
	char s[maxlength], test;
	int key;

	printf("Print 'e' for the end of program\n");
	printf("s - statistic of Hash Table\n x - delete Hash Table\n a - add element\n d - delete element\n f - find element\n");

	ht = create();
	do
	{
		scanf("%c", &test);

		if (test == 'a')
		{
			scanf("%s",&s);
			add(&ht, s);
			continue;
		}

		if (test == 'x')
		{
			delht(&ht);
			return 0;

		}
		if (test == 'd')
		{
			scanf("%s", &s);
			del(&ht, s);
			continue;
		}
		if (test == 's')
		{
			statistic(ht);
			continue;

		}
		if (test == 'f')
		{
			scanf("%d", &key);
			findel(ht, key);
			continue;

		}

	} while (test != 'e');

	delht(&ht);


	return 0;
}
