using AutoMapper;
using ECommerce.Services.Catalog.DesignPatterns;
using ECommerce.Services.Catalog.Dtos;
using ECommerce.Services.Catalog.Models;
using ECommerce.Shared.Dtos;
using MongoDB.Driver;

namespace ECommerce.Services.Catalog.Services
{
    internal class CategoryService:ICategoryService
    {
        private readonly IMongoCollection<Category> _cateogryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper,IDatabaseOptions databaseOptions)
        {
            MongoClient client = new MongoClient(databaseOptions.ConnectionString);
            IMongoDatabase database = client.GetDatabase(databaseOptions.DatabaseName);

            _mapper = mapper;
            _cateogryCollection = database.GetCollection<Category>(databaseOptions.CategoryCollectionName);
        }
        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            List<Category> categories = await _cateogryCollection.Find(categories => true).ToListAsync();
            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }

        public async Task<Response<CategoryDto>> CreateAsync(Category category)
        {
             await _cateogryCollection.InsertOneAsync(category);
             return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category),200);

        }
        public async Task<Response<CategoryDto>> GetByIdAsync (string id)
        {
            Category category = await _cateogryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();

            if (category==null)
            {
                return Response<CategoryDto>.Fail("Category not found.", 404);
            }
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
