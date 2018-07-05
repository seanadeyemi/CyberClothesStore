using Store.Domain;
using Store.Logic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store.Logic
{
    public class MockClothRepository : MockRepository<ICloth>, IClothRepository
    {

        MockCategoryRepository categoryRepository;

        List<Category> categoryList;

        public MockClothRepository() 
        {
            _collection = Clothes;
            categoryRepository = new MockCategoryRepository();
           // List<ICategory> cList;
            categoryList = new List<Category>();
           // cList = categoryRepository.Categories.ToList();
         //   cList.ForEach(c => { categoryList.Add((Category)c); });


        }

        public IEnumerable<ICloth> Clothes
        {
            get {
                categoryRepository = new MockCategoryRepository();

                return new List<Cloth>
            {
                new Cloth
                {
                     Id = 1,
                     Name = "Ankara #Style 1",
                     Price = 300,
                     ShortDescription = "Bird and floral patterns",
                     LongDescription = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).",
                      ImageThumbnailUrl = "ankara1.jpg",
                      ImageUrl = "ankara1.jpg",
                      InStock = 5,
                      Category =  categoryRepository.Categories.ElementAt(0),
                      IsFavorite = true
                          
                     
                },
                new Cloth
                {
                     Id = 2,
                     Name = "African Wax Batik #Style 1",
                     Price = 500,
                     ShortDescription = "Bird and floral patterns",
                     LongDescription = "Hand stamped batik created by applying wax with a carved stamp to a solid color base cloth. The wax forms a resist and the cloth is then over dyed. This process may be done multiple times, resulting in the cloth having many colors and design motifs. Each piece is a unique creation.",
                      ImageThumbnailUrl = "batik1.jpg",
                      ImageUrl = "batik1.jpg",
                      InStock = 22,
                      Category = categoryRepository.Categories.ElementAt(0),//categoryList[0],
                      IsFavorite = true
                }
                ,
                new Cloth
                {
                     Id = 3,
                     Name = "African Wax Batik #Style 2",
                     Price = 500,
                     ShortDescription = "100% cotton. Preshrunk, colorfast.",
                     LongDescription = "Hand stamped batik created by applying wax with a carved stamp to a solid color base cloth. The wax forms a resist and the cloth is then over dyed. This process may be done multiple times, resulting in the cloth having many colors and design motifs. Each piece is a unique creation. ",
                      ImageThumbnailUrl = "batik2.jpg",
                      ImageUrl = "batik2.jpg",
                      InStock = 15,
                      Category =  categoryRepository.Categories.ElementAt(0),
                      IsFavorite = true
                }
                ,
                new Cloth
                {
                     Id = 4,
                     Name = "Tie and Dye #Style 1",
                     Price = 500,
                     ShortDescription = "100% cotton Bazin jacquard. Preshrunk. Machine wash and dry.",
                     LongDescription = "This cloth begins as a solid colored piece which is then intricately folded. The folds are held in place by clamping, stitching or tying. The fold create a resist area where the original color of the cloth is preserved. The cloth is hand dipped into a dye bath, unfolded and retied or folded for another layer of color. Some pieces make as many as 5 trips to the dye bath before being rinsed, dried and ironed.",
                      ImageThumbnailUrl = "adire1.jpg",
                      ImageUrl = "adire1.jpg",
                      InStock = 32,
                      Category =  categoryRepository.Categories.ElementAt(1),
                      IsFavorite = true
                }
                ,
                new Cloth
                {
                     Id = 5,
                     Name = "Tie and Dye #Style 2",
                     Price = 500,
                     ShortDescription = "Ornamental patterns",
                     LongDescription = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.",
                      ImageThumbnailUrl = "tiedye1.jpg",
                      ImageUrl = "tiedye1.jpg",
                      InStock = 7,
                      Category =  categoryRepository.Categories.ElementAt(1),
                      IsFavorite = true
                }
                ,
                new Cloth
                {
                     Id = 6,
                     Name = "Kente #1",
                     Price = 200,
                     ShortDescription = "Diamond patterns",
                     LongDescription = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.",
                      ImageThumbnailUrl = "kente2.jpg",
                      ImageUrl = "kente2.jpg",
                      InStock = 14,
                      Category =  categoryRepository.Categories.ElementAt(0),
                      IsFavorite = true
                }
                 ,
                new Cloth
                {
                     Id = 7,
                     Name = "Kente #2",
                     Price = 400,
                     ShortDescription = "Diamond patterns",
                     LongDescription = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.",
                      ImageThumbnailUrl = "kente2.jpg",
                      ImageUrl = "kente2.jpg",
                      InStock = 9,
                      Category =  categoryRepository.Categories.ElementAt(0),
                      IsFavorite = true
                }



            }; }
        }

        public IEnumerable<ICloth> FavoriteClothes

        {
            get { return this.Find(c => c.IsFavorite == true); }


        }
        public IEnumerable<ICloth> GetCheapestClothes

        {
            get { return this.Find(c => c.Price < 1500).Take(6).OrderBy(c => c.Price); }


        }


    }
}
