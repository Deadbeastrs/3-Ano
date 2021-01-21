#include  <stdio.h>
#include  <stdlib.h>
#include  <libgen.h>
#include  <unistd.h>
#include  <sys/wait.h>
#include  <sys/types.h>
#include  <pthread.h>
#include  <math.h>
#include <string.h>
#include <signal.h>

#include  "fifo.h"
#include  "process.h"
#include  "delays.h"


typedef struct
{
    unsigned int id;
    char data[100];
    int semid;
} BUFFER;

static unsigned int nBuff = 5; ///< number of Buffers

static INFO* freeBuff = new INFO;
static INFO* pendingBuff = new INFO;
static BUFFER* pool;
static int sh;

/* ************************************************* */

void down(int semid,unsigned short index)
{
    struct sembuf op = {index, -1, 0};
    psemop(semid, &op, 1);
}

/* ************************************************* */

void up(int semid,unsigned short index)
{
    struct sembuf op = {index, 1, 0};
    psemop(semid, &op, 1);
}

BUFFER* create_pool(int* sh){
    *sh = pshmget(0x9239L, sizeof(BUFFER) * nBuff, 0600 | IPC_CREAT | IPC_EXCL);
    
    /*  attach shared memory to process addressing space */
    return (BUFFER*)pshmat(*sh, NULL, 0);
}

void poolDestroy(BUFFER* fifo, int* sh)
{
    /* detach shared memory from process addressing space */
    if (fifo != NULL)
    {
        pshmdt(fifo);
        fifo = NULL;
    }

    /* ask OS to destroy the shared memory */
    pshmctl(*sh, IPC_RMID, NULL);
    *sh = -1;
}

/* ************************************************* */

/* launcher of a process to run a given routine */
int proc_create(int (*routine)(char*, unsigned int), char* data, unsigned int n)
{
    int pid = pfork();

    /* parent side, even in case of error */
    if (pid != 0) return pid;

    /* child side: run given routine */
    routine(data, n);
    return 0;
}

int server(char* data, unsigned int niter)
{
    while(1){
        gaussianDelay(10, 5);
        unsigned id;
        fifoOut(pendingBuff, &id);
        char temp[100];
        strcpy(temp,pool[id].data);
        sprintf(pool[id].data, "*Servidor* (%s) recebida por servidor %s",temp,data);
        up(pool[id].semid,0);
    }
    exit(EXIT_SUCCESS);
}

int client(char* data, unsigned int niter)
{   
    /* make the job */
    unsigned int i;
    for (i = 0; i < niter; i++)
    {
        /* do something else */
        gaussianDelay(10, 5);
        unsigned id;
        fifoOut(freeBuff, &id);
        strcpy(pool[id].data,data);
        printf("Send ->%d,%s\n",id,pool[id].data);
        fifoIn(pendingBuff, id);
        down(pool[id].semid,0);
        char ans[100];
        strcpy(ans,pool[id].data);
        printf("Receive ->%s\n",ans);
        fifoIn(freeBuff, id);
    }
    exit(EXIT_SUCCESS);
}

/* ******************************************************* */

void exitR(int s){
    fifoDestroy(freeBuff);
    fifoDestroy(pendingBuff);
    for(unsigned int i = 0; i < nBuff;i++){
        psemctl(pool[i].semid, 0, IPC_RMID, NULL);
    }
    poolDestroy(pool,&sh);
    exit(EXIT_FAILURE);
}

int main(int argc, char *argv[])
{
    int nCli = 20;   ///< number of clients
    int nServ = 2;   ///< number of >Servers
    pool = create_pool(&sh);

    fifoCreate(0x1982L,freeBuff);
    fifoCreate(0x0103L,pendingBuff);
    /* command line processing */

    //Fill Buffer
    for(unsigned int i = 0; i < nBuff;i++){
        strcpy(pool[i].data,"-----");
        pool[i].id = i;
        pool[i].semid = psemget(0x2909L+i, 1, 0600 | IPC_CREAT | IPC_EXCL);
    }

    unsigned int range = FIFOSZ;
    if(nBuff < FIFOSZ){
        range = nBuff;
    }

    //Free id's
    for(unsigned int i = 0; i < range;i++){
        fifoIn(freeBuff,i);
    }
    
    /* launching the consumers */
    int cpid[nCli];   /* consumers' ids */
    for (int i = 0; i < nCli; i++)
    {
        char data[100];
        sprintf(data, "Info de Cliente %d",i);
        cpid[i] = proc_create(client,data,1);
        printf("- Client process %d was launched\n", i);
    }

    /* launching the servers */
    int ppid[nServ];   /* servers' ids */
    for (int i = 0; i < nServ; i++)
    {
        char data[100];
        sprintf(data, "%d",i);
        ppid[i] = proc_create(server,data, 1);
        printf("- Server process %d was launched\n", i);
    }
    signal (SIGINT,exitR);
    for (int i = 0; i < nCli; i++)
    {
        pid_t pid = pwaitpid(cpid[i], NULL, 0);
        printf("Client %d (process %d) has terminated\n", i, pid);
    }
    for (int i = 0; i < nServ; i++)
    {
        pid_t pid = pwaitpid(ppid[i], NULL, 0);
        printf("Server %d (process %d) has terminated\n", i, pid);
    }

    //Destroy semaphores
    for(unsigned int i = 0; i < nBuff;i++){
        psemctl(pool[i].semid, 0, IPC_RMID, NULL);
    }
    fifoDestroy(freeBuff);
    fifoDestroy(pendingBuff);
    poolDestroy(pool,&sh);

    return EXIT_SUCCESS;
}