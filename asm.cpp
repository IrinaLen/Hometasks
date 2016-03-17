// mashin.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

typedef union argum
{
	size_t adr;
	size_t number;
	char label[6];
} argu;

typedef struct instr
{
	int type;
	argu arg;
} instruction;

typedef struct lab
{
	char name[6];
	size_t line;

} label;

enum common_type
{
	INSTR_LD = 0,
	INSTR_ST = 1,
	INSTR_LDC,
	INSTR_ADD,
	INSTR_SUB,
	INSTR_CMP,
	INSTR_JMP,
	INSTR_BR,
	INSTR_RET,
	INSTR_BAD = -1
};

/*
0 ld <адрес> — загрузить число на вершину стека из указанной ячейки памяти.
1 st <адрес> — выгрузить число с вершины стека в указанную ячейку памяти.
2 ldc <целое> — загрузить указанную константу на вершину стека.
3 add — сложить два верхних числа на стеке и положить результат на вершину стека.
4 sub — вычесть из верхнего числа на стеке следующее за ним и положить результат на вершину стека.
5 cmp — сравнить два верхних числа на стеке и положить на вершину стека
*0, если числа равны
*1, если первое число больше
*-1, если первое число меньше
6 jmp <метка> — передать управление команде с указанной меткой.
7 br <метка> — передать управление команде с указанной меткой, если на вершине стека не 0.
8 ret – корректное завершение работы.

*/

size_t massize = 1024 * 256;
int codesize = 10000;
int lenofstring = 35;
int lenofcomand = 13;

