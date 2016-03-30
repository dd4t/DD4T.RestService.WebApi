using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DD4T.RestService.WebApi.Controllers;
using DD4T.ContentModel;
using System.Web.Http.Results;
using System.Net.Http;
using Newtonsoft.Json;

namespace DD4T.RestService.WebApi.Test
{
    [TestClass]
    public class BinaryControllerTest : BaseControllerTest
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            Initialize();
        }

        [TestMethod]
        public void BinaryControllerGetBinaryMetaByUrl()
        {
            var result = BinaryController.GetBinaryMetaByUrl(1, "png", "/media/123") as OkNegotiatedContentResult<IBinaryMeta>;
            Assert.IsNotNull(result);
            IBinaryMeta binaryMeta = result.Content;
            Assert.IsNotNull(binaryMeta);         
            Assert.AreEqual(binaryMeta.Id, "tcm:1-2");
        }
    }
}
