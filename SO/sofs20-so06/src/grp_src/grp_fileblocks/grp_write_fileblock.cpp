#include "fileblocks.h"

#include "daal.h"
#include "core.h"
#include "devtools.h"

#include <string.h>
#include <inttypes.h>

#include <math.h>

namespace sofs20
{
    void grpWriteFileBlock(int ih, uint32_t fbn, void *buf)
    {
        soProbe(332, "%s(%d, %u, %p)\n", __FUNCTION__, ih, fbn, buf);

        SOInode *inode = soGetInodePointer(ih);

        if (fbn > inode->blkcnt || fbn < 0)
        {
            throw SOException(EINVAL, __FUNCTION__);
        }

        uint32_t file_block = soGetFileBlock(ih, fbn);

        if (file_block == BlockNullReference)
        {
            file_block = soAllocFileBlock(ih, fbn);
        }

        soWriteDataBlock(file_block, buf);
    }
}; // namespace sofs20
