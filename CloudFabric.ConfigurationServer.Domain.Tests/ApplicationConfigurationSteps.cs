using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans.TestingHost;
using System;
using TechTalk.SpecFlow;

namespace CloudFabric.ConfigurationServer.Domain.Tests
{

    public class TestContext
    {
        public ApplicationConfigurationDocument ApplicationConfiguration { get; set; }
        public object Result { get; set; }
    }

    [Binding]
    public class ApplicationConfigurationSteps
    {
        private readonly TestContext Context;
        public ApplicationConfigurationSteps(TestContext context)
        {
            Context = context;
        }


        [Given(@"a new instance of an application is created")]
        public void GivenANewInstanceOfAnApplicationIsCreated()
        {
            Context.ApplicationConfiguration = new ApplicationConfigurationDocument(new ApplicationName("Test"));
        }


        [When(@"get the number of properties")]
        public void WhenGetTheNumberOfProperties()
        {
            Context.Result = Context.ApplicationConfiguration.GetNumberOfProperties();
        }
        
        [Then(@"the result should be ([0-9]*)")]
        public void ThenTheResultShouldBe(int result)
        {
            Assert.AreEqual((int)Context.Result, result);
        }
    }
}
