using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest.Resolvers.Writers;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface INestedType : IObjectType
	{
		[JsonProperty("include_in_parent")]
		bool? IncludeInParent { get; set; }

		[JsonProperty("include_in_root")]
		bool? IncludeInRoot { get; set; }
	}

	public class NestedType : ObjectType, INestedType
	{
		public NestedType() : base("nested") { }

		public bool? IncludeInParent { get; set; }
		public bool? IncludeInRoot { get; set; }
	}

	public class NestedObjectTypeDescriptor<TParent, TChild>
		: ObjectTypeDescriptorBase<NestedObjectTypeDescriptor<TParent, TChild>, INestedType, TParent, TChild>
		, INestedType
		where TParent : class
		where TChild : class
	{
		public NestedObjectTypeDescriptor(IConnectionSettingsValues connectionSettings)
			: base(connectionSettings) { }

		bool? INestedType.IncludeInParent { get; set; }
		bool? INestedType.IncludeInRoot { get; set; }

		public NestedObjectTypeDescriptor<TParent, TChild> IncludeInParent(bool includeInParent = true) =>
			Assign(a => a.IncludeInParent = includeInParent);

		public NestedObjectTypeDescriptor<TParent, TChild> IncludeInRoot(bool includeInRoot = true) =>
			Assign(a => a.IncludeInRoot = includeInRoot);
	}
}