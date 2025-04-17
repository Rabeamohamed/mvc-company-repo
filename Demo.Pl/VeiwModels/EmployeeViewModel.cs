﻿using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo.Pl.VeiwModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50, ErrorMessage = "The Maximum Lenght is 50 Chars")]
        [MinLength(5, ErrorMessage = "Minimum Length is 5 Chars")]
        public string Name { get; set; }

        [Range(22, 35, ErrorMessage = "Age Must be In Range From 22 To 35")]
        public int? Age { get; set; }

        [RegularExpression("^[0-9]{1,3}-[a-zA-z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address Must Be Like 123-Street-City-Country")]
        public string Adress { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }

        public IFormFile Image { get; set; }
        public string ImageName { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        [InverseProperty("Employees")]
        public Department Department { get; set; }
    }
}
