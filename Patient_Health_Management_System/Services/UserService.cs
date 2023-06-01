﻿namespace Patient_Health_Management_System.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User> GetUserByUserId(string userId)
        {
            try
            {
                return await _userRepo.GetUserByUserId(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<User> CreateUser(UserForm userForm, string userId)
        {
            try
            {
                var uei = new User()
                {
                    UserId = userId,
                    Name = userForm.Name,
                    Email = userForm.Email,
                    Address = userForm.Address,
                    PhoneNumber = userForm.PhoneNumber,
                    CreatedBy = userId,
                    CreatedAt = DateTime.Now,
                    UpdatedBy = null,
                    UpdatedAt = DateTime.Parse(DefaultVariable.UpdatedAt),
                    IsDeleted = false,
                    DeletedBy = null,
                    DeletedAt = DateTime.Parse(DefaultVariable.DeletedAt)
                };
                return await _userRepo.CreateUser(uei);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateUserByUserId(UserForm userForm, string userId)
        {
            try
            {
                var uei = await _userRepo.GetUserByUserId(userId);
                if (uei == null)
                {
                    throw new Exception("Extra info not found");
                }
                else
                {
                    uei.Name = userForm.Name;
                    uei.Email = userForm.Email;
                    uei.Address = userForm.Address;
                    uei.PhoneNumber = userForm.PhoneNumber;
                    uei.UpdatedBy = userId;
                    uei.UpdatedAt = DateTime.Now;
                    await _userRepo.ModifyUserByUserId(uei);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteUserByUserId(string userId)
        {
            try
            {
                var uei = await _userRepo.GetUserByUserId(userId);
                if (uei == null)
                {
                    throw new Exception("Extra info not found");
                }
                else
                {
                    uei.IsDeleted = true;
                    uei.DeletedBy = userId;
                    uei.DeletedAt = DateTime.Now;
                    await _userRepo.ModifyUserByUserId(uei);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
