using Microsoft.AspNetCore.Mvc;

namespace ApiPwaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IMCController : ControllerBase
    {
        [HttpPost("calculate")]
        public IActionResult CalculateIMC([FromBody] IMCData data)
        {
            if (data.Height <= 0 || data.Weight <= 0)
            {
                return BadRequest("Altura e peso devem ser valores positivos.");
            }

            double heightInMeters = data.Height / 100.0; // Assegure que a altura esteja em metros.
            double imc = data.Weight / (heightInMeters * heightInMeters);
            var result = ClassifyIMC(imc);

            return Ok(new { IMC = imc, Classification = result.classification, Recommendation = result.recommendation });
        }

        private (string classification, string recommendation) ClassifyIMC(double imc)
        {
            return imc switch
            {
                < 18.5 => ("Baixo peso", "Aten��o - Procure um m�dico para realiza��o de exames que apontem a causa do baixo peso."),
                >= 18.5 and <= 24.9 => ("Peso ideal", "Parab�ns - Voc� est� no peso ideal, se alimente bem e fa�a exerc�cios."),
                >= 25 and <= 29.9 => ("Sobrepeso", "Aten��o - Procure um m�dico para realizar o tratamento, para reeduca��o alimentar e exerc�cios para melhorar sua sa�de."),
                >= 30 and <= 34.9 => ("Obesidade grau I", "Aten��o - Procure um m�dico para o tratamento, o tratamento da obesidade vai al�m da perda de peso."),
                >= 35 and <= 39.9 => ("Obesidade grau II", "Aten��o - Procure um m�dico para o tratamento, o tratamento da obesidade vai al�m da perda de peso."),
                _ => ("Obesidade grau III", "Aten��o - Procure um m�dico para o tratamento, o tratamento da obesidade vai al�m da perda de peso.")
            };
        }
    }

    public class IMCData
    {
        public double Weight { get; set; } // peso em kg
        public double Height { get; set; } // altura em cm, ajustar conforme entrada esperada
    }
}
