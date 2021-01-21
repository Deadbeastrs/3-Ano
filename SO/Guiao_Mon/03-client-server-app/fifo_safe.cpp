/*
 *  @brief A simple FIFO, whose elements are pairs of integers,
 *      one being the id of the producer and the other the value produced
 *
 * @remarks safe, bust waiting version
 *
 *  The following operations are defined:
 *     \li insertion of a value
 *     \li retrieval of a value.
 *
 * \author (2016) Artur Pereira <artur at ua.pt>
 */

#include <stdio.h>
#include <unistd.h>
#include <stdbool.h>
#include <errno.h>
#include <pthread.h>
#include <string.h>
#include <stdlib.h> 

#include "fifo.h"
#include "thread.h"

/* ************************************************* */

FIFO* fifoCreate(void){
    FIFO* fifo = (FIFO*) malloc(sizeof(FIFO));
    fifoInit(fifo);
    return fifo;
}

/* Initialization of the FIFO */
void fifoInit(FIFO* fifo)
{
    mutex_lock(&fifo->accessCR);

    unsigned int i;
    for (i = 0; i < FIFOSZ; i++)
    {
        fifo->slot[i].id = 99999;
    }
    fifo->ii = fifo->ri = 0;
    fifo->cnt = 0;

    cond_broadcast(&fifo->fifoNotFull);

    mutex_unlock(&fifo->accessCR);
}

/* ************************************************* */

/* Check if FIFO is full */
bool fifoFull(FIFO* fifo)
{
    return fifo->cnt == FIFOSZ;
}

/* ************************************************* */

/* Check if FIFO is empty */
bool fifoEmpty(FIFO* fifo)
{
    return fifo->cnt == 0;
}

/* ************************************************* */

/* Insertion of a pait <id, value> into the FIFO  */
void fifoIn(FIFO* fifo,unsigned int id)
{
    mutex_lock(&fifo->accessCR);

    /* wait while fifo is full */
    while (fifoFull(fifo))
    {
        cond_wait(&fifo->fifoNotFull, &fifo->accessCR); 
    }

    /* Insert pair */
    fifo->slot[fifo->ii].id = id;
    fifo->ii = (fifo->ii + 1) % FIFOSZ;
    fifo->cnt++;

    cond_broadcast(&fifo->fifoNotEmpty);

    mutex_unlock(&fifo->accessCR);
}

/* ************************************************* */

/* Retrieval of a pair <id, value> from the FIFO */

void fifoOut (FIFO* fifo,unsigned int* id)
{
    mutex_lock(&fifo->accessCR);

    /* wait while fifo is empty */
    while (fifoEmpty(fifo))
    {
        cond_wait(&fifo->fifoNotEmpty, &fifo->accessCR); 
    }
    *id = fifo->slot[fifo->ri].id;
    fifo->slot[fifo->ri].id = 99999;
    fifo->ri = (fifo->ri + 1) % FIFOSZ;
    fifo->cnt--;

    cond_broadcast(&fifo->fifoNotFull);

    mutex_unlock(&fifo->accessCR);
}

/* ************************************************* */

