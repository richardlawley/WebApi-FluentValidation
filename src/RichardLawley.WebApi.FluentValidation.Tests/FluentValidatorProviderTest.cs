using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Http.Dependencies;
using FluentValidation;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace RichardLawley.WebApi.FluentValidation.Tests
{
    [TestFixture]
    public class FluentValidatorProviderTest
    {
        private IDependencyScope _scope;
        private Mock<IScopedValidatorFactory> _mockFactory;
        private IFluentValidatorProvider _sut;

        public class TestModel { }

        public class TestModelValidator : AbstractValidator<TestModel> { }

        [SetUp]
        public void Setup()
        {
            _scope = new Mock<IDependencyScope>().Object;
            _mockFactory = new Mock<IScopedValidatorFactory>(MockBehavior.Strict);
            _sut = new FluentValidatorProvider(_mockFactory.Object);
        }

        [Test]
        public void GetValidators_ReturnsEmptyWhenNoValidators()
        {
            Type validatorType = typeof(IValidator<TestModel>);
            _mockFactory.Setup(x => x.GetValidator(validatorType, _scope)).Returns((IValidator)null);

            var validators = _sut.GetValidators(typeof(TestModel), _scope);

            validators.ShouldBeEmpty();
        }

        [Test]
        public void GetValidators_ReturnsValidatorWhenOneIsRegistered()
        {
            Type validatorType = typeof(IValidator<TestModel>);
            _mockFactory.Setup(x => x.GetValidator(validatorType, _scope)).Returns(() => new TestModelValidator());

            var validators = _sut.GetValidators(typeof(TestModel), _scope);

            validators.Count().ShouldBe(1);
            validators.Single().ShouldBeOfType<TestModelValidator>();
        }

        [Test]
        public void GetValidators_CachesThatNoValidatorExists()
        {
            Type validatorType = typeof(IValidator<TestModel>);
            _mockFactory.Setup(x => x.GetValidator(validatorType, _scope)).Returns((IValidator)null);

            var validators = _sut.GetValidators(typeof(TestModel), _scope);
            var validators2 = _sut.GetValidators(typeof(TestModel), _scope);

            _mockFactory.Verify(x => x.GetValidator(validatorType, _scope), Times.Once(), "Cache didn't prevent second lookup");
        }
    }
}