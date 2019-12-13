// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗  
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝  
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// -----------------------------------------------
//  
// This file is automatically generated 
// Please do not edit these files manually
// Run the following in the root of the repos:
//
// 		*NIX 		:	./build.sh codegen
// 		Windows 	:	build.bat codegen
//
// -----------------------------------------------
// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace Elasticsearch.Net.Specification.SnapshotLifecycleManagementApi
{
	///<summary>Request options for DeleteSnapshotLifecycle <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-delete.html</para></summary>
	public class DeleteSnapshotLifecycleRequestParameters : RequestParameters<DeleteSnapshotLifecycleRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.DELETE;
	}

	///<summary>Request options for ExecuteSnapshotLifecycle <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-execute.html</para></summary>
	public class ExecuteSnapshotLifecycleRequestParameters : RequestParameters<ExecuteSnapshotLifecycleRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.PUT;
	}

	///<summary>Request options for ExecuteRetention <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-execute-retention.html</para></summary>
	public class ExecuteRetentionRequestParameters : RequestParameters<ExecuteRetentionRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.POST;
	}

	///<summary>Request options for GetSnapshotLifecycle <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-get.html</para></summary>
	public class GetSnapshotLifecycleRequestParameters : RequestParameters<GetSnapshotLifecycleRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.GET;
	}

	///<summary>Request options for GetStatus <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-get-status.html</para></summary>
	public class GetSnapshotLifecycleManagementStatusRequestParameters : RequestParameters<GetSnapshotLifecycleManagementStatusRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.GET;
	}

	///<summary>Request options for PutSnapshotLifecycle <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-api-put.html</para></summary>
	public class PutSnapshotLifecycleRequestParameters : RequestParameters<PutSnapshotLifecycleRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.PUT;
	}

	///<summary>Request options for Start <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-start.html</para></summary>
	public class StartSnapshotLifecycleManagementRequestParameters : RequestParameters<StartSnapshotLifecycleManagementRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.POST;
	}

	///<summary>Request options for Stop <para>https://www.elastic.co/guide/en/elasticsearch/reference/current/slm-stop.html</para></summary>
	public class StopSnapshotLifecycleManagementRequestParameters : RequestParameters<StopSnapshotLifecycleManagementRequestParameters>
	{
		public override HttpMethod DefaultHttpMethod => HttpMethod.POST;
	}
}