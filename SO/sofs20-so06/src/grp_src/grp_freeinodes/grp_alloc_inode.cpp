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
#include <sys/stat.h>
#include <string.h>

#include <iostream>

#include "core.h"
#include "devtools.h"
#include "daal.h"
#include <math.h>

//#include <assert.h>

namespace sofs20
{
    uint16_t grpAllocInode(uint32_t mode)
    {
        soProbe(401, "%s(0x%x)\n", __FUNCTION__, mode);

        uint32_t perm = mode & 0777;
        uint32_t type = mode & ~0777;

        //       PRE CONDITIONS
        if(perm < 0 || perm > 511){
            throw SOException(EINVAL, __FUNCTION__);
        }
        if(type != S_IFREG && type != S_IFDIR && type != S_IFLNK){
            throw SOException(EINVAL, __FUNCTION__);
        }
        //--------------------------
        SOSuperblock* sb = soGetSuperblockPointer();

        int ind; //Index of bitmap array
        int bitpos; //Bit position in bitmap section
        uint32_t counter = 0;
        do{
            sb->iidx = (sb->iidx + 1)%sb->itotal;
            ind = sb->iidx / 32;
            bitpos = (uint32_t) pow(2,sb->iidx % 32);
            counter++;
            if(counter == sb->itotal){
                throw SOException(ENOSPC, __FUNCTION__);
            }
        }while((sb->ibitmap[ind] & (bitpos)) > 0 || counter >= sb->itotal);
        sb->ibitmap[ind] = sb->ibitmap[ind] | bitpos;
        int ih = soOpenInode(sb->iidx);
        SOInode* inode = soGetInodePointer(ih);
        inode->mode = mode;
        inode->owner = getuid();
        inode->group = getgid();
        inode->atime = time(0);
        inode->mtime = time(0);
        inode->ctime = time(0);
        soSaveInode(ih);
        //soCloseInode(ih);
        return sb->iidx;
    }
};
