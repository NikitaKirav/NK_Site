using Microsoft.EntityFrameworkCore;
using NK_Site.Interfaces;
using NK_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NK_Site.Data.Repository
{
    public class ConstantRepository : IConstant
    {
        private readonly ApplicationContext _db;

        public ConstantRepository(ApplicationContext db)
        {
            _db = db;
        }
        public IEnumerable<Constant> AllConstants => _db.Constants;
        public Constant GetObjectConstant(int constantId) => _db.Constants.FirstOrDefault(x => x.Id == constantId);

        public int? GetValueInt(string name)
        {
            try
            {
                string value = _db.Constants.FirstOrDefault(x => x.Name == name).Value;
                if (value != null)
                {
                    return Convert.ToInt32(value);
                }
            }
            catch { }
            return null;
        }

        public bool UpdateConstant(Constant constant)
        {
            _db.Entry(constant).State = EntityState.Modified;
            var result = _db.SaveChanges();
            return result == 1 ? true : false;
        }

        public bool CreateConstant(Constant constant)
        {
            _db.Constants.Add(constant);
            var result = _db.SaveChanges();
            return result == 1 ? true : false;
        }
    }
}
