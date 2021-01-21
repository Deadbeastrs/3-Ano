#include "direntries.h"

#include "core.h"
#include "devtools.h"
#include "daal.h"
#include "fileblocks.h"
#include "bin_direntries.h"

#include <errno.h>
#include <string.h>
#include <sys/stat.h>

namespace sofs20
{
    uint16_t grpGetDirentry(int pih, const char *name)
    {
        soProbe(201, "%s(%d, %s)\n", __FUNCTION__, pih, name);

        SOInode* inode = soGetInodePointer(pih);
        SODirentry entry[DPB];
        for(uint32_t block; block <= inode->size/BlockSize; block++)
        {
            soReadFileBlock(pih,block,entry);

            for(uint32_t j = 0;j<DPB;j++)
            {
                if(strcmp(entry[j].name,name)== 0)
                {
                    return entry[j].in;
                }
            }
        }
        return InodeNullReference;
        
    }
};

