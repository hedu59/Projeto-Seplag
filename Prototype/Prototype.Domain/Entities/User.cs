using Prototype.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Domain.Entities
{
    public class User : Entity
    {

        public User(string login, string password, string email)
        {
            this.Login = login;
            this.Password = password;
            this.Email = email;

        }

        public string Login { get;  private set; }
        public string Email { get;  private set; }
        public string Password { get; private set; }

        public void UpdateUser(string login, string email, string password)
        {
            Login = login;
            Email = email;
            Password = password;
        }
    }

}
