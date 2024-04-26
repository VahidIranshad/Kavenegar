using System.Runtime.Serialization;
using AutoMapper;
using Kavenegar.Application.Dto.Entity.BlogDtos;
using Kavenegar.Application.Profiles;
using Kavenegar.Domain.Entity;

namespace UnitTest.Mapping
{
    public class MappingTest
    {

        private static IConfigurationProvider _configuration;
        private static IConfigurationProvider configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile<MappingProfile>();
                    });
                }
                return _configuration;
            }
        }
        private static IMapper _mapper;
        private static IMapper mapper
        {
            get
            {
                if (_mapper == null)
                {
                    _mapper = configuration.CreateMapper();
                }
                return _mapper;
            }

        }
        [Theory]
        [InlineData(typeof(BLog), typeof(BlogDto))]
        [InlineData(typeof(BlogDto), typeof(BLog))]
        [InlineData(typeof(BLog), typeof(BlogCrudDto))]
        [InlineData(typeof(BlogCrudDto), typeof(BLog))]
        public void Map_SourceToDestination_ExistConfiguration(Type origin, Type destination)
        {
            var instance = FormatterServices.GetUninitializedObject(origin);

            mapper.Map(instance, origin, destination);
        }
    }
}
