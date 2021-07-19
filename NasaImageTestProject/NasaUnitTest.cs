using Microsoft.AspNetCore.Mvc;
using NasaImagesDemo;
using NasaImagesDemo.Controllers;
using NasaImagesDemo.Repository;
using System;
using System.Linq;
using Xunit;

namespace NasaImageTestProject
{
    public class NasaUnitTest
    {
        HomeController _controller;
        IRepository<ApodImage> _service;

        public NasaUnitTest()
        {
            _service = new GenericRepositoryFake();

            _controller = new HomeController(_service);
        
        }

        [Fact]
        public void Test_To_get_all_Images()
        {
            //Act
            var returnResult = _service.GetAllImages();

            //Assert
           
            Assert.Equal(4, returnResult.Count());
            Assert.NotEmpty(returnResult);           

        }
       
        [Fact]
        public void Test_To_load_List_of_Dates_from_local_File()
        {
            //Act
            var response = _controller.loadDatesfromFile();

            //Assert            
            Assert.NotEmpty(response);
            Assert.Equal(4, response.Count());
            Assert.Equal("April 31, 2018", response[3]);

        }
        [Fact]
        public void Test_To_format_Dates_from_Input_File()
        {
            //Act
            var  inPutDate = "April 30, 2018";   

            var result = _controller.formatDate(inPutDate);          
           
            //Assert            
            Assert.NotEmpty(result);
            Assert.Equal("2018-4-30", result);
            var ex = Assert.Throws<FormatException>(() => _controller.formatDate("April 31, 2018"));
            Assert.Contains("April 31, 2018' was not recognized as a valid DateTime.", ex.Message);
        }
        [Fact]
        public void Test_To_Check_For_Valid_Date_string_from_formated_Input()
        {
            //Act
            var inPutDate = "2018-4-30";        

            var result = _controller.IsValidDate(inPutDate);           

            //Assert            
            Assert.True(result);
            Assert.NotNull(inPutDate);
        }

        [Fact]
        public void Test_for_Invalid_Date_With_Exception()
        {

            //Arrange
            var stringDate = "2018-4-30";
            

            //Act
            var validResult = _controller.IsValidDate(stringDate);
            

            // Assert
            Assert.True(validResult);

            var ex = Assert.Throws<ArgumentNullException>(() => _controller.IsValidDate("April 31, 2018"));
            Assert.Contains("Value cannot be null", ex.Message);

        }


    }
}
