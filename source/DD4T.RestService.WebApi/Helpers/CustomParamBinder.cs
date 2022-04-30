using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using ModelMetadataProvider = System.Web.Http.Metadata.ModelMetadataProvider;

namespace DD4T.RestService.WebApi.Helpers
{
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public class ArrayParamAttribute : ParameterBindingAttribute
	{
		public override HttpParameterBinding GetBinding(HttpParameterDescriptor paramDesc)
		{
			return new ArrayParamBinding(paramDesc);
		}
	}

	public class ArrayParamBinding : HttpParameterBinding
	{
		public ArrayParamBinding(HttpParameterDescriptor paramDesc) : base(paramDesc) { }

		public override bool WillReadBody => false;

		public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			var idsAsString = actionContext.Request.GetRouteData().Values["ids"]?.ToString();
			if (string.IsNullOrEmpty(idsAsString))
				return Task.FromCanceled(cancellationToken);

			var ids = idsAsString
				.Trim('[', ']')
				.Split(',')
				.Where(str => !string.IsNullOrEmpty(str) && int.TryParse(str, out _))
				.Select(int.Parse)
				.ToArray();

			SetValue(actionContext, ids);

			var tcs = new TaskCompletionSource<object>();
			tcs.SetResult(null);
			return tcs.Task;
		}
	}
}