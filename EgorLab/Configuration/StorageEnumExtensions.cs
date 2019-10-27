using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgorLab.Configuration
{
    public static class StorageEnumExtensions
    {
        public static StorageEnum ToStorageEnum(this string value)
        {
            switch (value)
            {
                case var s when s.ToLowerInvariant() == "memcache"
                                || s.ToLowerInvariant() == "cache"
                                || s.ToLowerInvariant() == "mem":
                    return StorageEnum.MemCache;
                case var s when s.ToLowerInvariant() == "filestorage"
                                || s.ToLowerInvariant() == "file"
                                || s.ToLowerInvariant() == "storage":
                    return StorageEnum.FileStorage;
                default:
                    return StorageEnum.Undefined;
            }
        }
    }
}
