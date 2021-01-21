#include "direntries.h"

#include "core.h"
#include "devtools.h"
#include "daal.h"
#include "fileblocks.h"
#include "bin_direntries.h"

#include <errno.h>
#include <string.h>
#include <sys/stat.h>
#include <stdio.h>
#include <math.h> 

namespace sofs20{
    uint16_t grpDeleteDirentry(int pih, const char *name){
        soProbe(203, "%s(%d, %s)\n", __FUNCTION__, pih, name);

        SOInode *in = soGetInodePointer(pih);
        uint32_t targetDirIdx = 0;
        uint32_t targetDirBlk = 0;
        bool found = false;
        uint32_t blockNumber = (uint32_t)ceil((double)in->size/(double)BlockSize);

        for (uint32_t i = 0; i <= blockNumber; i++){
            SODirentry en[DPB];
            soReadFileBlock(pih, i, en);
            for (uint32_t k = 0; k < DPB; k++){
                if (!strcmp(en[k].name, name)){
                    targetDirBlk = i;
                    targetDirIdx = k;
                    found = true;
                }
                if (!strcmp(en[k].name, "\0") && found){ 
                    if (i != targetDirBlk){ 
                        SODirentry enPrev[DPB];
                        soReadFileBlock(pih, targetDirBlk, enPrev);
                        enPrev[targetDirIdx] = en[k-1];
                        soWriteFileBlock(pih, targetDirBlk, enPrev);
                    }else{
                        en[targetDirIdx] = en[k-1];
                    }
                    uint16_t iNumber = en[k-1].in;
                    for (int p = 0; p < SOFS20_FILENAME_LEN + 1; p++){
                        en[k-1].name[p] = '\0';
                    }
                    en[k-1].in = (uint16_t)0;
                    in->size -= sizeof(SODirentry);
                    soWriteFileBlock(pih, i, en);

                    if((in->size%BlockSize) == 0){
                        soFreeFileBlocks(pih,blockNumber - 1);
                    }
                    return iNumber;
                }
            }
            if ((i == (blockNumber) - 1) && found){
                if (i != targetDirBlk){ 
                    SODirentry enPrev[DPB];
                    soReadFileBlock(pih, targetDirBlk, enPrev);
                    enPrev[targetDirIdx] = en[DPB-1];
                    soWriteFileBlock(pih, targetDirBlk, enPrev);
                }else{
                    en[targetDirIdx] = en[DPB-1];
                }
                uint16_t iNumber = en[DPB-1].in;
                for (int p = 0; p < SOFS20_FILENAME_LEN + 1; p++){
                    en[DPB-1].name[p] = '\0';
                }
                en[DPB-1].in = (uint16_t)0;
                in->size -= sizeof(SODirentry);
                soWriteFileBlock(pih, i, en);
                return iNumber;
            }
        }
        throw SOException(ENOENT, __FUNCTION__);
    }
}; 