namespace DD4T.RestService.WebApi.Helpers
{
	public static class Extensions
	{

		public static string GetUrl(this string url, string extension)
		{
			if (url.EndsWith("/"))
				url = url.Remove(url.Length - 1);

			return $"/{url}.{extension}";
		}

		public static string ToPageTcmUri(this int id, int publicationId)
		{
			return $"tcm:{publicationId}-{id}-64";
		}

		public static string ToComponentTcmUri(this int id, int publicationId)
		{
			return $"tcm:{publicationId}-{id}-16";
		}

		public static string ToComponentTemplateTcmUri(this int id, int publicationId)
		{
			return $"tcm:{publicationId}-{id}-32";
		}

		public static string ToCategoryTcmUri(this int id, int publicationId)
		{
			return $"tcm:{publicationId}-{id}-512";
		}
	}
}