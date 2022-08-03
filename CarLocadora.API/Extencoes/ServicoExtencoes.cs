﻿using CarLocadora.Infra.Entity;
using Microsoft.EntityFrameworkCore;

namespace CarLocadora.API.Extencoes
{
    public static class ServicoExtencoes
    {
        public static void ConfigurarServicos(this IServiceCollection services)
        {
            string connectionString = "Data Source=localhost,1434;User ID=sa;Password=senha@1234xxxY;Initial Catalog=DBCarLocadora;";
            services.AddDbContext<EntityContext>(item => item.UseSqlServer(connectionString));

        }
    }
}
