using System;
using System.Collections.Generic;
using System.Text;
using MoviesAPI.Mapping;

namespace MoviesAPI.Tests.Services
{
    public static class AutoMapperConfig
    {
        private static object _thisLock = new object();
        private static bool _initialized = false;
        // Centralize automapper initialize
        public static void Initialize()
        {
            // This will ensure one thread can access to this static initialize call
            // and ensure the mapper is reseted before initialized
            lock(_thisLock)
            {
                if(!_initialized)
                {
                    AutoMapper.Mapper.Initialize(cfg => {
                        cfg.AddProfile<MovieMappingProfile>();
                    });
                    _initialized = true;
                }
            }
        }
    }
}
