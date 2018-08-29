using AutoMapper;
using RestaurantSystem.Data;
using System;

namespace RestaurantSystem.Services
{
    public class BaseEFService
    {
        protected BaseEFService(
            RMSContext dbContext,
            IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        protected RMSContext DbContext { get; private set; }

        protected IMapper Mapper { get; private set; }
    }
}
