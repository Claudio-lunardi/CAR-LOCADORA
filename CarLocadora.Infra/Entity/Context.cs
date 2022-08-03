using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Infra.Entity
{
    public class Context : DbContext
    {

        public Context(DbContextOptions<Context> options) : base(options)
        {







        }



    }
}
