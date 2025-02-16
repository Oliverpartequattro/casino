using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CasinoSimulator
{
    public class User
    {
        //username;age;password;balance
        public string Username { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public int Balance { get; set; }


        public User(string row)
        {
            Username = row.Split(";")[0];
            Age = int.Parse(row.Split(";")[1]);
            Password = row.Split(";")[2];
            Balance = int.Parse(row.Split(";")[3]);
        }
        public User(string username, int age, string password, int balance)
        {
            Username = username;
            Age = age;
            Password = password;
            Balance = balance;
        }
    }
}
