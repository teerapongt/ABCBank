using System;
using System.Collections.Generic;
using API.Application.Common.Exceptions;
using FluentValidation.Results;
using Xunit;

namespace API.UnitTests.Exceptions
{
    public class ValidationExceptionTests
    {
        [Fact]
        public void DefaultConstructorCreatesAnEmptyErrorDictionary()
        {
            var actual = new ValidationException().Errors;
            Assert.Equal(Array.Empty<string>(), actual.Keys);
        }

        [Fact]
        public void SingleValidationFailureCreatesASingleElementErrorDictionary()
        {
            var failures = new List<ValidationFailure>
            {
                new("Age", "must be over 18"),
            };

            var actual = new ValidationException(failures).Errors;

            Assert.Equal(new[] {"Age"}, actual.Keys);
            Assert.Equal(new[] {"must be over 18"}, actual["Age"]);
        }

        [Fact]
        public void MultipleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
        {
            var failures = new List<ValidationFailure>
            {
                new("Age", "must be 18 or older"),
                new("Age", "must be 25 or younger"),
                new("Password", "must contain at least 8 characters"),
                new("Password", "must contain a digit"),
                new("Password", "must contain upper case letter"),
                new("Password", "must contain lower case letter"),
            };

            var actual = new ValidationException(failures).Errors;

            Assert.Equal(new[] {"Age", "Password"}, actual.Keys);

            Assert.Equal(new[] {
                "must be 18 or older",
                "must be 25 or younger"
            }, actual["Age"]);
            
            Assert.Equal(new[] {
                "must contain at least 8 characters",
                "must contain a digit",
                "must contain upper case letter",
                "must contain lower case letter"
            }, actual["Password"]);
        }
    }
}