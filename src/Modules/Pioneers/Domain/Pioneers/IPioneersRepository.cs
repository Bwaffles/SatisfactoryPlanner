﻿namespace SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers
{
    /// <summary>
    ///     Handles the database access for the <see cref="Pioneer" /> through EntityFramework.
    /// </summary>
    public interface IPioneersRepository
    {
        Task AddAsync(Pioneer pioneer);

        Task<Pioneer> GetByIdAsync(PioneerId pioneerId);
    }
}