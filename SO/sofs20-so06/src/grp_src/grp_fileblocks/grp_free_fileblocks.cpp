#include "fileblocks.h"

#include "freedatablocks.h"
#include "daal.h"
#include "core.h"
#include "devtools.h"

#include <inttypes.h>
#include <errno.h>
#include <assert.h>

namespace sofs20
{
//#if false
    /* free all blocks between positions ffbn and RPB - 1
     * existing in the block of references given by i1.
     * Return true if, after the operation, all references become BlockNullReference.
     * It assumes i1 is valid.
     */
    static bool grpFreeIndirectFileBlocks(SOInode * ip, uint32_t i1, uint32_t ffbn);

    /* free all blocks between positions ffbn and RPB**2 - 1
     * existing in the block of indirect references given by i2.
     * Return true if, after the operation, all references become BlockNullReference.
     * It assumes i2 is valid.
     */
    static bool grpFreeDoubleIndirectFileBlocks(SOInode * ip, uint32_t i2, uint32_t ffbn);
//#endif

    /* ********************************************************* */

    void grpFreeFileBlocks(int ih, uint32_t ffbn)
    {
        soProbe(303, "%s(%d, %u)\n", __FUNCTION__, ih, ffbn);

        uint32_t i1_size = N_INDIRECT * RPB;
        uint32_t i2_size = N_DOUBLE_INDIRECT * RPB * RPB;
        SOInode* inode =  soGetInodePointer(ih);
        
        if (ffbn < N_DIRECT) 
        {
            for(uint32_t i = ffbn; i < 4;i++)
            {
                uint32_t toBeFree = inode->d[i];
                if(toBeFree != BlockNullReference)
                {
                    soFreeDataBlock(toBeFree);
                    inode->blkcnt -= 1;
                    inode->d[i] = BlockNullReference;
                }
                ffbn++;
            }
        }
        if(ffbn < N_DIRECT + i1_size) {
            
            uint32_t blk = (ffbn - N_DIRECT) / RPB;
            for(uint32_t i = blk; i < 3;i++)
            {
                uint32_t refBlock = inode->i1[i];
                if(refBlock != BlockNullReference)
                {
                    if(grpFreeIndirectFileBlocks(inode,refBlock,ffbn))
                    {
                        soFreeDataBlock(inode->i1[i]);
                        inode->i1[i] = BlockNullReference;
                        inode->blkcnt -= 1;
                    }          
                }
                if(i == blk)
                {
                    ffbn += 260-(ffbn % 260);
                }
                else
                {
                    ffbn += 256;
                }
                
            }
        }
        if(ffbn < N_DIRECT + i1_size + i2_size)
        {
            if(inode->i2[0] != BlockNullReference)
            {
                if(grpFreeDoubleIndirectFileBlocks(inode,inode->i2[0],ffbn))
                {
                    soFreeDataBlock(inode->i2[0]);
                    inode->i2[0] = BlockNullReference;
                    inode->blkcnt -= 1;
                }
                
            }
            
        }
            
        

    }

    /* ********************************************************* */

//#if false
    static bool grpFreeIndirectFileBlocks(SOInode * ip, uint32_t i1, uint32_t ffbn)
    {
        soProbe(303, "%s(..., %u, %u)\n", __FUNCTION__, i1, ffbn);

        uint32_t readBlock[RPB];
        soReadDataBlock(i1,(void *) readBlock);
        
        uint32_t blkIdx = (ffbn - N_DIRECT) % RPB;
        int flag = 0;
        for(uint32_t i = 0; i < RPB;i++)
        {
            if(i >= blkIdx)
            {
                if(readBlock[i] != BlockNullReference)
                {
                    ip->blkcnt -= 1;
                    soFreeDataBlock(readBlock[i]);
                    readBlock[i] = BlockNullReference;
                }
                
            }
            else if(readBlock[i] != BlockNullReference)
            {
                flag = 1;
            }  
        }
        soWriteDataBlock(i1,(void *) readBlock);
        if(flag)
        { 
            return false;
        } 
        return true;   
        
    }
//#endif

    /* ********************************************************* */

//#if false
    static bool grpFreeDoubleIndirectFileBlocks(SOInode * ip, uint32_t i2, uint32_t ffbn)
    {
        soProbe(303, "%s(..., %u, %u)\n", __FUNCTION__, i2, ffbn);
        uint32_t readBlock[RPB];
        int flag = 0;
        int flag2 = 0;
        soReadDataBlock(i2,(void *) readBlock);
        uint32_t blk = (ffbn - N_DIRECT-N_INDIRECT*RPB) / RPB;
        uint32_t blkIdx = (ffbn - N_DIRECT-N_INDIRECT*RPB) % RPB;
        for(uint32_t i = 0; i< RPB; i++)
        {
            if(i < blk)
            {
                if(readBlock[i]!=BlockNullReference)
                {
                    flag2 =1;
                }
            }
            else
            {
                if(readBlock[i] != BlockNullReference)
                {
                    uint32_t readBlock2[RPB];
                    
                    soReadDataBlock(readBlock[i],(void *) readBlock2);
                    for(uint32_t j = 0;j < RPB;j++)
                    {
                        if(j >= blkIdx)
                        {
                            if(readBlock2[j] != BlockNullReference)
                            {
                                
                                ip->blkcnt -= 1;
                                soFreeDataBlock(readBlock2[j]);
                                readBlock2[j] = BlockNullReference;
                                
                            }
                        
                        }
                        else
                        {
                            if(readBlock2[j] != BlockNullReference)
                            {
                                flag = 1;
                            }
                            
                        }
                        
                    }
                    soWriteDataBlock(readBlock[i],(void *) readBlock2);
                    if(!flag)
                    {
                        soFreeDataBlock(readBlock[i]);
                        readBlock[i]=BlockNullReference;
                        ip->blkcnt -= 1;
                    }
                    flag = 0;
                    blkIdx = 0;
                    soWriteDataBlock(readBlock[i],(void *) readBlock2);
                } 
            }
        }
        soWriteDataBlock(i2,(void *) readBlock);
        if(!flag2)
        {
            return true;
        }
        return false;
        /* replace the following line with your code */ 
    }
//#endif

    /* ********************************************************* */
};

