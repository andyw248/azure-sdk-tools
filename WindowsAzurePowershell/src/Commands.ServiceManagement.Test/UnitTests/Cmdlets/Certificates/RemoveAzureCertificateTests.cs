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

namespace Microsoft.WindowsAzure.Commands.ServiceManagement.Test.UnitTests.Cmdlets.Certificates
{
    using Commands.Utilities.Common;
    using Commands.Test.Utilities.CloudService;
    using Commands.Test.Utilities.Common;
    using Commands.ServiceManagement.Certificates;
    using VisualStudio.TestTools.UnitTesting;


    ////[TestClass]
    public class RemoveAzureCertificateTests : TestBase
    {
        FileSystemHelper files;

        //[TestInitialize]
        public void SetupTest()
        {
            files = new FileSystemHelper(this);
            //files.CreateAzureSdkDirectoryAndImportPublishSettings();
        }

        //[TestCleanup]
        public void CleanupTest()
        {
            //files.Dispose();
        }

        //[TestMethod]
        public void RemoveAzureCertificateTest()
        {
            // Setup
            bool deleted = false;
            SimpleServiceManagement channel = new SimpleServiceManagement();
            channel.DeleteCertificateThunk = ar => { deleted = true; };

            // Test
            RemoveAzureCertificate removeAzureCertificate = new RemoveAzureCertificate()
            {
                Channel = channel,
                ShareChannel = true,
                CommandRuntime = new MockCommandRuntime()
            };

            removeAzureCertificate.ExecuteCommand();
            Assert.IsTrue(deleted);
        }
    }
}