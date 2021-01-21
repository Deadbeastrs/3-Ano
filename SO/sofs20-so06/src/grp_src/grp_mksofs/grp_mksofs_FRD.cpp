#include "grp_mksofs.h"

#include "rawdisk.h"
#include "core.h"
#include "devtools.h"
#include "bin_mksofs.h"

#include <string.h>
#include <inttypes.h>

namespace sofs20
{
    /*
       filling in the contents of the root directory:
       the first 2 entries are filled in with "." and ".." references
       the other entries are empty.
       If rdsize is 2, a second block exists and should be filled as well.
       */
    void grpFillRootDir(uint32_t itotal)
    {
        /* replace the following line with your code */
        soProbe(606, "%s(%u)\n", __FUNCTION__, itotal);

        uint32_t itotal_blocks = itotal/IPB;
        uint32_t dbblock_start = 1 + itotal_blocks;

        SODirentry buf[DPB];
        memset(buf,0,BlockSize);
        buf[0].in = 0;
        buf[1].in = 0;
        strcpy(buf[0].name,".");
        strcpy(buf[1].name,"..");

        soWriteRawBlock(dbblock_start, (void*)&buf);
    }
};

