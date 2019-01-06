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

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snipt.CRM.Extensions
{
    public static class ServiceExtensions
    {
        public static List<T> GetList<T>(this IOrganizationService org, QueryBase fetchExpression, Func<Entity, T> action)
        {
            var res = org.RetrieveMultiple(fetchExpression);
            var list = new List<T>();
            foreach (var ent in res.Entities)
            {
                list.Add(action(ent));
            }
            return list;
        }

        public static List<T> GetList<T>(this IOrganizationService org, string fetchXML, Func<Entity, T> action)
        {
            return org.GetList<T>(new FetchExpression(fetchXML), action);
        }

        public static Entity GetSingle(this IOrganizationService org, string fetchXML)
        {
            return org.GetSingle(new FetchExpression(fetchXML));
        }

        public static Entity GetSingle(this IOrganizationService org, QueryBase fetchExpression)
        {
            var res = org.RetrieveMultiple(fetchExpression);
            return res.Entities.FirstOrDefault();
        }

        public static List<OptionMetadata> GetOptions(this IOrganizationService org, string optionName)
        {
            var retrieveOptionSetRequest = new RetrieveOptionSetRequest()
            {
                Name = optionName
            };
            var response = (RetrieveOptionSetResponse)org.Execute(retrieveOptionSetRequest);

            return ((OptionSetMetadata)response.OptionSetMetadata).Options.ToList();
        }

        public static EntityCollection Fetch(this IOrganizationService org, string fetchXML)
        {
            return org.RetrieveMultiple(new FetchExpression(fetchXML));
        }

        public static WhoAmIResponse WhoAmI(this IOrganizationService org)
        {
            return (WhoAmIResponse)org.Execute(new WhoAmIRequest());
        }
    }
}
