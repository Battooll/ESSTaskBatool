using ESSTaskBatool.Data;
using ESSTaskBatool.Repos;
using ESSTaskBatool.ViewModels;
//using Microsoft.Azure.NotificationHubs;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Internal;

namespace ESSTaskBatool.Services
{
    public class TanentServices : ITanent
    {
        private readonly Tanent1Context _db;
        public TanentServices(Tanent1Context db)
        {
            _db = db;
        }
        public async Task<int> AddTanent(RegisterViewModel tanent)
        {
            int result = 0;
            await _db.Tanent1infos.AddAsync(tanent.tanent);
            await _db.SaveChangesAsync();
            result += 1;
            return result;
        }

        public async Task<int> DeleteTanent(int id)
        {
            int tanentID = (int)await _db.Tanent1infos.Where(f => f.Id == id).Select(f => f.Id).FirstOrDefaultAsync();
            int result = 0;
            //check if the dbcontext is not null
            if (_db != null)
            {
                // if not find the specified tanent
                var tanent = await _db.Tanent1infos.FirstOrDefaultAsync(p => p.Id == tanentID);

                //check the returned value is not null
                if (tanent != null)
                    // if it not null delete the specified tanent
                    _db.Tanent1infos.Remove(tanent);

                //commit the changes on database
                await _db.SaveChangesAsync();
                //if the operation succecced return 1
                result += 1;
            }
            return result;
        }
        public async Task<List<Tanent1info>> GetAllTanent()
        {
            //check if the dbcontext is not null
            if (_db != null)
            {
                // if not find all tanents
                return await _db.Tanent1infos.ToListAsync();
            }

            return null;
        }

        public async Task<Tanent1info> GetTanent(int id)
        {
            int tanentId = (int)_db.Tanent1infos.Where(f => f.Id == id).Select(f => f.Id).FirstOrDefault();
            if (_db != null)
            {
                //get all factories
                return await _db.Tanent1infos.FirstOrDefaultAsync(f => f.Id == tanentId);
            }
            return null;
        }

        public async Task<int> UpdateTanent(int id, Tanent1info tanent)
        {
            int tanentId = (int)_db.Tanent1infos.Where(f => f.Id == id).Select(f => f.Id).FirstOrDefault();
            int result = 0;
            //check if the dbcontext is not null
            if (_db != null)
            {
                //if not create a new object from  specific factory 
                Tanent1info existtanent = await _db.Tanent1infos.Where(f => f.Id == id).FirstOrDefaultAsync();

                //check if the returned object  is not null
                if (existtanent != null)
                {

                    existtanent.Name = tanent.Name;
                    existtanent.Phone = tanent.Phone;
                    existtanent.Email = tanent.Email;
                    existtanent.Address = tanent.Address;
                }
                //edit the  returned object with new changes
                _db.Tanent1infos.Update(existtanent);

                //commit changes
                await _db.SaveChangesAsync();
                result += 1;
                return result;

            }
            return result;
        }
        public async Task<RegistrationResult> RegisterAsync(RegisterViewModel model)
        {
            // Check if the username is unique
            if (_db.Tanent1infos.Any(u => u.Username == model.tanent.Username))
            {
                return new RegistrationResult(false, new List<string> { "Username already exists." });
            }

            // Create a new tenant and user
            var tenant = new Tanent1info { Name = model.tanent.Username };
            var user = new Tanent1user
            {
                Username = model.Username,
                Password = model.Password,
                Tanent = tenant
            };

            try
            {
                // Create a new schema/database for the tanent
                var connectionString = $"Server=localhost;Database={model.tanent.Name}Db;User=Username;Password=Password;";
                //using (var temporaryDbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql")).Options))
                //{
                //    await temporaryDbContext.Database.MigrateAsync();
                //}

                // Save the tenant and user to the main database
                _db.Tanent1infos.Add(tenant);
                _db.Tanent1users.Add(user);
                await _db.SaveChangesAsync();

                return new RegistrationResult(true, null);
            }
            catch (Exception ex)
            {
                // Handle database creation or save errors
                return new RegistrationResult(false, new List<string> { $"Registration failed: {ex.Message}" });
            }
        }
    }
}
