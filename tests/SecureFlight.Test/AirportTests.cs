using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using SecureFlight.Api.Controllers;
using SecureFlight.Api.Models;
using SecureFlight.Core.Entities;
using SecureFlight.Core.Interfaces;
using SecureFlight.Infrastructure.Repositories;
using Xunit;

namespace SecureFlight.Test
{
  public class AirportTests
  {
    private readonly IMapper _mapper;

    public AirportTests()
    {
      _mapper = new MapperConfiguration(config =>
      {
        config.CreateMap<Airport, AirportDataTransferObject>().ReverseMap();
      }).CreateMapper();
    }


    [Fact]
    public async Task Update_Succeeds()
    {
      //Arrange
      var testingContext = new SecureFlightDatabaseTestContext();
      testingContext.CreateDatabase();
      
      var repository = new BaseRepository<Airport>(testingContext);

      var mockRepository = new Mock<IRepository<Airport>>();
      var mockService = new Mock<IService<Airport>>();

      var airportRet = new Airport("MY_CODE", "TEST", "SP", "COUNTRY");
      var airportInput = new AirportDataTransferObject("INPUT_CODE", "name", "sp", "brazil");

      mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(airportRet));

      //TODO: Add test code here
      //Act

      var controller = new AirportsController(mockRepository.Object, mockService.Object, _mapper);

      var result = await controller.Put(airportInput.Code, airportInput);      

      //Assert

      mockRepository.Verify(x => x.GetByIdAsync(airportInput.Code), Times.Once);
      mockRepository.Verify(x => x.Update(It.IsAny<Airport>()), Times.Once);
      Assert.Equal(200, ((IStatusCodeActionResult)result).StatusCode);

      testingContext.DisposeDatabase();      
    }
  }
}
