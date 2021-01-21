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
    unsigned int id;     ///< id of the producer
} ITEM;

typedef struct FIFO
{ 
    int semid;         ///< syncronization semaphore
    unsigned int ii;   ///< point of insertion
    unsigned int ri;   ///< point of retrieval
    unsigned int cnt;  ///< number of items stored
    ITEM slot[FIFOSZ];  ///< storage memory
} FIFO;

typedef struct INFO
{
    FIFO * fifo;
    int key;
    int shmid;
} INFO;

/* index of access, full and empty semaphores */
#define ACCESS 0
#define FULLNESS 1
#define EMPTINESS 2

/** \brief Create and init the fifo */
void fifoCreate(long key, INFO* newFifo);

/** \brief Connect to the fifo */
void fifoConnect(INFO* info);

/** \brief Disconnect from the fifo */
void fifoDisconnect(INFO* info);

/** \brief Destroy the fifo */
void fifoDestroy(INFO* info);

/**
 *  \brief Insertion of a value into the FIFO.
 *
 * \param id id of the producer
 * \param value value to be stored
 */
void fifoIn(INFO* info,unsigned int id);

/**
 *  \brief Retrieval of a value from the FIFO.
 *
 * \param idp pointer to recipient where to store the producer id
 * \param valuep pointer to recipient where to store the value 
 */
void fifoOut (INFO* info,unsigned int * idp);

#endif /* __SO_IPC_PRODUCER_CONSUMER_FIFO_ */