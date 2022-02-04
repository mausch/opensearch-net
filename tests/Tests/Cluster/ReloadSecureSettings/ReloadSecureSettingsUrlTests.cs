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

namespace Tests.Cluster.ReloadSecureSettings
{
	public class ReloadSecureSettingsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await POST("/_nodes/reload_secure_settings")
				.Fluent(c => c.Nodes.ReloadSecureSettings())
				.Request(c => c.Nodes.ReloadSecureSettings(new ReloadSecureSettingsRequest()))
				.FluentAsync(c => c.Nodes.ReloadSecureSettingsAsync())
				.RequestAsync(c => c.Nodes.ReloadSecureSettingsAsync(new ReloadSecureSettingsRequest()));

			await POST("/_nodes/foo/reload_secure_settings")
				.Fluent(c => c.Nodes.ReloadSecureSettings(n => n.NodeId("foo")))
				.Request(c => c.Nodes.ReloadSecureSettings(new ReloadSecureSettingsRequest("foo")))
				.FluentAsync(c => c.Nodes.ReloadSecureSettingsAsync(n => n.NodeId("foo")))
				.RequestAsync(c => c.Nodes.ReloadSecureSettingsAsync(new ReloadSecureSettingsRequest("foo")));
		}
	}
}
