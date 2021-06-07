using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using HRPortal.WebApi.Extension;
using HRPortal.WebApi.Models;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRPortal.WebApi.Controllers
{
    [EnableCors()]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        OnboardingDBContext _dbContext;
        public EmployeesController(OnboardingDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<EmployeesController>/in-review
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return _dbContext.Employees;
        }
        [HttpGet]
        [Route("in-review")]
        public IEnumerable<EmployeeResponse> GetInReviewEmployees()
        {

            /*var code = _dbContext.StatusRefs
                            .Where(x => string.Equals(x.Value,"In Review"))
                                    .FirstOrDefault().Code;
            var empl = _dbContext.Employees.Where(e => e.Status.Equals(code)).ToList(); */
            var empl = from emp in _dbContext.Employees
                                        join st in _dbContext.StatusRefs
                                        on emp.Status equals st.Code
                                        join dep in _dbContext.DepartmentRefs
                                        on emp.Department equals dep.Code

                                        where st.Value.Equals("In Review")
                                        select new EmployeeResponse
                                        {
                                            Id = emp.Id,
                                            FirstName = emp.FirstName,
                                            LastName = emp.LastName,
                                            Department = dep.Value,
                                            Mobile = emp.Mobile,
                                            Status = st.Value,
                                            EmailId = emp.EmailId
                                        };


            return empl;



        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return _dbContext.Employees.SingleOrDefault(x => x.Id == id);
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public long Post([FromBody] Employee emp)
        {
            long retId = 0;
                if(emp.Id > 0)
            {
                var data = _dbContext.Employees.Where(e => e.Id.Equals(emp.Id)).FirstOrDefault();
                if (data == null)
                {
                    throw new Exception("employee not found");
                }
                emp.CopyProperties(data);
                _dbContext.SaveChanges();
                retId = emp.Id;
            }
            else
            {
                var _emp = new Employee();
                emp.CopyProperties(_emp);

                _dbContext.Employees.Add(_emp);
                _dbContext.SaveChanges();
                retId = _emp.Id;
            }
            return retId;
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            try
            {
                var emp = _dbContext.Employees.Where(r => r.Id.Equals(id)).FirstOrDefault();

                if (emp == null)
                    throw new Exception("no movie found");

                _dbContext.Employees.Remove(emp);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
