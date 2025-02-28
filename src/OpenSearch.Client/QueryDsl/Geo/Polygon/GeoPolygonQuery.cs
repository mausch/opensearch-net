/* SPDX-License-Identifier: Apache-2.0
*
* The OpenSearch Contributors require contributions made to
* this file be licensed under the Apache-2.0 license or a
* compatible open source license.
*/
/*
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

using System.Collections.Generic;
using OpenSearch.Net.Utf8Json;
using OpenSearch.Net.Utf8Json.Internal;


namespace OpenSearch.Client
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(GeoPolygonQueryFormatter))]
	public interface IGeoPolygonQuery : IFieldNameQuery
	{
		IEnumerable<GeoLocation> Points { get; set; }

		GeoValidationMethod? ValidationMethod { get; set; }
	}

	public class GeoPolygonQuery : FieldNameQueryBase, IGeoPolygonQuery
	{
		public IEnumerable<GeoLocation> Points { get; set; }

		public GeoValidationMethod? ValidationMethod { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoPolygon = this;

		internal static bool IsConditionless(IGeoPolygonQuery q) => q.Field == null || !q.Points.HasAny();
	}

	public class GeoPolygonQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<GeoPolygonQueryDescriptor<T>, IGeoPolygonQuery, T>
			, IGeoPolygonQuery where T : class
	{
		protected override bool Conditionless => GeoPolygonQuery.IsConditionless(this);
		IEnumerable<GeoLocation> IGeoPolygonQuery.Points { get; set; }
		GeoValidationMethod? IGeoPolygonQuery.ValidationMethod { get; set; }

		public GeoPolygonQueryDescriptor<T> Points(IEnumerable<GeoLocation> points) => Assign(points, (a, v) => a.Points = v);

		public GeoPolygonQueryDescriptor<T> Points(params GeoLocation[] points) => Assign(points, (a, v) => a.Points = v);

		public GeoPolygonQueryDescriptor<T> ValidationMethod(GeoValidationMethod? validation) => Assign(validation, (a, v) => a.ValidationMethod = v);
	}

	internal class GeoPolygonQueryFormatter : IJsonFormatter<IGeoPolygonQuery>
	{
		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "_name", 0 },
			{ "boost", 1 },
			{ "validation_method", 2 }
		};

		public IGeoPolygonQuery Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			var query = new GeoPolygonQuery();
			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var property = reader.ReadPropertyNameSegmentRaw();
				if (Fields.TryGetValue(property, out var value))
				{
					switch (value)
					{
						case 0:
							query.Name = reader.ReadString();
							break;
						case 1:
							query.Boost = reader.ReadDouble();
							break;
						case 2:
							query.ValidationMethod = formatterResolver.GetFormatter<GeoValidationMethod>()
								.Deserialize(ref reader, formatterResolver);
							break;
					}
				}
				else
				{
					query.Field = property.Utf8String();
					var fieldCount = 0;
					while (reader.ReadIsInObject(ref fieldCount))
					{
						reader.ReadNext();
						reader.ReadIsNameSeparatorWithVerify();
						query.Points =
							formatterResolver.GetFormatter<IEnumerable<GeoLocation>>()
								.Deserialize(ref reader, formatterResolver);
					}
				}
			}

			return query;
		}

		public void Serialize(ref JsonWriter writer, IGeoPolygonQuery value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var written = false;

			writer.WriteBeginObject();

			if (!value.Name.IsNullOrEmpty())
			{
				writer.WritePropertyName("_name");
				writer.WriteString(value.Name);
				written = true;
			}

			if (value.Boost != null)
			{
				if (written)
					writer.WriteValueSeparator();

				writer.WritePropertyName("boost");
				writer.WriteDouble(value.Boost.Value);
				written = true;
			}

			if (value.ValidationMethod != null)
			{
				if (written)
					writer.WriteValueSeparator();

				writer.WritePropertyName("validation_method");
				formatterResolver.GetFormatter<GeoValidationMethod>()
					.Serialize(ref writer, value.ValidationMethod.Value, formatterResolver);
				written = true;
			}

			if (written)
				writer.WriteValueSeparator();

			var settings = formatterResolver.GetConnectionSettings();
			writer.WritePropertyName(settings.Inferrer.Field(value.Field));
			writer.WriteBeginObject();
			writer.WritePropertyName("points");
			formatterResolver.GetFormatter<IEnumerable<GeoLocation>>()
				.Serialize(ref writer, value.Points, formatterResolver);
			writer.WriteEndObject();

			writer.WriteEndObject();
		}
	}
}
