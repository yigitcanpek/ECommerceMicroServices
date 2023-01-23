using AutoMapper;
using ECommerce.Services.Catalog.DesignPatterns;
using ECommerce.Services.Catalog.Dtos;
using ECommerce.Services.Catalog.Models;
using ECommerce.Shared.Dtos;
using MongoDB.Driver;

namespace ECommerce.Services.Catalog.Services
{
    internal class CourseService:ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService( IMapper mapper,OptionsPattern databaseSettings)
        {
            MongoClient client = new MongoClient(databaseSettings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            List<Course> courses = await _courseCollection.Find(courses => true).ToListAsync();
        
            if (courses.Any())
            {
                foreach (Course course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x=> x.Id== course.Id).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> GetById(string id)
        {
            Course course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (course == null)
            {
                return Response<CourseDto>.Fail("Course not found", 404);
            }
            course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {

            List<Course> courses = await _courseCollection.Find<Course>(x=> x.UserId== userId).ToListAsync();

            if (courses.Any())
            {
                foreach (Course course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
            }

            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses),200);

        }

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            Course newCourse = _mapper.Map<Course>(courseCreateDto);

            newCourse.CreatedTime = DateTime.Now;
            await _courseCollection.InsertOneAsync(newCourse);

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }
        
        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            Course updateCourse = _mapper.Map<Course>(courseUpdateDto);
            Course result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, updateCourse);
            if (result == null)
            {
                return Response<NoContent>.Fail("Course not found.", 404);
            }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            DeleteResult result = await _courseCollection.DeleteOneAsync(x=> x.Id==id);
            if (result.DeletedCount >0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Course not found",404);
            }
        }


    }
}
