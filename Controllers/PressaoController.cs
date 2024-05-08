using Microsoft.AspNetCore.Mvc;


namespace ApiPwaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PressaoArterialController : ControllerBase
    {
        private readonly List<(int MinSistolic, int MaxSistolic, int MinDiastolic, int MaxDiastolic, string Classification)> rules = new()
        {
            (0, 119, 0, 79, "PA �tima"),
            (120, 129, 80, 84, "PA normal"),
            (130, 139, 85, 89, "Pr�-hipertens�o"),
            (140, 159, 90, 99, "HA Est�gio 1"),
            (160, 179, 100, 109, "HA Est�gio 2"),
            (180, int.MaxValue, 110, int.MaxValue, "HA Est�gio 3"),
        };

        [HttpPost("calculate")]
        public ActionResult<PressaoResult> CalculatePressao([FromBody] PressaoData data)
        {
            double pam = (data.Sistolic + 2 * data.Diastolic) / 3.0;
            var result = new PressaoResult
            {
                PAM = pam,
                Classification = ClassifyPressao(data.Sistolic, data.Diastolic),
                Recommendation = GetRecommendation(data.Sistolic, data.Diastolic)
            };

            return Ok(result);
        }

        private string ClassifyPressao(int sistolic, int diastolic)
        {
            var classification = rules.FirstOrDefault(r => sistolic >= r.MinSistolic && sistolic <= r.MaxSistolic && diastolic >= r.MinDiastolic && diastolic <= r.MaxDiastolic);
            return classification.Classification ?? "Fora dos padr�es comuns";
        }

        private string GetRecommendation(int sistolic, int diastolic)
        {
            if (sistolic >= 180 || diastolic >= 110)
                return "Situa��o cr�tica: procurar atendimento de emerg�ncia imediatamente.";
            if (sistolic < 90 && diastolic < 60)
                return "Poss�vel hipotens�o: considerar medidas para aumentar a press�o.";
            return "Manter monitoramento regular e consultar um m�dico se houver sintomas preocupantes.";
        }
    }

    public class PressaoData
    {
        public int Sistolic { get; set; }
        public int Diastolic { get; set; }
    }

    public class PressaoResult
    {
        public double PAM { get; set; }
        public string Classification { get; set; }
        public string Recommendation { get; set; }
    }
}
