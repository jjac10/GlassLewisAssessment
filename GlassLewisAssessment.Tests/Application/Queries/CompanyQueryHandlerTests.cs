using AutoMapper;
using GlassLewisAssessment.application.Mappers;
using GlassLewisAssessment.application.Queries;
using GlassLewisAssessment.application.Queries.Handlers;
using GlassLewisAssessment.domain.Entities;
using GlassLewisAssessment.infrastructure.Data;
using GlassLewisAssessment.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GlassLewisAssessment.Tests.Application.Queries
{
    public class CompanyQueryHandlerTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly GetAllCompaniesQueryHandler _handlerGetAll;
        private readonly GetCompanyByIdQueryHandler _handlerGetById;
        private readonly GetCompanyByIsinQueryHandler _handlerGetByIsin;

        public CompanyQueryHandlerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                .UseInMemoryDatabase("TestQueriesDB")
            .Options;

            _context = new ApplicationDbContext(options);

            var config = new MapperConfiguration(config => config.AddProfile<CompanyMapperProfile>());

            _handlerGetAll = new GetAllCompaniesQueryHandler(new CompanyRepository(_context), config.CreateMapper());
            _handlerGetById = new GetCompanyByIdQueryHandler(new CompanyRepository(_context), config.CreateMapper());
            _handlerGetByIsin = new GetCompanyByIsinQueryHandler(new CompanyRepository(_context), config.CreateMapper());

            // Arrange
            SeedDatabase(_context);
        }

        private void SeedDatabase(ApplicationDbContext context)
        {
            // Agregar datos de ejemplo a la base de datos en memoria
            context.Companies.Add(new Company 
            {
                Id = 1,
                Name = "Apple Inc.",
                Exchange = "NASDAQ",
                Ticker = "AAPL",
                Isin = "US0378331005",
                Website = "http://www.apple.com"
            });

            context.Companies.Add(new Company 
            { 
                Id = 2,
                Name = "British Airways Plc",
                Exchange = "Pink Sheets",
                Ticker = "BAIRY",
                Isin = "US1104193065"
            });

            // Guardar cambios
            context.SaveChanges();
        }

        [Fact]
        public async Task GetAllCompanies_Succesfully()
        {
            // Act
            var result = await _handlerGetAll.Handle(new GetAllCompaniesQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, company => company.Name.Contains("British"));
        }

        [Fact]
        public async Task GetCompanyById_Succesfully()
        {
            // Act
            var result = await _handlerGetById.Handle(new GetCompanyByIdQuery(1), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("US0378331005", result.Isin);
            Assert.Equal("Apple Inc.", result.Name);
        }

        [Fact]
        public async Task GetCompanyByIsin_Succesfully()
        {
            // Act
            var result = await _handlerGetByIsin.Handle(new GetCompanyByIsinQuery("US1104193065"), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("US1104193065", result.Isin);
            Assert.Equal("British Airways Plc", result.Name);
        }

        [Fact]
        public async Task GetCompanyById_ShouldReturnNull_WhenCompanyNotFound()
        {
            // Act
            var result = await _handlerGetById.Handle(new GetCompanyByIdQuery(999), CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }
    }
}