using ImageProject.DataLayout.Modal;
using ImageProject.DataLayout.UnitOfWork;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class Bll
{

    private string GetNewId() => Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");

    //============================================= Category ==============================================
    public List<Category> CategoryGetFilter(Category category, Paging paging, ref int RowCount)
    {
        var predicat = PredicateBuilder.New<Category>();
        predicat.And(p => p.IsActive == true);
        using (GenericUoW uw = new GenericUoW())
        {
            return uw.Repository<Category>().GetAllFilter(predicat,paging,ref RowCount).ToList();
        }
    }
    public Category CategoryInsert(Category category)
    {
        category.CategoryId = GetNewId();
        using (GenericUoW uw = new GenericUoW())
        {
            uw.Repository<Category>().Insert(category);
        }
        return category;
    }


    //============================================= CategoryItem ==============================================

    public List<CategoryItem> CategoryItemGetFilter(CategoryItem categoryItem, Paging paging, ref int RowCount)
    {
        var predicat = PredicateBuilder.New<CategoryItem>();
        if (categoryItem != null)
        {
            predicat.And(p => p.CategoryItemId > 0);
        }
        paging.sortBy = paging.sortBy ?? "CategoryItemId";
        using (GenericUoW uw = new GenericUoW())
        {
            return uw.Repository<CategoryItem>().GetAllFilter(predicat,paging, ref RowCount).ToList();
        }
    }
    public List<CategoryItem> CategoryItemGetAll(ref int RowCount)
    {
        List<CategoryItem> list = null;
        using (GenericUoW uw = new GenericUoW())
        {
            list = uw.Repository<CategoryItem>().GetAll(ref RowCount).ToList();
        }
        return list;
    }
    
    public async Task<int> CategoryItemInsertAsync(CategoryItem categoryItem)
    {
        using (GenericUoW uw = new GenericUoW())
        {
            await uw.Repository<CategoryItem>().InsertAsync(categoryItem);
            await uw.SaveChangesAsync();
        }
        return categoryItem.CategoryItemId;
    }
    public async Task<int> CategoryItemUpdateAsync(CategoryItem categoryItem)
    {
        using (GenericUoW uw = new GenericUoW())
        {
            await uw.Repository<CategoryItem>().UpdateAsync(categoryItem);
            await uw.SaveChangesAsync();
        }
        return categoryItem.CategoryItemId;
    }
    public async Task<bool> CategoryItemDeleteAsync(int categoryItemId)
    {
        using (GenericUoW uw = new GenericUoW())
        {
           await uw.Repository<CategoryItem>().DeleteAsync(categoryItemId);
           await uw.SaveChangesAsync();
        }
        return true;
    }



}
