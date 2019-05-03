using CloudFabric.ConfigurationServer.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orleans.TestingHost;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

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

        [Given(@"the following application named: '(.*)' and the configuration properties")]
        public void GivenTheFollowingApplicationNamedAndTheConfigurationProperties(string name, IEnumerable<ConfigurationProperty> properties)
        {
            var applicationName = new ApplicationName(name);
            Context.ApplicationConfiguration = new ApplicationConfigurationDocument(applicationName, properties: properties.ToList());
        }

        [Given(@"the following environment named: '(.*)' and the configuration properties")]
        public void GivenTheFollowingEnvironmentNamedAndTheConfigurationProperties(string name, IEnumerable<ConfigurationProperty> properties)
        {
            var environmentName = new EnvironmentName(name);
            Context.ApplicationConfiguration.AddEnvironment(new EnvironmentConfigurationDocument(environmentName, properties.ToList()));
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

        [When(@"I add a new property Name: '(.*)' Value: '(.*)'")]
        public void WhenIAddANewPropertyNameValue(string name, string value)
        {
            Context.ApplicationConfiguration.AddProperty(new ConfigurationProperty(name, value));
        }

        [Then(@"the result should be ([0-9]*)")]
        public void ThenTheResultShouldBe(int result)
        {
            Assert.AreEqual((int)Context.Result, result);
        }

        

        [Then(@"the property Name: '(.*)' Value: '(.*)' should exist")]
        public void ThenThePropertyNameValueShouldExist(string name, string value)
        {
            Assert.AreEqual(Context.ApplicationConfiguration.GetProperty(name)?.Value, value);
        }

        [Then(@"the application with the following environment '(.*)' has the following properties")]
        public void ThenTheApplicationWithTheFollowingEnvironmentHasTheFollowingProperties(string envName, IEnumerable<ConfigurationProperty> properties)
        {

        }


        //[StepArgumentTransformation]
        //public static EnvironmentName Convert(string name) => new EnvironmentName(name);
        //[StepArgumentTransformation]
        //public static ApplicationName Conver(string name) => new ApplicationName(name);
        [StepArgumentTransformation]
        public static IEnumerable<ConfigurationProperty> Convert(Table table) => table.CreateSet<ConfigurationProperty>();

    }
}
