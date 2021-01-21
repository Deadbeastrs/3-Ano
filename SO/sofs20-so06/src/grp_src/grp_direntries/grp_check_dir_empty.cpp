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
    bool grpCheckDirEmpty(int ih)
    {
        soProbe(205, "%s(%d)\n", __FUNCTION__, ih);

        /* replace the following line with your code */
        
        SOInode* inode = soGetInodePointer(ih);
        SODirentry entry[DPB];
        if(inode->size != 128)
        {
            return false;
        }
        else
        {
            soReadFileBlock(ih,0,entry);
            if(strcmp(entry[0].name,".")== 0 and strcmp(entry[1].name,"..") == 0)
            {
                return true;
            }
        }
        return false;
    }
};

