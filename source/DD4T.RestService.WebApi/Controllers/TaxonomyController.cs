using DD4T.ContentModel;
using DD4T.ContentModel.Contracts.Providers;
using DD4T.ContentModel.Contracts.Logging;
using DD4T.RestService.WebApi.Helpers;
using System;
using System.Web.Http;

namespace DD4T.RestService.WebApi.Controllers
{
	[RoutePrefix("taxonomy")]
	public class TaxonomyController : ApiController
	{
		private readonly ITaxonomyProvider TaxonomyProvider;
		private readonly ILogger Logger;

		public TaxonomyController(ITaxonomyProvider taxonomyProvider, ILogger logger)
		{
			TaxonomyProvider = taxonomyProvider ?? throw new ArgumentNullException("taxonomyProvider");
			Logger = logger ?? throw new ArgumentNullException("logger");
		}

		[HttpGet]
		[Route("GetKeyword/{publicationId:int}/{categoryUriToLookIn:int}/{keywordName}")]
		public IHttpActionResult GetKeyword(int publicationId, int categoryUriToLookIn, string keywordName)
		{
			Logger.Debug($"ResolveLink publicationId={publicationId}, categoryUriToLookIn={categoryUriToLookIn}, keywordName={keywordName}");
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			TaxonomyProvider.PublicationId = publicationId;
			var keyword = TaxonomyProvider.GetKeyword(categoryUriToLookIn.ToCategoryTcmUri(publicationId), keywordName);

			return Ok(keyword);
		}
	}
}
