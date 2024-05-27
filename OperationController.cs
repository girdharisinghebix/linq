using CoreProject.ImplementBLL;
using CoreProject.InterfaceRepository;
using CoreProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace CoreProject.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {

        IStudentRepository _IStudentRepository;
        public OperationController(IStudentRepository iStudentRepository)
        {
            _IStudentRepository = iStudentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentBLL model)
        {

            //int customerid = 0;

            var customerid = await _IStudentRepository.SaveData(model);

            return Ok(customerid);

        }


        [HttpGet]
        [Route("FetchStudent")]
        public IActionResult FetchStudent()
        {

            List<StudentA> lsa1 = new List<StudentA>
            {

             new StudentA() { Id = 1,RollNumber=10 },

             new StudentA() { Id = 2,RollNumber=20 },

             new StudentA() { Id = 3,RollNumber=30 }

            };


            List<StudentB> lsa2 = new List<StudentB> {

             new StudentB() { Idd = 1, Name = "Pawan" },
             new StudentB() { Idd = 2, Name = "Vijay" },

              new StudentB() { Idd = 3, Name = "sanjay" }



            };

        //    List<MyClass> list2 = new List<MyClass>
        //{
        //    new MyClass { Id = 2, Value = 20 },
        //    new MyClass { Id = 4, Value = 40 }
        //};


            /*

            var aLookup = lsa1.ToLookup(x => new {Id= x.Id });
            foreach (var bItem in lsa2)
            {
                foreach (var aItem in aLookup[new { Id=bItem.Idd }])
                    aItem.Name = bItem.Name;
            }

            */


            Parallel.ForEach(lsa1, item1 =>
            {
                // Search for matching item in list2
                StudentB matchingItem = lsa2.Find(item2 => item2.Idd == item1.Id);

                // If a matching item is found, update properties of item1
                if (matchingItem != null)
                {
                    item1.Name = matchingItem.Name; // Update Value property of MyClass instances
                                                      // You can update other properties here if needed
                }
            });

            return Ok(lsa1);

        }




        public class StudentA

        {
            public int Id{ get; set; }

            public string Name { get; set; }

            public int  RollNumber { get; set; }
        }

        public class StudentB

        {
            public int Idd { get; set; }

            public string Name { get; set; }
        }



    }
}
