#include "direntries.h"

#include "core.h"
#include "devtools.h"
#include "daal.h"
#include "fileblocks.h"
#include "direntries.h"
#include "bin_direntries.h"

#include <errno.h>
#include <string.h>
#include <libgen.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/stat.h>

namespace sofs20
{
    uint16_t grpTraversePath(char *path, char *current_path, uint16_t inodeh)
    {

        if (inodeh == InodeNullReference)
        {
            throw SOException(ENOTDIR, __FUNCTION__);
        }

        if (!strcmp(path, current_path))
        {
            return inodeh;
        }

        char *tmp_path = strdupa(path);
        char *next_dir = strdupa(path);
        while (strcmp(dirname(tmp_path), current_path))
        {
            next_dir = strdupa(tmp_path);
        }

        SOInode *inode = soGetInodePointer(inodeh);

        if (!S_ISDIR(inode->mode))
        {
            throw SOException(EACCES, __FUNCTION__);
        }

        if (!((inode->owner == getuid() || inode->group == getgid()) && soCheckInodeAccess(inodeh, X_OK)))
        {
            throw SOException(EACCES, __FUNCTION__);
        }

        uint16_t next_inoden = soGetDirentry(inodeh, basename(next_dir));
        if (next_inoden == InodeNullReference)
        {
            throw SOException(ENOTDIR, __FUNCTION__);
        }
        uint16_t next_inodeh = soOpenInode(next_inoden);

        return grpTraversePath(path, next_dir, next_inodeh);
    }

    uint16_t grpTraversePath(char *path)
    {
        soProbe(221, "%s(%s)\n", __FUNCTION__, path);

        if ((path[0] != '/'))
        {
            throw SOException(ENOTDIR, __FUNCTION__);
        }

        uint16_t inodeh = soOpenInode(0);
        return grpTraversePath(path, (char *)"/", inodeh);
    }
} // namespace sofs20