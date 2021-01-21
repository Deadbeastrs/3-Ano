/**
 *  @file 
 *
 *  @brief A simple FIFO, whose elements are pairs of integers,
 *      one representing the producer and the other the value produced
 *
 *  The following operations are defined:
 *     \li insertion of a value
 *     \li retrieval of a value.
 *
 * \author (2016) Artur Pereira <artur at ua.pt>
 */

#ifndef __SO_IPC_PRODUCER_CONSUMER_FIFO_
#define __SO_IPC_PRODUCER_CONSUMER_FIFO_

#define  FIFOSZ         10

typedef struct ITEM
{
    unsigned int id;
} ITEM;

typedef struct FIFO
{ 
    unsigned int ii;   ///< point of insertion
    unsigned int ri;   ///< point of retrieval
    unsigned int cnt;  ///< number of items stored
    ITEM slot[FIFOSZ];  ///< storage memory
    pthread_mutex_t accessCR = PTHREAD_MUTEX_INITIALIZER;
    pthread_cond_t fifoNotFull = PTHREAD_COND_INITIALIZER;
    pthread_cond_t fifoNotEmpty = PTHREAD_COND_INITIALIZER;
} FIFO;

FIFO* fifoCreate(void);

/**
 * \brief Init the fifo 
 */
void fifoInit(FIFO* fifo);

/**
 *  \brief Insertion of a value into the FIFO.
 *
 * \param id id of the producer
 * \param value value to be stored
 */
void fifoIn(FIFO* fifo,unsigned int id);

/**
 *  \brief Retrieval of a value from the FIFO.
 *
 * \param idp pointer to recipient where to store the producer id
 * \param valuep pointer to recipient where to store the value 
 */
void fifoOut (FIFO* fifo,unsigned int* id);

bool fifoEmpty(FIFO* fifo);
#endif /* __SO_IPC_PRODUCER_CONSUMER_FIFO_ */
