#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <stdbool.h>
#include <errno.h>
#include <sys/shm.h>
#include <sys/sem.h>

#include "fifo.h"
#include "delays.h"
#include "process.h"

/* ************************************************* */

void down(INFO* info,unsigned short index)
{
    struct sembuf op = {index, -1, 0};
    psemop(info->fifo->semid, &op, 1);
}

/* ************************************************* */

void up(INFO* info,unsigned short index)
{
    struct sembuf op = {index, 1, 0};
    psemop(info->fifo->semid, &op, 1);
}

/* ************************************************* */

/* Creation of the FIFO */
void fifoCreate(long key, INFO* newFifo)
{
    /* create the shared memory */
    newFifo->key = key;
    newFifo->shmid = pshmget(key, sizeof(FIFO), 0600 | IPC_CREAT | IPC_EXCL);
    
    /*  attach shared memory to process addressing space */
    newFifo->fifo = (FIFO*)pshmat(newFifo->shmid, NULL, 0);

    /* init fifo */
    unsigned int i;
    for (i = 0; i < FIFOSZ; i++)
    {
        newFifo->fifo->slot[i].id = 99999999;
    }
    newFifo->fifo->ii = newFifo->fifo->ri = 0;
    newFifo->fifo->cnt = 0;

    /* create access, full and empty semaphores */
    newFifo->fifo->semid = psemget(key, 3, 0600 | IPC_CREAT | IPC_EXCL);

    /* init semaphores */
    for (i = 0; i < FIFOSZ; i++)
    {
        up(newFifo,EMPTINESS);
    }
    up(newFifo,ACCESS);
}

void fifoDestroy(INFO* info)
{
    /* destroy semaphore set */
    psemctl(info->fifo->semid, 0, IPC_RMID, NULL);

    /* detach shared memory from process addressing space */
    if (info->fifo != NULL)
    {
        pshmdt(info->fifo);
        info->fifo = NULL;
    }

    /* ask OS to destroy the shared memory */
    pshmctl(info->shmid, IPC_RMID, NULL);
    info->shmid = -1;
    delete info;
    info = NULL;
}

/* ************************************************* */

/* Insertion of a pait id into the FIFO  */
void fifoIn(INFO* info,unsigned int id)
{
    /* decrement emptiness, blocking if full */
    down(info,EMPTINESS);

    /* lock access */
    down(info,ACCESS);

    /* Insert pair */
    info->fifo->slot[info->fifo->ii].id = id;
    gaussianDelay(1, 0.5);
    info->fifo->ii = (info->fifo->ii + 1) % FIFOSZ;
    info->fifo->cnt++;

    /* unlock access and increment fullness */
    up(info,ACCESS);
    up(info,FULLNESS);
}

/* ************************************************* */

/* Retrieval of a pair <id, value> from the FIFO */

void fifoOut (INFO* info,unsigned int * idp)
{
    /* decrement fullness, blocking if full */
    down(info,FULLNESS);

    /* lock access */
    down(info,ACCESS);

    /* Retrieve pair */
    *idp = info->fifo->slot[info->fifo->ri].id;
    info->fifo->slot[info->fifo->ri].id = 99999999;
    info->fifo->ri = (info->fifo->ri + 1) % FIFOSZ;
    info->fifo->cnt--;

    /* unlock access and increment fullness */
    up(info,ACCESS);
    up(info,EMPTINESS);
}

/* ************************************************* */

