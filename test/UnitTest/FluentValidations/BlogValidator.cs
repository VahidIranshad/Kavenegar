using Kavenegar.Application.Dto.Entity.BlogDtos;

namespace UnitTest.FluentValidations
{
    public class BlogValidator
    {
        BlogCrudDtoValidator _validator;
        public BlogValidator()
        {
            _validator = new BlogCrudDtoValidator();
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public async Task Applications_Validators_CreateData_ReturnsCorrectNumberOfErrors(BlogCrudDto data, int numberExpectedErrors)
        {
            var validationResult = await _validator.ValidateAsync(data);
            Assert.Equal(numberExpectedErrors, validationResult.Errors.Count);
        }

        public static IEnumerable<object[]> GetData()
        {
            var allData = new List<object[]>
        {
            /*Error : Without Error, */
            new object[] { new BlogCrudDto { Id = 1, Title = "A", Content = "", AuthorId = Kavenegar.Domain.Const.UserConst.Admin_Id}, 0},
            /*Error : Title, */
            new object[] { new BlogCrudDto { Id = 1, Title = null, Content = "", AuthorId = Kavenegar.Domain.Const.UserConst.Admin_Id }, 1},
            /*Error : Title, Content*/
            new object[] { new BlogCrudDto {
                Id = 1,
                Title = String.Concat(Enumerable.Repeat("-", 801)),
                Content = String.Concat(Enumerable.Repeat("-", 4001)) ,
                AuthorId = Kavenegar.Domain.Const.UserConst.Admin_Id}, 2},
        };

            return allData;
        }
    }
}
