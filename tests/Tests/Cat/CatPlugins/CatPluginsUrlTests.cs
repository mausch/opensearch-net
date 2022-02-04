/* SPDX-License-Identifier: Apache-2.0
*
* The OpenSearch Contributors require contributions made to
* this file be licensed under the Apache-2.0 license or a
* compatible open source license.
*
* Modifications Copyright OpenSearch Contributors. See
* GitHub history for details.
*
*  Licensed to Elasticsearch B.V. under one or more contributor
*  license agreements. See the NOTICE file distributed with
*  this work for additional information regarding copyright
*  ownership. Elasticsearch B.V. licenses this file to you under
*  the Apache License, Version 2.0 (the "License"); you may
*  not use this file except in compliance with the License.
*  You may obtain a copy of the License at
*
* 	http://www.apache.org/licenses/LICENSE-2.0
*
*  Unless required by applicable law or agreed to in writing,
*  software distributed under the License is distributed on an
*  "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
*  KIND, either express or implied.  See the License for the
*  specific language governing permissions and limitations
*  under the License.
*/

using System.Threading.Tasks;
using OpenSearch.OpenSearch.Xunit.XunitPlumbing;
using Osc;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Cat.CatPlugins
{
	public class CatPluginsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_cat/plugins")
			.Fluent(c => c.Cat.Plugins())
			.Request(c => c.Cat.Plugins(new CatPluginsRequest()))
			.FluentAsync(c => c.Cat.PluginsAsync())
			.RequestAsync(c => c.Cat.PluginsAsync(new CatPluginsRequest()));
	}
}
