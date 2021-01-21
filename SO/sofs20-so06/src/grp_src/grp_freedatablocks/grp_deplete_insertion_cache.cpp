/*
 *  \author António Rui Borges - 2012-2015
 *  \authur Artur Pereira - 2016-2020
 */

#include "freedatablocks.h"

#include "core.h"
#include "devtools.h"
#include "daal.h"

#include <stdio.h>
#include <errno.h>
#include <string.h>
#include <iostream>
using namespace std;

namespace sofs20
{
    /* only fill the current block to its end */
    void grpDepleteInsertionCache(void)
    {
        soProbe(444, "%s()\n", __FUNCTION__);
        SOSuperblock* sb = soGetSuperblockPointer();
        if(sb->insertion_cache.idx == REF_CACHE_SIZE){
            uint32_t refBlock = sb->reftable.blk_idx + ((sb->reftable.ref_idx + sb->reftable.count) / RPB);
            if(refBlock >= sb->rt_size){
                refBlock = refBlock - sb->rt_size;
            }
            uint32_t refPos = (sb->reftable.count - (RPB - sb->reftable.ref_idx)) % RPB;
            uint32_t* rt = soGetReferenceBlockPointer(refBlock);
            int c = 0;
            for(int i = REF_CACHE_SIZE-1; i >= 0;i--){
                if(refPos > RPB) break; 
                rt[refPos++] = sb->insertion_cache.ref[i];
                sb->insertion_cache.ref[i] = BlockNullReference;
                c++;
            }
            sb->reftable.count = sb->reftable.count + c;
        }
        soSaveReferenceBlock();
        soSaveSuperblock();
    }
};