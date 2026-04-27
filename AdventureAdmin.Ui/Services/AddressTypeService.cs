using AdventureAdmin.Data.Context;
using AdventureAdmin.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class AddressTypeService : Aplicada1.Core.IService<AddressType, int>
{
    public async Task<bool> Guardar(AddressType entidad)
    {
        using var context = new AdventureWorksContext();
        context.AddressTypes.Add(entidad);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<AddressType?> Buscar(int id)
    {
        using var context = new AdventureWorksContext();
        return await context.AddressTypes
            .FirstOrDefaultAsync(a => a.AddressTypeId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        using var context = new AdventureWorksContext();
        var existe = await context.AddressTypes
            .FirstOrDefaultAsync(a => a.AddressTypeId == id);

        if (existe == null) return false;

        context.AddressTypes.Remove(existe);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<List<AddressType>> GetList(Expression<Func<AddressType, bool>> criterio)
    {
        using var context = new AdventureWorksContext();
        return await context.AddressTypes
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> Actualizar(AddressType entidad)
    {
        using var context = new AdventureWorksContext();
        context.Entry(entidad).State = EntityState.Modified;
        return await context.SaveChangesAsync() > 0;
    }
}