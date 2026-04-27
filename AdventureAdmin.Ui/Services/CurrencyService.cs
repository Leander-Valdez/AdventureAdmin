using AdventureAdmin.Data.Context;
using AdventureAdmin.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdventureAdmin.Ui.Services;

public class CurrencyService : Aplicada1.Core.IService<Currency, string>
{
    public async Task<bool> Guardar(Currency entidad)
    {
        using var context = new AdventureWorksContext();
        context.Currencies.Add(entidad);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<Currency?> Buscar(string id)
    {
        using var context = new AdventureWorksContext();
        return await context.Currencies
            .FirstOrDefaultAsync(c => c.CurrencyCode == id);
    }

    public async Task<bool> Eliminar(string id)
    {
        using var context = new AdventureWorksContext();
        var existe = await context.Currencies
            .FirstOrDefaultAsync(c => c.CurrencyCode == id);

        if (existe == null) return false;

        context.Currencies.Remove(existe);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<List<Currency>> GetList(Expression<Func<Currency, bool>> criterio)
    {
        using var context = new AdventureWorksContext();
        return await context.Currencies
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> Actualizar(Currency entidad)
    {
        using var context = new AdventureWorksContext();
        context.Entry(entidad).State = EntityState.Modified;
        return await context.SaveChangesAsync() > 0;
    }
}