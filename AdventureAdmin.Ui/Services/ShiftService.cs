using AdventureAdmin.Data.Context;
using AdventureAdmin.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class ShiftService : Aplicada1.Core.IService<Shift, int>
{
    public async Task<bool> Guardar(Shift entidad)
    {
        using var context = new AdventureWorksContext();
        context.Shifts.Add(entidad);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<Shift?> Buscar(int id)
    {
        using var context = new AdventureWorksContext();
        return await context.Shifts
            .FirstOrDefaultAsync(s => s.ShiftId == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        using var context = new AdventureWorksContext();
        var existe = await context.Shifts
            .FirstOrDefaultAsync(s => s.ShiftId == id);

        if (existe == null) return false;

        context.Shifts.Remove(existe);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<List<Shift>> GetList(Expression<Func<Shift, bool>> criterio)
    {
        using var context = new AdventureWorksContext();
        return await context.Shifts
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> Actualizar(Shift entidad)
    {
        using var context = new AdventureWorksContext();
        context.Entry(entidad).State = EntityState.Modified;
        return await context.SaveChangesAsync() > 0;
    }
}