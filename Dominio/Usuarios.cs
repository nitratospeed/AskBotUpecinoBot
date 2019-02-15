using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuisBot.Dominio
{
    public class LogIn
    {
        public string access_token { get; set; }
    }

    public class SignUp
    {
        public string access_token { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public string role { get; set; }
    }

    public class UsuarioObtenido
    {
        public string token { get; set; }
        public user user { get; set; }
    }

    public class user
    {
        public string name { get; set; }
    }

    public class UsuarioError
    {
        public string message { get; set; }
    }
}