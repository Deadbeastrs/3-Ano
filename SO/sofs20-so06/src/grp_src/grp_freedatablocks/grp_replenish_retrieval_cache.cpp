/*
 *  \author António Rui Borges - 2012-2015
 *  \authur Artur Pereira - 2016-2020
 */

#include "freedatablocks.h"

#include <string.h>
#include <errno.h>
#include <iostream>

#include "core.h"
#include "devtools.h"
#include "daal.h"
#include "math.h"

namespace sofs20
{
    void grpReplenishRetrievalCache(void)
    {
        soProbe(443, "%s()\n", __FUNCTION__);
        SOSuperblock *sp_block = soGetSuperblockPointer();
        bool is_retc_empty = sp_block->retrieval_cache.idx == REF_CACHE_SIZE;

        if (!is_retc_empty)
            return;

        bool should_use_insc = sp_block->reftable.count == 0;
        if (should_use_insc)
        {
            uint32_t block_id = std::max<uint32_t>(sp_block->insertion_cache.idx - 1, 0);
            uint32_t target_block_id = REF_CACHE_SIZE - 1;
            while (sp_block->insertion_cache.ref[block_id] != BlockNullReference)
            {
                uint32_t ref = sp_block->insertion_cache.ref[block_id];
                sp_block->retrieval_cache.ref[target_block_id] = ref;
                sp_block->insertion_cache.ref[block_id] = BlockNullReference;
                sp_block->retrieval_cache.idx--;
                sp_block->insertion_cache.idx--;
                if (block_id > 0)
                    block_id--;
                if (target_block_id > 0)
                    target_block_id--;
            }
        }
        else
        {
            uint32_t target_block_id = REF_CACHE_SIZE - 1;
            uint32_t *first_ref_block = soGetReferenceBlockPointer(sp_block->reftable.blk_idx);

            for (uint32_t i = 0; i < REF_CACHE_SIZE; i++)
            {
                uint32_t ref = first_ref_block[sp_block->reftable.ref_idx];
                if (ref == BlockNullReference)
                {
                    sp_block->reftable.ref_idx = 0;
                    break;
                }
                sp_block->retrieval_cache.ref[target_block_id] = ref;
                sp_block->retrieval_cache.idx--;
                first_ref_block[sp_block->reftable.ref_idx] = BlockNullReference;
                sp_block->reftable.count--;
                sp_block->reftable.ref_idx++;
                if (target_block_id > 0)
                    target_block_id--;
            }
            soSaveReferenceBlock();
        }
        soSaveSuperblock();
    }
} // namespace sofs20