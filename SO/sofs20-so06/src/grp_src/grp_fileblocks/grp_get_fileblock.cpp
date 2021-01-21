#include "fileblocks.h"

#include "daal.h"
#include "core.h"
#include "devtools.h"

#include <errno.h>

namespace sofs20
{
    static uint32_t grpGetIndirectFileBlock(SOInode *ip, uint32_t fbn);
    static uint32_t grpGetDoubleIndirectFileBlock(SOInode *ip, uint32_t fbn);

    uint32_t grpGetFileBlock(int ih, uint32_t fbn)
    {
        soProbe(301, "%s(%d, %u)\n", __FUNCTION__, ih, fbn);

        uint32_t i1_size = N_INDIRECT * RPB;
        uint32_t i2_size = N_DOUBLE_INDIRECT * RPB * RPB;

        if (fbn >= N_DIRECT + i1_size + i2_size)
        {
            throw SOException(EINVAL, __FUNCTION__);
        }

        SOInode *inode = soGetInodePointer(ih);

        uint32_t ref;

        if (fbn < N_DIRECT)
        {
            ref = inode->d[fbn];
        }
        else if (fbn < N_DIRECT + i1_size)
        {
            ref = grpGetIndirectFileBlock(inode, fbn - N_DIRECT);
        }
        else
        {
            ref = grpGetDoubleIndirectFileBlock(inode, fbn - N_DIRECT - i1_size);
        }
        return ref;
    }

    static uint32_t grpGetIndirectFileBlock(SOInode *ip, uint32_t afbn)
    {
        soProbe(301, "%s(%d, ...)\n", __FUNCTION__, afbn);

        uint32_t i1_idx = afbn / RPB;
        uint32_t ref_idx = afbn % RPB;

        if (ip->i1[i1_idx] == BlockNullReference)
        {
            return BlockNullReference;
        }

        uint32_t ref[RPB];
        soReadDataBlock(ip->i1[i1_idx], &ref);

        return ref[ref_idx];
    }

    static uint32_t grpGetDoubleIndirectFileBlock(SOInode *ip, uint32_t afbn)
    {
        soProbe(301, "%s(%d, ...)\n", __FUNCTION__, afbn);
        uint32_t i2_idx = afbn / (RPB * RPB);
        uint32_t ref_idx = afbn % RPB % RPB;
        uint32_t refs1_idx = (afbn / RPB) % RPB;

        if (ip->i2[i2_idx] == BlockNullReference)
        {
            return BlockNullReference;
        }

        uint32_t refs[RPB];
        soReadDataBlock(ip->i2[i2_idx], &refs);

        if (refs[refs1_idx] == BlockNullReference)
        {
            return BlockNullReference;
        }

        uint32_t ref[RPB];
        soReadDataBlock(refs[refs1_idx], &ref);

        return ref[ref_idx];
    }
}; // namespace sofs20
