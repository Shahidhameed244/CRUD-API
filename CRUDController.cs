using CRUD_Web_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRUD_Web_Api.Controllers
{
    public class CRUDController : ApiController
    {
        CRUDEntities db = new CRUDEntities();
        [HttpGet]
        public HttpResponseMessage GetAllUsers()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var query = db.USERS.Select(e => new
            {
                id = e.ID,
                name = e.NAME,
                email = e.EMAIL,
                password = e.PASSWORD,
                role = e.ROLE
            });
            var result = query.ToList();
            return Request.CreateResponse(HttpStatusCode.OK,result);
        }

        [HttpPost]
        public HttpResponseMessage InsertNewRecord(string name,string email,string password,int role)
        {
            if (db.USERS.Any(e => e.EMAIL.Equals(email)))
            {
                return Request.CreateResponse(HttpStatusCode.OK,"Already User Added");
            }
            else
            {
                USER user = new USER();
                user.NAME = name;
                user.EMAIL = email;
                user.PASSWORD = password;   
                user.ROLE = role;  
                db.USERS.Add(user);
                db.SaveChanges();   
                return Request.CreateResponse(HttpStatusCode.OK,"New User Added");
            }
            
        }
        [HttpPut]
        public HttpResponseMessage UpdateUser(int id,string username)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var query = db.USERS.Where(e => e.ID == id).FirstOrDefault();
            if (query != null)
            {
                query.NAME = username;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Name Updated");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Please Enter A Valid Id");
            }
        }
        [HttpDelete]
        public HttpResponseMessage DeleteUser(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var query = db.USERS.Where(e => e.ID == id).FirstOrDefault();
            if (query != null)
            {
                db.USERS.Remove(query);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Record Deleted");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Please Enter A Valid Id");
            }
        }

    }
}
