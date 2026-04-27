using AdventureAdmin.Data.Context;
using AdventureAdmin.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class CountryRegionService : Aplicada1.Core.IService<CountryRegion, string>
{
    public async Task<bool> Guardar(CountryRegion entidad)
    {
        using var context = new AdventureWorksContext();
        context.CountryRegions.Add(entidad);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<CountryRegion?> Buscar(string id)
    {
        using var context = new AdventureWorksContext();
        return await context.CountryRegions
            .FirstOrDefaultAsync(c => c.CountryRegionCode == id);
    }

    public async Task<bool> Eliminar(string id)
    {
        using var context = new AdventureWorksContext();
        var existe = await context.CountryRegions
            .FirstOrDefaultAsync(c => c.CountryRegionCode == id);

        if (existe == null) return false;

        context.CountryRegions.Remove(existe);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<List<CountryRegion>> GetList(Expression<Func<CountryRegion, bool>> criterio)
    {
        using var context = new AdventureWorksContext();
        return await context.CountryRegions
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> Actualizar(CountryRegion entidad)
    {
        using var context = new AdventureWorksContext();
        context.Entry(entidad).State = EntityState.Modified;
        return await context.SaveChangesAsync() > 0;
    }
}