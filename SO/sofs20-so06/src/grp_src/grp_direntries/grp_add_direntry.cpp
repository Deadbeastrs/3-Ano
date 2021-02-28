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
    void grpAddDirentry(int pih, const char *name, uint16_t cin)
    {
        soProbe(202, "%s(%d, %s, %u)\n", __FUNCTION__, pih, name, cin);
        
        SOInode* in = soGetInodePointer(pih);
            
        SODirentry dir[DPB];
        soReadFileBlock(pih,0,dir);
        const char *voidString = "\0";
        if(in->size % BlockSize == 0) {
            SODirentry buf[DPB];
            memset(buf,0,BlockSize);
            uint32_t blk = soAllocFileBlock(pih, in->size / BlockSize);
            soWriteDataBlock(blk, buf);
        }
        for(uint32_t block = 0; block < in->blkcnt; block++)
        {
            soReadFileBlock(pih,block,dir);
            for(uint32_t i=0;i<DPB;i++) {
                if(strcmp(dir[i].name, voidString)== 0) {
                    strcpy(dir[i].name, name);
                    dir[i].in = cin;
                    soWriteFileBlock(pih, block, dir);
                    in->size += sizeof(SODirentry);
                    soSaveInode(pih);
                    return;
                }
            }
        }
        
        
        /* replace the following line with your code */
        //binAddDirentry(pih, name, cin);
    }
};

