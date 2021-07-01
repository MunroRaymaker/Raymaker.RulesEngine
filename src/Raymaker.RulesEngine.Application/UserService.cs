﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raymaker.RulesEngine.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly UserValidator userValidator;

        public UserService() : this(new UserRepository()) { }

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.userValidator = new UserValidator();
        }

        public User GetUser(string userName)
        {
            return this.userRepository.GetUser(userName);
        }

        public bool UpdateUser(User user)
        {
            if (!userValidator.HasValidEmail(user.Email)) return false;

            return this.userRepository.UpdateUser(user);
        }
    }
}