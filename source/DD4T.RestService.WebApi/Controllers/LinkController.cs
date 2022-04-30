using DD4T.ContentModel.Contracts.Providers;
using DD4T.ContentModel.Contracts.Logging;
using DD4T.RestService.WebApi.Helpers;
using System;
using System.Web.Http;

namespace DD4T.Rest.WebApi.Controllers
{
	[RoutePrefix("link")]
	public class LinkController : ApiController
	{
		private readonly ILinkProvider LinkProvider;
		private readonly ILogger Logger;

		public LinkController(ILinkProvider linkProvider, ILogger logger)
		{
			LinkProvider = linkProvider ?? throw new ArgumentNullException("linkProvider");
			Logger = logger ?? throw new ArgumentNullException("logger");
		}

		[HttpGet]
		[Route("ResolveLink/{publicationId:int}/{componentUri:int}")]
		public IHttpActionResult ResolveLink(int publicationId, int componentUri)
		{
			Logger.Debug($"ResolveLink publicationId={publicationId}, componentUri={componentUri}");
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			LinkProvider.PublicationId = publicationId;
			var link = LinkProvider.ResolveLink(componentUri.ToComponentTcmUri(publicationId));

			if (string.IsNullOrEmpty(link))
				return NotFound();

			return Ok(link);
		}

		[HttpGet]
		[Route("ResolveLink/{publicationId:int}/{componentUri:int}/{sourcePageUri:int}/{excludeComponentTemplateUri:int}")]
		public IHttpActionResult ResolveLink(int publicationId, int componentUri, int sourcePageUri, int excludeComponentTemplateUri)
		{
			Logger.Debug($"GetContentByUrl publicationId={publicationId}, componentUri={componentUri}, sourcePageUri={sourcePageUri}, excludeComponentTemplateUri{excludeComponentTemplateUri}");
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			LinkProvider.PublicationId = publicationId;
			var link = LinkProvider.ResolveLink(sourcePageUri.ToPageTcmUri(publicationId), componentUri.ToComponentTcmUri(publicationId), excludeComponentTemplateUri.ToComponentTemplateTcmUri(publicationId));

			if (string.IsNullOrEmpty(link))
				return NotFound();

			return Ok(link);
		}
	}
}
