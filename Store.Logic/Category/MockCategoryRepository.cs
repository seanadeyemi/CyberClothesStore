using Store.Domain;
using Store.Logic.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Logic
{
    public class MockCategoryRepository : MockRepository<ICategory>, ICategoryRepository
    {
        public IEnumerable<ICategory> Categories { get
            { return new List<Category>
        {
            new Category
            {
                Id = 1,
                Name = "African Batik",
                 Description = "Exquisite African designs of Batik",
                 Clothes = new List<Cloth>
                 {
                      new Cloth
                        {
                             Id = 1,
                             Name = "Ankara",
                             Price = 300,
                             ShortDescription = "Bird and floral patterns",
                             LongDescription = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like)."

                        }
                        
                 }
            },
                new Category
            {
                Id = 2,
                Name = "African Tie Dye",
                 Description = "Exquisite African designs of Tie Dye",
                 Clothes = new List<Cloth>
                 {
                      new Cloth
                        {
                             Id = 1,
                             Name = "Ankara",
                             Price = 300,
                             ShortDescription = "Bird and floral patterns",
                             LongDescription = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like)."

                        }
                        
                 }
            },
            new Category
            {
                 Id = 3,
                Name = "European Wears",
                 Description = "Exquisite European designs",
                 Clothes = new List<Cloth>
                 {
                      new Cloth
                        {
                             Id = 3,
                             Name = "Silk",
                             Price = 300,
                             ShortDescription = "Bird and floral patterns",
                             LongDescription = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like)."

                        },
                        new Cloth
                        {
                             Id = 4,
                             Name = "Wool",
                             Price = 500,
                             ShortDescription = "Bird and floral patterns",
                             LongDescription = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc."

                        }
                 }
            }

        };

            }

        }
    }
}
