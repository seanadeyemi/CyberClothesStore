using Store.DomainN;
using Store.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class UserRepository : Repository<User>, IUserRepository<IClothAppUser>
    {
        private readonly ClothDbContext _DbContext;
        public UserRepository(ClothDbContext context) : base(context)
        {
            _DbContext = context;
        }

        public List<IClothAppUser> UserList
        {
            get
            {
                
                return _DbContext.Users.ToList<IClothAppUser>();
                //return _DbContext.Users.Cast<IClothAppUser>().ToList();
            }
            
        }

    }
}
