/*
 *  \author António Rui Borges - 2012-2015
 *  \authur Artur Pereira - 2016-2020
 */

#include "freeinodes.h"

#include <stdio.h>
#include <errno.h>
#include <inttypes.h>
#include <time.h>
#include <unistd.h>
#include <sys/types.h>

#include "core.h"
#include "devtools.h"
#include "daal.h"

#include <math.h>

namespace sofs20
{
    void grpFreeInode(uint16_t in)
    {
        soProbe(402, "%s(%u)\n", __FUNCTION__, in);
        
        // precondition
        soOpenSuperblock();
        SOSuperblock* sb = soGetSuperblockPointer();
        if(!(in > 0 && in < sb->itotal)) throw SOException(EINVAL, __FUNCTION__);

        // increase free inodes in the superblock meta data
        sb->ifree = sb->ifree+1;

        // insert in the list of free inodes
        int pos = in/32;
        sb->ibitmap[pos] = (uint32_t)sb->ibitmap[pos] & (BlockNullReference - (uint32_t)pow(2, (in % 32)));
        soSaveSuperblock();

        // clean inode
        int inode_location = soOpenInode(in);
        SOInode* inode = soGetInodePointer(inode_location);   
        inode->lnkcnt = 0;
        inode->owner = 0;
        inode->group = 0;
        inode->size = 0;
        inode->blkcnt = 0;
        inode->atime = 0;
        inode->mtime = 0;
        inode->ctime = 0;
        inode->d[N_DIRECT] = {0};
        inode->d[0] = BlockNullReference;
        inode->d[1] = BlockNullReference;
        inode->d[2] = BlockNullReference;
        inode->d[3] = BlockNullReference;
        inode->i1[N_INDIRECT] = {};
        inode->i1[0] = BlockNullReference;
        inode->i1[1] = BlockNullReference;
        inode->i1[2] = BlockNullReference;
        inode->i2[N_DOUBLE_INDIRECT] = {};
        inode->i2[0] = BlockNullReference;
        soSaveInode(inode_location);
        soCloseInode(inode_location);
    }
};

