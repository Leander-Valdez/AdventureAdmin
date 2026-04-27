using AdventureAdmin.Data.Context;
using AdventureAdmin.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class ProductDescriptionService : Aplicada1.Core.IService<ProductDescription, int>
{
    public async Task<bool> Guardar(ProductDescription entidad)
    {
        using var context = new AdventureWorksContext();
        context.ProductDescriptions.Add(entidad);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<ProductDescription?> Buscar(int id)
    {
        using var context = new AdventureWorksContext();
        return await context.ProductDescriptions
            .FirstOrDefaultAsync(p => p.ProductDescriptionId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        using var context = new AdventureWorksContext();
        var existe = await context.ProductDescriptions
            .FirstOrDefaultAsync(p => p.ProductDescriptionId == id);

        if (existe == null) return false;

        context.ProductDescriptions.Remove(existe);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<List<ProductDescription>> GetList(Expression<Func<ProductDescription, bool>> criterio)
    {
        using var context = new AdventureWorksContext();
        return await context.ProductDescriptions
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> Actualizar(ProductDescription entidad)
    {
        using var context = new AdventureWorksContext();
        context.Entry(entidad).State = EntityState.Modified;
        return await context.SaveChangesAsync() > 0;
    }
}