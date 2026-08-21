using Infrastructure.DataBS;
using Infrastructure.DataCad;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfrastructureIntegrationTests
{
    public class TestCadNotasFixture
    {
        public ContextCad ContextCad { get; }

        public TestCadNotasFixture()
        {

            var options =new DbContextOptionsBuilder<ContextCad>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            ContextCad = new ContextCad(options);
        }

    }
}
