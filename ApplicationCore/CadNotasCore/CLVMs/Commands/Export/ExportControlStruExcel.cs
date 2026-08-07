using ApplicationCore.Interfaces;
using ClosedXML.Excel;
using Domain.Entities.EntitiesCad;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApplicationCore.CadNotasCore.CLVMs.Commands.Export
{
    public record ExportControlStruExcel(int Mascara) : IRequest<byte[]>;

    public class ExportControlStruExcelCommandHandler(IRepositoryCad<CadClvm> repos) : IRequestHandler<ExportControlStruExcel, byte[]>
    {
        private readonly IRepositoryCad<CadClvm> _repository = repos;
        public async Task<byte[]> Handle(ExportControlStruExcel req, CancellationToken cancellationToken)
        {

            var items = await _repository.ListAsync(new ExportSpecification(req.Mascara), cancellationToken);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("CODIGO");
            dataTable.Columns.Add("ESPECIFICACAO_MATERIAL");
            dataTable.Columns.Add("TIPO_COMPONENTE");
            dataTable.Columns.Add("COMPRIMENTO");
            dataTable.Columns.Add("ESPESSURA");
            dataTable.Columns.Add("LARGURA");
            dataTable.Columns.Add("PESO");
            dataTable.Columns.Add("LVM");
            dataTable.Columns.Add("NUMERO_RIR");
            dataTable.Columns.Add("ITEM_RIR");
            dataTable.Columns.Add("CERTIFICADO");
            dataTable.Columns.Add("CORRIDA");
            dataTable.Columns.Add("SITE_FABRICANTE");
            dataTable.Columns.Add("QUANTIDADE_APROVADA");
            dataTable.Columns.Add("UNIDADE_MEDIDA");
            dataTable.Columns.Add("DATA_RECEBIMENTO");
            dataTable.Columns.Add("NOTA_FISCAL");
            dataTable.Columns.Add("LOTE");
            dataTable.Columns.Add("ORDEM_COMPRA");
            dataTable.Columns.Add("FORNECEDOR");
            dataTable.Columns.Add("CLM");
            dataTable.Columns.Add("RNC");
            dataTable.Columns.Add("NOME_INSPETOR");
            dataTable.Columns.Add("NUMERO_CERTIFICACAO_INSPETOR");
            dataTable.Columns.Add("STATUS");
            dataTable.Columns.Add("CODIGO_INTERNO_MATERIAL");
            dataTable.Columns.Add("MATERIAL_DESATIVADO");
            dataTable.Columns.Add("OBSERVACAO");

            foreach (var model in items)
            {
                DataRow errorRow = dataTable.NewRow();
                errorRow["CODIGO"] = model.CodigoStru;
                errorRow["ESPECIFICACAO_MATERIAL"] = model.EspecificacaoMaterial;
                errorRow["TIPO_COMPONENTE"] = model.TipoComponente;
                errorRow["COMPRIMENTO"] = model.Comprimento;
                errorRow["ESPESSURA"] = model.Espessura;
                errorRow["LARGURA"] = model.Largura;
                errorRow["PESO"] = model.Peso;
                errorRow["LVM"] = "";
                errorRow["NUMERO_RIR"] = model.NumeroRIR;
                errorRow["ITEM_RIR"] = model.Item;
                errorRow["CERTIFICADO"] = model.Certificado;
                errorRow["CORRIDA"] = model.Corrida;
                errorRow["SITE_FABRICANTE"] = "ANGRA YARD";
                errorRow["QUANTIDADE_APROVADA"] = model.Quantidade;
                errorRow["UNIDADE_MEDIDA"] = model.UMName == "N/A" ? string.Empty : model.UMName;
                errorRow["DATA_RECEBIMENTO"] = model.DtInspecao;
                errorRow["NOTA_FISCAL"] = model.NotaFiscal;
                errorRow["LOTE"] = "";
                errorRow["ORDEM_COMPRA"] = "";
                errorRow["FORNECEDOR"] = model.Fornecedor;
                errorRow["CLM"] = "";
                errorRow["RNC"] = "";
                errorRow["NOME_INSPETOR"] = "0";
                errorRow["NUMERO_CERTIFICACAO_INSPETOR"] = model.NumeroCertificadoInspetor;
                errorRow["STATUS"] = model.StatusName;
                errorRow["CODIGO_INTERNO_MATERIAL"] = "";
                errorRow["MATERIAL_DESATIVADO"] = "";
                errorRow["OBSERVACAO"] = model.Obs;

                dataTable.Rows.Add(errorRow);
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
