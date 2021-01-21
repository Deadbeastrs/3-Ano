/*
 *  \author António Rui Borges - 2012-2015
 *  \authur Artur Pereira - 2009-2020
 */

#include "freedatablocks.h"

#include <stdio.h>
#include <errno.h>
#include <inttypes.h>
#include <time.h>
#include <unistd.h>
#include <sys/types.h>

#include "core.h"
#include "devtools.h"
#include "daal.h"

namespace sofs20
{
    uint32_t grpAllocDataBlock()
    {
        soProbe(441, "%s()\n", __FUNCTION__);

        SOSuperblock* sb = soGetSuperblockPointer();
        
        uint32_t i = sb->retrieval_cache.idx;
        if(i == REF_CACHE_SIZE){
            soReplenishRetrievalCache();
        }

        uint32_t frrc = sb->retrieval_cache.ref[i];
        sb->retrieval_cache.ref[i] = BlockNullReference;
        sb->retrieval_cache.idx += 1;

        if(sb->dbfree == 0){
            throw SOException(ENOSPC, __FUNCTION__);
        }
        sb->dbfree --;
        soSaveSuperblock();
        return frrc;
    }
};

