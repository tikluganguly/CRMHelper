﻿//Copyright 2017 Tiklu Ganguly

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using Microsoft.Xrm.Sdk;

namespace Snipt.CRM.Extensions
{
    public static class EntityExtensions
    {
        public static T Get<T>(this Entity ent, string attribute, T defaultVal = default(T))
        {
            return (ent.Contains(attribute)) ? ent.GetAttributeValue<T>(attribute) : defaultVal;
        }
    }
}
