﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.WindowsAzure.Management.ScenarioTest.StoreTests
{
    using System.Collections.Generic;
    using System.Management.Automation.Runspaces;
    using Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.WindowsAzure.Management.ScenarioTest.Common.CustomPowerShell;
    using Microsoft.WindowsAzure.Management.Store.Properties;

    [TestClass]
    public class StoreTests : WindowsAzurePowerShellTest
    {
        public static string StoreCredentialFile = "store.publishsettings";

        public static string StoreSubscriptionName = "Store";

        private CustomHost customHost;

        public StoreTests()
            : base(
            "Store\\Common.ps1",
            "Store\\StoreTests.ps1")
        {
            customHost = new CustomHost();
        }

        [TestInitialize]
        public override void TestSetup()
        {
            base.TestSetup();
            customHost = new CustomHost();
            powershell.ImportCredentials(StoreCredentialFile);
            powershell.AddScript(string.Format("Set-AzureSubscription -Default {0}", StoreSubscriptionName));
        }

        private void PromptSetup()
        {
            PromptSetup(
                new List<int>(),
                new List<int>(),
                new List<string>(),
                new List<string>(),
                PromptAnswer.Yes);
        }

        private void PromptSetup(
            List<int> expectedDefaultChoices,
            List<int> promptChoices,
            List<string> expectedPromptMessages,
            List<string> expectedPromptCaptions,
            PromptAnswer defaultAnswer)
        {
            customHost.CustomUI.PromptChoices = promptChoices;
            customHost.CustomUI.ExpectedDefaultChoices = expectedDefaultChoices;
            customHost.CustomUI.ExpectedPromptMessages = expectedPromptMessages;
            customHost.CustomUI.ExpectedPromptCaptions = expectedPromptCaptions;
            customHost.CustomUI.DefaultAnswer = defaultAnswer;
            powershell.Runspace = RunspaceFactory.CreateRunspace(customHost);
            powershell.Runspace.Open();
            powershell.SetVariable(
                "freeAddOnIds",
                new string[] { "activecloudmonitoring", "sendgrid_azure" });
        }

        #region Get-AzureStoreAddOn Scenario Tests

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.Store)]
        public void TestGetAzureStoreAddOnListAvailableWithInvalidCredentials()
        {
            RunPowerShellTest("Test-WithInvalidCredentials { Get-AzureStoreAddOn -ListAvailable }");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.Store)]
        public void TestGetAzureStoreAddOnListAvailableWithDefaultCountry()
        {
            RunPowerShellTest("Test-GetAzureStoreAddOnListAvailableWithDefaultCountry");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.Store)]
        public void TestGetAzureStoreAddOnListAvailableWithNoAddOns()
        {
            RunPowerShellTest("Test-GetAzureStoreAddOnListAvailableWithNoAddOns");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.Store)]
        public void TestGetAzureStoreAddOnListAvailableWithCountry()
        {
            RunPowerShellTest("Test-GetAzureStoreAddOnListAvailableWithCountry");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.Store)]
        public void TestGetAzureStoreAddOnListAvailableWithInvalidCountryName()
        {
            RunPowerShellTest("Test-GetAzureStoreAddOnListAvailableWithInvalidCountryName");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.Store)]
        public void TestGetAzureStoreAddOnWithInvalidCredentials()
        {
            RunPowerShellTest("Test-WithInvalidCredentials { Get-AzureStoreAddOn Name }");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.Store)]
        public void TestGetAzureStoreAddOnWithNoAddOns()
        {
            RunPowerShellTest("Test-GetAzureStoreAddOnEmpty");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.Store)]
        public void TestGetAzureStoreAddOnWithOneAddOn()
        {
            PromptSetup();
            RunPowerShellTest(
                "Test-GetAzureStoreAddOnWithOneAddOn",
                "AddOn-TestCleanup");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.Store)]
        public void TestGetAzureStoreAddOnWithMultipleAddOns()
        {
            PromptSetup();
            RunPowerShellTest(
                "AddOn-TestCleanup",
                "Test-GetAzureStoreAddOnWithMultipleAddOns",
                "AddOn-TestCleanup");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.Store)]
        public void TestGetAzureStoreAddOnWithExistingAddOn()
        {
            PromptSetup();
            RunPowerShellTest(
                "Test-GetAzureStoreAddOnWithExistingAddOn",
                "AddOn-TestCleanup");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.Store)]
        public void TestGetAzureStoreAddOnCaseInsinsitive()
        {
            PromptSetup();
            RunPowerShellTest(
                "Test-GetAzureStoreAddOnCaseInsinsitive",
                "AddOn-TestCleanup");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.Store)]
        public void TestGetAzureStoreAddOnWithInvalidName()
        {
            RunPowerShellTest("Test-GetAzureStoreAddOnWithInvalidName");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.Store)]
        public void TestGetAzureStoreAddOnValidNonExisting()
        {
            RunPowerShellTest("Test-GetAzureStoreAddOnValidNonExisting");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.Store)]
        public void TestGetAzureStoreAddOnWithAppService()
        {
            PromptSetup();
            RunPowerShellTest("Test-GetAzureStoreAddOnWithAppService");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.Store)]
        public void TestGetAzureStoreAddOnPipedToRemoveAzureAddOn()
        {
            PromptSetup();
            RunPowerShellTest(
                "Test-GetAzureStoreAddOnPipedToRemoveAzureAddOn",
                "AddOn-TestCleanup");
        }

        #endregion
    }
}