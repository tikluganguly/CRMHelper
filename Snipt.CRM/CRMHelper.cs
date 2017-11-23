//Copyright 2017 Tiklu Ganguly

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
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;

namespace Snipt.CRM
{
    public static class CRMHelper
    {
        public static OrganizationServiceProxy GetOrgService()
        {
            var url = new Uri(CloudConfigurationManager.GetSetting("CRM:ServiceUrl"));
            var cred = new ClientCredentials();
            cred.UserName.UserName = CloudConfigurationManager.GetSetting("CRM:UserName");
            cred.UserName.Password = CloudConfigurationManager.GetSetting("CRM:Password");
            var org = new OrganizationServiceProxy(url, null, cred, null);
            org.ServiceConfiguration.CurrentServiceEndpoint.Behaviors.Add(new ProxyTypesBehavior());
            return org;
        }

        public static List<T> GetList<T>(QueryBase fetchExpression, Func<Entity, T> action)
        {
            using (var org = GetOrgService())
            {
                var res = org.RetrieveMultiple(fetchExpression);
                var list = new List<T>();
                foreach (var ent in res.Entities)
                {
                    list.Add(action(ent));
                }
                return list;
            }
        }

        public static List<T> GetList<T>(string fetchXML, Func<Entity, T> action)
        {
            return GetList<T>(new FetchExpression(fetchXML), action);
        }

        public static Entity GetSingle(string fetchXML)
        {
            return GetSingle(new FetchExpression(fetchXML));
        }

        public static Entity GetSingle(QueryBase fetchExpression)
        {
            using (var org = GetOrgService())
            {
                var res = org.RetrieveMultiple(fetchExpression);
                return res.Entities.FirstOrDefault();
            }
        }

        public static List<OptionMetadata> GetOptions(string optionName)
        {   
            using (var org = GetOrgService())
            {
                var retrieveOptionSetRequest = new RetrieveOptionSetRequest()
                {
                    Name = optionName
                };
                var response = (RetrieveOptionSetResponse)org.Execute(retrieveOptionSetRequest);

                return ((OptionSetMetadata)response.OptionSetMetadata).Options.ToList();
            }
        }
    }
}
