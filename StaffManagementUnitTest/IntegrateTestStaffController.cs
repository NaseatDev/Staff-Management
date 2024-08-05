using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StaffManagement.Service.Dtos.Staff;
using StaffManagement.Service.Models;
using StaffManagement.Service.Repositories.Staff;
using System.Linq.Expressions;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace StaffManagement.UnitTest
{
    [TestClass]
    public class IntegrateTestStaffController
    {
        private readonly StaffRepository _service;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<StaffDbContext> _contextMock;
        public IntegrateTestStaffController()
        {
            _contextMock = new Mock<StaffDbContext>();
            _mapperMock = new Mock<IMapper>();
            _service = new StaffRepository(_contextMock.Object, _mapperMock.Object);
        }

        #region Unit Test retrive staff

        [Fact]
        public async Task GetById_ShouldReturnOkResult_WhenUserExists()
        {
            var staffId = "123456";
            var staffEntity = new Staff
            {
                FullName = "Naseat Man",
                Birthday = new DateOnly(2018, 12, 12),
                Gender = 1,
                StaffId = "123456"
            };

            var staffDto = new StaffDto
            {
                FullName = "Naseat Man",
                Birthday = new DateOnly(2018, 12, 12),
                Gender = 1,
                StaffId = "123456"
            };

            _contextMock.Setup(c => c.Staff.FindAsync(staffId)).ReturnsAsync(staffEntity);
            _mapperMock.Setup(m => m.Map<StaffDto>(staffEntity)).Returns(staffDto);


            var actualResult = await _service.GetByIdAsync(staffId);

            // Assert
            Assert.NotNull(actualResult);
            Assert.AreEqual(200, actualResult.StatusCode);
            Assert.AreEqual(staffDto, actualResult.Data);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenStaffNotExists()
        {
            var staffId = "123456";
            _contextMock.Setup(c => c.Staff.FindAsync(staffId)).ReturnsAsync((Staff)null);

            var actualResult = await _service.GetByIdAsync(staffId);

            // Assert
            Assert.False(actualResult.Succeeded);
            Assert.AreEqual(404, actualResult.StatusCode);
            Assert.Null(actualResult.Data);
        }

        #endregion

        #region Unit Test for save staff


        [Fact]
        public async Task SaveAsync_ShouldReturnSuccess_WhenDataIsValid()
        {
            // Arrange
            var staffDto = new StaffDto { StaffId = "123456", Gender = 1, CreatedDate = DateTime.Now, Birthday = new DateOnly(1992, 12, 01) };
            var staffEntity = new Staff { StaffId = "123456", Gender = 1, CreatedDate = DateTime.Now, Birthday = new DateOnly(1992, 12, 01) };

            _contextMock.Setup(c => c.Staff.FindAsync(It.IsAny<Func<Staff, bool>>())).ReturnsAsync((Staff)null!);
            _mapperMock.Setup(m => m.Map<Staff>(staffDto)).Returns(staffEntity);
            _contextMock.Setup(c => c.Staff.AddAsync(staffEntity, default));
            _contextMock.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _service.SaveAsync(staffDto);

            // Assert
            Assert.True(result.Succeeded);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Fact]
        public async Task SaveAsync_ShouldReturnFailure_WhenDuplicateStaffIdExists()
        {
            // Arrange
            var staffDto = new StaffDto { StaffId = "123456", Gender = 1, CreatedDate = DateTime.Now, Birthday = new DateOnly(1992, 12, 01) };
            var existingStaff = new Staff { StaffId = "123456", Gender = 1, CreatedDate = DateTime.Now, Birthday = new DateOnly(1992, 12, 01) };

            _contextMock.Setup(c => c.Staff.FindAsync(staffDto.StaffId)).ReturnsAsync(existingStaff);

            // Act
            var result = await _service.SaveAsync(staffDto);

            // Assert
            Assert.False(result.Succeeded);
            Assert.AreEqual(409, result.StatusCode);
        }

        [Fact]
        public async Task SaveAsync_ShouldReturnFailure_WhenExceptionOccurs()
        {
            // Arrange
            var staffDto = new StaffDto { StaffId = "123456" };
            _contextMock.Setup(c => c.Staff.FindAsync(It.IsAny<Func<Staff, bool>>())).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _service.SaveAsync(staffDto);

            // Assert
            Assert.False(result.Succeeded);
            Assert.AreEqual(500, result.StatusCode);
        }

        #endregion

        #region Unit Test for delete staff

        [Fact]
        public async Task DeleteAsync_ShouldReturnSuccess_WhenStaffExists()
        {
            // Arrange
            var staffId = "123456";
            var staffEntity = new Staff { StaffId = staffId };

            _contextMock.Setup(c => c.Staff.FindAsync(staffId)).ReturnsAsync(staffEntity);
            _contextMock.Setup(c => c.Staff.Remove(staffEntity));
            _contextMock.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _service.DeleteAsync(staffId);

            // Assert
            Assert.True(result.Succeeded);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFailure_WhenStaffDoesNotExist()
        {
            // Arrange
            var staffId = "123456";

            _contextMock.Setup(c => c.Staff.FindAsync(staffId)).ReturnsAsync((Staff)null);

            var result = await _service.DeleteAsync(staffId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.AreEqual(404, result.StatusCode);
        }

        #endregion

        #region Unit Test for update staff

        [Fact]
        public async Task UpdateAsync_ShouldReturnSuccess_WhenDataIsValid()
        {
            // Arrange
            var staffDto = new StaffDto { StaffId = "123456", FullName = "Naseat Man" };
            var existingStaff = new Staff { StaffId = "123456", CreatedDate = DateTime.Now };

            _contextMock.Setup(c => c.Staff.FindAsync(staffDto.StaffId)).ReturnsAsync(existingStaff);
            _contextMock.Setup(c => c.Staff.FindAsync(It.IsAny<Expression<Func<Staff, bool>>>())).ReturnsAsync((Staff)null);
            _mapperMock.Setup(m => m.Map(staffDto, existingStaff)).Verifiable();
            _contextMock.Setup(c => c.Staff.Update(existingStaff)).Verifiable();
            _contextMock.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _service.UpdateAsync(staffDto);

            // Assert
            Assert.True(result.Succeeded);
            _mapperMock.Verify();
            _contextMock.Verify();
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFailure_WhenStaffNotFound()
        {
            // Arrange
            var staffDto = new StaffDto { StaffId = "123456", FullName = "Naseat Man" };

            _contextMock.Setup(c => c.Staff.FindAsync(staffDto.StaffId)).ReturnsAsync((Staff)null);

            // Act
            var result = await _service.UpdateAsync(staffDto);

            // Assert
            Assert.False(result.Succeeded);
        }


        #endregion

    }
}
