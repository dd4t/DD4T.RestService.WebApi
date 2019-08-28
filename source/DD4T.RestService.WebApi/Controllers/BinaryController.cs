using DD4T.ContentModel.Contracts.Providers;
using DD4T.ContentModel.Contracts.Logging;
using DD4T.RestService.WebApi.Helpers;
using System;
using System.IO;
using System.Web.Http;
using DD4T.ContentModel;
using System.Web.Http.Description;

namespace DD4T.RestService.WebApi.Controllers
{
	[RoutePrefix("binary")]
	public class BinaryController : ApiController
	{
		private readonly IBinaryProvider BinaryProvider;
		private readonly ILogger Logger;

		public BinaryController(IBinaryProvider binaryProvider, ILogger logger)
		{
			Logger = logger ?? throw new ArgumentNullException("logger");
			BinaryProvider = binaryProvider ?? throw new ArgumentNullException("binaryProvider");
		}

		[HttpGet]
		[Route("GetBinaryByUri/{publicationId:int}/{id:int}")]
		public IHttpActionResult GetBinaryByUri(int publicationId, int id)
		{
			Logger.Debug($"GetBinaryByUri  publicationId={publicationId}, componentId={id}");
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			BinaryProvider.PublicationId = publicationId;
			var binary = BinaryProvider.GetBinaryByUri(id.ToComponentTcmUri(publicationId));

			if (binary == null)
				return NotFound();

			return Ok(binary);
		}

		[HttpGet]
		[Route("GetBinaryByUrl/{publicationId:int}/{extension}/{*url}")]
		public IHttpActionResult GetBinaryByUrl(int publicationId, string extension, string url)
		{
			Logger.Debug($"GetBinaryByUrl  publicationId={publicationId}, url={url}, extension={extension}");
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			BinaryProvider.PublicationId = publicationId;
			var binary = BinaryProvider.GetBinaryByUrl(url.GetUrl(extension));

			if (binary == null)
				return NotFound();

			return Ok(binary);
		}

		[HttpGet]
		[Route("GetBinaryStreamByUri/{publicationId:int}/{id:int}")]
		public IHttpActionResult GetBinaryStreamByUri(int publicationId, int id)
		{
			Logger.Debug($"GetBinaryStreamByUri  publicationId={publicationId}, componentId={id}");
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			BinaryProvider.PublicationId = publicationId;
			var binary = BinaryProvider.GetBinaryStreamByUri(id.ToComponentTcmUri(publicationId));

			if (binary == null)
				return NotFound();

			return Ok(binary);
		}

		[HttpGet]
		[Route("GetBinaryStreamByUrl/{publicationId:int}/{extension}/{*url}")]
		public IHttpActionResult GetBinaryStreamByUrl(int publicationId, string extension, string url)
		{
			Logger.Debug($"GetBinaryStreamByUrl  publicationId={publicationId}, url={url}, extension={extension}");
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			BinaryProvider.PublicationId = publicationId;
			var binary = BinaryProvider.GetBinaryStreamByUrl(url.GetUrl(extension));

			if (binary == null)
				return NotFound();

			return Ok(binary);
		}

		[Obsolete("Use GetBinaryMetaByUri method")]
		[HttpGet]
		[Route("GetLastPublishedDateByUri/{publicationId:int}/{id:int}")]
		public IHttpActionResult GetLastPublishedDateByUri(int publicationId, int id)
		{
			Logger.Debug($"GetLastPublishedDateByUri  publicationId={publicationId}, componentId={id}");
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			BinaryProvider.PublicationId = publicationId;
			var lastPublishedDate = BinaryProvider.GetLastPublishedDateByUri(id.ToComponentTcmUri(publicationId));

			if (lastPublishedDate == DateTime.MinValue)
				return NotFound();

			return Ok(lastPublishedDate);
		}

		[Obsolete("Use GetBinaryMetaByUrl method")]
		[HttpGet]
		[Route("GetLastPublishedDateByUrl/{publicationId:int}/{extension}/{*url}")]
		public IHttpActionResult GetLastPublishedDateByUrl(int publicationId, string extension, string url)
		{
			Logger.Debug("GetLastPublishedDateByUrl  publicationId={0}, url={1}, extension={2}", publicationId, url, extension);
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			BinaryProvider.PublicationId = publicationId;
			var lastPublishedDate = BinaryProvider.GetLastPublishedDateByUrl(url.GetUrl(extension));

			if (lastPublishedDate == DateTime.MinValue)
				return NotFound();

			return Ok(lastPublishedDate);
		}

		[HttpGet]
		[Route("GetBinaryMetaByUri/{publicationId:int}/{id:int}")]
		public IHttpActionResult GetBinaryMetaByUri(int publicationId, int id)
		{
			Logger.Debug("GetBinaryMetaByUri publicationId={0}, componentId={1}", publicationId, id);
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			BinaryProvider.PublicationId = publicationId;
			var binaryMeta = BinaryProvider.GetBinaryMetaByUri(id.ToComponentTcmUri(publicationId));

			if (binaryMeta == null)
				return NotFound();

			return Ok(binaryMeta);
		}

		[HttpGet]
		[Route("GetBinaryMetaByUrl/{publicationId:int}/{extension}/{*url}")]
		[ResponseType(typeof(BinaryMeta))]
		public IHttpActionResult GetBinaryMetaByUrl(int publicationId, string extension, string url)
		{
			Logger.Debug("GetBinaryMetaByUrl publicationId={0}, url={1}, extension={2}", publicationId, url, extension);
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			BinaryProvider.PublicationId = publicationId;
			if (!(BinaryProvider.GetBinaryMetaByUrl(url.GetUrl(extension)) is IBinaryMeta binaryMeta))
				return NotFound();

			Logger.Debug($"about to return binarymeta {binaryMeta.Id}");
			return Ok(binaryMeta);
		}

		[HttpGet]
		[Route("GetUrlForUri/{publicationId:int}/{id:int}")]
		public IHttpActionResult GetUrlForUri(int publicationId, int id)
		{
			Logger.Debug("GetUrlForUri publicationId={0}, componentId={1}", publicationId, id);
			if (publicationId <= 0)
				return BadRequest(Messages.EmptyPublicationId);

			BinaryProvider.PublicationId = publicationId;
			var url = BinaryProvider.GetUrlForUri(id.ToComponentTcmUri(publicationId));

			if (string.IsNullOrEmpty(url))
				return NotFound();

			return Ok(url);
		}
	}
}
