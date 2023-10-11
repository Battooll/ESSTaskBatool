using ESSTaskBatool.Data;
using ESSTaskBatool.Repos;
using ESSTaskBatool.ViewModels;
//using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System;
using Microsoft.Azure.NotificationHubs;
using Microsoft.AspNetCore.Identity;

namespace ESSTaskBatool.Services
{
    public class UserServices : Repos.IUser
    {
        private readonly Tanent1Context _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserServices(Tanent1Context db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<int> AddUser(Tanent1user user)
        {
            int result = 0;
            await _db.Tanent1users.AddAsync(user);
            await _db.SaveChangesAsync();
            result += 1;
            return result;
        }

        public async Task<int> DeleteUser(int id)
        {
            int tanentID = (int)await _db.Tanent1users.Where(f => f.Id == id).Select(f => f.Id).FirstOrDefaultAsync();
            int result = 0;
            //check if the dbcontext is not null
            if (_db != null)
            {
                // if not find the specified tanent
                var user = await _db.Tanent1users.FirstOrDefaultAsync(p => p.Id == tanentID);

                //check the returned value is not null
                if (user != null)
                    // if it not null delete the specified tanent
                    _db.Tanent1users.Remove(user);

                //commit the changes on database
                await _db.SaveChangesAsync();
                //if the operation succecced return 1
                result += 1;
            }
            return result;
        }

        public async Task<List<Tanent1user>> GetAllUsers()
        {
            //check if the dbcontext is not null
            if (_db != null)
            {
                // if not find all users
                return await _db.Tanent1users.ToListAsync();
            }

            return null;
        }


        public async Task<Tanent1user> GetUser(int id)
        {
            int usertId = (int)_db.Tanent1users.Where(f => f.Id == id).Select(f => f.Id).FirstOrDefault();
            if (_db != null)
            {
                //get all factories
                return await _db.Tanent1users.FirstOrDefaultAsync(f => f.Id == usertId);
            }
            return null;
        }

        public async Task<int> UpdateUser(int id, Tanent1user user)
        {
            int userId = (int)_db.Tanent1users.Where(f => f.Id == id).Select(f => f.Id).FirstOrDefault();
            int result = 0;
            //check if the dbcontext is not null
            if (_db != null)
            {
                //if not create a new object from  specific factory 
                Tanent1user existUser = await _db.Tanent1users.Where(f => f.Id == id).FirstOrDefaultAsync();

                //check if the returned object  is not null
                if (existUser != null)
                {

                    existUser.Name = user.Name;
                    existUser.Phone = user.Phone;
                    existUser.Email = user.Email;
                    existUser.Address = user.Address;
                }
                //edit the  returned object with new changes
                _db.Tanent1users.Update(existUser);

                //commit changes
                await _db.SaveChangesAsync();
                result += 1;
                return result;

            }
            return result;
        }
        public async Task<AuthenticateResult> AuthenticateAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.user.Username);

            if (user == null)
            {
                return new AuthenticateResult(false, new List<string> { "Invalid username or password." });
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.user.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return new AuthenticateResult(true, null);
            }
            else
            {
                return new AuthenticateResult(false, new List<string> { "Invalid username or password." });
            }
        }


    }
}
