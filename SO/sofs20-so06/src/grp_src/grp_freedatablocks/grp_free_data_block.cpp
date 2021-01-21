/*
 *  \author António Rui Borges - 2012-2015
 *  \authur Artur Pereira - 2016-2020
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
    void grpFreeDataBlock(uint32_t bn)
    {
        soProbe(442, "%s(%u)\n", __FUNCTION__, bn);

        /* replace the following line with your code */
        //binFreeDataBlock(bn);
        SOSuperblock *sb = soGetSuperblockPointer();
        
        uint32_t max = sb->dbtotal;
        
        if(bn >= max)
        {
            throw SOException(EINVAL,__FUNCTION__);
        }
        else
        {
            if((sb->insertion_cache.idx) ==REF_CACHE_SIZE)
            {
                soDepleteInsertionCache();
                sb->insertion_cache.idx = 0;
            }
            sb->dbfree += 1;
            uint32_t id = sb->insertion_cache.idx;
            sb->insertion_cache.ref[id] = bn;
            sb->insertion_cache.idx +=1;
            
            
        }
        soSaveSuperblock();
    }
};

