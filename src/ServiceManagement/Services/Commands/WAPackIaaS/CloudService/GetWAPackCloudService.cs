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

namespace Microsoft.WindowsAzure.Commands.WAPackIaaS.CloudService
{
    using Microsoft.WindowsAzure.Commands.Utilities.WAPackIaaS.DataContract;
    using Microsoft.WindowsAzure.Commands.Utilities.WAPackIaaS.Operations;
    using System;
    using System.Collections.Generic;
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Get, "WAPackCloudService", DefaultParameterSetName = WAPackCmdletParameterSets.Empty)]
    public class GetWAPackCloudService : IaaSCmdletBase
    {
        [Parameter(Position = 0, ParameterSetName = WAPackCmdletParameterSets.FromName, ValueFromPipeline = true, HelpMessage = "CloudService Name.")]
        [ValidateNotNullOrEmpty]
        public string Name
        {
            get;
            set;
        }

        public override void ExecuteCmdlet()
        {
            IEnumerable<CloudService> results = null;
            var cloudServiceOperations = new CloudServiceOperations(this.WebClientFactory);
            var vmRoleOperations = new VMRoleOperations(this.WebClientFactory);

            if (this.ParameterSetName == WAPackCmdletParameterSets.FromName)
            {
                var cloudService = cloudServiceOperations.Read(this.Name);
                results = new List<CloudService>() { cloudService };
            }
            else if (this.ParameterSetName == WAPackCmdletParameterSets.Empty)
            {
                results = cloudServiceOperations.Read();
            }

            this.GenerateCmdletOutput(results);
        }
    }
}