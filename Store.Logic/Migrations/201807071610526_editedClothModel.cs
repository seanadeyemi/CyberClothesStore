namespace ClassLibrary1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedClothModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clothes", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Clothes", new[] { "CategoryId" });
            RenameColumn(table: "dbo.Clothes", name: "CategoryId", newName: "Category_Id");
            AlterColumn("dbo.Clothes", "Category_Id", c => c.Int());
            CreateIndex("dbo.Clothes", "Category_Id");
            AddForeignKey("dbo.Clothes", "Category_Id", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clothes", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Clothes", new[] { "Category_Id" });
            AlterColumn("dbo.Clothes", "Category_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Clothes", name: "Category_Id", newName: "CategoryId");
            CreateIndex("dbo.Clothes", "CategoryId");
            AddForeignKey("dbo.Clothes", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
