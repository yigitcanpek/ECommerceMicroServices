using Dapper;
using ECommerce.Shared.Dtos;
using Npgsql;
using System.Data;

namespace ECommerce.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {

        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;
        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
           int status = await _dbConnection.ExecuteAsync("delete from discount where id=@Id", new {Id=id});
            return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found", 404);

        }

        public  async Task<Response<Entities.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            IEnumerable<Entities.Discount> discount = await _dbConnection.QueryAsync<Entities.Discount>("Select * from discount where userid=@UserId and code=@Code", new { UserId = userId, Code = code });
            Entities.Discount hasDiscount = discount.FirstOrDefault();

            return hasDiscount == null ? Response<Entities.Discount>.Fail("Discount not found", 404) : Response<Entities.Discount>.Success(hasDiscount, 200);
        }

        public async Task<Response<List<Entities.Discount>>> GetlAll()
        {
            IEnumerable<Entities.Discount> discounts = await _dbConnection.QueryAsync<Entities.Discount>("Select * from discount");

            return Response<List<Entities.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Entities.Discount>> GetlById(int id)
        {
            Entities.Discount discounts = (await _dbConnection.QueryAsync<Entities.Discount>("Select * from discount where id=@Id", new {Id=id })).SingleOrDefault();
            if (discounts==null)
            {
                return Response<Entities.Discount>.Fail("Discount not found",404);
            }
            return Response<Entities.Discount>.Success(discounts, 200);
        }

        public async Task<Response<NoContent>> Save(Entities.Discount discount)
        {
            int saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES(@UserId,@Rate,@Code)",discount);
            if (saveStatus>0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("An error accured while adding",500);

        }

        public async Task<Response<NoContent>> Update(Entities.Discount discount)
        {
            int status = await _dbConnection.ExecuteAsync("Update discount set userid=@UserId,code=@Code,rate=@Rate where id =@Id",new {Id=discount.Id,UserId=discount.UserId,Code=discount.Code,Rate=discount.Rate});

            if (status>0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Discount not found", 404);
        }
    }
}
