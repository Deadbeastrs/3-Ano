#include "grp_mksofs.h"

#include "rawdisk.h"
#include "core.h"
#include "devtools.h"
#include "bin_mksofs.h"

#include <string.h>
#include <inttypes.h>
#include <math.h>

namespace sofs20
{
    void grpFillSuperblock(const char *name, uint32_t ntotal, uint32_t itotal, uint32_t dbtotal)
    {
        soProbe(602, "%s(%s, %u, %u, %u)\n", __FUNCTION__, name, ntotal, itotal, dbtotal);

        uint32_t itotal_blocks = ceil((float)itotal / IPB);

        SOSuperblock sp_block = SOSuperblock();
        sp_block.magic = 0xFFFF;
        sp_block.version = VERSION_NUMBER;
        sp_block.mntstat = 0;
        strcpy(sp_block.name, name);
        sp_block.ntotal = ntotal;
        sp_block.itotal = itotal;
        sp_block.ifree = itotal - 1;
        sp_block.iidx = 0;
        sp_block.ibitmap[MAX_INODES / 32] = {};
        sp_block.ibitmap[0] = 1;
        sp_block.dbp_start = itotal_blocks + 1;
        sp_block.dbtotal = dbtotal;
        sp_block.dbfree = dbtotal - 1;
        sp_block.rt_start = 1 + itotal_blocks + dbtotal;
        sp_block.rt_size = ntotal - 1 - itotal_blocks - dbtotal;
        sp_block.reftable = SOSuperblock::ReferenceTable();
        sp_block.insertion_cache = SOSuperblock::ReferenceCache();
        sp_block.retrieval_cache = SOSuperblock::ReferenceCache();

        sp_block.insertion_cache.idx = 0;
        for (int i = 0; i < REF_CACHE_SIZE; i++)
        {
            sp_block.insertion_cache.ref[i] = BlockNullReference;
        }

        int k = 0;
        sp_block.retrieval_cache.idx = dbtotal > REF_CACHE_SIZE ? 0 : REF_CACHE_SIZE - dbtotal + 1;
        for (int i = 0; i < REF_CACHE_SIZE; i++)
        {
            if (i < (int)sp_block.retrieval_cache.idx)
            {
                sp_block.retrieval_cache.ref[i] = BlockNullReference;
            }
            else
            {
                sp_block.retrieval_cache.ref[i] = k + 1;
                k++;
            }
        }

        if (dbtotal > REF_CACHE_SIZE)
        {
            sp_block.reftable.count = dbtotal - REF_CACHE_SIZE - 1;
        }

        soWriteRawBlock(0, (void *)&sp_block);
    }
}; // namespace sofs20
