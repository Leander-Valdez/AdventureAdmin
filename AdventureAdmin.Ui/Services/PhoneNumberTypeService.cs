using AdventureAdmin.Data.Context;
using AdventureAdmin.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class PhoneNumberTypeService : Aplicada1.Core.IService<PhoneNumberType, int>
{
    public async Task<bool> Guardar(PhoneNumberType entidad)
    {
        using var context = new AdventureWorksContext();
        context.PhoneNumberTypes.Add(entidad);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<PhoneNumberType?> Buscar(int id)
    {
        using var context = new AdventureWorksContext();
        return await context.PhoneNumberTypes
            .FirstOrDefaultAsync(p => p.PhoneNumberTypeId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        using var context = new AdventureWorksContext();
        var existe = await context.PhoneNumberTypes
            .FirstOrDefaultAsync(p => p.PhoneNumberTypeId == id);

        if (existe == null) return false;

        context.PhoneNumberTypes.Remove(existe);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<List<PhoneNumberType>> GetList(Expression<Func<PhoneNumberType, bool>> criterio)
    {
        using var context = new AdventureWorksContext();
        return await context.PhoneNumberTypes
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> Actualizar(PhoneNumberType entidad)
    {
        using var context = new AdventureWorksContext();
        context.Entry(entidad).State = EntityState.Modified;
        return await context.SaveChangesAsync() > 0;
    }
}