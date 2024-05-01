using CoreProject.DataAccessLayer.Models;
using CoreProject.InterfaceRepository;
using CoreProject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreProject.ImplementInterfaceRepsitory
{
    public class StudentRepository : IStudentRepository
    {
        BlogDbContext db = new BlogDbContext();
        public async Task<List<StudentBLL>> InnerJoinStudentDetail(int id)
        {

            List<int> ab = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
            string abc = string.Join(",", ab);

            List<StudentBLL> allItems = (from all in GetStudent()
                                         where all.StudentId == 1
                                         select new StudentBLL()
                                         {

                                             StudentId = all.StudentId,
                                             StudentName = all.StudentName,
                                             FatherName = all.FatherName,
                                             MotherName = all.MotherName,
                                             CourseId = all.CourseId,
                                             CourseName = all.CourseName,
                                             stdsemesterbll = (

                                                       from _StudentCourseMs in db.StudentCourseMs
                                                       join _PracAttemptMarks in db.PracAttemptMarks on _StudentCourseMs.StudentId equals _PracAttemptMarks.StudentId
                                                       join _PracSubMark in db.PracSubMarks on _PracAttemptMarks.PracSubMarkId equals _PracSubMark.PracSubMarkId
                                                       join _Subjects in db.Subjects on _PracSubMark.SubjectId equals _Subjects.SubjectId
                                                       where _PracAttemptMarks.StudentId == all.StudentId
                                                       select new SemesterMarkBLL()
                                                       {
                                                           SubjectId = _PracSubMark.SubjectId,
                                                           SubjectName = _Subjects.SubjectName
                                                           //Mark =_PracSubMark.Marks,
                                                           //AttemptMark=_PracAttemptMarks.PraAttemptSubMark
                                                       }).ToList(),

                                         }).ToList();
            return allItems;
        }


        public async Task<List<StudentBLL>> LeftJoinStudentDetail()
        {
            List<StudentBLL> allItems = (

                                        from _Student in db.Students
                                        join _StudentCourseMs in db.StudentCourseMs on _Student.StudentId equals _StudentCourseMs.StudentId
                                        into temp1
                                        from temp2 in temp1.DefaultIfEmpty()
                                        join _Courses in db.Courses on temp2.CourseId equals _Courses.CourseId
                                        into temp3
                                        from temp4 in temp3.DefaultIfEmpty()
                                        join _PracAttemptMarks in db.PracAttemptMarks on _Student.StudentId equals _PracAttemptMarks.StudentId
                                        into temp5
                                        from temp6 in temp5.DefaultIfEmpty()

                                        group new { _Student.StudentId, temp4.CourseId, temp6.PraAttemptSubMark }
                                          by new
                                          {
                                              _Student.StudentId,
                                              temp4.CourseId,
                                              temp4.CourseName

                                          } into g
                                        select new StudentBLL()
                                        {
                                            StudentId = g.Key.StudentId,
                                            CourseId = g.Key.CourseId,
                                            CourseName = g.Key.CourseName,
                                            TotalMarks = g.Sum(p => (p.PraAttemptSubMark == null ? 0 : p.PraAttemptSubMark)),
                                            //TotalMarks = db.PracAttemptMarks.Where(x => x.StudentId == _Student.StudentId).Sum(x => x.PraAttemptSubMark)

                                            stdaddressdetail = (from _StudentAddressDetail in db.StudentAddressDetails

                                                                where _StudentAddressDetail.StudentId == g.Key.StudentId
                                                                select new StudentAddressDetailBLL()
                                                                {
                                                                    StudentId = _StudentAddressDetail.StudentId,
                                                                    PinCode = _StudentAddressDetail.PinCode
                                                                }).ToList(),

                                            stdsemesterbll = (from _StudentCourse_M in db.StudentCourseMs
                                                              join _Course in db.Courses on _StudentCourse_M.CourseId equals _Course.CourseId
                                                              into temp7

                                                              from temp8 in temp7.DefaultIfEmpty()

                                                              join _Semester in db.Semesters on temp8.CourseId equals _Semester.CourseId

                                                              into temp9

                                                              from temp10 in temp9.DefaultIfEmpty()

                                                              join _Subject in db.Subjects on temp10.SemesterId equals _Subject.SemesterId

                                                               into temp11

                                                              from temp12 in temp11.DefaultIfEmpty()


                                                              join _ThSubMark in db.ThSubMarks on temp12.SubjectId equals _ThSubMark.SubjectId

                                                              into temp13

                                                              from temp14 in temp13.DefaultIfEmpty()


                                                              join _ThAttemptMarks in db.ThAttemptMarks

                                                              on new { ThSubMarkId = (int?)temp14.ThSubMarkId, StudentId = _StudentCourse_M.StudentId }
                                                              equals new { ThSubMarkId = _ThAttemptMarks.ThSubMarkId, StudentId = _ThAttemptMarks.StudentId }


                                                              //  on temp14.ThSubMarkId equals _ThAttemptMarks.ThSubMarkId 

                                                              //new
                                                              //{
                                                              //    temp14.ThSubMarkId
                                                              //} equals new { _ThAttemptMarks.ThSubMarkId }

                                                              into temp15

                                                              from temp16 in temp15.DefaultIfEmpty()

                                                              where _StudentCourse_M.StudentId == g.Key.StudentId

                                                              select new SemesterMarkBLL()
                                                              {
                                                                  SemesterId = 1

                                                              }).ToList()

                                        }).ToList();




            return allItems;
        }




        //public async Task<List<StudentBLL>> LeftJoinStudentDetail()
        //{
        //    List<StudentBLL> allItems = (

        //                                from _Student in db.Students
        //                                join _StudentCourseMs in db.StudentCourseMs on _Student.StudentId equals _StudentCourseMs.StudentId
        //                                into temp1
        //                                from temp2 in temp1.DefaultIfEmpty()
        //                                join _Courses in db.Courses on temp2.CourseId equals _Courses.CourseId
        //                                into temp3
        //                                from temp4 in temp3.DefaultIfEmpty()
        //                                join _PracAttemptMarks  in db.PracAttemptMarks  on _Student.StudentId equals _PracAttemptMarks.StudentId
        //                                into temp5
        //                                from temp6 in temp5.DefaultIfEmpty()

        //                                group new { _Student.StudentId, temp4.CourseId,temp6.PraAttemptSubMark }
        //                                  by new
        //                                  {
        //                                      _Student.StudentId,
        //                                       temp4.CourseId,
        //                                      temp4.CourseName

        //                                  } into g
        //                                select new StudentBLL()
        //                                {
        //                                    StudentId = g.Key.StudentId,
        //                                    CourseId = g.Key.CourseId,
        //                                    CourseName = g.Key.CourseName,
        //                                  //  TotalMarks= g.Sum(p => (p.PraAttemptSubMark == null ? 0 : p.PraAttemptSubMark)),
        //                                    //TotalMarks = db.PracAttemptMarks.Where(x => x.StudentId == _Student.StudentId).Sum(x => x.PraAttemptSubMark)
        //                                }).ToList();




        //    return allItems;
        //}

        public static List<StudentBLL> GetStudent()
        {
            BlogDbContext db = new BlogDbContext();
            List<StudentBLL> allItems = (from _Stdcrs in db.StudentCourseMs
                                             /*  where _Stdcrs.StudentId == id*/
                                         join _Student in db.Students on _Stdcrs.StudentId equals _Student.StudentId
                                         join _Course in db.Courses on _Stdcrs.CourseId equals _Course.CourseId
                                         select new StudentBLL()
                                         {
                                             StudentId = _Stdcrs.StudentId,
                                             StudentName = _Student.StudentName,
                                             FatherName = _Student.FatherName,
                                             MotherName = _Student.MotherName,
                                             CourseId = _Course.CourseId,
                                             CourseName = _Course.CourseName,

                                             stdaddressdetail = (from _StudentAddressDetail in db.StudentAddressDetails

                                                                 where _StudentAddressDetail.StudentId == _Stdcrs.StudentId
                                                                 select new StudentAddressDetailBLL()
                                                                 {
                                                                     StudentId = _StudentAddressDetail.StudentId,
                                                                     PinCode = _StudentAddressDetail.PinCode
                                                                 }).ToList(),
                                         }).ToList();

            return allItems;
        }

        public bool checkstudent()
        {
            var ab = db.Students.Where(x => x.StudentId == 1).Select(x => x.StudentId).FirstOrDefault();
            var ab1 = (from _std in db.Students where _std.StudentId == 1 select new { id = _std.StudentId }).FirstOrDefault();

            return true;

        }


        public async Task<List<StudentBLL>> ConditionTest(TestParam param)
        {

            List<int> ab = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
            string abc = string.Join(",", ab);

            List<StudentBLL> allItems = (from all in db.Students
                                         where

                                         (param.Action == "" || all.Action == param.Action) &&

                                         (param.Status == 0 || all.Status == param.Status)

                                         select new StudentBLL()
                                         {

                                             StudentId = all.StudentId,
                                             StudentName = all.StudentName,
                                             FatherName = all.FatherName,
                                             MotherName = all.MotherName,

                                         }).ToList();

            return allItems;
        }

        public async Task<List<StudentBLL>> GroupByFunc(TestParam param)
        {
            List<int> ab = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
            string abc = string.Join(",", ab);

            List<StudentBLL> allItems = (
                                        from _student in db.Students
                                            //join _StudentCourseMs in db.StudentCourseMs on  _student.StudentId equals

                                            //_StudentCourseMs.StudentId

                                            // into temp1
                                            // from temp2 in temp1.DefaultIfEmpty()

                                        join _ThAttemptMarks in db.ThAttemptMarks

                                        // //on new { StudentId = (int?)_StudentCourseMs.StudentId, StudentIdd = _StudentCourseMs.StudentId }
                                        // //equals new { StudentId = _ThAttemptMarks.StudentId, StudentIdd =  }
                                        on _student.StudentId equals _ThAttemptMarks.StudentId

                                        into temp3
                                        from temp4 in temp3.DefaultIfEmpty()


                                            //join _Semesters in db.Semesters
                                            //on _StudentCourseMs.CourseId equals _Semesters.CourseId

                                            // join _ThAttemptMarks in db.ThAttemptMarks
                                            //  on _student.StudentId equals _ThAttemptMarks.StudentId


                                            //  where _student.StudentId == 1


                                        group new {
                                            _student.StudentId,
                                            _student.StudentName,
                                            temp4.ThAttemptSubMark,

                                        }
                                         by new
                                         {
                                             _student.StudentId,
                                             _student.StudentName
                                             // _ThAttemptMarks.ThAttemptSubMark


                                         } into g


                                        select new StudentBLL()
                                        {

                                            StudentId = g.Key.StudentId,
                                            StudentName = g.Key.StudentName,

                                            TotalMarks = g.Sum(p => (p.ThAttemptSubMark == null ? 0 : p.ThAttemptSubMark)),
                                           // TotalTheorySubjectCount = (from _sub in db.ThAttemptMarks where _sub.StudentId == g.Key.StudentId select _sub.ThAttemptSubMark).Sum()

                                           TotalTheorySubjectCount = g.Count(p=>p.ThAttemptSubMark.HasValue)


                                        }).ToList();

            return allItems;
        }
    }
}
