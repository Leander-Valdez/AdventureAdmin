using AdventureAdmin.Data.Context;
using AdventureAdmin.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class SpecialOfferService : Aplicada1.Core.IService<SpecialOffer, int>
{
    public async Task<bool> Guardar(SpecialOffer entidad)
    {
        using var context = new AdventureWorksContext();
        context.SpecialOffers.Add(entidad);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<SpecialOffer?> Buscar(int id)
    {
        using var context = new AdventureWorksContext();
        return await context.SpecialOffers
            .FirstOrDefaultAsync(s => s.SpecialOfferId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        using var context = new AdventureWorksContext();
        var existe = await context.SpecialOffers
            .FirstOrDefaultAsync(s => s.SpecialOfferId == id);

        if (existe == null) return false;

        context.SpecialOffers.Remove(existe);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<List<SpecialOffer>> GetList(Expression<Func<SpecialOffer, bool>> criterio)
    {
        using var context = new AdventureWorksContext();
        return await context.SpecialOffers
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> Actualizar(SpecialOffer entidad)
    {
        using var context = new AdventureWorksContext();
        context.Entry(entidad).State = EntityState.Modified;
        return await context.SaveChangesAsync() > 0;
    }
}