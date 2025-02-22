/**
 * @file
 *
 * \brief A hospital pediatric urgency with a Manchester triage system.
 */

#include  <stdio.h>
#include  <stdlib.h>
#include  <string.h>
#include  <libgen.h>
#include  <unistd.h>
#include  <sys/wait.h>
#include  <sys/types.h>
#include  <thread.h>
#include  <math.h>
#include  <stdint.h>
#include  <signal.h>
#include  <utils.h>
#include  "settings.h"
#include  "pfifo.h"

#include "thread.h"
//#include "process.h"

#include <iostream>

#define USAGE "Synopsis: %s [options]\n" \
   "\t----------+-------------------------------------------\n" \
   "\t  Option  |          Description                      \n" \
   "\t----------+-------------------------------------------\n" \
   "\t -p num   | number of patients (dfl: 4)               \n" \
   "\t -n num   | number of nurses (dfl: 1)                 \n" \
   "\t -d num   | number of doctors (dfl: 1)                \n" \
   "\t -h       | this help                                 \n" \
   "\t----------+-------------------------------------------\n"

/**
 * \brief Patient data structure
 */
typedef struct
{
   char name[MAX_NAME+1];
   int done; // 0: waiting for consultation; 1: consultation finished
   int k;
   pthread_mutex_t mut;
   pthread_cond_t cond;
} Patient;

typedef struct
{
    int num_patients;
    Patient all_patients[MAX_PATIENTS];
    PriorityFIFO triage_queue;
    PriorityFIFO doctor_queue;
} HospitalData;

static int countDoc;

static int countNur;

static pthread_mutex_t doc = PTHREAD_MUTEX_INITIALIZER;

static pthread_mutex_t nur = PTHREAD_MUTEX_INITIALIZER;

HospitalData * hd = NULL;

/**
 *  \brief patient verification test
 */
#define check_valid_patient(id) do { check_valid_id(id); check_valid_name(hd->all_patients[id].name); } while(0)

int random_manchester_triage_priority();
void new_patient(Patient* patient); // initializes a new patient
void random_wait();

/* ************************************************* */

/* changes may be required to this function */
void init_simulation(uint32_t np)
{
   printf("Initializing simulation\n");
   hd = (HospitalData*)mem_alloc(sizeof(HospitalData)); // mem_alloc is a malloc with NULL pointer verification
   memset(hd, 0, sizeof(HospitalData));
   hd->num_patients = np;
   init_pfifo(&hd->triage_queue);
   init_pfifo(&hd->doctor_queue);
}

/* ************************************************* */

void* nurse_iteration(void* arg)
{
   while(1){
      mutex_lock(&nur);
      if(countNur == 0){
         mutex_unlock(&nur);
         return NULL;
      }else{
         countNur -= 1;
      }
      mutex_unlock(&nur);
      printf("\e[34;01mNurse: get next patient\e[0m\n");
      uint32_t patient = retrieve_pfifo(&hd->triage_queue);
      check_valid_patient(patient);
      printf("\e[34;01mNurse: evaluate patient %u priority\e[0m\n", patient);
      uint32_t priority = random_manchester_triage_priority();
      printf("\e[34;01mNurse: add patient %u with priority %u to doctor queue\e[0m\n", patient, priority);
      insert_pfifo(&hd->doctor_queue, patient, priority); 
   }
   return NULL;
}

/* ************************************************* */

void* doctor_iteration(void* arg)
{
   while(1){
      mutex_lock(&doc);
      if(countDoc == 0){
         mutex_unlock(&doc);
         return NULL;
      }else{
         countDoc -= 1;
      }
      mutex_unlock(&doc);
      printf("\e[32;01mDoctor: get next patient\e[0m\n");
      uint32_t patient = retrieve_pfifo(&hd->doctor_queue);
      check_valid_patient(patient);
      printf("\e[32;01mDoctor: treat patient %u\e[0m\n", patient);
      random_wait();
      printf("\e[32;01mDoctor: patient %u treated\e[0m\n", patient);
      hd->all_patients[patient].done = 1;
      printf("Chamar paciente %d\n" , patient);
      cond_broadcast(&hd->all_patients[patient].cond);
   }
   return NULL;
}

/* ************************************************* */

void patient_goto_urgency(int id)
{
   new_patient(&hd->all_patients[id]);
   check_valid_name(hd->all_patients[id].name);
   printf("\e[30;01mPatient %s (number %u): get to hospital\e[0m\n", hd->all_patients[id].name, id);
   insert_pfifo(&hd->triage_queue, id, 1); // all elements in triage queue with the same priority!
}

/* changes may be required to this function */
void patient_wait_end_of_consultation(int id)
{
   check_valid_name(hd->all_patients[id].name);
   printf("\e[30;01mPatient %s (number %u): health problems treated\e[0m\n", hd->all_patients[id].name, id);
}

