using AutoMapper;
using GlassLewisAssessment.application.Commands;
using GlassLewisAssessment.application.Commands.Handlers;
using GlassLewisAssessment.application.DTOs;
using GlassLewisAssessment.application.Mappers;
using GlassLewisAssessment.infrastructure.Data;
using GlassLewisAssessment.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GlassLewisAssessment.Tests.Application.Commands
{
    public class CompanyCommandHandlerTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly CreateCompanyCommandHandler _handlerCreateCompany;
        private readonly UpdateCompanyCommandHandler _handlerUpdateCompany;

        public CompanyCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                .UseInMemoryDatabase("TestCommandsDB")
                                .Options;

            _context = new ApplicationDbContext(options);

            var config = new MapperConfiguration(config => config.AddProfile<CompanyMapperProfile>());

            _handlerCreateCompany = new CreateCompanyCommandHandler(new CompanyRepository(_context), config.CreateMapper());
            _handlerUpdateCompany = new UpdateCompanyCommandHandler(new CompanyRepository(_context), config.CreateMapper());
        }

        [Fact]
        public async Task CreateCompany_Succesfully()
        {
            //Arrange
            var companyDTO = new CompanyDTO
            {
                Name = "Apple Inc.",
                Exchange = "NASDAQ",
                Ticker = "AAPL",
                Isin = "US0378331005",
                Website = "http://www.apple.com"
            };
            
            var command = new CreateCompanyCommand(companyDTO);

            //Act
            var resultId = await _handlerCreateCompany.Handle(command, CancellationToken.None);
            companyDTO.Isin = "DE000PAH0038";
            var resultId2 = await _handlerCreateCompany.Handle(command, CancellationToken.None);
            
            // Assert
            Assert.True(resultId > 0 && resultId2 > 0);
        }

        [Fact]
        public async Task CreateCompanyIsDuplicate_Exception()
        {
            // Arrange
            var companyDTO = new CompanyDTO
            {
                Name = "Panasonic Corp",
                Exchange = "Tokyo Stock Exchange",
                Ticker = "6752",
                Isin = "JP3866800000",
                Website = "http://www.panasonic.co.jp"
            };
            var command = new CreateCompanyCommand(companyDTO);

            // Act
            var resultId = await _handlerCreateCompany.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(resultId > 0);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _handlerCreateCompany.Handle(command, CancellationToken.None));
            Assert.Equal("The Isin is already taken.", exception.Message);
        }

        [Fact]
        public async Task CreateCompanyIsinInvalidFormat_Exception()
        {
            // Arrange
            var companyDTO = new CompanyDTO
            {
                Name = "Heineken NV",
                Exchange = "Euronext Amsterdam",
                Ticker = "HEIA",
                Isin = "1NL0000009165"
            };
            var command = new CreateCompanyCommand(companyDTO);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _handlerCreateCompany.Handle(command, CancellationToken.None));
            Assert.Equal("Invalid Isin format.", exception.Message);
        }

        [Fact]
        public async Task UpdateCompanyCommand_Succesfully()
        {
            // Arrange
            var companyDTO = new CompanyDTO
            {
                Name = "New Company",
                Exchange = "NASDAQ",
                Ticker = "NC",
                Isin = "US9876543210",
                Website = "http://newcompany.com"
            };
            var id = await _handlerCreateCompany.Handle(new CreateCompanyCommand(companyDTO), CancellationToken.None);

            companyDTO.Name = "Updated Company";
            companyDTO.Exchange = "NYSE";
            companyDTO.Ticker = "UC";
            companyDTO.Isin = "US1234567890";
            companyDTO.Website = "http://updatedcompany.com";

            var command = new UpdateCompanyCommand(id, companyDTO);

            // Act
            var result = await _handlerUpdateCompany.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(result, id);
        }

        [Fact]
        public async Task UpdateCompanyCommand_ShouldReturnFalse_WhenCompanyNotFound()
        {
            // Arrange
            var companyDTO = new CompanyDTO
            {
                Name = "New Company",
                Exchange = "NASDAQ",
                Ticker = "NC",
                Isin = "US9876543210",
                Website = "http://newcompany.com"
            };
            var command = new UpdateCompanyCommand(0, companyDTO);

            // Act
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => _handlerUpdateCompany.Handle(command, CancellationToken.None));
            // Assert
            Assert.Equal("Company not found.", exception.Result.Message);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}