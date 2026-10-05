using Domain.Entities.EntitiesCad.ECadObra;
using Domain.Entities.EntitiesCad.ECadProjeto;
using System;
using System.Collections.Generic;
using System.Text;


namespace Domain.Entities.EntitiesCad.EObraVSProjeto
{
    public class ObraVSProjeto
    {
        public int IdObra { get; private set; }

        public int IdProjeto { get; private set; }

        public DateTime? DataRegistro { get; private set; }

        public EntityObra Obra { get; private set; } = null!;

        public EntityProjeto Projeto { get; private set; } = null!;

        private ObraVSProjeto() { }

        public ObraVSProjeto(int idObra, int idProjeto)
        {
            IdObra = idObra;
            IdProjeto = idProjeto;
        }

        public ObraVSProjeto(int idObra)
        {
            IdObra = idObra;
        }

    }
}
