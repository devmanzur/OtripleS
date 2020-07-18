using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using OtripleS.Web.Api.Models.Courses;
using Xunit;

namespace OtripleS.Web.Api.Tests.Acceptance.APIs.Courses
{
    public partial class CoursesApiTests
    {
        [Fact]
        public async Task ShouldGetAllCoursesAsync()
        {
            // given
            IEnumerable<Course> randomCourses = GetRandomCourses();
            IEnumerable<Course> inputCourses = randomCourses;

            foreach (Course course in inputCourses)
            {
                await this.otripleSApiBroker.PostCourseAsync(course);
            }

            List<Course> expectedCourses = inputCourses.ToList();

            // when
            List<Course> actualCourses = await this.otripleSApiBroker.GetAllCoursesAsync();

            // then
            foreach (Course expectedCourse in expectedCourses)
            {
                Course actualCourse = actualCourses.Single(course => course.Id == expectedCourse.Id);
                actualCourse.Should().BeEquivalentTo(expectedCourse);
                await this.otripleSApiBroker.DeleteCourseByIdAsync(actualCourse.Id);
            }
        }
    }
}