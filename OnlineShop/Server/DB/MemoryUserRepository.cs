using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Shared;

namespace OnlineShop.Server.DB
{
    //Класс для хранения зарегистрированных пользователей в списке в оперативной памяти
    public class MemoryUserRepository : IUserRepository
    {
        private List<User> _users;

        //Пусть с самого начала будет существовать пользователь с логином 123 и паролем 123
        public MemoryUserRepository()
        {
            _users = new List<User>() { new User() { Login = "123", Password = "123" } };
        }

        //Функция позволяет проверить существование пользователя в БД и вернуть полные сведения о пользователе
        public User Login(UserCredentials credentials)
        {
            var user = _users.Find(usr => usr.Login == credentials.Login && usr.Password == credentials.Password);
            return user;
        }

        //Функция позволяет зарегистрировать пользователя в системе. Возвращает null, если пользователь с таким логином уже существует
        public User Register(UserCredentials credentials)
        {
            if (!UserExist(credentials.Login))
            {
                var user = new User() 
                { 
                    Login = credentials.Login, 
                    Password = credentials.Password,
                    Registered = DateTime.Now    
                };
                _users.Add(user);
                return user;
            }
            return null;
        }

        //Функция позволяет проверить существование пользователя по заданному логину login в системе
        public bool UserExist(string login) =>
            _users.Find(usr => usr.Login == login) != null;
    }
}
