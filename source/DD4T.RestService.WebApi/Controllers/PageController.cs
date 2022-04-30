using DD4T.ContentModel.Contracts.Providers;
using DD4T.ContentModel.Contracts.Logging;
using DD4T.RestService.WebApi.Helpers;
using System;
using System.Web.Http;

namespace DD4T.RestService.WebApi.Controllers
{
	[RoutePrefix("page")]
	public class PageController : ApiController
	{
		private readonly IPageProvider PageProvider;
		private readonly ILogger Logger;

		public PageController(IPageProvider pageProvider, ILogger logger)
		{
			PageProvider = pageProvider ?? throw new ArgumentNullException("pageProvider");
			Logger = logger ?? throw new ArgumentNullException("logger");
		}

		[HttpGet]
		[Route("GetContentByUrl/{publicationId:int}/{extension}/{*url}")]
		public IHttpActionResult GetContentByUrl(int publicationId, string extension, string url)
		{
			Logger.Debug($"PageController.GetContentByUrl publicationId={publicationId}, Url={url}, extension={extension}");
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			PageProvider.PublicationId = publicationId;
			var content = PageProvider.GetContentByUrl(url.GetUrl(extension));

			if (string.IsNullOrEmpty(content))
				return NotFound();

			return Ok(content);
		}

		[HttpGet]
		[Route("GetContentByUri/{publicationId:int}/{id:int}")]
		public IHttpActionResult GetContentByUri(int publicationId, int id)
		{
			Logger.Debug($"PageController.GetContentByUri publicationId={publicationId}, id={id}");
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			PageProvider.PublicationId = publicationId;
			var content = PageProvider.GetContentByUri(id.ToPageTcmUri(publicationId));

			if (string.IsNullOrEmpty(content))
				return NotFound();

			return Ok(content);
		}

		[HttpGet]
		[Route("GetLastPublishedDateByUrl/{publicationId:int}/{extension}/{*url}")]
		public IHttpActionResult GetLastPublishedDateByUrl(int publicationId, string extension, string url)
		{
			Logger.Debug($"PageController.GetLastPublishedDateByUrl publicationId={publicationId}, Url={url}, Extension={extension}");
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			PageProvider.PublicationId = publicationId;
			var content = PageProvider.GetLastPublishedDateByUrl(url.GetUrl(extension));

			return Ok(content);
		}

		[HttpGet]
		[Route("GetLastPublishedDateByUri/{publicationId:int}/{id:int}")]
		public IHttpActionResult GetLastPublishedDateByUri(int publicationId, int id)
		{
			Logger.Debug($"PageController.GetLastPublishedDateByUri publicationId={publicationId}, id={id}");
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			PageProvider.PublicationId = publicationId;
			var content = PageProvider.GetLastPublishedDateByUri(id.ToPageTcmUri(publicationId));

			return Ok(content);
		}
	}
}
