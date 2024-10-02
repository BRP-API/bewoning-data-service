//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Options;
//using Newtonsoft.Json;
//using Rvig.Data.Mappers;
//using Rvig.Data.Providers;
//using Rvig.HaalCentraalApi.Shared.Options;
//using System.IO;
//using System.Text;

//namespace Test.Rvig.HaalCentraalApi.Util
//{
//    public static class TestHelper
//    {
//        private static readonly string _filePath = "./TestData/";

//        public static T? GetFromJson<T>(string subFolder, string fileName)
//        {
//            var json = File.ReadAllText(Path.Combine(_filePath, Path.Combine(subFolder, fileName)));
//            return JsonConvert.DeserializeObject<T>(json);
//        }

//        public static RvIGDataPersonenMapper CreatePersoonMapperTestClass(ICurrentDateTimeProvider dateTimeProvider)
//        {
//			throw new CustomNotImplementedException();
//            ////var httpContextMock = new Mock<IHttpContextAccessor>();d
//            ////var apiOptionsMock = Options.Create(new HaalcentraalApiOptions
//            ////{d
//            ////    BagHostName = "{baghost}",d
//            ////    BrpHostName = "{brphost}"d
//            ////});d

//            ////return new RvIGDataMapper(httpContextMock.Object, dateTimeProvider, apiOptionsMock); d
//        }
//    }
//}
