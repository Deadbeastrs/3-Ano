#include "fileblocks.h"

#include "freedatablocks.h"
#include "daal.h"
#include "core.h"
#include "devtools.h"

#include <errno.h>

#include <iostream>

namespace sofs20
{

//#if false
    static uint32_t grpAllocIndirectFileBlock(SOInode * ip, uint32_t afbn);
    static uint32_t grpAllocDoubleIndirectFileBlock(SOInode * ip, uint32_t afbn);
//#endif

    /* ********************************************************* */

    uint32_t grpAllocFileBlock(int ih, uint32_t fbn)
    {
        soProbe(302, "%s(%d, %u)\n", __FUNCTION__, ih, fbn);

        uint32_t i1_size = N_INDIRECT * RPB;
        uint32_t i2_size = N_DOUBLE_INDIRECT * RPB * RPB;
        SOInode* inode =  soGetInodePointer(ih);
        uint32_t blk;
        if (fbn >= N_DIRECT + i1_size + i2_size) {
            throw SOException(EINVAL, __FUNCTION__);
        }

        if (fbn < N_DIRECT) {            
            blk = soAllocDataBlock();
            inode->blkcnt += 1;
            inode->d[fbn] = blk;
        }
        else if(fbn < N_DIRECT + i1_size) {
            blk = grpAllocIndirectFileBlock(inode,fbn);
        }
        //fbn < N_DIRECT + i1_size + i2_size
        else {
            blk = grpAllocDoubleIndirectFileBlock(inode,fbn);
        }
        return blk;

    }

    /* ********************************************************* */

//#if false
    /*
    */
    static uint32_t grpAllocIndirectFileBlock(SOInode * ip, uint32_t afbn)
    {
        soProbe(302, "%s(%d, ...)\n", __FUNCTION__, afbn);
        
        uint32_t blk = (afbn - N_DIRECT) / RPB;
        uint32_t blkIdx = (afbn - N_DIRECT) % RPB;
        uint32_t cleanBlock[RPB];
        uint32_t readBlock[RPB];
        uint32_t newBlkNum;
        
        if(ip->i1[blk] == BlockNullReference){
            ip->blkcnt += 1;
            ip->i1[blk] = soAllocDataBlock();
            for (uint32_t i = 0; i < RPB; i++) {
                cleanBlock[i] = BlockNullReference;
            }
            soWriteDataBlock(ip->i1[blk],(void *) cleanBlock);
        }
        soReadDataBlock(ip->i1[blk],(void *) readBlock);
        newBlkNum = soAllocDataBlock();
        ip->blkcnt += 1;
        readBlock[blkIdx] = newBlkNum;
        soWriteDataBlock(ip->i1[blk],(void *) readBlock);
        return newBlkNum;
    }
//#endif

    /* ********************************************************* */

//#if false
    /*
    */
    static uint32_t grpAllocDoubleIndirectFileBlock(SOInode * ip, uint32_t afbn)
    {
        soProbe(302, "%s(%d, ...)\n", __FUNCTION__, afbn);
        
        uint32_t i1_size = N_INDIRECT * RPB;
        uint32_t blk = (afbn - N_DIRECT - (i1_size)) / RPB;
        uint32_t indBlk = (afbn - N_DIRECT - (i1_size)) % RPB;
        uint32_t i2cleanBlock[RPB];
        uint32_t i2readBlock[RPB];
        uint32_t i1readBlock[RPB];

        if(ip->i2[0] == BlockNullReference){
            ip->blkcnt += 1;
            ip->i2[0] = soAllocDataBlock();
            for (uint32_t i = 0; i < RPB; i++) {
                i2cleanBlock[i] = BlockNullReference;
            }
            soWriteDataBlock(ip->i2[0],(void *) i2cleanBlock); //Write Clean i2 Block
        }
        soReadDataBlock(ip->i2[0],(void *) i2readBlock); 
        if(i2readBlock[blk] == BlockNullReference){
            uint32_t cleanBlock[RPB];
            ip->blkcnt += 1;
            i2readBlock[blk] = soAllocDataBlock();
            for (uint32_t i = 0; i < RPB; i++) {
                cleanBlock[i] = BlockNullReference;
            }
            soWriteDataBlock(i2readBlock[blk],(void *) cleanBlock); //Write Clean i1 Block
            soWriteDataBlock(ip->i2[0],(void *) i2readBlock); //Write changed i2 Block
        }
        soReadDataBlock(i2readBlock[blk],(void *) i1readBlock);
        uint32_t newBlkNum = soAllocDataBlock();
        ip->blkcnt += 1;
        i1readBlock[indBlk] = newBlkNum;
        soWriteDataBlock(i2readBlock[blk],(void *) i1readBlock); //Write changed i1 Block
        return newBlkNum;
    }
//#endif
};

