using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using API.Application.Account.Commands.Create;
using API.Application.Account.Commands.Deposit;
using API.Application.Account.Queries.GetByIBAN;
using API.Application.Common.Mappings;
using AutoMapper;
using Xunit;

namespace API.UnitTests.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(Domain.Entities.Account), typeof(CreateDto))]
        [InlineData(typeof(Domain.Entities.Account), typeof(DepositDto))]
        [InlineData(typeof(Domain.Entities.Account), typeof(GetByIBANDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);

            _mapper.Map(instance, source, destination);
        }

        private object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type);

            // Type without parameterless constructor
            return FormatterServices.GetUninitializedObject(type);
        }
    }
}