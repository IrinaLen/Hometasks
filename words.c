#include <stdio.h>
#include <stdlib.h>
#include <ctype.h>
#include <time.h>

#define HashTableSize 40000
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
	size_t(*h)(char *);

} HashTable;

//////////////////////////

size_t hashf1(char *s)
{
	size_t i = 0, h = 0;
	const int magicnumb = 47;
	while (s[i] != '\0')
	{
		h = h * magicnumb + (unsigned int)s[i];
		i++;
	}
	return (h);
}

size_t hashf2(char *s)
{
	size_t i = 0, h = 0;
	while (s[i] != '\0')
	{
		h = h + (unsigned int)s[i];
		i++;
	}
	return (h);
}

size_t hashf3(char *s)
{
	size_t i = 0, h = 0;
	const int magicnumb = 47;
	while (s[i] != '\0')
	{
		h = h  + (unsigned int)s[i] * magicnumb;
		i++;
	}
	return (h);
}


/////////////////////////////
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
	f->h = hashf1; 
	fillht(&f);
	return f;
}

void cleaning(node1 *head)
{
	node1 *tmp;

	while (head != NULL)
	{
		tmp = head;
		head = head->next;
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
		cleaning(p);
	}
	free(*ht);
}

///////////////////////////

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
	hash1 = ((*ht)->h(s)) % HashTableSize;
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
void statistic(HashTable *ht, FILE *FR)
{
	int i, nozero = 0, maxl = 0, minl = 350, l = 0, interval;
	node1 *p;

	if (ht == NULL)
	{
        	fprintf(FR, "error. haven't got a table\n");
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

	fprintf(FR, "Not NULL: %d\n", nozero);

	if (nozero == 0)
	{
		fprintf("Error.\n");
	}
	else
	{
		fprintf(FR, "Max length: %d\n", maxl);
		fprintf(FR, "Min length: %d\n", minl);
		fprintf(FR, "Medium length: %d\n", l / HashTableSize);
		fprintf(FR, "Total length: %d\n", l);
	}

}

void printtotal (HashTable *ht)
{
    int i;
    for (i = 0; i < HashTableSize; i++)
    {
        if (ht->hashtab[i] == NULL)
        {
            continue;
        }
        findel(ht, i);
    }
}

int main()
{
    HashTable *htable;
    htable = NULL;
    FILE *fo, *fr;
    fo = fopen("The_Picture_of_Dorian_Gray.txt", "r");
    char s[maxlength], c;
    int i = 0;
    time_t start = clock();
    htable = create();
    if (fo != NULL)
    {
        c = fgetc(fo);
    }
    else
    {
        printf("Error.");
        return 0;
    }

    while (c != EOF)
    {
        if (c < 'A' || (c > 'Z' && c < 'a') || (c > 'z' && c < 'À') || (c > 'ß' && c < 'à') || (c > 'ï' && c < 'ð') || c > 'ÿ') //if it's not a letter
        {
            c = fgetc(fo);
            continue;
        }

        i = 0;
        while (((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= 'À' && c <= 'ß') || (c >= 'à' && c <= 'ï') || (c >= 'ð' && c <= 'ÿ')) && c != EOF)
        {
            if (c >= 'À' && c <= 'Ï')
            {
                c = c + 32;

            }
            if (c > 'Ð' && c < 'ß')
            {
                c = c + 80;
            }
            if (c > 'A' && c < 'Z')
            {
                c = tolower(c);
            }
            s[i] = c;
            i++;
            c = fgetc(fo);
        }
        s[i] = '\0';
        add(&htable, s);
    }
   
    fr = fopen("text1.txt", "wt");
    if (fr)
    {
        fprintf(fr, "%fl\n", (double) (clock() - start) / CLOCKS_PER_SEC);
        statistic(htable, fr);
    }
    delht(&htable);
    fclose(fo);
    fclose(fr);

    return 0;
}
