#include "grp_mksofs.h"

#include "rawdisk.h"
#include "core.h"
#include "devtools.h"
#include "bin_mksofs.h"

#include <string.h>
#include <time.h>
#include <unistd.h>
#include <sys/stat.h>
#include <inttypes.h>

namespace sofs20
{
    void grpFillInodeTable(uint32_t itotal, bool date)
    {
        soProbe(604, "%s(%u)\n", __FUNCTION__, itotal);

        SOInode inode0 = SOInode();
        inode0.mode = S_IFDIR | 755;
        inode0.lnkcnt = 2;
        inode0.owner = getuid();
        inode0.group = getgid();
        inode0.size = sizeof(SODirentry) * 2;
        inode0.blkcnt = 1;
        inode0.atime = date ? time(0) : 0;
        inode0.mtime = date ? time(0) : 0;
        inode0.ctime = date ? time(0) : 0;
        inode0.d[N_DIRECT] = {0};
        inode0.d[0] = 0;
        inode0.d[1] = BlockNullReference;
        inode0.d[2] = BlockNullReference;
        inode0.d[3] = BlockNullReference;
        inode0.i1[N_INDIRECT] = {};
        inode0.i1[0] = BlockNullReference;
        inode0.i1[1] = BlockNullReference;
        inode0.i1[2] = BlockNullReference;
        inode0.i2[N_DOUBLE_INDIRECT] = {};
        inode0.i2[0] = BlockNullReference;

        SOInode inodes[itotal];
        inodes[0] = inode0;
        for (uint32_t i = 1; i < itotal; i++)
        {
            SOInode inode = SOInode();
            inode.mode = 0;
            inode.lnkcnt = 0;
            inode.owner = 0;
            inode.group = 0;
            inode.size = 0;
            inode.blkcnt = 0;
            inode.atime = 0;
            inode.mtime = 0;
            inode.ctime = 0;
            inode.d[N_DIRECT] = {};
            inode.d[0] = BlockNullReference;
            inode.d[1] = BlockNullReference;
            inode.d[2] = BlockNullReference;
            inode.d[3] = BlockNullReference;
            inode.i1[N_INDIRECT] = {};
            inode.i1[0] = BlockNullReference;
            inode.i1[1] = BlockNullReference;
            inode.i1[2] = BlockNullReference;
            inode.i2[N_DOUBLE_INDIRECT] = {};
            inode.i2[0] = BlockNullReference;
            inodes[i] = inode;
        }
        soWriteRawBlock(1, (void *)&inodes);
    }
}; // namespace sofs20