/* changes are required to this function */
void* patient_life(void* arg)
{  
   uint32_t* id = (uint32_t*)arg;
   patient_goto_urgency(*id);
   mutex_lock(&hd->all_patients[*id].mut);
   while(hd->all_patients[*id].done == 0){
      printf("A espera %d\n" , *id);
      cond_wait(&hd->all_patients[*id].cond, &hd->all_patients[*id].mut);
   }
   patient_wait_end_of_consultation(*id);
   memset(&(hd->all_patients[*id]), 0, sizeof(Patient)); // patient finished
   mutex_unlock(&hd->all_patients[*id].mut);
   return NULL;
}

/* ************************************************* */

int main(int argc, char *argv[])
{
   uint32_t npatients = 10;  ///< number of patients
   uint32_t nnurses = 5;    ///< number of triage nurses
   uint32_t ndoctors = 5;   ///< number of doctors

   /* command line processing */
   int option;
   while ((option = getopt(argc, argv, "p:n:d:h")) != -1)
   {
      switch (option)
      {
         case 'p':
            npatients = atoi(optarg);
            if (npatients < 1 || npatients > MAX_PATIENTS)
            {
               fprintf(stderr, "Invalid number of patients!\n");
               return EXIT_FAILURE;
            }
            break;
         case 'n':
            nnurses = atoi(optarg);
            if (nnurses < 1)
            {
               fprintf(stderr, "Invalid number of nurses!\n");
               return EXIT_FAILURE;
            }
            break;
         case 'd':
            ndoctors = atoi(optarg);
            if (ndoctors < 1)
            {
               fprintf(stderr, "Invalid number of doctors!\n");
               return EXIT_FAILURE;
            }
            break;
         case 'h':
            printf(USAGE, basename(argv[0]));
            return EXIT_SUCCESS;
         default:
            fprintf(stderr, "Non valid option!\n");
            fprintf(stderr, USAGE, basename(argv[0]));
            return EXIT_FAILURE;
      }
   }

   countDoc = npatients;
   countNur = npatients;
   /* start random generator */
   srand(getpid());

   /* init simulation */
   init_simulation(npatients);

   /* dummy code to show a very simple sequential behavior */
   pthread_t pac[npatients];
   pthread_t nur[npatients];
   pthread_t doc[npatients];

   uint32_t pacArr[npatients];
   for(uint32_t i = 0; i < npatients; i++)
   {
      printf("\n");
      pacArr[i] = i;
      random_wait(); // random wait for patience creation
      thread_create(&pac[i], NULL,patient_life, &pacArr[i]);
   }

   for(uint32_t i = 0; i < nnurses; i++)
   {
      printf("\n");
      random_wait(); // random wait for patience creation
      thread_create(&nur[i], NULL,nurse_iteration, NULL);
   }
   
   for(uint32_t i = 0; i < ndoctors; i++)
   {
      printf("\n");
      random_wait(); // random wait for patience creation
      thread_create(&doc[i], NULL,doctor_iteration, NULL);
   }

   //Join 

   for(uint32_t i = 0; i < npatients; i++)
   {
      thread_join(pac[i],NULL);
      printf("Server npatients %d has terminated\n", i);
   }

   for(uint32_t i = 0; i < ndoctors; i++)
   {
      thread_join(doc[i],NULL);
      printf("Server ndoctors %d has terminated\n", i);
   }

   for(uint32_t i = 0; i < nnurses; i++)
   {
      thread_join(nur[i],NULL);
      printf("Server nnurses %d has terminated\n", i);
   }

   return EXIT_SUCCESS;
}


/* YOU MAY IGNORE THE FOLLOWING CODE */

int random_manchester_triage_priority()
{
   int result;
   int perc = (int)(100*(double)rand()/((double)RAND_MAX)); // in [0;100]
   if (perc < 10)
      result = RED;
   else if (perc < 30)
      result = ORANGE;
   else if (perc < 50)
      result = YELLOW;
   else if (perc < 75)
      result = GREEN;
   else
      result = BLUE;
   return result;
}

static char **names = (char *[]) {"Ana", "Miguel", "Luis", "Joao", "Artur", "Maria", "Luisa", "Mario", "Augusto", "Antonio", "Jose", "Alice", "Almerindo", "Gustavo", "Paulo", "Paula", NULL};

char* random_name()
{
   static int names_len = 0;
   if (names_len == 0)
   {
      for(names_len = 0; names[names_len] != NULL; names_len++)
         ;
   }

   return names[(int)(names_len*(double)rand()/((double)RAND_MAX+1))];
}

void new_patient(Patient* patient)
{
   strcpy(patient->name, random_name());
   patient->mut = PTHREAD_MUTEX_INITIALIZER;
   patient->cond = PTHREAD_COND_INITIALIZER;
   patient->done = 0;
}

void random_wait()
{
   usleep((useconds_t)(MAX_WAIT*(double)rand()/(double)RAND_MAX));
}

