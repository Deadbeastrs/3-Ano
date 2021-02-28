#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <assert.h>
#include "thread.h"
#include "process.h"
#include "utils.h"

#define MAX_NA 20 // maximum number of active entities of type A
#define MAX_NB 10 // maximum number of active entities of type B
#define MIN_Ni  1 // minimum number of A's random generated numbers
#define MAX_Ni 50 // maximum number of A's random generated numbers

/*  functions to manipulate the Stack data type *
 *  (implementation in the bottom of the file)  */

#define MAX_ELEMS (MAX_NA*MAX_Ni)

typedef struct
{
   int array[MAX_ELEMS];
   int size;
} Stack;

void init(Stack *s);            // initialize stack s
void push(Stack *s, int v);     // insert a number into stack s
int pop(Stack *s);              // retrieve a number from stack s
void clean_and_print(Stack *s); // empties and print the contents of stack s


/* auxiliary functions */

void sort(int *array, int size); // sorts the first size elements of array


/* data structure shared among all intervening threads/processes */

#define MAX_ACTIVE_ENTITIES (MAX_NA+MAX_NB)

static int NA = 2;
static int NB = 1;

typedef struct
{
   Stack stack;
   int num[MAX_NA]; // shared variable attached to each active entity A 
   // if desired, insert your code here
} Data;

Data* mem = NULL;

// if desired, insert your code here


/**
 \brief type A active entity main function.

O ciclo de vida pretendido para as entidades activas do tipo A e:
  (a) escolher aleatoriamente o numero de numeros Ni a ser gerados;
  (b) gerar aleatoriamente os Ni numeros e ordena-los (utilize a funcao sort);
  (c) entregar, por ordem crescente, cada numero esperando que seja recolhido
      antes de entregar o proximo;
  (d) repetir o passo (c) enquanto for necessario;
  (e) terminar.

NOTA: Deve arranjar uma forma comunicar as entidades activas B quer da
existencia de numero na variavel partilhada, quer da situacao em que ja nao
ha mais numeros. Para tal pode recorrer a outras variaveis partilhadas ou
tao so utilizar valores fora do intervalo dos numeros inteiros gerados.

NOTA2: utilize a funcao random_int (include/utils.h) para gerar um numero
aleatorio.
 */

void A(int i) // life cycle of a type A thread/process
{
   assert (i >= 1 && i <= NA);

   int Ni = random_int(MIN_Ni, MAX_Ni);
   printf("Starting active entity A%d\n", i);

   // insert your code here

   printf("Ending active entity A%d\n", i);
}


/**
 \brief type B active entity main function.

O ciclo de vida pretendido para as entidades activas do tipo B e:
   (a) esperar que haja numeros em todas as NA variaveis partilhadas (uma de
       cada entidade activa A) ou apenas nas que ainda nao entregaram tudo;
   (b) recolher o menor dos numeros de todas essas variaveis, sinalizar da
       sua recolha e coloca-lo na pilha;
   (c) repetir os passos (a)-(b) enquanto for necessario;
   (d) terminar.

NOTA: Sugere-se que se resolva primeiro este problema com apenas uma
entidade activa do tipo B e so quando esta solucao estiver a funcionar se
generalize para valores NB maiores que 1.
 */
void B(int i) // life cycle of a type B thread/process
{
   assert (i >= 1 && i <= NB);

   printf("Starting active entity B%d\n", i);

   // insert your code here

   printf("Ending active entity B%d\n", i);
}


// if desired, insert other functions here


void process_args(int argc, char *argv[]);

/**
 \brief main function

O programa principal (main) deve lancar todas as entidades activas (do tipo
A e B), esperar que terminem a sua execucao, e invocar a função clean_and_print
que esvazia e imprime o conteudo da pilha (pelo que, devem aparecer todos os
numeros gerados por ordem decrescente).
 */
int main(int argc, char *argv[])
{
   process_args(argc, argv);

   mem = (Data*)mem_alloc(sizeof(Data));
   memset(mem->num, 0, sizeof(mem->num));
   init(&mem->stack);

   /* start random generator */
   srand(getpid());

   // insert your code here

   clean_and_print(&mem->stack);

   // insert your code here
}


/* functions to manipulate the Stack data type: implementation *
 *                 (NO MODIFICATION REQUIRED!)                 */

void init(Stack *s)
{
   assert (s != NULL);

   s->size = 0;
   memset(s->array, 0, sizeof(s->array));
}

void push(Stack *s, int v)
{
   assert (s != NULL);
   assert (s->size < MAX_ELEMS);
   // in this problem the stack will never be full!

   s->array[s->size] = v;
   s->size++;
}

int pop(Stack *s)
{
   assert (s != NULL);
   assert (s->size > 0);

   s->size--;
   int res = s->array[s->size];
   return res;
}

void clean_and_print(Stack *s)
{
   assert (s != NULL);

   printf("Stack contents:");
   int i = 0;
   int last = 1000; // larger then 100
   while(s->size > 0)
   {
      int v = pop(s);
      assert (v >= 1 && v <= 100 && v <= last);
      printf("%s%4d", i%15 == 0 ? "\n   " : "", v);
      last = v;
      i++;
   }
   printf("\n");
}


/* auxiliary functions: implementation *
 *     (NO MODIFICATION REQUIRED!)     */

/* qsort int comparison function */ 
static int int_cmp(const void *a, const void *b) 
{ 
   const int *ia = (const int *)a;
   const int *ib = (const int *)b;
   return *ia  - *ib; 
   /* integer comparison: returns negative if b > a 
    *    and positive if a > b */ 
} 

void sort(int *array, int size)
{
   assert (array != NULL);
   assert (size >= 0);

   qsort(array, size, sizeof(int), int_cmp);
}

#define USAGE "Synopsis: %s [options]\n" \
   "\t----------+-------------------------------------------\n" \
"\t  Option  |          Description                      \n" \
"\t----------+-------------------------------------------\n" \
"\t -A num   | number of A active entities (dfl: %d)      \n" \
"\t -B num   | number of B active entities (dfl: %d)      \n" \
"\t -h       | this help                                 \n" \
"\t----------+-------------------------------------------\n"

void process_args(int argc, char *argv[])
{
   /* command line processing */
   int option;
   while ((option = getopt(argc, argv, "A:B:h")) != -1)
   {
      switch (option)
      {
         case 'A':
            NA = atoi(optarg);
            if (NA < 1 || NA > MAX_NA)
            {
               fprintf(stderr, "Invalid number of A active entities!\n");
               exit(EXIT_FAILURE);
            }
            break;

         case 'B':
            NB = atoi(optarg);
            if (NB < 1 || NB > MAX_NB)
            {
               fprintf(stderr, "Invalid number of B active entities!\n");
               exit(EXIT_FAILURE);
            }
            break;

         case 'h':
            printf(USAGE, basename(argv[0]), NA, NB);
            exit(EXIT_FAILURE);

         default:
            fprintf(stderr, "Non valid option!\n");
            fprintf(stderr, USAGE, basename(argv[0]), NA, NB);
            exit(EXIT_FAILURE);
      }
   }
}
