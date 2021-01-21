#include "grp_mksofs.h"

#include "core.h"
#include "devtools.h"
#include "bin_mksofs.h"

#include <inttypes.h>
#include <iostream>
#include <math.h>

namespace sofs20
{   
    void grpComputeStructure(uint32_t ntotal, uint32_t & itotal, uint32_t & dbtotal)
    {
        soProbe(601, "%s(%u, %u, ...)\n", __FUNCTION__, ntotal, itotal);
        if(itotal == 0){
            itotal = ntotal / 16;
        }
        uint32_t max = ceil(float(ntotal) / 8.0);
        if(itotal > max){
            itotal = max;
        }
        if(itotal % IPB != 0 || itotal < IPB){
            itotal = itotal + IPB - (itotal % IPB);
        }
        if(itotal % 32 != 0 || itotal < 32){
            itotal = itotal + 32 - (itotal % 32);
        }
        if(itotal < IPB){
            itotal = IPB;
        }
        uint32_t itotalBlocks = ceil((float)itotal/IPB);
        uint32_t reference_table_size = 0;
        dbtotal = ntotal - 1 - itotalBlocks;
        if(dbtotal > REF_CACHE_SIZE) {
            reference_table_size = ceil((float)(dbtotal - REF_CACHE_SIZE)/RPB);
            dbtotal -= reference_table_size;
        }
    }
};

