using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyCollegeMVC.Data;
using MyCollegeMVC.Models;

namespace MyCollegeMVC.Controllers
{
    public class CollegeController : Controller
    {
        //static IList<College> CollegeList = new List<College>
        //{
        //new College() { Id = 1, Name = "Poornima", Type = "Private" }
        //};

        private readonly CollegeDbContext collegeDbContext;

        public CollegeController(CollegeDbContext collegeDbContext)
        {
            this.collegeDbContext = collegeDbContext;
        }


        public IActionResult Index()

        {
            //List<College> list = new List<College>();
            //College college = new College();
            //college.Name = "Poornima";
            //college.Type = "Private";
            //college.Id = 1;
            //list.Add(college);
           // return View(collegeDbContext.CollegeData.ToList());
            return View(collegeDbContext.CollegeData.FromSqlRaw("Getallcollege").ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddCollege addCollege)
        {
            var college = new College()
            {
                Id = addCollege.Id,
                Name = addCollege.Name,
                Type = addCollege.Type
            };
            await collegeDbContext.CollegeData.AddAsync(college);
            await collegeDbContext.SaveChangesAsync();
            //studentList.Add(student);
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public ActionResult Edit(int Id)
        //{

        //    var college = collegeDbContext.CollegeData.Where(s => s.Id == Id).FirstOrDefault();

        //    // update student to the database

        //    return View(college);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Edit(UpdateCollege update)
        //{
        //    //update student in DB using EntityFramodel ework in real-life application

        //    //update list by remodel oving old student and adding updated student for demodel o purpose
        //    var college = await collegeDbContext.CollegeData.FindAsync(update.Id);
        //    if (college!= null)
        //    {
        //        college.Name = update.Name;
        //        college.Type = update.Type;
        //        await collegeDbContext.SaveChangesAsync();
        //    }
        //    return RedirectToAction("Index");
        //    // var student = studentDbContext.StudentInformation.Where(s => s.Id == update.Id).FirstOrDefault();
        //    //studentDbContext.StudentInformation.Remove(student);
        //    //studentDbContext.StudentInformation.Add(update);


        //}

//---------------------------------------------------------------------------------------------------------
        
        //[HttpGet]
        //public async Task<ActionResult> Edit(int Id)
        //{
        //    //here, get the student from the database in the real application

        //    //getting a student from collection for demo purpose
        //    //var std = shopList.Where(s => s.ProductId == ProductId).FirstOrDefault();
        //    var college = await collegeDbContext.CollegeData.FirstOrDefaultAsync(s => s.Id == Id);
        //    if (college != null)
        //    {
        //        var college1 = new UpdateCollege()
        //        {
        //            Id = college.Id,
        //            Name = college.Name,
        //            Type = college.Type,
        //        };
        //        return View(college1);
        //    }
        //    return RedirectToAction("Index");

        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(UpdateCollege update)

        //{

        //    var college = await collegeDbContext.CollegeData.FindAsync(update.Id);
        //    if (college != null)
        //    {
        //        college.Id = update.Id;
        //        college.Type = update.Type;
        //        college.Name = update.Name;


        //        await collegeDbContext.SaveChangesAsync();

        //    }
        //    return RedirectToAction("Index");
        //}
        
        public IActionResult Edit(int Id)
        {
            return View(collegeDbContext.CollegeData.FromSqlRaw($"Getsinglecollege {Id}").AsEnumerable().FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, string Name)
        {
            var param = new SqlParameter[]
            {
                new SqlParameter()
                {
                    ParameterName = "@Name",
                    SqlDbType=System.Data.SqlDbType.VarChar,
                    Value=Name
                },
                new SqlParameter()
                {
                    ParameterName = "@Id",
                    SqlDbType=System.Data.SqlDbType.Int,
                    Value=Id
                }
            };

            var Updatecollege = await collegeDbContext.Database.ExecuteSqlRawAsync($"Exec Updatecollege @Id, @Name", param);
            if(Updatecollege == 1)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Details(College clg)
        {
            var college = collegeDbContext.CollegeData.Where(s => s.Id == clg.Id).First();
            return View(college);
        }

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var college = await collegeDbContext.CollegeData.FirstAsync(s => s.Id == Id);
            if (college == null)
            {
                return NotFound();
            }

            return View(college);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            var college = await collegeDbContext.CollegeData.FindAsync(Id);
            {

                collegeDbContext.CollegeData.Remove(college);
                await collegeDbContext.SaveChangesAsync();

            }
            return RedirectToAction("Index");
            //var student = studentDbContext.StudentInformation.Where(s => s.Id == Id).First();
            //studentDbContext.StudentInformation.Remove(student);
            //return RedirectToAction("Index");
        }

        public ActionResult Get()
        {
            return View(collegeDbContext.DepartmentData.ToList());
        }
    }
}
