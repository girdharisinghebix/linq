using CoreProject.DataAccessLayer.Models;
using CoreProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreProject.DataAccessLayer
{
    public  class StudentDataLayer
    {


        BlogDbContext db = new BlogDbContext();
        public async Task<int> AddStudent(Student std)
        {

            // db.Customers.AddAsync(cust);
            await db.Students.AddAsync(std);
            db.SaveChanges();
            var id = std.StudentId;
            //db.SaveChangesAsync();
            return id;



        }

        public async Task<int> AddAddresDetail(StudentAddressDetail std)
        {

            // db.Customers.AddAsync(cust);
            await db.StudentAddressDetails.AddAsync(std);
            db.SaveChanges();
            
            var id = std.AddressId;
            //db.SaveChangesAsync();
            return id;



        }

        public int addStudentRecord(Student std,StudentAddressDetail sda)
        {

            db.Students.Add(std);
            db.SaveChanges();
            sda.StudentId = std.StudentId;
            db.StudentAddressDetails.Add(sda);              
            db.SaveChanges();
            var id = std.StudentId;
            return id;

            //return std.StudentId;


        }

        public int addbulkrecord(Student std, List<StudentAddressDetail> sda)
        {
          

            db.Students.Add(std);
            db.SaveChanges();
            var abc = (from _ab in sda select new StudentAddressDetail
               {   PinCode = _ab.PinCode,
                   StudentId = std.StudentId ,
                   AddressId = _ab.AddressId,
                   AddressDetail = _ab.AddressDetail
               }).ToList();         
            db.AddRange(abc);
            db.SaveChanges();
            var id = std.StudentId;
            return id;

            //return std.StudentId;


        }
    }
}
