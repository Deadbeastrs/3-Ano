#include "direntries.h"

#include "core.h"
#include "devtools.h"
#include "daal.h"
#include "fileblocks.h"
#include "bin_direntries.h"
#include <string.h>
#include <errno.h>
#include <sys/stat.h>
#include <math.h>
namespace sofs20
{
    void grpRenameDirentry(int pih, const char *name, const char *newName)
    {
        soProbe(204, "%s(%d, %s, %s)\n", __FUNCTION__, pih, name, newName);

        /* replace the following line with your code */
        SOInode* inode = soGetInodePointer(pih);
        SODirentry entry[DPB];
        uint32_t x = 0;
        uint32_t y = 0;
        uint32_t blockNumber = (uint32_t)ceil((double)inode->size/(double)BlockSize);
        for(uint32_t block = 0; block <= blockNumber; block++)
        {
            soReadFileBlock(pih,block,entry);
            for(uint32_t j = 0;j<DPB;j++)
            {
                if(strcmp(entry[j].name,name) == 0)
                {
                    x = block;
                    y = j;
                    soReadFileBlock(pih,x,entry);
                    for(int i = 0;i<62;i++)
                    {
                        entry[y].name[i]='\0';
                    }
                    strcpy(entry[y].name,newName);
                    soWriteFileBlock(pih,x,entry);
                }  
            }
        }
        
    }
};

