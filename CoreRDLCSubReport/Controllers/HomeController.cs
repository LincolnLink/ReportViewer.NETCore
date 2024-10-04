using CoreRDLCSubReport.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Reporting.NETCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoreRDLCSubReport.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _wenHostEnvironment;

        public HomeController(ILogger<HomeController> logger,
            IWebHostEnvironment _wenHostEnvironment)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        //Configuração
        public IActionResult PrintReport()
        {
            string renderFormat = "PDF";
            string extension = "pdf";
            string mimetype = "application/pdf";
            using var report = new LocalReport();
            var dt = new DataTable();
            dt = GetEmployeeList();

            
            report.DataSources.Add(new ReportDataSource("dsEmployee", dt));

            var parameters = new[] { new ReportParameter("param1", "RDLC Sub-Report Example") };
            report.ReportPath = $"{this._wenHostEnvironment.WebRootPath}\\Reports\\rptEmployee.rdlc";
            report.SetParameters(parameters);

            var pdf = report.Render(renderFormat);
            return File(pdf, mimetype, "report." + extension);


            // DEFINIÇÃO DOS DADOS.
            //1° cria o arquivo "dsEmployee" depois disso cria uma tabela com o nome "dsEmployee", e cria as colunas, se for do tipo inteiro deve ser trocada.
            //2° esse script vai criar um vinculo com objeto tipo tabela o "dt" que já está alimentado com o "dsEmployee" que é o desgner da tabela.
            //3° depois de criar o arquivo "dsEmployee" que é do tipo XSD, vc deve arrastar para o projeto de cima.
            //4° cria a variavel parametrs, acima está a sintax passando uma key e valor.

            // DEFINIÇÃO DO LAYOUT DO ARQUIVO.
            //1° não consegi fazer essa etapa, acredito que seja por ser o VS 2022
            //2° cria o relatorio do tipo "RDLC" com o nome "rptEmployee.rdlc" no projeto de baixo e depois arrasta ele projeto de cima(cria uma copia).
            //3° no relatorio cria um novo datasets, da nome para ele , e informa o data source, se exibir as colunas é porq deu certo.
            //4° edita o layout: adiciona um header e footer, clicando fora do layout.
            //5° insere uma tabela, a propria tabela te da opção de valores, e coloca a coluna.
            //6° insere um textBox.
            // -- VOLTA A ESSE ARQUIVO DO C# PARA MAIS CONFIGURAÇÕES.
            //7° informa o "ReportPath", acredito que seja o local do arquivo.
            //8° configuração do SetParameters.
            //9° cria a configuração do tipo, para ele renderizar em pdf:  var pdf = report.Render(renderFormat);
            //10° retorna o pdf usando File();
            // -- VOLTA AO DOCUMENTO RDLC
            //11° adiciona um parameters, escola o mesmo nome que colocou no arquivo c#, nesse caso foi o "param1". e a opção  "Allow blank value('')", aperta ok.
            //12° você pode arrastar e soltar.


            //CRIANDO OUTRO DATASET.
            // Cria outro dataset no projeto de baixo, com o nome "dsEmployeeDetails" do tipo XSD
            // cria uma tabela "dsEmployeeDetails", com as colunas "EmpId" e "Details" id bota do tipo system.int32.
            // arrasta para o projeto de cima, na mesma pasta dos dataset.
            // cria uma outro arquivo relatorio RDLC, com o nome "rptEmployeeDetails.rdlc"
            // arasta o rdlc para o projeto de cima para começar a edição.
            // no arquivo RDLC deve ser aplicado um datasets, vc deve vincular a tabela que foi criada no arquivo XSD.
            // de um nome, encontre a tabela(tem que aparecer as colunas)
            // 
        }

        //Alimentando a tabela com os valores
        public DataTable GetEmployeeList()
        {
            var dt = new DataTable();
            dt.Columns.Add("EmpId");
            dt.Columns.Add("EmpName");
            dt.Columns.Add("Department");
            dt.Columns.Add("Designation");
            DataRow row;
            for(int i = 101; i <= 120; i++)
            {
                row = dt.NewRow();
                row["EmpId"] = i;
                row["EmpName"] = "Mr. Robert " + i;
                row["Department"] = "Information Technology";
                row["Designation"] = "Software Engineer";

                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}

// 1° Cria um projeto MVC dotNet Core 3.1, no HomeController inicia o codigo.
// 2° Cria um projeto Windows form dotNet Core 3.1.
// 3° Cria um arquivo do tipo "dataset" depois cria um tabela dentro dele com o nome "dsEmployee" a tabela e o arquivo, cria a tabela com as colunas.
// 4° Cria uma pasta chamada "ReportDataSet" no projeto principal e arrasta p arquivo dsEmployee.xsd para a pasta "ReportDataSet". 
// 5° 


