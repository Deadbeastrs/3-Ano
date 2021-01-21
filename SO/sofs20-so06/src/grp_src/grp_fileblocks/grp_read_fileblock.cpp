#include "fileblocks.h"

#include "daal.h"
#include "core.h"
#include "devtools.h"

#include <string.h>
#include <inttypes.h>

#include <math.h>
#include <sys/stat.h>

namespace sofs20
{
    void grpReadFileBlock(int ih, uint32_t fbn, void *buf)
    {
        soProbe(331, "%s(%d, %u, %p)\n", __FUNCTION__, ih, fbn, buf);

        uint32_t block = soGetFileBlock(ih, fbn);
        if (block == BlockNullReference)
        {
            char padding[BlockSize] = {'\0'};
            buf = &padding;
        }
        else
        {
            soReadDataBlock(block, buf);
        }
    }
}; // namespace sofs20
