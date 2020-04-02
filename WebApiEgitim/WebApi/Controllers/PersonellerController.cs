using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.DataModel;
using WebApi.Filters;
using WebApi.Helper;
using WebApi.Models;

namespace WebApi.Controllers
{
    [RoutePrefix("api/Personeller"),Authorize]
    public class PersonellerController : ApiController
    {
        NorthwindEntities db = new NorthwindEntities();
        HttpResponseMessage responseMessage = new HttpResponseMessage();


        [Route("SelectPersonellerAll"),HttpGet]
        public HttpResponseMessage SelectPersonellerAll()
        {
            List<PersonellerDTO> personeller = PersonellerDTO.ConvertToPersonelDTOs(db.Personeller.Where(k => k.IsActive == true).ToList());
            if (personeller == null)
            {
                responseMessage = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Personel listesi boş geldi.");
            }
            else
            {
                responseMessage = Request.CreateResponse(HttpStatusCode.OK, personeller);
                responseMessage.Headers.Location = new Uri(Url.Link("GetById", new { id = personeller[0].PersonelID, crudstatus = CrudeStatusCode.Select }));
            }

            return responseMessage;
        }



        [Route("SelectPersoneller/{id:int}", Name = "GetById"),HttpGet]
        public HttpResponseMessage SelectPersoneller(int id)
        {
            Personeller personel = db.Personeller.Where(k => k.PersonelID == id).FirstOrDefault();

            if (personel == null)
            {
                responseMessage = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Personel boş geldi.");
            }
            else
            {
                responseMessage = Request.CreateResponse(HttpStatusCode.OK,PersonellerDTO.ConvertToPersonelDTO(personel));
                responseMessage.Headers.Location = new Uri(Url.Link("GetById", new { id = personel.PersonelID, crudstatus = CrudeStatusCode.Select }));
            }
            
            return responseMessage;
        }



        [Route("AddPersoneller"), PersonelAddValidation, HttpPost]
        public HttpResponseMessage AddPersoneller(PersonellerDTO pdto)
        {
            Personeller yenipersonel = PersonellerDTO.ConvertToPersonel(pdto);

            if(yenipersonel == null)
            {
                responseMessage = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Personel boş geldi");
            }

            try
            {
                db.Personeller.Add(yenipersonel);
                db.SaveChanges();
                responseMessage = Request.CreateResponse(HttpStatusCode.Created, PersonellerDTO.ConvertToPersonelDTO(yenipersonel));
                responseMessage.Headers.Location = new Uri(Url.Link("GetById", new { id = yenipersonel.PersonelID, crudstatus = CrudeStatusCode.Insert }));
            }
            catch (Exception ex)
            {
                responseMessage = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Yeni personel kaydı gerçekleştirilemedi");
            }
            return responseMessage;
        }



        [Route("UpdatePersoneller"), PersonelAddValidation, HttpPost]
        public HttpResponseMessage UpdatePersoneller(PersonellerDTO pdto)
        {
            
            Personeller personel = db.Personeller.Where(k => k.PersonelID == pdto.PersonelID).FirstOrDefault();

            if (personel == null)
            {
                responseMessage = Request.CreateErrorResponse(HttpStatusCode.NotFound, "İlgili personel bulunamadı.");
            }

            try
            {
                PersonellerDTO.ConvertToPersonel(pdto, personel);
                db.SaveChanges();
                responseMessage = Request.CreateResponse(HttpStatusCode.OK, pdto);
                responseMessage.Headers.Location = new Uri(Url.Link("GetById", new { id = personel.PersonelID, crudstatus = CrudeStatusCode.Update }));
            }

            catch (Exception ex)
            {
                responseMessage = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }


            return responseMessage;
        }

        [Route("DeletePersoneller"),HttpPost]
        public HttpResponseMessage DeletePersoneller(PersonellerDTO pdto)
        {
            Personeller personel = db.Personeller.Where(k => k.PersonelID == pdto.PersonelID).FirstOrDefault();

            if (personel == null)
            {
                responseMessage =  Request.CreateErrorResponse(HttpStatusCode.NotFound, "İlgili personel bulunamadı.");
            }

            try
            {
                personel.IsActive = false;
                db.SaveChanges();
                responseMessage = Request.CreateResponse(HttpStatusCode.OK, pdto);
                responseMessage.Headers.Location = new Uri(Url.Link("GetById", new { id = personel.PersonelID, crudstatus = CrudeStatusCode.Delete }));
            }

            catch (Exception ex)
            {
                responseMessage = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }


            return responseMessage;
        }

    }
}
