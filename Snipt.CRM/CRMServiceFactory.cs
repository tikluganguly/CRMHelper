//Copyright 2019 Tiklu Ganguly

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using Microsoft.Azure;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.ServiceModel.Description;

namespace Snipt.CRM
{
    public static class CRMServiceFactory
    {
        public static OrganizationServiceProxy GetOrgServiceProxy()
        {
            var url = new Uri(CloudConfigurationManager.GetSetting("CRM:ServiceUrl"));
            var cred = new ClientCredentials();
            cred.UserName.UserName = CloudConfigurationManager.GetSetting("CRM:UserName");
            cred.UserName.Password = CloudConfigurationManager.GetSetting("CRM:Password");
            var org = new OrganizationServiceProxy(url, null, cred, null);
            org.ServiceConfiguration.CurrentServiceEndpoint.Behaviors.Add(new ProxyTypesBehavior());
            return org;
        }
    }
}
