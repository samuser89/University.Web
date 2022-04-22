using System.Threading.Tasks;
using System.Web.Mvc;
using University.BL.Models;
using University.BL.Repositories;
using University.BL.Repositories.Implements;
using AutoMapper;
using System.Linq;
using University.BL.DTOs;
using System;
using University.BL.Controls;
using Newtonsoft.Json;

namespace University.Web.Controllers
{
    public class OfficeAssignmentController : Controller
    {
        private readonly IMapper mapper = MvcApplication.MapperConfiguration.CreateMapper();

        private readonly IOfficeAssignmentRepository officeAssignmentRepository = new OfficeAssignmentRepository(new UniversityModel());
        private readonly IInstructorRepository instructorRepository = new InstructorRepository(new UniversityModel());

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<ActionResult> IndexJson()
        {
            var officeAssignmentsModel = await officeAssignmentRepository.GetAll();
            var officeAssignmentsDTO = officeAssignmentsModel.Select(x => mapper.Map<OfficeAssignmentDTO>(x));
            return Json(officeAssignmentsDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetInstructors()
        {
            var instructorsModel = await instructorRepository.GetAll();
            var instructorsDTO = instructorsModel.Select(x => mapper.Map<InstructorDTO>(x));
            var instructorsSelect = instructorsDTO.Select(x => new SelectControl
            {
                Id = x.ID,
                Text = x.FullName

            });

            return Json(JsonConvert.SerializeObject(instructorsSelect), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return PartialView(new OfficeAssignmentDTO());
        }

        [HttpPost]
        public async Task<ActionResult> Create(OfficeAssignmentDTO officeAssignmentDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var officeAssignmentModel = mapper.Map<OfficeAssignment>(officeAssignmentDTO);
                    await officeAssignmentRepository.Insert(officeAssignmentModel);
                }

                return Json(new ResponseDTO
                {
                    Message = "The process is successful",
                    IsSuccess = true,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResponseDTO
                {
                    Message = ex.Message,
                    IsSuccess = false,
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var officeAssignmentModel = await officeAssignmentRepository.GetById(id);
            var officeAssignmentDTO = mapper.Map<OfficeAssignmentDTO>(officeAssignmentModel);
            return PartialView(officeAssignmentDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(OfficeAssignmentDTO officeAssignmentDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var officeAssignmentModel = mapper.Map<OfficeAssignment>(officeAssignmentDTO);
                    await officeAssignmentRepository.Update(officeAssignmentModel);
                }

                return Json(new ResponseDTO
                {
                    Message = "The process is successful",
                    IsSuccess = true,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResponseDTO
                {
                    Message = ex.Message,
                    IsSuccess = false,
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await officeAssignmentRepository.Delete(id);
                return Json(new ResponseDTO
                {
                    Message = "The process is successful",
                    IsSuccess = true,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResponseDTO
                {
                    Message = ex.Message,
                    IsSuccess = false,
                }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
