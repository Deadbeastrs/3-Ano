#include  <stdio.h>
#include  <stdlib.h>
#include  <libgen.h>
#include  <unistd.h>
#include  <sys/wait.h>
#include  <sys/types.h>
#include  <pthread.h>
#include  <math.h>
#include <string.h>

#include  "fifo.h"
#include  "thread.h"
#include  "delays.h"

typedef struct
{
    unsigned int id;
    char data[100];
    pthread_mutex_t accessBUFF = PTHREAD_MUTEX_INITIALIZER;
    pthread_cond_t infoDone = PTHREAD_COND_INITIALIZER;
} BUFFER;

typedef struct
{
    char data[100];
    unsigned int niter;
} ARGV;

/* The consumer thread */

static BUFFER* pool;
static FIFO* freeBuff = fifoCreate();
static FIFO* pendingBuff = fifoCreate();

void *client(void *argp)
{
    /* cast argument to real type */
    ARGV* argv = (ARGV*)argp;

    /* make the job */
    unsigned int i;
    for (i = 0; i < argv->niter; i++)
    {
        /* do something else */
        gaussianDelay(10, 5);

        unsigned id;
        fifoOut(freeBuff, &id);
        strcpy(pool[id].data,argv->data);
        fifoIn(pendingBuff, id);
        mutex_lock(&pool[id].accessBUFF);
        cond_wait(&pool[id].infoDone,&pool[id].accessBUFF);
        mutex_unlock(&pool[id].accessBUFF);
        char ans[100];
        strcpy(ans,pool[id].data);
        printf("Receive ->%s\n",ans);
        fifoIn(freeBuff, id);
    }
    return NULL;
}

void *server(void *argp)
{
    /* cast argument to real type */
    unsigned int* argv = (unsigned int*)argp;

    /* make the job */
    /* do something else */
    while(1){
        gaussianDelay(10, 5);
        /* retrieve an item from the fifo */
        unsigned id;
        fifoOut(pendingBuff, &id);
        char temp[100];
        strcpy(temp,pool[id].data);
        sprintf(pool[id].data, "*Servidor* (%s) recebida por servidor %d",temp,*argv);
        gaussianDelay(10, 5);
        cond_broadcast(&pool[id].infoDone);
    }
    return NULL;
}

int main(int argc, char *argv[])
{
    unsigned int nBuff = 100; ///< number of Buffers
    int nCli = 20;   ///< number of clients
    int nServ = 4;   ///< number of >Servers

    /* command line processing */

    //Fill Buffer
    pool = new BUFFER[nBuff];
    for(unsigned int i = 0; i < nBuff;i++){
        strcpy(pool[i].data,"-----");
        pool[i].id = i;
    }
    
    fifoInit(freeBuff);
    fifoInit(pendingBuff);

    unsigned int range = FIFOSZ;
    if(nBuff < FIFOSZ){
        range = nBuff;
    }

    //Free id's
    for(unsigned int i = 0; i < range;i++){
        fifoIn(freeBuff,i);
    }
    
    /* launching the client thread */
    pthread_t cthr[nCli];   /* consumers' ids */
    ARGV carg[nCli];        /* consumers' args */
    int i;
    for (i = 0; i < nCli; i++)
    {
        sprintf(carg[i].data, "Info de Cliente %d",i);
        carg[i].niter = 1;
        thread_create(&cthr[i], NULL, client, &carg[i]);
    }

    /* launching the servers */
    pthread_t pthr[nServ];   /* producers' ids */
    unsigned int parg[nServ];        /* producers' args */
    //unsigned int id;
    for (i = 0; i < nServ; i++)
    {
        parg[i] = i;
        thread_create(&pthr[i], NULL, server, &parg[i]);
    }

    /* wait for threads to conclude */
    for (i = 0; i < nCli; i++)
    {
        thread_join(cthr[i], NULL);
        printf("Client thread %d has terminated\n", i);
    }

    delete[] pool;
    return EXIT_SUCCESS;
}