﻿using ClassLibrary1;
using Store.Domain;
using Store.Logic.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Logic
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        //private readonly ClothDbContext _DbContext;
        public CategoryRepository(ClothDbContext context) : base(context)
        {
            //_DbContext = context;
        }


        public IEnumerable<Category> Categories
        {
            get { return this.GetAll(); }
        }

    }
}
