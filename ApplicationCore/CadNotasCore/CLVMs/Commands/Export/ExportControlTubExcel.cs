using ApplicationCore.Interfaces;
using ClosedXML.Excel;
using Domain.Entities.EntitiesCad;
using MediatR;
using System.Data;

namespace ApplicationCore.CadNotasCore.CLVMs.Commands.Export
{
    public record ExportControlTubExcel(int Mascara) : IRequest<byte[]>;

    public class ExportControlTubExcelCommandHandler(IRepositoryCad<EntityClvm> repos) : IRequestHandler<ExportControlTubExcel, byte[]>
    {
        private readonly IRepositoryCad<EntityClvm> _repository = repos;
        public async Task<byte[]> Handle(ExportControlTubExcel req, CancellationToken cancellationToken)
        {

            var items = await _repository.ListAsync(new ExportSpecification(req.Mascara), cancellationToken);
            
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("NUMERO_RIR");
            dataTable.Columns.Add("ITEM_RIR");
            dataTable.Columns.Add("DATA_RIR");
            dataTable.Columns.Add("ORDEM_COMPRA");
            dataTable.Columns.Add("FORNECEDOR");
            dataTable.Columns.Add("NOTA_FISCAL");
            dataTable.Columns.Add("CODIGO_MATERIAL");
            dataTable.Columns.Add("DIAMETRO_1");
            dataTable.Columns.Add("DIAMETRO_2");
            dataTable.Columns.Add("CERTIFICADO");
            dataTable.Columns.Add("CORRIDA");
            dataTable.Columns.Add("CODIGO_INTERNO_MATERIAL");
            dataTable.Columns.Add("QUANTIDADE");
            dataTable.Columns.Add("CLM");
            dataTable.Columns.Add("RNC");
            dataTable.Columns.Add("INSPETOR");
            dataTable.Columns.Add("NUMERO_CERTIFICACAO_INSPETOR");
            dataTable.Columns.Add("TESTE_IMPACTO");
            dataTable.Columns.Add("CONTRATOS");
            dataTable.Columns.Add("SITES");
            dataTable.Columns.Add("STATUS");
            dataTable.Columns.Add("OBS");

            foreach (var model in items)
            {
                DataRow Row = dataTable.NewRow();

                Row["NUMERO_RIR"] = model.NumeroRIR;
                Row["ITEM_RIR"] = model.Item;
                Row["DATA_RIR"] = model.DtInspecao;
                Row["ORDEM_COMPRA"] = "";
                Row["FORNECEDOR"] = model.Fornecedor;
                Row["NOTA_FISCAL"] = model.NotaFiscal;
                Row["CODIGO_MATERIAL"] = model.CodigoMaterial;
                Row["DIAMETRO_1"] = model.Diametro1;
                Row["DIAMETRO_2"] = model.Diametro2;
                Row["CERTIFICADO"] = model.Certificado;
                Row["CORRIDA"] = model.Corrida;
                Row["CODIGO_INTERNO_MATERIAL"] = model.CodigoInternoMaterial;
                Row["QUANTIDADE"] = model.Quantidade;
                Row["CLM"] = "";
                Row["RNC"] = "";
                Row["INSPETOR"] = model.Inspetor;
                Row["NUMERO_CERTIFICACAO_INSPETOR"] = model.NumeroCertificadoInspetor;
                Row["TESTE_IMPACTO"] = "";
                Row["CONTRATOS"] = model.Contrato;
                Row["SITES"] = model.Fabricante;
                Row["STATUS"] = model.StatusName;
                Row["OBS"] = model.Obs;


                dataTable.Rows.Add(Row);
            }

            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("ControlTub");
            worksheet.Cell(1, 1).InsertTable(dataTable);
            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();

        }
    }
}