int main()
{
	long int *data, *stack;
	instruction *code;
	label *marks;
	FILE *fo;

	fo = fopen("numper2.txt", "rt");
	if (fo == NULL)
	{
		printf("Error.");
		return 0;
	}
	data = (long int*)malloc(massize * sizeof(long int));
	stack = (long int*)malloc(massize * sizeof(long int));
	code = (instruction*)malloc(codesize * sizeof(instruction));
	marks = (label*)malloc(codesize * sizeof(label));

	char s[lenofstring], comand[lenofcomand];
	int l = 0, i, instnumb = 0, marknumb = 0, length;
	int k;
	for (i = 0; i < massize; i++)
	{
		data[i] = 0;
		stack[i] = 0;
	}

	fgets(s, lenofstring, fo);
	
	while (!feof(fo)) 
	{
		length = strlen(s);
		l = 0; i = 0;
		while (s[i] != ':' && s[i] != '\0') //поиск метки
		{
			l++;
			i++;
		}

		if (l != length)
		{
			i = 0;
			while (s[i] != ':')
			{
				marks[marknumb].name[i] = s[i];
				i++;
			}
			marks[marknumb].name[i] = '\0';
			i = i + 2;//for new symbol
			l = l + 2;
			for (k = marknumb - 1; k > -1; k--)//проверка наличия метки
			{
				if (strcmp(marks[marknumb].name, marks[k].name) == 0)
				{
					printf("Label error.");
					return 0;
				}
			}
			
			marks[marknumb].line = instnumb;
			marknumb++;
		}
		else
		{
			i = 0;
			l = 0;
		}

		while (s[i] != ' ' && s[i] != '\n' && s[i] != ';')
		{
			comand[i - l] = s[i];
			i++;
		}
		comand[i - l] = '\0';

		if (strcmp(comand, "ld") == 0)
		{
			code[instnumb].type = INSTR_LD;
			while (i < length && s[i] != ';' && s[i] != '\n')
			{
				comand[i - l - 3] = s[i];
				i++;
			}
			comand[i - l - 3] = '\0';

			code[instnumb].arg.adr = atoll(comand);
		}
		else if (strcmp(comand, "st") == 0)
		{
			code[instnumb].type = INSTR_ST;
			while (i < length && s[i] != ';' && s[i] != '\n')
			{
				comand[i - l - 3] = s[i];
				i++;
			}
			comand[i - l - 3] = '\0';


			code[instnumb].arg.adr = atoll(comand);
		}
		else if (strcmp(comand, "ldc") == 0)
		{
			code[instnumb].type = INSTR_LDC;
			while (i < length && s[i] != ';' && s[i] != '\n')
			{
				comand[i - l - 4] = s[i];
				i++;
			}
			comand[i - l - 4] = '\0';

			code[instnumb].arg.number = atoi(comand);

		}
		else if (strcmp(comand, "add") == 0)
		{
			code[instnumb].type = INSTR_ADD;
		}
		else if (strcmp(comand, "sub") == 0)
		{
			code[instnumb].type = INSTR_SUB;
		}
		else if (strcmp(comand, "cmp") == 0)
		{
			code[instnumb].type = INSTR_CMP;
		}
		else if (strcmp(comand, "jmp") == 0)
		{
			code[instnumb].type = INSTR_JMP;
			while (i < length && s[i] != ';' && s[i] != '\n')
			{
				comand[i - l - 4] = s[i];
				i++;
			}
			comand[i - l - 4] = '\0';


			strcpy(code[instnumb].arg.label, comand);
		}
		else if (strcmp(comand, "br") == 0)
		{
			code[instnumb].type = INSTR_BR;
			while (i < length && s[i] != ';' && s[i] != '\n')
			{
				comand[i - l - 3] = s[i];
				i++;
			}
			comand[i - l - 3] = '\0';


			strcpy(code[instnumb].arg.label, comand);
		}
		else if (strcmp(comand, "ret") == 0)
		{
			code[instnumb].type = INSTR_RET;
		}
		else
		{
			code[instnumb].type = INSTR_BAD;
		}

		instnumb++;
		fgets(s, lenofstring, fo);

	}

	fclose(fo);

	bool stop = false;
	bool error = false;
	long int stnumb = -1;
	instnumb = -1;

	while (!stop && !error)
	{
		instnumb++;
		printf("%2d - ", instnumb);
		switch (code[instnumb].type)
		{
		case INSTR_BAD:
			error = true;
			break;

		case INSTR_LD:  //0 ld <адрес> — загрузить число на вершину стека из указанной ячейки памяти.
			stnumb++;
			if (code[instnumb].arg.adr < 0 || code[instnumb].arg.adr >= massize || stnumb >= massize)
			{
				error = true;
				break;
			}
			stack[stnumb] = data[code[instnumb].arg.adr];
			break;

		case INSTR_ST: //1 st <адрес> — выгрузить число с вершины стека в указанную ячейку памяти.
			if (code[instnumb].arg.adr < 0 || code[instnumb].arg.adr >= massize || stnumb >= massize || stnumb < 0)
			{
				error = true;
				break;
			}
			data[code[instnumb].arg.adr] = stack[stnumb];
			stack[stnumb] = 0;
			stnumb--;
			break;

		case INSTR_LDC://2 ldc <целое> — загрузить указанную константу на вершину стека.
			stnumb++;
			if (stnumb >= massize)
			{
				error = true;
				break;
			}
			stack[stnumb] = code[instnumb].arg.number;
			break;

		case INSTR_ADD://3 add — сложить два верхних числа на стеке и положить результат на вершину стека.
			if (stnumb < 1)
			{
				error = true;
				break;
			}
			stack[stnumb - 1] = stack[stnumb - 1] + stack[stnumb];
			stack[stnumb] = 0;
			stnumb--;
			break;

		case INSTR_SUB://4 sub — вычесть из верхнего числа на стеке следующее за ним и положить результат на вершину стека.
			if (stnumb < 1)
			{
				error = true;
				break;
			}
			stack[stnumb - 1] = stack[stnumb] - stack[stnumb - 1];
			stack[stnumb] = 0;
			stnumb--;
			break;

		case INSTR_CMP://5 cmp — сравнить два верхних числа на стеке и положить на вершину стека *0, если числа равны *1, если первое число больше *-1, если первое число меньше
			if (stnumb < 1 || stnumb == massize)
			{
				error = true;
				break;
			}
			if (stack[stnumb] == stack[stnumb - 1])
			{
				stack[stnumb] = 0;
				stack[stnumb - 1] = 0;
				stnumb--;
				break;
			}
			if (stack[stnumb] > stack[stnumb - 1])
			{
				stack[stnumb] = 0;
				stack[stnumb - 1] = 1;
				stnumb--;
				break;
			}
			if (stack[stnumb] < stack[stnumb - 1])
			{
				stack[stnumb] = 0;
				stack[stnumb - 1] = -1;
				stnumb--;
				break;
			}

		case INSTR_JMP://6 jmp <метка> — передать управление команде с указанной меткой.
			marknumb = 0;
			while (strcmp(code[instnumb].arg.label, marks[marknumb].name) != 0)
			{
				marknumb++;
			}
			instnumb = marks[marknumb].line - 1;
			break;

		case INSTR_BR://7 br <метка> — передать управление команде с указанной меткой, если на вершине стека не 0.
			if (stack[stnumb] == 0)
			{
				break;
			}
			marknumb = 0;
			while (strcmp(code[instnumb].arg.label, marks[marknumb].name) != 0)
			{
				marknumb++;
			}
			instnumb = marks[marknumb].line - 1;
			break;

		case INSTR_RET://8 ret – корректное завершение работы.
			stop = true;
			printf("%ld\n", stack[stnumb]);
			break;
		}
	}




	free(data);
	free(stack);
	free(code);
	free(marks);

	return 0;
}
