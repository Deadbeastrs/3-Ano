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
    void grpResetFreeDataBlocks(uint32_t ntotal, uint32_t itotal, uint32_t dbtotal)
    {
        soProbe(607, "%s(%u, %u, %u)\n", __FUNCTION__, ntotal, itotal, dbtotal);

        uint32_t itotal_blocks = ceil((float)itotal / IPB);

        /* replace the following line with your code */

        uint32_t start = 2 + itotal_blocks;
        uint32_t end = start + dbtotal - 1;
        uint32_t buf[RPB] = {0};

        for(uint32_t i = start; i<end;i++) {
            soWriteRawBlock(i,&buf);
        }
    }
};

//[-----|--------|---------------------------|-----]