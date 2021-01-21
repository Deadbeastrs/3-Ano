#include "grp_mksofs.h"

#include "rawdisk.h"
#include "core.h"
#include "devtools.h"
#include "bin_mksofs.h"

#include <inttypes.h>
#include <string.h>
#include <math.h>
#include <iostream>
#include <array>

namespace sofs20
{
    void grpFillReferenceTable(uint32_t ntotal, uint32_t itotal, uint32_t dbtotal)
    {
        soProbe(605, "%s(%u, %u, %u)\n", __FUNCTION__, ntotal, itotal, dbtotal);

        uint32_t itotal_blocks = ceil((float)itotal / IPB);
        uint32_t buf[RPB];
        for(uint32_t i = 0; i < RPB;i++){
            buf[i] = BlockNullReference;
        }
        uint32_t refTableStart = 1 + dbtotal + itotal_blocks;
        int k = 0;
        if(dbtotal > REF_CACHE_SIZE){
            for(uint32_t i = REF_CACHE_SIZE; i < dbtotal; i++){
                if(k == RPB - 1 || i == dbtotal - 2 ){
                    buf[k] = i+1;
                    k = 0;
                    soWriteRawBlock(refTableStart,buf);
                    refTableStart += 1;
                    for(uint32_t i = 0; i < RPB;i++){
                        buf[i] = BlockNullReference;
                    }
                }else{
                    buf[k] = i+1;
                    k++;
                }
            }
        }
    }
};
