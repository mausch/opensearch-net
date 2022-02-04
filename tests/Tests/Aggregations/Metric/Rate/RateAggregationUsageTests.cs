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

using System;
using OpenSearch.OpenSearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Osc;
using Tests.Core.Extensions;
using Tests.Core.ManagedOpenSearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Osc.Infer;

namespace Tests.Aggregations.Metric.Rate
{
	/**
	 * A rate metrics aggregation can be used only inside a date_histogram and calculates a rate of documents or a field in each
	 * date_histogram bucket. The field values can be generated by a provided script or extracted from specific numeric or histogram fields in the documents.
	 *
	 * Be sure to read the OpenSearch documentation on {ref_current}/search-aggregations-metrics-rate-aggregation.html[Rate Aggregation].
	*/
	public class RateAggregationWithoutModeUsageTests : AggregationUsageTestBase<ReadOnlyCluster>
	{
		public RateAggregationWithoutModeUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override object AggregationJson => new
		{
			by_date = new
			{
				date_histogram = new
				{
					field = "startedOn",
					calendar_interval = "month"
				},
				aggs = new
				{
					my_rate = new
					{
						rate = new
						{
							unit = "month"
						}
					}
				}
			}
		};

		// We're also testing that we can skip providing the field on this aggregation

		protected override Func<AggregationContainerDescriptor<Project>, IAggregationContainer> FluentAggs => a => a
			.DateHistogram("by_date", d => d
				.Field(f => f.StartedOn)
				.CalendarInterval(DateInterval.Month)
				.Aggregations(a => a
					.Rate("my_rate", m => m
						.Unit(DateInterval.Month)
					)));

		protected override AggregationDictionary InitializerAggs =>
			new DateHistogramAggregation("by_date")
			{
				Field = Field<Project>(p => p.StartedOn),
				CalendarInterval = DateInterval.Month,
				Aggregations = new RateAggregation("my_rate")
				{
					Unit = DateInterval.Month
				}
			};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldBeValid();

			var dateHistogram = response.Aggregations.DateHistogram("by_date");
			dateHistogram.Should().NotBeNull();
			dateHistogram.Buckets.Should().NotBeNull();
			dateHistogram.Buckets.Count.Should().BeGreaterThan(10);
			foreach (var item in dateHistogram.Buckets)
			{
				var rate = item.Rate("my_rate");
				rate.Should().NotBeNull();
				rate.Value.Should().BeGreaterOrEqualTo(1);
			}
		}
	}
}
