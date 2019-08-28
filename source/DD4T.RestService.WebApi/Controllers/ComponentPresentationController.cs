using DD4T.ContentModel.Contracts.Providers;
using DD4T.ContentModel.Contracts.Logging;
using DD4T.ContentModel.Querying;
using DD4T.RestService.WebApi.Helpers;
using System;
using System.Linq;
using System.Web.Http;

namespace DD4T.RestService.WebApi.Controllers
{
	[RoutePrefix("componentpresentation")]
	public class ComponentPresentationController : ApiController
	{
		private readonly IComponentPresentationProvider ComponentPresentationProvider;
		private readonly ILogger Logger;

		public ComponentPresentationController(IComponentPresentationProvider componentPresentationProvider, ILogger logger)
		{
			Logger = logger ?? throw new ArgumentNullException("logger");
			ComponentPresentationProvider = componentPresentationProvider ?? throw new ArgumentNullException("componentPresenstationProvider");
		}

		[HttpGet]
		[Route("GetContent/{publicationId:int}/{id:int}/{templateId?}")]
		public IHttpActionResult GetContent(int publicationId, int id, int templateId = 0)
		{
			Logger.Debug($"GetContent  publicationId={publicationId}, componentId={id} tempalteid={templateId}");
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			ComponentPresentationProvider.PublicationId = publicationId;

			var content = (templateId == 0) ?
				ComponentPresentationProvider.GetContent(id.ToComponentTcmUri(publicationId)) :
				ComponentPresentationProvider.GetContent(id.ToComponentTcmUri(publicationId), templateId.ToComponentTemplateTcmUri(publicationId));

			if (string.IsNullOrEmpty(content))
				return NotFound();

			return Ok(content);
		}

		[HttpGet]
		[Route("GetLastPublishedDate/{publicationId:int}/{id:int}")]
		public IHttpActionResult GetLastPublishedDate(int publicationId, int id)
		{
			Logger.Debug($"GetLastPublishedDate  publicationId={publicationId}, componentId={id}");
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			ComponentPresentationProvider.PublicationId = publicationId;
			var content = ComponentPresentationProvider.GetLastPublishedDate(id.ToComponentTcmUri(publicationId));

			return Ok(content);
		}

		[HttpGet]
		[Route("GetContentMultiple/{publicationId:int}/{ids}")]
		//api/componentpresentation/GetContentMultiple/3/1,2,3,4
		public IHttpActionResult GetContentMultiple(int publicationId, [ArrayParam] int[] ids)
		{
			Logger.Debug($"GetContentMultiple  publicationId={publicationId}, componentId={ids}");
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			ComponentPresentationProvider.PublicationId = publicationId;

			var tcmuris = ids.Select(compId => compId.ToComponentTcmUri(publicationId)).ToArray();
			var content = ComponentPresentationProvider.GetContentMultiple(tcmuris);

			if (content.Count == 0)
				return NotFound();

			return Ok(content);
		}

		[HttpGet]
		[Route("FindComponents/{publicationId:int}/{queryParameters}")]
		public IHttpActionResult FindComponents(int publicationId, IQuery queryParameters)
		{
			Logger.Debug($"FindComponents publicationId={publicationId}, queryParameters={queryParameters}");
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			ComponentPresentationProvider.PublicationId = publicationId;
			var content = ComponentPresentationProvider.FindComponents(queryParameters);

			if (content.Count == 0)
				return NotFound();

			return Ok(content);
		}
	}
}
